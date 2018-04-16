using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapModule.Base;
using DevExpress.Persistent.Base;
using System.Collections;
using System.ComponentModel;

namespace SnapExampleEF.BusinessObjects {
    [NavigationItem("Reports")]
    [ImageName("BO_Report"), System.ComponentModel.DisplayName("Document")]
    public class DocumentData : IDocumentData {
        public DocumentData() {
            DataSources = new List<DocumentDataSource>();
		}

        [Browsable(false)]
        public Int32 ID { get; protected set; }

        public string DocumentName { get; set; }

        [Browsable(false)]
        public byte[] Content { get; set; }

        public virtual IList<DocumentDataSource> DataSources { get; set; }

        [DataSourceProperty("DataSources")]
        public virtual DocumentDataSource InplaceDocumentDataSource { get; set; }

        IList IDocumentData.DataSources {
            get {
                return (IList)DataSources;
            }
        }

        IDocumentDataSource IDocumentData.InplaceDocumentDataSource {
            get {
                return InplaceDocumentDataSource;
            }
        }
    }
}
