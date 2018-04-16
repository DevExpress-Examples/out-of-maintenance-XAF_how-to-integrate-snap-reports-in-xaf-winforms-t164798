using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapModule.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.DC;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;

namespace SnapExampleEF.BusinessObjects {
    public class DocumentDataSource : IDocumentDataSource {

        [Browsable(false)]
        public Int32 ID { get; protected set; }

        public string Name { get; set; }

        [Browsable(false)]
        [FieldSize(512)]
        public String DataTypeName { get; set; }

        private Type _DataType;
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [RuleRequiredField]
        [NotMapped]
        public Type DataType {
            get {
                if (_DataType == null && !String.IsNullOrEmpty(DataTypeName)) {
                    ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(DataTypeName);
                    if (typeInfo != null) {
                        return typeInfo.Type;
                    }
                }
                return _DataType;
            }
            set {
                _DataType = value;
                DataTypeName = _DataType == null ? String.Empty : _DataType.FullName;
            }
        }
        [CriteriaOptions("DataType"), FieldSize(FieldSizeAttribute.Unlimited)]
        public string Criteria { get; set; }

        [Browsable(false)]
        public virtual DocumentData DocumentData { get; set; }
    }
}
