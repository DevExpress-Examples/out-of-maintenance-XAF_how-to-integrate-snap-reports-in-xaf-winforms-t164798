Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data.Entity
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.ExpressApp.EF.Updating
Imports System.Data.Common

Namespace SnapExampleEF.BusinessObjects
    Public Class SnapExampleDbContext
        Inherits DbContext

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Entity(Of Person)().ToTable("Parties_Person")
            modelBuilder.Entity(Of ModuleInfo)().ToTable("ModulesInfo")

            modelBuilder.Entity(Of DocumentData)().HasMany(Function(r) r.DataSources).WithOptional(Function(p) p.DocumentData).WillCascadeOnDelete(True)
        End Sub

        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)
        End Sub
        Public Sub New(ByVal connection As DbConnection)
            MyBase.New(connection, False)
        End Sub

        Public Property Addresses() As DbSet(Of Address)
        Public Property Countries() As DbSet(Of Country)
        Public Property DocumentDatas() As DbSet(Of DocumentData)
        Public Property DocumentDataSources() As DbSet(Of DocumentDataSource)
        Public Property ModulesInfo() As DbSet(Of ModuleInfo)
        Public Property Parties() As DbSet(Of Party)
        Public Property Persons() As DbSet(Of Person)
        Public Property PhoneNumbers() As DbSet(Of PhoneNumber)
        Public Property Tasks() As DbSet(Of Task)
    End Class
End Namespace
