Imports Microsoft.VisualBasic
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

        Private privateDocumentName As String
        Public Property DocumentName() As String Implements IDocumentData.DocumentName
            Get
                Return privateDocumentName
            End Get
            Set(ByVal value As String)
                privateDocumentName = value
            End Set
        End Property

        Private privateContent As Byte()
        <Browsable(False)> _
        Public Property Content() As Byte() Implements IDocumentData.Content
            Get
                Return privateContent
            End Get
            Set(ByVal value As Byte())
                privateContent = value
            End Set
        End Property

        Private privateDataSources As IList(Of DocumentDataSource)
        Public Overridable Property DataSources() As IList(Of DocumentDataSource)
            Get
                Return privateDataSources
            End Get
            Set(ByVal value As IList(Of DocumentDataSource))
                privateDataSources = value
            End Set
        End Property

        Private privateInplaceDocumentDataSource As DocumentDataSource
        <DataSourceProperty("DataSources")> _
        Public Overridable Property InplaceDocumentDataSource() As DocumentDataSource
            Get
                Return privateInplaceDocumentDataSource
            End Get
            Set(ByVal value As DocumentDataSource)
                privateInplaceDocumentDataSource = value
            End Set
        End Property

        Private ReadOnly Property DataSourcesImpl() As IList Implements IDocumentData.DataSources
            Get
                Return CType(DataSources, IList)
            End Get
        End Property

        Private ReadOnly Property InplaceDocumentDataSourceImpl() As IDocumentDataSource Implements IDocumentData.InplaceDocumentDataSource
            Get
                Return InplaceDocumentDataSource
            End Get
        End Property
    End Class
End Namespace
