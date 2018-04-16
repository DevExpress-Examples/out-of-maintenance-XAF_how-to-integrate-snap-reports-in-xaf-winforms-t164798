using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using SnapModule.Base;
using DevExpress.ExpressApp.Design;
using SnapModule.Controllers;
using DevExpress.ExpressApp.Utils;

namespace SnapModule {
    public sealed partial class SnapModule : ModuleBase {
        public SnapModule() {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
        }
        private Type _DocumentDataType;
        [TypeConverter(typeof(BusinessClassTypeConverter<IDocumentData>))]
        public Type DocumentDataType {
            get {
                return _DocumentDataType;
            }
            set {
                if (value != null) {
                    Guard.TypeArgumentIs(typeof(IDocumentData), value, "value");
                }
                _DocumentDataType = value;
            }
        }
        protected override IEnumerable<Type> GetDeclaredControllerTypes() {
            return new Type[] { 
                typeof(DocumentDataController),
                typeof(InplaceDocumentsController)
            };
        }
    }
}
