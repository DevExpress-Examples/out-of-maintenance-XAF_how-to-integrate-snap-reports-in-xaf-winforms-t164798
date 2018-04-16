Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports SnapModule.Base
Imports DevExpress.Persistent.Base
Imports DevExpress.Data.Filtering
Imports System.Collections
Imports SnapModule.Helpers

Namespace SnapModule.Controllers
    Public Class InplaceDocumentsController
        Inherits ObjectViewController

        Private documentPrintSelectionAction As SingleChoiceAction
        Private inplaceDocumentsCacheHelper As InplaceDocumentsCacheHelper
        Public Sub New()
            documentPrintSelectionAction = New SingleChoiceAction(Me, "DocumentPrintSelection", PredefinedCategory.Reports)
            documentPrintSelectionAction.Caption = "Print Selected Objects"
            AddHandler documentPrintSelectionAction.Execute, AddressOf documentPrintSelectionAction_Execute
            documentPrintSelectionAction.ItemType = SingleChoiceActionItemType.ItemIsOperation
            documentPrintSelectionAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
            documentPrintSelectionAction.ImageName = "Action_Printing_Preview"
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            Dim documentDataType As Type = Application.Modules.FindModule(Of SnapModule)().DocumentDataType
            inplaceDocumentsCacheHelper = New InplaceDocumentsCacheHelper(Application.CreateObjectSpace(documentDataType), documentDataType)
            Dim documents As IList(Of IDocumentData) = inplaceDocumentsCacheHelper.GetDocumentDataList(View.ObjectTypeInfo.Type)
            Dim items As New List(Of ChoiceActionItem)()
            For Each document As IDocumentData In documents
                Dim item As New ChoiceActionItem(document.DocumentName, document)
                items.Add(item)
            Next document
            items.Sort(Function(left As ChoiceActionItem, right As ChoiceActionItem) Comparer(Of String).Default.Compare(left.Caption, right.Caption))
            documentPrintSelectionAction.Items.Clear()
            documentPrintSelectionAction.Items.AddRange(items)
        End Sub
        Protected Overrides Sub OnDeactivated()
            MyBase.OnDeactivated()
            inplaceDocumentsCacheHelper.ClearInplaceDocumentsCache()
            inplaceDocumentsCacheHelper = Nothing
        End Sub
        Protected Overrides Sub OnFrameAssigned()
            MyBase.OnFrameAssigned()
            If (Frame IsNot Nothing) AndAlso ((Frame.Context = TemplateContext.LookupWindow) OrElse (Frame.Context = TemplateContext.LookupControl)) Then
                Me.Active.SetItemValue("DisableForLookupListView", False)
            End If
        End Sub
        Private Sub documentPrintSelectionAction_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)
            If TypeOf e.SelectedChoiceActionItem.Data Is IDocumentData Then
                Dim criteria As CriteriaOperator = CType(ObjectSpace, BaseObjectSpace).GetObjectsCriteria(View.ObjectTypeInfo, e.SelectedObjects)
                SnapDocumentHelper.ShowPrintPreview(DirectCast(e.SelectedChoiceActionItem.Data, IDocumentData), Application.CreateObjectSpace(View.ObjectTypeInfo.Type), criteria)
            End If
        End Sub
    End Class
End Namespace
