Public Class Clase_Cargar

    Private _total As Integer = 0
    'el numero del producto que se esta enviando a imprimir...
    Private siguientenombre As Integer = 0

    Private _aDatos As New ArrayList 'Aqui se van a almacenar todos los datos...
    Private _clsdatos As claseDatos



    Private Class claseDatos
        Public id As String
        Public nombre As String
      
        Public Sub New(ByVal _id As String, _
                        ByVal _nombre As String)
            id = _id
            nombre = _nombre
         
        End Sub

    End Class

    Public ReadOnly Property id() As String
        Get
            Return _clsdatos.id
        End Get
    End Property

    Public ReadOnly Property nombre() As String
        Get
            Return _clsdatos.nombre
        End Get
    End Property

 

    Public ReadOnly Property total() As Integer
        Get
            Return _total
        End Get
    End Property




    Public Sub AgregarNombre(ByVal _id As String, _
                        ByVal _nombre As String)
        _aDatos.Add(New claseDatos(_id, _nombre))
        If _total = 0 Then
            _clsdatos = _aDatos.Item(siguientenombre)
        End If
        _total += 1
    End Sub



    Public Function siguiente() As Boolean
        If siguientenombre < (_total - 1) Then
            siguientenombre += 1
            _clsdatos = _aDatos.Item(siguientenombre)
            Return True
        End If
    End Function



End Class
