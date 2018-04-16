Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SnapModule.Base
Imports DevExpress.Persistent.Base
Imports System.Collections
Imports System.ComponentModel

Namespace SnapExampleEF.BusinessObjects
    <NavigationItem("Reports"), ImageName("BO_Report"), System.ComponentModel.DisplayName("Document")> _
    Public Class DocumentData
        Implements IDocumentData

        Public Sub New()
            DataSources = New List(Of DocumentDataSource)()
        End Sub

        Private privateID As Int32
        <Browsable(False)> _
        Public Property ID() As Int32
            Get
                Return privateID
            End Get
            Protected Set(ByVal value As Int32)
                privateID = value
            End Set
        End Property

        Public Property DocumentName() As String Implements IDocumentData.DocumentName

        <Browsable(False)> _
        Public Property Content() As Byte() Implements IDocumentData.Content

        Public Overridable Property DataSources() As IList(Of DocumentDataSource)

        <DataSourceProperty("DataSources")> _
        Public Overridable Property InplaceDocumentDataSource() As DocumentDataSource

        Private ReadOnly Property IDocumentData_DataSources() As IList Implements IDocumentData.DataSources
            Get
                Return DirectCast(DataSources, IList)
            End Get
        End Property

        Private ReadOnly Property IDocumentData_InplaceDocumentDataSource() As IDocumentDataSource Implements IDocumentData.InplaceDocumentDataSource
            Get
                Return InplaceDocumentDataSource
            End Get
        End Property
    End Class
End Namespace
