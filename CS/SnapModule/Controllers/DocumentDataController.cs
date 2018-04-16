using DevExpress.ExpressApp;
using SnapModule.Base;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using SnapModule.Helpers;

namespace SnapModule.Controllers {
    public class DocumentDataController : ViewController {
        SimpleAction showDocumentDesignerAction, showDocumentPrintPreviewAction, editDocumentDataAction;
        NewObjectViewController newObjectViewController;
        ListViewProcessCurrentObjectController processCurrentObjectController;
        public DocumentDataController() {
            TargetObjectType = typeof(IDocumentData);
            TargetViewType = ViewType.ListView;

            showDocumentDesignerAction = new SimpleAction(this, "ShowDocumentDesigner", DevExpress.Persistent.Base.PredefinedCategory.View);
            showDocumentDesignerAction.Caption = "Show Designer";
            showDocumentDesignerAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            showDocumentDesignerAction.ImageName = "Action_Report_ShowDesigner";
            showDocumentDesignerAction.Execute += new SimpleActionExecuteEventHandler(showDocumentDesignerAction_Execute);

            showDocumentPrintPreviewAction = new SimpleAction(this, "ShowDocumentPrintPreview", DevExpress.Persistent.Base.PredefinedCategory.View);
            showDocumentPrintPreviewAction.Caption = "Show Print Preview";
            showDocumentPrintPreviewAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            showDocumentPrintPreviewAction.ImageName = "Action_Printing_Preview";
            showDocumentPrintPreviewAction.Execute += new SimpleActionExecuteEventHandler(showDocumentPrintPreviewAction_Execute);

            editDocumentDataAction = new SimpleAction(this, "EditDocumentData", DevExpress.Persistent.Base.PredefinedCategory.View);
            editDocumentDataAction.Caption = "Edit Document Data";
            editDocumentDataAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            editDocumentDataAction.ImageName = "Action_Edit";
            editDocumentDataAction.Execute += new SimpleActionExecuteEventHandler(editDocumentDataAction_Execute);
        }
        protected override void OnActivated() {
            base.OnActivated();
            newObjectViewController = Frame.GetController<NewObjectViewController>();
            if (newObjectViewController != null) {
                newObjectViewController.ObjectCreating += DocumentDataController_ObjectCreating;
            }
            processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null) {
                processCurrentObjectController.CustomProcessSelectedItem += processCurrentObjectController_CustomProcessSelectedItem;
            }
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            if (newObjectViewController != null) {
                newObjectViewController.ObjectCreating -= DocumentDataController_ObjectCreating;
                newObjectViewController = null;
            }
            if (processCurrentObjectController != null) {
                processCurrentObjectController.CustomProcessSelectedItem -= processCurrentObjectController_CustomProcessSelectedItem;
                processCurrentObjectController = null;
            }
        }
        private void DocumentDataController_ObjectCreating(object sender, ObjectCreatingEventArgs e) {
            if (!e.Cancel && typeof(IDocumentData).IsAssignableFrom(e.ObjectType) &&
                Frame.Context != TemplateContext.LookupControl && Frame.Context != TemplateContext.LookupWindow) {
                e.ShowDetailView = false;
                IDocumentData documentData = (IDocumentData)e.ObjectSpace.CreateObject(e.ObjectType);
                e.NewObject = documentData;
                ShowDesigner(documentData, e.ObjectSpace);
            }
        }
        private void processCurrentObjectController_CustomProcessSelectedItem(object sender, CustomProcessListViewSelectedItemEventArgs e) {
            if (!e.Handled && Frame.Context != TemplateContext.LookupControl && Frame.Context != TemplateContext.LookupWindow &&
                showDocumentDesignerAction.Active && showDocumentDesignerAction.Enabled) {
                e.Handled = true;
                showDocumentDesignerAction.DoExecute();
            }
        }
        private void showDocumentDesignerAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            IObjectSpace newObjectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            ShowDesigner((IDocumentData)newObjectSpace.GetObject(View.CurrentObject), newObjectSpace);
        }
        private void ShowDesigner(IDocumentData documentData, IObjectSpace objectSpace) {
            objectSpace.Committed += delegate { ObjectSpace.Refresh(); };
            SnapDocumentHelper.ShowDesigner(documentData, objectSpace, Application);
        }
        private void showDocumentPrintPreviewAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            IObjectSpace newObjectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            SnapDocumentHelper.ShowPrintPreview((IDocumentData)newObjectSpace.GetObject(View.CurrentObject), newObjectSpace);
        }
        private void editDocumentDataAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            ListViewProcessCurrentObjectController.ShowObject(e.CurrentObject, e.ShowViewParameters, Application, Frame, View);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += delegate { ObjectSpace.Refresh(); };
        }
    }
}
