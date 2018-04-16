using System.Collections;

namespace SnapModule.Base {
    public interface IDocumentData {
        string DocumentName { get; set; }
        byte[] Content { get; set; }
        IList DataSources { get; }
        IDocumentDataSource InplaceDocumentDataSource { get; }
    }
}
