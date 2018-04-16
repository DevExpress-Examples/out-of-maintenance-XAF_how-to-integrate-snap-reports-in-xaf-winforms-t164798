Imports DevExpress.Persistent.BaseImpl
Imports SnapModule.Base
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports System.IO
Imports System.Collections

Namespace SnapExampleXPO.Module.BusinessObjects
    <NavigationItem("Reports"), ImageName("BO_Report"), System.ComponentModel.DisplayName("Document")> _
    Public Class DocumentData
        Inherits BaseObject
        Implements IDocumentData

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Private _DocumentName As String
        Public Property DocumentName() As String Implements IDocumentData.DocumentName
            Get
                Return _DocumentName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("DocumentName", _DocumentName, value)
            End Set
        End Property

        <Delayed(True), Browsable(False)> _
        Public Property Content() As Byte() Implements IDocumentData.Content
            Get
                Return GetDelayedPropertyValue(Of Byte())("Content")
            End Get
            Set(ByVal value As Byte())
                SetDelayedPropertyValue(Of Byte())("Content", value)
            End Set
        End Property

        <Association("DocumentData-DataSources"), Aggregated> _
        Public ReadOnly Property DataSources() As XPCollection(Of DocumentDataSource)
            Get
                Return GetCollection(Of DocumentDataSource)("DataSources")
            End Get
        End Property

        Private _InplaceDocumentDataSource As DocumentDataSource
        <DataSourceProperty("DataSources")> _
        Public Property InplaceDocumentDataSource() As DocumentDataSource
            Get
                Return _InplaceDocumentDataSource
            End Get
            Set(ByVal value As DocumentDataSource)
                SetPropertyValue("InplaceDocumentDataSource", _InplaceDocumentDataSource, value)
            End Set
        End Property

        Private ReadOnly Property IDocumentData_DataSources() As IList Implements IDocumentData.DataSources
            Get
                Return DataSources
            End Get
        End Property

        Private ReadOnly Property IDocumentData_InplaceDocumentDataSource() As IDocumentDataSource Implements IDocumentData.InplaceDocumentDataSource
            Get
                Return InplaceDocumentDataSource
            End Get
        End Property
    End Class

End Namespace