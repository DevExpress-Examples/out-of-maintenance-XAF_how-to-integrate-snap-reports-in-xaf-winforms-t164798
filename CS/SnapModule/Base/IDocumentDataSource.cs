using System;

namespace SnapModule.Base {
    public interface IDocumentDataSource {
        string Name { get; set; }
        Type DataType { get; }
        string Criteria { get; }
    }
}
