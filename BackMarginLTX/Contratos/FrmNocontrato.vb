Imports MySql.Data.MySqlClient

Public Class FrmNocontrato
    Dim consultaexistencia As Integer = 0
    Dim unioncontrato As String = String.Empty
 

    Private Sub btnnocontrato_Click(sender As Object, e As EventArgs) Handles btnnocontrato.Click



        If Trim(txtaño.Text.Length) >= 1 And Trim(txtnumero.Text.Length) >= 1 Then
            unioncontrato = "LTX-" & txtnumero.Text & "-" & txtaño.Text
            If tipo = 1 Then
                consultaexistencia = conexion.EjecutarEscalar("SELECT COUNT(idcontrato) FROM contratos_backmarginltx WHERE idcontrato='" & unioncontrato & "'")
                If consultaexistencia = 0 Then
                    Nocontratofrm = unioncontrato
                    estadocontrato = 1
                    añoingresar = txtaño.Text
                    correlativo = txtnumero.Text
                    MessageBox.Show("El numero de contrato que ingreso si es valido ", "Numero de contrato Valido", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Else
                    MessageBox.Show("El numero de contrato que desea ingresar ya existe", "Contrato ya Existe")
                End If

            ElseIf tipo = 2 Then
                consultaexistencia = 0
                consultaexistencia = conexion.EjecutarEscalar("SELECT COUNT(idcontrato) FROM contratos_backmarginltx WHERE idcontrato='" & unioncontrato & "'  AND estado=1")
                If consultaexistencia > 0 Then
                    Nocontratofrm = unioncontrato
                    estadocontrato = 1
                    añoingresar = txtaño.Text
                    correlativo = txtnumero.Text
                    MessageBox.Show("El numero de contrato que ingreso si es valido ", "Numero de contrato Valido", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Else
                    MessageBox.Show("El numero de contrato que desea ingresar No existe", "Contrato No Existe", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Else

        End If
    End Sub



    Private Sub FrmNocontrato_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        estadocontrato = 0
    End Sub
End Class