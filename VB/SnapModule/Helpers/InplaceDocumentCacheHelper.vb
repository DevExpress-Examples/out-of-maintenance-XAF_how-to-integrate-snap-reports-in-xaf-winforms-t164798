Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Utils
Imports SnapModule.Base
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections

Namespace SnapModule.Helpers
    Public Class InplaceDocumentsCacheHelper
        Private objectSpace As IObjectSpace
        Private documentDataType As Type
        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal documentDataType As Type)
            Me.objectSpace = objectSpace
            Me.documentDataType = documentDataType
        End Sub
        Private isCompleteCache As Boolean = False
        Private inplaceDocumentsCache As New LightDictionary(Of Type, List(Of IDocumentData))()
        Public Overridable Function GetDocumentDataList(ByVal targetObjectType As Type) As List(Of IDocumentData)
            EnsureCache()
            Dim cachedDocuments As New List(Of IDocumentData)()
            For Each key As Type In inplaceDocumentsCache.Keys
                If key.IsAssignableFrom(targetObjectType) Then
                    cachedDocuments.AddRange(inplaceDocumentsCache(key))
                End If
            Next key
            Return cachedDocuments
        End Function
        Public Sub ClearInplaceDocumentsCache()
            isCompleteCache = False
            inplaceDocumentsCache.Clear()
        End Sub
        Protected Overridable Function CollectAllInplaceDocumentsData() As IList
            Return objectSpace.GetObjects(documentDataType, CriteriaOperator.Parse("InplaceDocumentDataSource is not null"))
        End Function
        Private Sub EnsureCache()
            If (Not isCompleteCache) Then
                Dim allInPlaceDocumentsData As IList = CollectAllInplaceDocumentsData()
                For Each item As IDocumentData In allInPlaceDocumentsData
                    If item.InplaceDocumentDataSource IsNot Nothing Then
                        Dim items As List(Of IDocumentData)
                        If (Not inplaceDocumentsCache.TryGetValue(item.InplaceDocumentDataSource.DataType, items)) Then
                            items = New List(Of IDocumentData)()
                            inplaceDocumentsCache(item.InplaceDocumentDataSource.DataType) = items
                        End If
                        items.Add(item)
                    End If
                Next item
                isCompleteCache = True
            End If
        End Sub
    End Class
End Namespace
