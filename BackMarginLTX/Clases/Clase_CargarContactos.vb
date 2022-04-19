Public Class Clase_CargarContactos
    Private _total As Integer = 0
    'el numero del producto que se esta enviando a imprimir...
    Private sigregistro As Integer = 0

    Private _aDatos As New ArrayList 'Aqui se van a almacenar todos los datos...
    Private _clsdatos As claseDatos

    Private Function ValidaNulos(Of tipo)(ByVal valor As Object, ByVal ReturnIfNull As tipo) As tipo
        If IsDBNull(valor) Or valor Is Nothing Then 'si es nulo retornamos el valor deseado...
            Return ReturnIfNull
        End If
        Return valor 'si no es nulo retornamos el valor original...
    End Function

    Private Class claseDatos
        Public nombre As String
        Public cargo As String
        Public telefono As String
        Public correo As String

        Public Sub New(ByVal _nombre As String, _
                        ByVal _cargo As String, _
                        ByVal _telefono As String, _
                        ByVal _correo As String)
            nombre = Trim(_nombre)
            cargo = Trim(_cargo)
            telefono = _telefono
            correo = _correo

        End Sub

    End Class

    ' el cargo del contacto...

    Public ReadOnly Property cargo() As String
        Get
            Return _clsdatos.cargo
        End Get
    End Property


    ' El correo...

    Public ReadOnly Property correo() As String
        Get
            Return _clsdatos.correo
        End Get
    End Property
    'El nombre...
    Public ReadOnly Property nombre() As String
        Get
            Return _clsdatos.nombre
        End Get
    End Property

    ' El numero de telefono...

    Public ReadOnly Property telefono() As String
        Get
            Return _clsdatos.telefono
        End Get
    End Property


    Public ReadOnly Property total() As Integer
        Get
            Return _total
        End Get
    End Property




    Public Sub AgregarContacto(ByVal _nombre As String, _
                        ByVal _cargo As String, _
                        ByVal _telefono As String, _
                        ByVal _correo As String)
        _aDatos.Add(New claseDatos(_nombre, _
                                    _cargo, _
                                    _telefono, _
                                    _correo))
        If _total = 0 Then
            _clsdatos = _aDatos.Item(sigregistro)
        End If
        _total += 1
    End Sub

    'Se obtienen los datos del siguiente producto,,
    'Si retorna false quiere decir que ya no existen productos pendientes...

    Public Function siguiente() As Boolean

        If sigregistro < (_total - 1) Then
            sigregistro += 1
            _clsdatos = _aDatos.Item(sigregistro)
            Return True
        End If
    End Function

End Class
