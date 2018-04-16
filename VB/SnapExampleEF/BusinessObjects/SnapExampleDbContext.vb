Imports Microsoft.VisualBasic
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

        Private privateAddresses As DbSet(Of Address)
        Public Property Addresses() As DbSet(Of Address)
            Get
                Return privateAddresses
            End Get
            Set(ByVal value As DbSet(Of Address))
                privateAddresses = value
            End Set
        End Property
        Private privateCountries As DbSet(Of Country)
        Public Property Countries() As DbSet(Of Country)
            Get
                Return privateCountries
            End Get
            Set(ByVal value As DbSet(Of Country))
                privateCountries = value
            End Set
        End Property
        Private privateDocumentDatas As DbSet(Of DocumentData)
        Public Property DocumentDatas() As DbSet(Of DocumentData)
            Get
                Return privateDocumentDatas
            End Get
            Set(ByVal value As DbSet(Of DocumentData))
                privateDocumentDatas = value
            End Set
        End Property
        Private privateDocumentDataSources As DbSet(Of DocumentDataSource)
        Public Property DocumentDataSources() As DbSet(Of DocumentDataSource)
            Get
                Return privateDocumentDataSources
            End Get
            Set(ByVal value As DbSet(Of DocumentDataSource))
                privateDocumentDataSources = value
            End Set
        End Property
        Private privateModulesInfo As DbSet(Of ModuleInfo)
        Public Property ModulesInfo() As DbSet(Of ModuleInfo)
            Get
                Return privateModulesInfo
            End Get
            Set(ByVal value As DbSet(Of ModuleInfo))
                privateModulesInfo = value
            End Set
        End Property
        Private privateParties As DbSet(Of Party)
        Public Property Parties() As DbSet(Of Party)
            Get
                Return privateParties
            End Get
            Set(ByVal value As DbSet(Of Party))
                privateParties = value
            End Set
        End Property
        Private privatePersons As DbSet(Of Person)
        Public Property Persons() As DbSet(Of Person)
            Get
                Return privatePersons
            End Get
            Set(ByVal value As DbSet(Of Person))
                privatePersons = value
            End Set
        End Property
        Private privatePhoneNumbers As DbSet(Of PhoneNumber)
        Public Property PhoneNumbers() As DbSet(Of PhoneNumber)
            Get
                Return privatePhoneNumbers
            End Get
            Set(ByVal value As DbSet(Of PhoneNumber))
                privatePhoneNumbers = value
            End Set
        End Property
        Private privateTasks As DbSet(Of Task)
        Public Property Tasks() As DbSet(Of Task)
            Get
                Return privateTasks
            End Get
            Set(ByVal value As DbSet(Of Task))
                privateTasks = value
            End Set
        End Property
    End Class
End Namespace
