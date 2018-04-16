namespace SnapExampleEF {
    partial class SnapExampleEFModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Person));
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Party));
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Address));
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.PhoneNumber));
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Country));
        }

        #endregion
    }
}
