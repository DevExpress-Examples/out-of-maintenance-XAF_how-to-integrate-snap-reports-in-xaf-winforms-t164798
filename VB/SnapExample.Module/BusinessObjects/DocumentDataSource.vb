Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports System
Imports DevExpress.Persistent.Base
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Editors
Imports SnapModule.Base
Imports DevExpress.Persistent.Validation

Namespace SnapExampleXPO.Module.BusinessObjects
    Public Class DocumentDataSource
        Inherits BaseObject
        Implements IDocumentDataSource

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Private _Name As String
        Public Property Name() As String Implements IDocumentDataSource.Name
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Name", _Name, value)
            End Set
        End Property
        Private _DataType As Type
        <TypeConverter(GetType(LocalizedClassInfoTypeConverter)), ValueConverter(GetType(TypeToStringConverter)), RuleRequiredField> _
        Public Property DataType() As Type
            Get
                Return _DataType
            End Get
            Set(ByVal value As Type)
                SetPropertyValue("DataType", _DataType, value)
            End Set
        End Property
        Private _Criteria As String
        <CriteriaOptions("DataType"), Size(SizeAttribute.Unlimited)> _
        Public Property Criteria() As String
            Get
                Return _Criteria
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Criteria", _Criteria, value)
            End Set
        End Property
        Private _DocumentData As DocumentData
        <Association("DocumentData-DataSources")> _
        Public Property DocumentData() As DocumentData
            Get
                Return _DocumentData
            End Get
            Set(ByVal value As DocumentData)
                SetPropertyValue("DocumentData", _DocumentData, value)
            End Set
        End Property
        Private ReadOnly Property DataTypeImpl As Type Implements IDocumentDataSource.DataType
            Get
                Return DataType
            End Get
        End Property
        Private ReadOnly Property CriteriaImpl As String Implements IDocumentDataSource.Criteria
            Get
                Return Criteria
            End Get
        End Property
    End Class
End Namespace
