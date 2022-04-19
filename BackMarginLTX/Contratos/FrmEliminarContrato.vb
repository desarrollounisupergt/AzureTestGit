Public Class FrmEliminarContrato
    Dim dscontratos As New DataSet
    Dim contrato As String
    Dim consultaeliminar As String
    Dim consultahistorico As String
    Dim formatocontrato As String
    Dim fecha As Date
    Dim fechaenviar As String
    Dim hora As String
    Dim fechaactualizada As String
    Dim resultado As String
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
        FrmMenuContratos.WindowState = FormWindowState.Normal
    End Sub

    Private Sub FrmEliminarContrato_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        FrmMenuContratos.WindowState = FormWindowState.Normal
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
       
        formatocontrato = lblinicial.Text & "-" & txtnumero.Text & "-" & txtaño.Text
        contrato = conexion.EjecutarEscalar("SELECT COUNT(idcontrato) FROM contratos_backmarginltx WHERE idcontrato='" & formatocontrato & "' AND estado=1")


        If contrato = 1 Then
            dscontratos = conexion.llenarDataSet("SELECT IdNombreProveedor,fechainicio,fechafinal FROM contratos_backmarginltx WHERE Idcontrato='" & formatocontrato & "'")
            txtproveedor.Text = dscontratos.Tables(0).Rows(0).Item(0)
            txtfechainicial.Text = dscontratos.Tables(0).Rows(0).Item(1)
            txtfechafinal.Text = dscontratos.Tables(0).Rows(0).Item(2)

        Else
            MessageBox.Show("El Numero de Contrato que ingreso no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            limpiartext()
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        resultado = MessageBox.Show("Esta seguro que desea eliminar el contrato", "Eliminar Contrato", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        fecha = Now
        fechaenviar = Format(fecha, "yyyy-MM-dd")
        hora = Format(fecha, "hh:mm:ss")
        fechaactualizada = fechaenviar & "  " & hora

        If resultado = vbYes Then
            If Trim(txtaño.Text.Length) > 0 And Trim(txtnumero.Text.Length) > 0 And Trim(txtproveedor.Text.Length) > 0 Then
                consultaeliminar = conexion.EjecutarNonQuery("UPDATE contratos_backmarginltx SET estado=0 WHERE idcontrato='" & formatocontrato & "'")
                MessageBox.Show("Se Elimino el contrato con exito.", "Cambios realizados", MessageBoxButtons.OK, MessageBoxIcon.Information)
                consultahistorico = conexion.EjecutarNonQuery("INSERT INTO historico_bmltx(fechamodificado,usuario,Tipo_modificacion,Elemento_modificado,Detalle_ElementoM,Detalle_NElemento,Nocontrato) " & _
                                                              "VALUES('" & fechaactualizada & "','" & nombreusuario & "','Elimnacion ','Contrato','se elimino el contrato No " & formatocontrato & "','No hay elementos a reemplazar','" & formatocontrato & "')")
                limpiartext()
            Else
                MessageBox.Show("Algun campo se encuentra vacio y no se puede realizar la eliminacion del contrato", "Campo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            limpiartext()
            Exit Sub
        End If

       
    End Sub

    Public Sub limpiartext()
        txtaño.Text = ""
        txtfechafinal.Text = ""
        txtfechainicial.Text = ""
        txtnumero.Text = ""
        txtproveedor.Text = ""
    End Sub

 
End Class