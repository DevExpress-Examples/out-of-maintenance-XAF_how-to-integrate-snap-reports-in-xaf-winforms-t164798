Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SnapModule.Base
Imports DevExpress.Persistent.Validation
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.DC
Imports System.ComponentModel.DataAnnotations.Schema
Imports DevExpress.ExpressApp

Namespace SnapExampleEF.BusinessObjects
    Public Class DocumentDataSource
        Implements IDocumentDataSource

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

        Private privateName As String
        Public Property Name() As String Implements IDocumentDataSource.Name
            Get
                Return privateName
            End Get
            Set(ByVal value As String)
                privateName = value
            End Set
        End Property

        Private privateDataTypeName As String
        <Browsable(False), FieldSize(512)> _
        Public Property DataTypeName() As String
            Get
                Return privateDataTypeName
            End Get
            Set(ByVal value As String)
                privateDataTypeName = value
            End Set
        End Property

        Private _DataType As Type
        <TypeConverter(GetType(LocalizedClassInfoTypeConverter)), RuleRequiredField, NotMapped> _
        Public Property DataType() As Type
            Get
                If _DataType Is Nothing AndAlso (Not String.IsNullOrEmpty(DataTypeName)) Then
                    Dim typeInfo As ITypeInfo = XafTypesInfo.Instance.FindTypeInfo(DataTypeName)
                    If typeInfo IsNot Nothing Then
                        Return typeInfo.Type
                    End If
                End If
                Return _DataType
            End Get
            Set(ByVal value As Type)
                _DataType = value
                DataTypeName = If(_DataType Is Nothing, String.Empty, _DataType.FullName)
            End Set
        End Property
        Private privateCriteria As String
        <CriteriaOptions("DataType"), FieldSize(FieldSizeAttribute.Unlimited)> _
        Public Property Criteria() As String
            Get
                Return privateCriteria
            End Get
            Set(ByVal value As String)
                privateCriteria = value
            End Set
        End Property

        Private privateDocumentData As DocumentData
        <Browsable(False)> _
        Public Overridable Property DocumentData() As DocumentData
            Get
                Return privateDocumentData
            End Get
            Set(ByVal value As DocumentData)
                privateDocumentData = value
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
