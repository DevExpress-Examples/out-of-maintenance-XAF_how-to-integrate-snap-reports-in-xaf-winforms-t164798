Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Namespace SnapModule.DatabaseUpdate
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
    End Class
End Namespace
