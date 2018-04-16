Imports Microsoft.VisualBasic
Imports System

Namespace SnapModule.Base
    Public Interface IDocumentDataSource
        Property Name() As String
        ReadOnly Property DataType() As Type
        ReadOnly Property Criteria() As String
    End Interface
End Namespace
