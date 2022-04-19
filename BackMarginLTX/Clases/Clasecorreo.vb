Imports System.IO
Imports Cifrado
Public Class Clasecorreo

    Private _error As String
    Public Sub New(ByVal conex As String)

        conexion.BuscarConexionEnXml(conex)
    End Sub
    Public Property MensajeError() As String
        Get
            Return _error
        End Get
        Set(ByVal value As String)
            _error = value
        End Set
    End Property
    Public Function envia_correo(ByVal destinatario As String, ByVal token As String, ByVal sistema As String) As Boolean
        Dim Mensaje As New System.Net.Mail.MailMessage()
        Dim SMTP As New System.Net.Mail.SmtpClient

        Dim host As String
        Dim puerto As String
        Dim estado As Boolean
        Dim asunto As String
        Dim cuerpo As String
        Dim usuario_correo As String
        Dim clave_correo As String
        Dim envia As String


        Try

            envia = conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=4")
            host = conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=5")
            asunto = conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=6")
            cuerpo = token & conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=7") & sistema
            puerto = conexion.EjecutarEscalar("SELECT ivalor FROM parametros_sistema WHERE id=8")
            usuario_correo = conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=9")
            clave_correo = Cifrado.Usuario.IniciarDesencriptacion(conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=10"))
            estado = conexion.EjecutarEscalar("SELECT svalor FROM parametros_sistema WHERE id=11")

            'Configuración SMTP

            If estado = True Then
                SMTP.Host = host
                SMTP.Port = puerto
                SMTP.EnableSsl = True
                SMTP.Credentials = New System.Net.NetworkCredential(usuario_correo, clave_correo)
            Else
                SMTP.Host = host
            End If

            Mensaje.[To].Add(destinatario)
            Mensaje.From = New System.Net.Mail.MailAddress(envia, "Sistemas Unisuper", System.Text.Encoding.UTF8)
            Mensaje.Subject = asunto
            Mensaje.SubjectEncoding = System.Text.Encoding.UTF8 'Codificación
            Mensaje.Body = cuerpo
            Mensaje.BodyEncoding = System.Text.Encoding.UTF8
            Mensaje.Priority = System.Net.Mail.MailPriority.Normal
            Mensaje.IsBodyHtml = False

            SMTP.Send(Mensaje)
            Return True
        Catch ex As Exception
            MensajeError = ex.ToString
            Return False

            Exit Function

        Finally

            SMTP.Dispose()
        End Try
    End Function
End Class
