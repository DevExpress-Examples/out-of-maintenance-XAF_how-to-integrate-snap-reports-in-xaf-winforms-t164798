Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports SnapModule.Base
Imports DevExpress.Snap
Imports DevExpress.XtraPrinting
Imports System.IO
Imports DevExpress.Snap.Core.API
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Editors
Imports System.Data
Imports System.Collections
Imports DevExpress.ExpressApp.SystemModule
Imports SnapModule.Editors

Namespace SnapModule.Helpers
    Public Class SnapDocumentHelper
        Private documentData As IDocumentData
        Private objectSpace As IObjectSpace
        Private application As XafApplication
        Private inPlaceCriteria As CriteriaOperator
        Private document As SnapDocument

        Public Sub New(ByVal documentData As IDocumentData, ByVal objectSpace As IObjectSpace, ByVal application As XafApplication)
            Me.New(documentData, objectSpace, application, Nothing)
        End Sub

        Public Sub New(ByVal documentData As IDocumentData, ByVal objectSpace As IObjectSpace, ByVal application As XafApplication, ByVal inPlaceCriteria As CriteriaOperator)
            Me.documentData = documentData
            Me.objectSpace = objectSpace
            Me.application = application
            Me.inPlaceCriteria = inPlaceCriteria
        End Sub

        Public Shared Sub ShowDesigner(ByVal documentData As IDocumentData, ByVal objectSpace As IObjectSpace, ByVal application As XafApplication)
            Dim helper As New SnapDocumentHelper(documentData, objectSpace, application)
            Dim designer As New SnapDesigner(helper)
            designer.ShowDialog()
        End Sub
        Public Shared Sub ShowPrintPreview(ByVal documentData As IDocumentData, ByVal objectSpace As IObjectSpace)
            ShowPrintPreview(documentData, objectSpace, Nothing)
        End Sub
        Public Shared Sub ShowPrintPreview(ByVal documentData As IDocumentData, ByVal objectSpace As IObjectSpace, ByVal inPlaceCriteria As CriteriaOperator)
            Using server As New SnapDocumentServer()
                Dim helper As New SnapDocumentHelper(documentData, objectSpace, Nothing, inPlaceCriteria)
                helper.LoadDocument(server.Document)
                Using ps As New PrintingSystem()
                    Dim link As New PrintableComponentLink(ps)
                    link.Component = server
                    link.CreateDocument()
                    link.ShowPreviewDialog()
                End Using
            End Using
        End Sub
        Public Sub LoadDocument(ByVal document As SnapDocument)
            Me.document = document
            If documentData.Content IsNot Nothing Then
                Using stream As New MemoryStream()
                    stream.Write(documentData.Content, 0, documentData.Content.Length)
                    stream.Flush()
                    stream.Seek(0, SeekOrigin.Begin)
                    document.LoadDocument(stream, SnapDocumentFormat.Snap)
                End Using
            End If
            SynchronizeDataSources()
        End Sub

        Private Sub SynchronizeDataSources()
            Dim dataSourcesToRemove As New List(Of DataSourceInfo)()
            For Each dataSourceInfo As DataSourceInfo In document.DataSources
                If Not(TypeOf dataSourceInfo.DataSource Is DataSet) Then
                    Dim exists As Boolean = False
                    For Each dataSource As IDocumentDataSource In documentData.DataSources
                        If dataSource.Name = dataSourceInfo.DataSourceName Then
                            exists = True
                            Exit For
                        End If
                    Next dataSource
                    If (Not exists) Then
                        dataSourcesToRemove.Add(dataSourceInfo)
                    End If
                End If
            Next dataSourceInfo
            For Each dataSourceInfo As DataSourceInfo In dataSourcesToRemove
                document.DataSources.Remove(dataSourceInfo)
            Next dataSourceInfo
            For Each dataSource As IDocumentDataSource In documentData.DataSources
                AddDataSource(dataSource, dataSource Is documentData.InplaceDocumentDataSource)
            Next dataSource
            document.Fields.Update()
        End Sub
        Private Sub AddDataSource(ByVal dataSource As IDocumentDataSource, ByVal isInplaceDataSource As Boolean)
            Dim criteria As CriteriaOperator = If(isInplaceDataSource, inPlaceCriteria, Nothing)
            If (Not String.IsNullOrEmpty(dataSource.Criteria)) Then
                Dim dataSourceCriteria As CriteriaOperator = CriteriaEditorHelper.GetCriteriaOperator(dataSource.Criteria, dataSource.DataType, objectSpace)
                If ReferenceEquals(criteria, Nothing) Then
                    criteria = dataSourceCriteria
                Else
                    criteria = New GroupOperator(GroupOperatorType.And, criteria, dataSourceCriteria)
                End If
            End If
            Dim list As IList = New ProxyCollection(objectSpace, XafTypesInfo.Instance.FindTypeInfo(dataSource.DataType), objectSpace.GetObjects(dataSource.DataType, criteria))
            If document.DataSources(dataSource.Name) IsNot Nothing Then
                document.DataSources(dataSource.Name).DataSource = list
            Else
                document.DataSources.Add(dataSource.Name, list)
            End If
        End Sub
        Friend Sub SaveDocument()
            If objectSpace.IsNewObject(documentData) Then
                ShowDocumentDataObject()
            End If
            Using stream As New MemoryStream()
                document.SaveDocument(stream)
                stream.Seek(0, SeekOrigin.Begin)
                Dim bytes(stream.Length - 1) As Byte
                stream.Read(bytes, 0, bytes.Length)
                documentData.Content = bytes
                objectSpace.CommitChanges()
            End Using
        End Sub
        Friend Sub ShowDocumentDataObject()
            Dim documentDetailView As DetailView = application.CreateDetailView(objectSpace, documentData, False)
            documentDetailView.Caption = "Save Document"
            Dim svp As New ShowViewParameters(documentDetailView)
            svp.TargetWindow = TargetWindow.NewModalWindow
            svp.Context = TemplateContext.PopupWindow
            Dim dc As DialogController = application.CreateController(Of DialogController)()
            dc.SaveOnAccept = False
            svp.Controllers.Add(dc)
            application.ShowViewStrategy.ShowView(svp, New ShowViewSource(Nothing, Nothing))
            SynchronizeDataSources()
        End Sub
        Friend Sub CreateNewDocument(ByVal document As SnapDocument)
            documentData = CType(objectSpace.CreateObject(CType(documentData, Object).GetType()), IDocumentData)
        End Sub
        Friend Sub ShowNewDataSourceDialog()
            Dim newDataSourceObjectSpace As IObjectSpace = objectSpace.CreateNestedObjectSpace()
            Dim dataSourceType As Type = XafTypesInfo.Instance.FindTypeInfo(CType(documentData, Object).GetType()).FindMember("DataSources").ListElementType
            Dim dataSource As IDocumentDataSource = CType(newDataSourceObjectSpace.CreateObject(dataSourceType), IDocumentDataSource)
            Dim dataSourceDetailView As DetailView = application.CreateDetailView(newDataSourceObjectSpace, dataSource)
            Dim svp As New ShowViewParameters(dataSourceDetailView)
            svp.TargetWindow = TargetWindow.NewModalWindow
            svp.Context = TemplateContext.PopupWindow
            Dim dc As DialogController = application.CreateController(Of DialogController)()
            svp.Controllers.Add(dc)
            AddHandler dc.Accepting, Function() AnonymousMethod1(newDataSourceObjectSpace, dataSource)
            application.ShowViewStrategy.ShowView(svp, New ShowViewSource(Nothing, Nothing))
        End Sub
        
        Private Function AnonymousMethod1(ByVal newDataSourceObjectSpace As IObjectSpace, ByVal dataSource As IDocumentDataSource) As Boolean
            newDataSourceObjectSpace.CommitChanges()
            Dim newDataSource As IDocumentDataSource = CType(objectSpace.GetObject(dataSource), IDocumentDataSource)
            documentData.DataSources.Add(newDataSource)
            AddDataSource(newDataSource, False)
            Return True
        End Function
        Friend Function QueryCanClose() As Boolean
            Dim result As ConfirmationResult = application.AskConfirmation(ConfirmationType.NeedSaveChanges)
            If result = ConfirmationResult.Cancel Then
                Return False
            End If
            If result = ConfirmationResult.Yes Then
                SaveDocument()
            End If
            Return True
        End Function
        Friend Function GetDocumentName() As String
            Return documentData.DocumentName
        End Function
        Friend ReadOnly Property Modified() As Boolean
            Get
                Return objectSpace.IsModified
            End Get
        End Property
    End Class
End Namespace
