Public Class frmtasadecambio

    Private Sub btnaceptar_Click(sender As Object, e As EventArgs) Handles btnaceptar.Click
        estado = True
        If chbtasacambio.CheckState = CheckState.Checked Then
            tasadecambio = txttasacambio.Text
            PreguntaTasaC = True
            Me.Close()
        Else
            tasadecambio = txttasacambio.Text
            PreguntaTasaC = False
            Me.Close()
        End If
    End Sub

    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()
        estado = False
    End Sub
End Class