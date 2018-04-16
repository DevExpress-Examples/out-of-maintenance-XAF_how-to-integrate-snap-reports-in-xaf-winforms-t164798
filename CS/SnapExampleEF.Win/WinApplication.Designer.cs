namespace SnapExampleXPO.Win {
    partial class SnapExampleXPOWindowsFormsApplication {
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
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.snapModule1 = new SnapModule.SnapModule();
            this.snapExampleEFModule1 = new SnapExampleEF.SnapExampleEFModule();
            this.validationWindowsFormsModule1 = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
            this.validationModule1 = new DevExpress.ExpressApp.Validation.ValidationModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // snapModule1
            // 
            this.snapModule1.DocumentDataType = typeof(SnapExampleEF.BusinessObjects.DocumentData);
            // 
            // validationModule1
            // 
            this.validationModule1.AllowValidationDetailsAccess = true;
            this.validationModule1.IgnoreWarningAndInformationRules = false;
            // 
            // SnapExampleXPOWindowsFormsApplication
            // 
            this.ApplicationName = "SnapExampleXPO";
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.snapModule1);
            this.Modules.Add(this.snapExampleEFModule1);
            this.Modules.Add(this.validationModule1);
            this.Modules.Add(this.validationWindowsFormsModule1);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.SnapExampleXPOWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.SnapExampleXPOWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private SnapModule.SnapModule snapModule1;
        private SnapExampleEF.SnapExampleEFModule snapExampleEFModule1;
        private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWindowsFormsModule1;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule1;
    }
}
