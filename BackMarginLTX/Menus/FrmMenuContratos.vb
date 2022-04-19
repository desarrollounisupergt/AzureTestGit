Public Class FrmMenuContratos

    Public crearcontrato As New frmcontrato
    Public modificarcontrato As New Frmmodificardetalle
    Public eliminarcontrato As New FrmEliminarContrato

    Private Sub btncrearcontrato_Click(sender As Object, e As EventArgs) Handles btncrearcontrato.Click
        crearcontrato = New frmcontrato
        tipo = 1
        crearcontrato.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnModificarcontrato_Click(sender As Object, e As EventArgs) Handles btnModificarcontrato.Click
        modificarcontrato = New Frmmodificardetalle
        tipo = 2
        modificarcontrato.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btneliminarcontrato_Click(sender As Object, e As EventArgs) Handles btneliminarcontrato.Click
        eliminarcontrato = New FrmEliminarContrato
        eliminarcontrato.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub


   
    Private Sub FrmMenuContratos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        FrmMenuPrincipal.WindowState = FormWindowState.Normal
    End Sub


    Private Sub FrmMenuContratos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        verificarprivilegios(nivelusuario)
    End Sub
    Public Sub verificarprivilegios(ByVal idprivilegios As String)

        Select Case idprivilegios
            Case 1
                btncrearcontrato.Enabled = True
                btneliminarcontrato.Enabled = True
                btnModificarcontrato.Enabled = True
            Case 3
                btncrearcontrato.Enabled = True
                btnModificarcontrato.Enabled = True


        End Select
    End Sub
End Class