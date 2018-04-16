using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using SnapModule.Base;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using System.Collections;
using SnapModule.Helpers;

namespace SnapModule.Controllers {
    public class InplaceDocumentsController : ObjectViewController {
        private SingleChoiceAction documentPrintSelectionAction;
        private InplaceDocumentsCacheHelper inplaceDocumentsCacheHelper;
        public InplaceDocumentsController() {
            documentPrintSelectionAction = new SingleChoiceAction(this, "DocumentPrintSelection", PredefinedCategory.Reports);
            documentPrintSelectionAction.Caption = "Print Selected Objects";
            documentPrintSelectionAction.Execute += new SingleChoiceActionExecuteEventHandler(documentPrintSelectionAction_Execute);
            documentPrintSelectionAction.ItemType = SingleChoiceActionItemType.ItemIsOperation;
            documentPrintSelectionAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
            documentPrintSelectionAction.ImageName = "Action_Printing_Preview";
        }
        protected override void OnActivated() {
            base.OnActivated();
            Type documentDataType = Application.Modules.FindModule<SnapModule>().DocumentDataType;
            inplaceDocumentsCacheHelper = new InplaceDocumentsCacheHelper(Application.CreateObjectSpace(documentDataType), documentDataType);
            IList<IDocumentData> documents = inplaceDocumentsCacheHelper.GetDocumentDataList(View.ObjectTypeInfo.Type);
            List<ChoiceActionItem> items = new List<ChoiceActionItem>();
            foreach (IDocumentData document in documents) {
                ChoiceActionItem item = new ChoiceActionItem(document.DocumentName, document);
                items.Add(item);
            }
            items.Sort(delegate(ChoiceActionItem left, ChoiceActionItem right) {
                return Comparer<string>.Default.Compare(left.Caption, right.Caption);
            });
            documentPrintSelectionAction.Items.Clear();
            documentPrintSelectionAction.Items.AddRange(items);
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            inplaceDocumentsCacheHelper.ClearInplaceDocumentsCache();
            inplaceDocumentsCacheHelper = null;
        }
        protected override void OnFrameAssigned() {
            base.OnFrameAssigned();
            if ((Frame != null) && ((Frame.Context == TemplateContext.LookupWindow) || (Frame.Context == TemplateContext.LookupControl))) {
                this.Active.SetItemValue("DisableForLookupListView", false);
            }
        }
        private void documentPrintSelectionAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
            if (e.SelectedChoiceActionItem.Data is IDocumentData) {
                CriteriaOperator criteria = ((BaseObjectSpace)ObjectSpace).GetObjectsCriteria(View.ObjectTypeInfo, e.SelectedObjects);
                SnapDocumentHelper.ShowPrintPreview((IDocumentData)e.SelectedChoiceActionItem.Data, Application.CreateObjectSpace(View.ObjectTypeInfo.Type), criteria);
            }
        }
    }
}
