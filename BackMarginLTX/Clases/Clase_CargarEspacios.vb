Public Class Clase_CargarEspacios
    Private _total As Integer = 0

    Private sigregistro As Integer = 0

    Private _aDatos As New ArrayList 'Aqui se van a almacenar todos los datos...
    Private _clsdatos As claseDatos

    Private Class claseDatos
        Public idtipoespacio As String
        Public idtienda As String
        Public cantidad As String
        Public costoespacio As Decimal
        Public totalpagar As Decimal
        Public tipocobro As String
        Public moneda As String

        Public Sub New(ByVal _idtipoespacio As String, _
                       ByVal _idtienda As String, ByVal _cantidad As String, ByVal _costoespacio As Decimal, _
                       ByVal _totalpagar As Decimal, ByVal _tipocobro As String, ByVal _moneda As String)

            idtipoespacio = _idtipoespacio
            idtienda = _idtienda
            cantidad = _cantidad
            costoespacio = _costoespacio
            totalpagar = _totalpagar
            tipocobro = _tipocobro
            moneda = _moneda
        End Sub

    End Class
    Public ReadOnly Property tipocobro() As String
        Get
            Return _clsdatos.tipocobro
        End Get
    End Property

    Public ReadOnly Property moneda() As String
        Get
            Return _clsdatos.moneda
        End Get
    End Property

    Public ReadOnly Property totalpagar() As String
        Get
            Return _clsdatos.totalpagar
        End Get
    End Property
    Public ReadOnly Property idtipoespacio() As String
        Get
            Return _clsdatos.idtipoespacio
        End Get
    End Property

    Public ReadOnly Property idtienda() As String
        Get
            Return _clsdatos.idtienda
        End Get
    End Property
    Public ReadOnly Property cantidad() As String
        Get
            Return _clsdatos.cantidad
        End Get
    End Property
    Public ReadOnly Property costosespacio() As Decimal
        Get
            Return _clsdatos.costoespacio
        End Get
    End Property

    Public ReadOnly Property total() As Integer
        Get
            Return _total
        End Get
    End Property


    Public Sub AgregarEspacio(ByVal _idtipoespacio As String, _
                        ByVal _idtienda As String, ByVal _cantidad As String, _
                        ByVal _costosespacio As Decimal, ByVal _totalpagar As Decimal, ByVal _tipocobro As String, _
                        ByVal _moneda As String)
        _aDatos.Add(New claseDatos(_idtipoespacio, _
                                    _idtienda, _cantidad, _
                                    _costosespacio, _totalpagar, _tipocobro, _moneda))
        If _total = 0 Then
            _clsdatos = _aDatos.Item(sigregistro)
        End If
        _total += 1
    End Sub

    Public Function siguiente() As Boolean
        If sigregistro < (_total - 1) Then

            sigregistro += 1
            _clsdatos = _aDatos.Item(sigregistro)
            Return True
        End If
    End Function

End Class
