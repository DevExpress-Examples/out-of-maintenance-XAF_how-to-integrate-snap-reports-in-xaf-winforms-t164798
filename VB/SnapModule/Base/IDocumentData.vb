Imports System.Collections

Namespace SnapModule.Base
    Public Interface IDocumentData
        Property DocumentName() As String
        Property Content() As Byte()
        ReadOnly Property DataSources() As IList
        ReadOnly Property InplaceDocumentDataSource() As IDocumentDataSource
    End Interface
End Namespace
