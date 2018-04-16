Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Data.Filtering

Namespace SnapExampleXPO.Module.DatabaseUpdate
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            CreatePerson("Mary", "Tellitson")
            CreatePerson("John", "Nilsen")
        End Sub
        Private Sub CreatePerson(ByVal firstName As String, ByVal lastName As String)
            Dim person As Person = ObjectSpace.FindObject(Of Person)(CriteriaOperator.Parse("FirstName = ? and LastName = ?", firstName, lastName))
            If person Is Nothing Then
                person = ObjectSpace.CreateObject(Of Person)()
                person.FirstName = firstName
                person.LastName = lastName
            End If
        End Sub
    End Class
End Namespace
