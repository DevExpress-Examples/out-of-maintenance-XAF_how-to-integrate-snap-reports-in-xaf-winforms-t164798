Imports DevExpress.ExpressApp
Imports SnapModule.Base
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.SystemModule
Imports SnapModule.Helpers

Namespace SnapModule.Controllers
    Public Class DocumentDataController
        Inherits ViewController

        Private showDocumentDesignerAction, showDocumentPrintPreviewAction, editDocumentDataAction As SimpleAction
        Private newObjectViewController As NewObjectViewController
        Private processCurrentObjectController As ListViewProcessCurrentObjectController
        Public Sub New()
            TargetObjectType = GetType(IDocumentData)
            TargetViewType = ViewType.ListView

            showDocumentDesignerAction = New SimpleAction(Me, "ShowDocumentDesigner", DevExpress.Persistent.Base.PredefinedCategory.View)
            showDocumentDesignerAction.Caption = "Show Designer"
            showDocumentDesignerAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            showDocumentDesignerAction.ImageName = "Action_Report_ShowDesigner"
            AddHandler showDocumentDesignerAction.Execute, AddressOf showDocumentDesignerAction_Execute

            showDocumentPrintPreviewAction = New SimpleAction(Me, "ShowDocumentPrintPreview", DevExpress.Persistent.Base.PredefinedCategory.View)
            showDocumentPrintPreviewAction.Caption = "Show Print Preview"
            showDocumentPrintPreviewAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            showDocumentPrintPreviewAction.ImageName = "Action_Printing_Preview"
            AddHandler showDocumentPrintPreviewAction.Execute, AddressOf showDocumentPrintPreviewAction_Execute

            editDocumentDataAction = New SimpleAction(Me, "EditDocumentData", DevExpress.Persistent.Base.PredefinedCategory.View)
            editDocumentDataAction.Caption = "Edit Document Data"
            editDocumentDataAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            editDocumentDataAction.ImageName = "Action_Edit"
            AddHandler editDocumentDataAction.Execute, AddressOf editDocumentDataAction_Execute
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            newObjectViewController = Frame.GetController(Of NewObjectViewController)()
            If newObjectViewController IsNot Nothing Then
                AddHandler newObjectViewController.ObjectCreating, AddressOf DocumentDataController_ObjectCreating
            End If
            processCurrentObjectController = Frame.GetController(Of ListViewProcessCurrentObjectController)()
            If processCurrentObjectController IsNot Nothing Then
                AddHandler processCurrentObjectController.CustomProcessSelectedItem, AddressOf processCurrentObjectController_CustomProcessSelectedItem
            End If
        End Sub
        Protected Overrides Sub OnDeactivated()
            MyBase.OnDeactivated()
            If newObjectViewController IsNot Nothing Then
                RemoveHandler newObjectViewController.ObjectCreating, AddressOf DocumentDataController_ObjectCreating
                newObjectViewController = Nothing
            End If
            If processCurrentObjectController IsNot Nothing Then
                RemoveHandler processCurrentObjectController.CustomProcessSelectedItem, AddressOf processCurrentObjectController_CustomProcessSelectedItem
                processCurrentObjectController = Nothing
            End If
        End Sub
        Private Sub DocumentDataController_ObjectCreating(ByVal sender As Object, ByVal e As ObjectCreatingEventArgs)
            If (Not e.Cancel) AndAlso GetType(IDocumentData).IsAssignableFrom(e.ObjectType) AndAlso Frame.Context <> TemplateContext.LookupControl AndAlso Frame.Context <> TemplateContext.LookupWindow Then
                e.ShowDetailView = False
                Dim documentData As IDocumentData = DirectCast(e.ObjectSpace.CreateObject(e.ObjectType), IDocumentData)
                e.NewObject = documentData
                ShowDesigner(documentData, e.ObjectSpace)
            End If
        End Sub
        Private Sub processCurrentObjectController_CustomProcessSelectedItem(ByVal sender As Object, ByVal e As CustomProcessListViewSelectedItemEventArgs)
            If (Not e.Handled) AndAlso Frame.Context <> TemplateContext.LookupControl AndAlso Frame.Context <> TemplateContext.LookupWindow AndAlso showDocumentDesignerAction.Active.ResultValue AndAlso showDocumentDesignerAction.Enabled.ResultValue Then
                e.Handled = True
                showDocumentDesignerAction.DoExecute()
            End If
        End Sub
        Private Sub showDocumentDesignerAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
            Dim newObjectSpace As IObjectSpace = Application.CreateObjectSpace()
            ShowDesigner(DirectCast(newObjectSpace.GetObject(View.CurrentObject), IDocumentData), newObjectSpace)
        End Sub
        Private Sub ShowDesigner(ByVal documentData As IDocumentData, ByVal objectSpace As IObjectSpace)
            AddHandler objectSpace.Committed, Sub() ObjectSpace.Refresh()
            SnapDocumentHelper.ShowDesigner(documentData, objectSpace, Application)
        End Sub
        Private Sub showDocumentPrintPreviewAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
            Dim newObjectSpace As IObjectSpace = Application.CreateObjectSpace()
            SnapDocumentHelper.ShowPrintPreview(DirectCast(newObjectSpace.GetObject(View.CurrentObject), IDocumentData), newObjectSpace)
        End Sub
        Private Sub editDocumentDataAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
            ListViewProcessCurrentObjectController.ShowObject(e.CurrentObject, e.ShowViewParameters, Application, Frame, View)
            AddHandler e.ShowViewParameters.CreatedView.ObjectSpace.Committed, Sub() ObjectSpace.Refresh()
        End Sub
    End Class
End Namespace
