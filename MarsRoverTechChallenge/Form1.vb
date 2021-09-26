Public Class Form1


#Region "Properties"


    Public Property Instructions() As String
        Get
            Return txtInstructions.Text
        End Get
        Set(ByVal value As String)
            txtInstructions.Text = value
        End Set
    End Property


    Public Property Results() As String
        Get
            Return txtResults.Text
        End Get
        Set(ByVal value As String)
            txtResults.Text = value
        End Set
    End Property

#End Region

#Region "Business"




#End Region

#Region "Events"
    Private Sub btSend_Click(sender As Object, e As EventArgs) Handles btSend.Click
        Dim Control As New Model.ControlModule

        Results = Control.Process(Instructions)

    End Sub
#End Region
End Class
