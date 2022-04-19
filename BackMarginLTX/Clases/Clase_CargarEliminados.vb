Public Class Clase_CargarEliminados
    Private _total As Integer = 0
    'el numero del producto que se esta enviando a imprimir...
    Private siguientelemente As Integer = 0

    Private _aDatos As New ArrayList 'Aqui se van a almacenar todos los datos...
    Private _clsdatos As claseDatos



    Private Class claseDatos
        Public idelemento As String
        Public tienda As String
        Public tipoelemento As String

        Public Sub New(ByVal _id As String, _
                        ByVal _nombre As String, ByVal _tipoelemento As String)
            idelemento = _id
            tienda = _nombre
            tipoelemento = _tipoelemento
        End Sub

    End Class

    Public ReadOnly Property idelemento() As String
        Get
            Return _clsdatos.idelemento
        End Get
    End Property

    Public ReadOnly Property tienda() As String
        Get
            Return _clsdatos.tienda
        End Get
    End Property

    Public ReadOnly Property tipoelemento() As String
        Get
            Return _clsdatos.tipoelemento
        End Get
    End Property

    Public ReadOnly Property total() As Integer
        Get
            Return _total
        End Get
    End Property




    Public Sub Agregarelemento(ByVal _id As String, _
                        ByVal _nombre As String, ByVal _tipoelemento As String)
        _aDatos.Add(New claseDatos(_id, _nombre, _tipoelemento))
        If _total = 0 Then
            _clsdatos = _aDatos.Item(siguientelemente)
        End If
        _total += 1
    End Sub



    Public Function siguiente() As Boolean
        If siguientelemente < (_total - 1) Then
            siguientelemente += 1
            _clsdatos = _aDatos.Item(siguientelemente)
            Return True
        End If

    End Function

End Class
