Imports DLLConexionAD
Public Class claseUsuario
    Private conexion As New claseConexion
    Private validaAD As New DLLConexionAD.ConexionAD
#Region "Variables"

    'Private crypto As New Cifrado.ClaseCifrarConexion

    Public Sub New(ByVal conex As String)

        conexion.BuscarConexionEnXml(conex)
    End Sub

    Private _nombre As String
    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
        End Set
    End Property


    Private _apellido As String
    Public Property apellido() As String
        Get
            Return _apellido
        End Get
        Set(value As String)
            _apellido = value
        End Set
    End Property

    Private _idusuario As Integer
    Public Property idusuario() As Integer
        Get
            Return _idusuario
        End Get
        Set(value As Integer)
            _idusuario = value
        End Set
    End Property



    Private _usuario As String

    Public Property usuario() As String
        Get
            Return _usuario
        End Get
        Set(ByVal value As String)
            _usuario = value
        End Set
    End Property



    Private _password As String
    Public Property password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set

    End Property

    Private _email As String
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Private _puesto As String
    Public Property puesto() As String
        Get
            Return _puesto
        End Get
        Set(ByVal value As String)
            _puesto = value
        End Set
    End Property

    Private _nivel As Integer
    Public Property nivel() As Integer
        Get
            Return _nivel
        End Get
        Set(ByVal value As Integer)
            _nivel = value
        End Set
    End Property

    Private _activo As Integer

    Public Property activo() As Integer
        Get
            Return _activo
        End Get
        Set(ByVal value As Integer)
            _activo = value
        End Set
    End Property

    Private _ds_datos As New DataSet
    Public Property ds_datos As DataSet
        Get
            Return _ds_datos
        End Get
        Set(value As DataSet)
            _ds_datos = value
        End Set
    End Property


    Private _ds_permisos As New DataSet
    Public Property ds_permisos As DataSet
        Get
            Return _ds_permisos
        End Get
        Set(value As DataSet)
            _ds_permisos = value
        End Set
    End Property
    Private _token As String
    Private _sistema As String
    Public MsgSalida As String

    Private _grupousuario As String
    Public Property GrupoUsuario() As String
        Get
            Return _grupousuario
        End Get
        Set(ByVal value As String)
            _grupousuario = value
        End Set
    End Property

    Private _idgrupoUsuario As Integer
    Public Property IdgrupoUsuario() As Integer
        Get
            Return _idgrupoUsuario
        End Get
        Set(ByVal value As Integer)
            _idgrupoUsuario = value
        End Set
    End Property

    Public listaGrupos As String()


#End Region



    Public Function validarUsuario() As Boolean
        Try

            _sistema = conexion.EjecutarEscalar("SELECT tvalor FROM parametros_sistema WHERE id=12")
            'DirectorySearcher dirSearch


            validaAD.Dominio = String.Empty
            validaAD.Username = String.Empty
            validaAD.Nombre = String.Empty
            validaAD.Apellido = String.Empty
            validaAD.Email = String.Empty
            validaAD.Puesto = String.Empty
            If validaAD.ValidarUsuario(usuario, password) = True Then


                usuario = validaAD.Username
                nombre = validaAD.Nombre
                apellido = validaAD.Apellido
                email = validaAD.Email
                puesto = validaAD.Puesto

                For Each fila As String In ConexionAD.ListaGrupos
                    listaGrupos = fila.Split(",")
                    For Each item As String In listaGrupos
                        If ValidarGrupo(item) = True Then
                            IdgrupoUsuario = conexion.EjecutarEscalar("SELECT id_grupo FROM grupoad_acceso WHERE nombre_grupo='" & item & "'")
                            GrupoUsuario = conexion.EjecutarEscalar("SELECT nombre_grupo FROM grupoad_acceso WHERE nombre_grupo='" & item & "'")
                        End If
                    Next
                Next


                Return True
            Else

                Return False
            End If

        Catch ex As Exception
            MsgSalida = validaAD.MensajeSalida
            Throw New InvalidOperationException(ex.Message)
        Finally

            conexion.CerrarConexion()
        End Try

    End Function
    Private Function ValidarGrupo(ByVal _grupoItem As String) As Boolean
        If conexion.EjecutarEscalar("SELECT COUNT(nombre_grupo) FROM grupoad_acceso WHERE nombre_grupo='" & _grupoItem & "'") > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub cargar_opciones(ByVal idusuarios As Integer)
        Try


            conexion.AbrirConexion()
            _ds_permisos = conexion.llenarDataSet("SELECT p.idopcion, p.activo,descripcion FROM hg_permisos p INNER JOIN hg_opciones o ON p.idopcion=o.id WHERE p.idusuario=" & idusuarios & " ", "Permisos")

        Catch ex As Exception
            Throw New InvalidOperationException(ex.Message)
        Finally
            conexion.CerrarConexion()
        End Try
    End Sub


    Public Sub cargar_listaopciones()
        Try

            conexion.AbrirConexion()

            _ds_permisos = conexion.llenarDataSet("SELECT id as 'idopcion', 1 AS 'Activo', descripcion FROM hg_opciones", "Permisos")

        Catch ex As Exception
            Throw New InvalidOperationException(ex.Message)
        Finally
            conexion.CerrarConexion()
        End Try
    End Sub


End Class
