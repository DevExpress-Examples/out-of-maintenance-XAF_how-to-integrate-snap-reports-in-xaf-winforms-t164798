using System;
using DevExpress.XtraBars;
using SnapModule.Helpers;

namespace SnapModule.Editors {
    public partial class SnapDesigner : DevExpress.XtraBars.Ribbon.RibbonForm {
        const string DefaultCaption = "Document Designer";
        SnapDocumentHelper helper;
        public SnapDesigner(SnapDocumentHelper helper) {
            InitializeComponent();
            this.helper = helper;
            helper.LoadDocument(snapControl1.Document);
            saveToObjectItem.Enabled = helper.Modified;
            UpdateText();
        }
        private void saveToObjectItem_ItemClick(object sender, ItemClickEventArgs e) {
            helper.SaveDocument();
            snapControl1.Modified = false;
            UpdateText();
        }
        private void newDocumentItem_ItemClick(object sender, ItemClickEventArgs e) {
            if (!Modified || helper.QueryCanClose()) {
                snapControl1.CreateNewDocument();
                helper.CreateNewDocument(snapControl1.Document);
                UpdateText();
            }
        }
        private void addXafDataSourceItem_ItemClick(object sender, ItemClickEventArgs e) {
            helper.ShowNewDataSourceDialog();
            snapControl1.Modified = true;
        }
        private void editObjectItem_ItemClick(object sender, ItemClickEventArgs e) {
            helper.ShowDocumentDataObject();
            UpdateText();
        }
        private void snapControl1_ModifiedChanged(object sender, EventArgs e) {
            saveToObjectItem.Enabled = Modified;
        }
        private void SnapDesigner_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            if (Modified) {
                e.Cancel = !helper.QueryCanClose();
            }
        }
        private void UpdateText() {
            if (String.IsNullOrEmpty(helper.GetDocumentName())) {
                Text = DefaultCaption;
            } else {
                Text = String.Format("{0} - {1}", DefaultCaption, helper.GetDocumentName());
            }
        }
        private bool Modified {
            get {
                return helper.Modified || snapControl1.Modified;;
            }
        }
    }
}