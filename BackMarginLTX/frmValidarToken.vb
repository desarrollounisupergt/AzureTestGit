Public Class frmValidarToken
    Shared c As Integer
    Dim tiempo_vigencia As Integer
    Dim clsUsuario As New claseUsuario("conexion2.xml")

    Private Sub frmValidarToken_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tiempo_vigencia = conexion.EjecutarEscalar("SELECT ivalor FROM parametros_sistema WHERE id=3")
        Label3.ForeColor = System.Drawing.Color.Black
        MessageBox.Show("El TOKEN de seguridad se ha enviado a su correo electrónico.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)

        c = tiempo_vigencia
        If nivelusuario > 0 Then
            Timer1.Enabled = True
            Timer1.Interval = 1000
            Timer1.Start()
        Else
            Me.Close()
        End If




    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        c = c - 1
        Label3.Text = c
        If c < 11 Then

            Label3.ForeColor = System.Drawing.Color.Red
        End If
        If c = 0 Then
            conexion.EjecutarNonQuery("DELETE FROM tmp_usuario_token WHERE usuario='" & nombreusuario & "'")
            nivelusuario = 0
            Me.Close()
            Exit Sub

        End If
    End Sub
    Dim intentos As Integer = 0
    Private Sub btnaceptar_Click(sender As Object, e As EventArgs) Handles btnaceptar.Click
        Dim recuperar_token As String = String.Empty
        Dim ingresar_token As String = String.Empty
        If txtToken.Text.Length = 0 Then
            MessageBox.Show("Debe ingresar El TOKEN de seguridad enviado a su correo electrónico.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
            intentos = intentos + 1
            If intentos > 3 Then
                nivelusuario = 0
                Me.Close()
            End If
            Exit Sub
        Else
            recuperar_token = Cifrado.Usuario.IniciarDesencriptacion(conexion.EjecutarEscalar("SELECT token FROM tmp_usuario_token WHERE usuario='" & nombreusuario & "'"))
            Dim lista As String = conexion.EjecutarEscalar("SELECT tvalor FROM parametros_sistema WHERE id=2")

            listaCaracteresEspeciales = lista.Split(",")
            ingresar_token = limpiarCadenaTexto(txtToken.Text, "")
            If recuperar_token = ingresar_token Then
                conexion.EjecutarNonQuery("DELETE FROM tmp_usuario_token WHERE usuario='" & nombreusuario & "'")
                Timer1.Stop()
                FrmMenuPrincipal.Show()
                Me.Close()
            Else
                MessageBox.Show("El TOKEN ingresado es incorrecto.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
                intentos = intentos + 1
                txtToken.Text = String.Empty
                txtToken.Focus()
                If intentos > 3 Then
                    nivelusuario = 0
                    Me.Close()
                End If

            End If
        End If
    End Sub
End Class