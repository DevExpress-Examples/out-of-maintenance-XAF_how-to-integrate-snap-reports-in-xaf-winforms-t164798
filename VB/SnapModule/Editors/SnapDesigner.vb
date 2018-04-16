Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraBars
Imports SnapModule.Helpers

Namespace SnapModule.Editors
    Partial Public Class SnapDesigner
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm
        Private Const DefaultCaption As String = "Document Designer"
        Private helper As SnapDocumentHelper
        Public Sub New(ByVal helper As SnapDocumentHelper)
            InitializeComponent()
            Me.helper = helper
            helper.LoadDocument(snapControl1.Document)
            saveToObjectItem.Enabled = helper.Modified
            UpdateText()
        End Sub
        Private Sub saveToObjectItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles saveToObjectItem.ItemClick
            helper.SaveDocument()
            snapControl1.Modified = False
            UpdateText()
        End Sub
        Private Sub newDocumentItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles newDocumentItem.ItemClick
            If (Not Modified) OrElse helper.QueryCanClose() Then
                snapControl1.CreateNewDocument()
                helper.CreateNewDocument(snapControl1.Document)
                UpdateText()
            End If
        End Sub
        Private Sub addXafDataSourceItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles addXafDataSourceItem.ItemClick
            helper.ShowNewDataSourceDialog()
            snapControl1.Modified = True
        End Sub
        Private Sub editObjectItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles editObjectItem.ItemClick
            helper.ShowDocumentDataObject()
            UpdateText()
        End Sub
        Private Sub snapControl1_ModifiedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles snapControl1.ModifiedChanged
            saveToObjectItem.Enabled = Modified
        End Sub
        Private Sub SnapDesigner_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
            If Modified Then
                e.Cancel = Not helper.QueryCanClose()
            End If
        End Sub
        Private Sub UpdateText()
            If String.IsNullOrEmpty(helper.GetDocumentName()) Then
                Text = DefaultCaption
            Else
                Text = String.Format("{0} - {1}", DefaultCaption, helper.GetDocumentName())
            End If
        End Sub
        Private ReadOnly Property Modified() As Boolean
            Get
                Return helper.Modified OrElse snapControl1.Modified

            End Get
        End Property
    End Class
End Namespace