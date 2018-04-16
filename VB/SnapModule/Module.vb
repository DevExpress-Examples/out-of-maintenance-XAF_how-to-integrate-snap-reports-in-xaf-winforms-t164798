Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Updating
Imports SnapModule.Base
Imports DevExpress.ExpressApp.Design
Imports SnapModule.Controllers
Imports DevExpress.ExpressApp.Utils

Namespace SnapModule
    Public NotInheritable Partial Class SnapModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
            Return New ModuleUpdater() { updater }
        End Function
        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
        End Sub
        Private _DocumentDataType As Type
        <TypeConverter(GetType(BusinessClassTypeConverter(Of IDocumentData)))> _
        Public Property DocumentDataType() As Type
            Get
                Return _DocumentDataType
            End Get
            Set(ByVal value As Type)
                If value IsNot Nothing Then
                    Guard.TypeArgumentIs(GetType(IDocumentData), value, "value")
                End If
                _DocumentDataType = value
            End Set
        End Property
        Protected Overrides Function GetDeclaredControllerTypes() As IEnumerable(Of Type)
            Return New Type() { GetType(DocumentDataController), GetType(InplaceDocumentsController) }
        End Function
    End Class
End Namespace
