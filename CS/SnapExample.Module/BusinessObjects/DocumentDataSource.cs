using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;
using SnapModule.Base;
using DevExpress.Persistent.Validation;

namespace SnapExampleXPO.Module.BusinessObjects {
    public class DocumentDataSource : BaseObject, IDocumentDataSource {
        public DocumentDataSource(Session session)
            : base(session) {
        }
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        private Type _DataType;
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ValueConverter(typeof(TypeToStringConverter))]
        [RuleRequiredField]
        public Type DataType {
            get { return _DataType; }
            set { SetPropertyValue("DataType", ref _DataType, value); }
        }
        private string _Criteria;
        [CriteriaOptions("DataType"), Size(SizeAttribute.Unlimited)]
        public string Criteria {
            get { return _Criteria; }
            set { SetPropertyValue("Criteria", ref _Criteria, value); }
        }
        private DocumentData _DocumentData;
        [Association("DocumentData-DataSources")]
        public DocumentData DocumentData {
            get { return _DocumentData; }
            set { SetPropertyValue("DocumentData", ref _DocumentData, value); }
        }
    }
}
