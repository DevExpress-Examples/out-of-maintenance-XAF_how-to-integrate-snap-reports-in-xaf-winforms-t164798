using DevExpress.Persistent.BaseImpl;
using SnapModule.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.IO;
using System.Collections;

namespace SnapExampleXPO.Module.BusinessObjects {
    [NavigationItem("Reports")]
    [ImageName("BO_Report"), System.ComponentModel.DisplayName("Document")]
    public class DocumentData : BaseObject, IDocumentData {
        public DocumentData(Session session) : base(session) { }

        private string _DocumentName;
        public string DocumentName {
            get { return _DocumentName; }
            set { SetPropertyValue("DocumentName", ref _DocumentName, value); }
        }

        [Delayed(true), Browsable(false)]
        public byte[] Content {
            get { return GetDelayedPropertyValue<byte[]>("Content"); }
            set { SetDelayedPropertyValue<byte[]>("Content", value); }
        }

        [Association("DocumentData-DataSources"), Aggregated]
        public XPCollection<DocumentDataSource> DataSources {
            get {
                return GetCollection<DocumentDataSource>("DataSources");
            }
        }

        private DocumentDataSource _InplaceDocumentDataSource;
        [DataSourceProperty("DataSources")]
        public DocumentDataSource InplaceDocumentDataSource {
            get { return _InplaceDocumentDataSource; }
            set { SetPropertyValue("InplaceDocumentDataSource", ref _InplaceDocumentDataSource, value); }
        }

        IList IDocumentData.DataSources {
            get {
                return DataSources;
            }
        }

        IDocumentDataSource IDocumentData.InplaceDocumentDataSource {
            get {
                return InplaceDocumentDataSource;
            }
        }
    }

}