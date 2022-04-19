Public Class Frmcambiarcontraseña
    Dim cambiocontraseña As String
    Dim contraseñaAnterior As String
    Dim contraseñaNueva As String
    Dim contraseñaActural As String
    Dim consulta As String
    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click

        Me.Close()
    End Sub
    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        Try
        If txtcontraseñaanterios.Text.Length > 0 And txtnuevacontraseña.Text.Length > 0 And txtusuarios.Text.Length > 0 Then
            consulta = conexion.EjecutarEscalar("SELECT COUNT(IdUsuarios) FROM usuarios_backmarginltx WHERE usuario='" & txtusuarios.Text.ToUpper & "'")
            If consulta = 1 Then
                contraseñaActural = conexion.EjecutarEscalar("SELECT contraseña FROM usuarios_backmarginltx WHERE usuario='" & txtusuarios.Text.ToUpper & "'")
                    contraseñaAnterior = conexion.EjecutarEscalar("SELECT CAST(SHA1('" & txtcontraseñaanterios.Text.ToUpper & "') AS CHAR)")
                    contraseñaNueva = conexion.EjecutarEscalar("SELECT CAST(SHA1('" & txtnuevacontraseña.Text.ToUpper & "') AS CHAR)")
                If String.Compare(contraseñaActural, contraseñaAnterior) = 0 Then
                    consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET contraseña=SHA1('" & txtnuevacontraseña.Text.ToUpper & "') WHERE usuario='" & txtusuarios.Text & "'")
                        MessageBox.Show("Se realizo con exito el cambio de contraseña", "Cambio de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtcontraseñaanterios.Text = ""
                        txtnuevacontraseña.Text = ""
                        txtusuarios.Text = ""
                    Else
                        MessageBox.Show("La contraseña anteior no coincide con la contraseña actual", "Contraseña no valida", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtcontraseñaanterios.Text = ""
                        txtnuevacontraseña.Text = ""
                        Exit Sub
                    End If

                    If String.Compare(contraseñaActural, contraseñaNueva) = 0 Then
                        MessageBox.Show("La Nueva Contraseña que está ingresando es identica a la contraseña que tiene actualmente", "Contraseñas identicas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtcontraseñaanterios.Text = ""
                        txtnuevacontraseña.Text = ""
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("El usuario que esta ingresando no es valido", "Usuario incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtcontraseñaanterios.Text = ""
                    txtnuevacontraseña.Text = ""
                    txtusuarios.Text = ""
                    Exit Sub
                End If
        Else
            MessageBox.Show("Tiene algún campo vacio, no se puede realizar el  cambio de contraseña", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtcontraseñaanterios.Text = ""
            txtnuevacontraseña.Text = ""
            txtusuarios.Text = ""
            Exit Sub
        End If
        Catch ex As Exception
            MessageBox.Show("")
        End Try

    End Sub

    Private Sub btnguardarU_Click(sender As Object, e As EventArgs) Handles btnguardarU.Click
        Dim id As Integer
        If txtusuarioA.Text.Length > 0 And txtusuarioN.Text.Length > 0 Then
            consulta = conexion.EjecutarEscalar("SELECT COUNT(IdUsuarios) FROM usuarios_backmarginltx WHERE usuario='" & txtusuarioA.Text.ToUpper & "'")
            If consulta = 1 Then
                id = conexion.EjecutarEscalar("SELECT idusuarios FROM usuarios_backmarginltx WHERE usuario='" & txtusuarioA.Text & "'")
                If String.Compare(txtusuarioA.Text, txtusuarioN.Text) <> 0 Then
                    consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET Usuario='" & txtusuarioN.Text & "' WHERE idusuarios='" & id & "'")
                    MessageBox.Show("Se realizo con exito el cambio de Usuario", "Cambio de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtusuarioA.Text = ""
                    txtusuarioN.Text = ""
                Else
                    MessageBox.Show("El Usuario que esta ingresando es identico al actual", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtusuarioA.Text = ""
                    txtusuarioN.Text = ""
                    Exit Sub
                End If

            Else
                MessageBox.Show("El usuario que esta ingresando no es valido", "Usuario incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtusuarioA.Text = ""
                txtusuarioN.Text = ""
                Exit Sub
            End If
        Else
            MessageBox.Show("Tiene algún campo vacio, no se puede realizar el  cambio de contraseña", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtusuarioA.Text = ""
            txtusuarioN.Text = ""
        End If
    End Sub

    Private Sub Frmcambiarcontraseña_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CAMBIAMOS LA CONEXION A LA DE CONTRATOS
        CargarConexionContratos()

      
    End Sub
End Class