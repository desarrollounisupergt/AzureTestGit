Imports MySql.Data.MySqlClient
Imports System.Reflection

Public Class Frminiciodesesion
    Dim clsUsuario As New claseUsuario("conexion2.xml")
    Dim correo As New Clasecorreo("conexion2.xml")
    Dim usuario As Integer
    Dim contraseñaactual As String
    Dim contraseñaBD As String
    Dim validar_version As String
    Private _token As String
    Private _sistema As String
    Shared c As Integer
    Dim tiempo_vigencia As Integer
    Private Sub Frminiciodesesion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' cargarConexionMysql()
        txtusuario.Text = String.Empty
        txtcontraseña.Text = String.Empty
        cargarConexionMysql1("conexion2.xml")
        validar_version = conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=1")
        lblVersion.Text = "V." & Application.ProductVersion
        If Assembly.GetExecutingAssembly().GetName().Version.ToString <> validar_version Then
            'txtUsuario.Enabled = False
            btnaceptar.Enabled = False
            MessageBox.Show("La versión del sistema no es la correcta, por favor comuníquese al Departamento de Sistemas. ", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

    End Sub

    Private Sub btnaceptar_Click(sender As Object, e As EventArgs) Handles btnaceptar.Click

        Dim lista As String = conexion.EjecutarEscalar("SELECT tvalor FROM parametros_sistema WHERE id=2")

        listaCaracteresEspeciales = lista.Split(",")


        If Trim(txtcontraseña.Text.Length) > 0 And Trim(txtusuario.Text.Length) > 0 Then
            txtusuario.Text = limpiarCadenaTexto(txtusuario.Text, "")
            txtcontraseña.Text = limpiarCadenaTexto(txtcontraseña.Text, "")

            clsUsuario.usuario = txtusuario.Text
                clsUsuario.password = txtcontraseña.Text


            If clsUsuario.validarUsuario() = True Then


                _token = Cifrado.Usuario.Token_de_Acceso
                _sistema = conexion.EjecutarEscalar("SELECT tvalor FROM parametros_sistema WHERE id=12")
                If correo.envia_correo(clsUsuario.email, _token, _sistema) = True Then
                    _token = Cifrado.Usuario.IniciarEncriptacion((_token))
                    conexion.EjecutarNonQuery("DELETE FROM tmp_usuario_token WHERE usuario='" & clsUsuario.usuario & "'")
                    conexion.EjecutarNonQuery("INSERT INTO tmp_usuario_token(usuario,token,fecha)VALUES('" & clsUsuario.usuario & "','" & _token & "',NOW())")
                    nombreusuario = txtusuario.Text
                    'nivelusuario = clsUsuario.IdgrupoUsuario
                    'Me.Close()
                    txtcontraseña.Text = String.Empty
                    txtusuario.Text = String.Empty

                    'frmValidarToken.ShowDialog()
                    Panel2.Visible = True
                    InicializarConteo()

                Else
                    MessageBox.Show("No se ha podido enviar el token, ", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Usuario o clave no valida, ", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtusuario.Text = String.Empty
                txtcontraseña.Text = String.Empty

                Exit Sub
                End If


            '===========================================================================================================================================================
            'SE CONECTARA POR MEDIO DE AD, YA NO SE CONSULTARA LA TABLA DE USUARIOS DEL SISTEMA LBOCH ENERO 2021
            'Dim consulta As String
            ''consulta = conexion.EjecutarEscalar(" SELECT COUNT(IdUsuarios) FROM usuarios_backmarginltx WHERE usuario='" & txtusuario.Text.ToUpper & "'")
            ''If consulta > 0 Then
            ''    contraseñaactual = conexion.EjecutarEscalar(" SELECT CAST(SHA1('" & txtcontraseña.Text.ToUpper & "')AS CHAR) ")

            ''    contraseñaBD = conexion.EjecutarEscalar("SELECT contraseña FROM usuarios_backmarginltx WHERE usuario='" & txtusuario.Text.ToUpper & "'")

            ''    If String.Compare(contraseñaactual, contraseñaBD) = 0 Then
            ''        nivelusuario = conexion.EjecutarEscalar("SELECT privilegios FROM usuarios_backmarginltx WHERE estado=1 AND usuario='" & txtusuario.Text & "'")
            ''        If nivelusuario >= 1 Then
            ''            nombreusuario = txtusuario.Text.ToUpper
            ''            Me.Close()
            ''        Else
            ''            MessageBox.Show("El Usuario no tiene permisos para ingresar al sistema, ", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''        End If

            ''    ElseIf String.Compare(contraseñaactual, contraseñaBD) <> 0 Then
            ''        MessageBox.Show("Contraseña Incorrecta.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''        txtcontraseña.Text = ""
            ''    End If
            ''Else
            ''    MessageBox.Show("El Usuario no existe en la Base de Datos.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''End If
            ''''===========================================================================================================================================================

        Else
            MessageBox.Show("El campo de Usuario o de  Contraseña se encuentran vacios.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End
        End If

    End Sub

    Private Sub btncancelar_Click(sender As Object, e As EventArgs) Handles btncancelar.Click
        nivelusuario = 0
        End
    End Sub

    Private Sub InicializarConteo()
        tiempo_vigencia = conexion.EjecutarEscalar("SELECT ivalor FROM parametros_sistema WHERE id=3")
        Label3.ForeColor = System.Drawing.Color.Black
        MessageBox.Show("El TOKEN de seguridad se ha enviado a su correo electrónico.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)

        c = tiempo_vigencia
        'If nivelusuario > 0 Then
        Timer1.Enabled = True
            Timer1.Interval = 1000
            Timer1.Start()
        'Else
        'Me.Close()
        'End If

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        c = c - 1
        Label3.Text = c
        If c < 11 Then
            Label3.ForeColor = System.Drawing.Color.Red
        Else
            Label3.ForeColor = System.Drawing.Color.Black
        End If
        If c = 0 Then
            conexion.EjecutarNonQuery("DELETE FROM tmp_usuario_token WHERE usuario='" & nombreusuario & "'")
            nivelusuario = 0
            Timer1.Stop()
            txtToken.Text = String.Empty
            Label3.Text = 120
            txtusuario.Focus()
            Panel2.Visible = False
            Exit Sub

        End If
    End Sub
    Dim intentos As Integer = 0


    Private Sub btnValidarToken_Click(sender As Object, e As EventArgs) Handles btnValidarToken.Click
        Dim recuperar_token As String = String.Empty
        Dim ingresar_token As String = String.Empty
        If txtToken.Text.Length = 0 Then
            MessageBox.Show("Debe ingresar El TOKEN de seguridad enviado a su correo electrónico.", "BackMargin Express", MessageBoxButtons.OK, MessageBoxIcon.Information)
            intentos = intentos + 1
            If intentos > 3 Then
                nivelusuario = 0
                Panel2.Visible = False
                txtToken.Text = String.Empty
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
                nivelusuario = clsUsuario.IdgrupoUsuario
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