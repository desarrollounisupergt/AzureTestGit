Public Class Clase_CargarUpc
    Private _total As Integer = 0

    Private siguienteupc As Integer = 0

    Private _aDatos As New ArrayList 'Aqui se van a almacenar todos los datos...
    Private _clsdatos As claseDatos

  

    Private Class claseDatos
        Public subcategoria As String
        Public descripcion As String
        Public idsubcategoria As String
        Public sku As String
        Public costo As String
        Public formatocobro As String
        Public tipocobro As String
        Public cantidadcobro As String
        Public idtienda As String
        Public moneda As String

        Public Sub New(ByVal _subcategoria As String, _
                        ByVal _descripcion As String, _
                        ByVal _sku As String, _
                        ByVal _costo As String, ByVal _formatocobro As String, ByVal _idsubcategoria As String, _
                        ByVal _tipocobro As String, ByVal _cantidadcobro As String, ByVal _idtienda As String, _
                        ByVal _moneda As String)
            subcategoria = Trim(_subcategoria)
            descripcion = Trim(_descripcion)
            sku = _sku
            costo = _costo
            formatocobro = _formatocobro
            idsubcategoria = _idsubcategoria
            tipocobro = _tipocobro
            cantidadcobro = _cantidadcobro
            idtienda = _idtienda
            moneda = _moneda
        End Sub

    End Class
    Public ReadOnly Property moneda() As String
        Get
            Return _clsdatos.moneda
        End Get
    End Property
    Public ReadOnly Property tipocobro() As String
        Get
            Return _clsdatos.tipocobro
        End Get
    End Property
    Public ReadOnly Property idtienda() As String
        Get
            Return _clsdatos.idtienda
        End Get
    End Property
    Public ReadOnly Property cantidadcobro() As String
        Get
            Return _clsdatos.cantidadcobro
        End Get
    End Property

    Public ReadOnly Property subcategoria() As String
        Get
            Return _clsdatos.subcategoria
        End Get
    End Property

    Public ReadOnly Property costo() As String
        Get
            Return _clsdatos.costo
        End Get
    End Property

    Public ReadOnly Property descripcion() As String
        Get
            Return _clsdatos.descripcion
        End Get
    End Property

    Public ReadOnly Property formatocobro() As String
        Get
            Return _clsdatos.formatocobro
        End Get
    End Property


    Public ReadOnly Property sku() As String
        Get
            Return _clsdatos.sku
        End Get
    End Property



    Public ReadOnly Property idsubcategoria() As String
        Get
            Return _clsdatos.idsubcategoria
        End Get
    End Property

    Public ReadOnly Property total() As Integer
        Get
            Return _total
        End Get
    End Property




    Public Sub AgregarProducto(ByVal _subcategoria As String, _
                        ByVal _descripcion As String, _
                        ByVal _sku As String, _
                        ByVal _costo As String, _
                        ByVal _formatocobro As String, ByVal _idsubcategoria As String, _
                        ByVal _tipocobro As String, ByVal _cantidadcobro As String, ByVal _idtienda As String, _
                        ByVal _moneda As String)
        _aDatos.Add(New claseDatos(_subcategoria, _
                                    _descripcion, _
                                    _sku, _
                                     _costo, _formatocobro, _idsubcategoria, _
                                     _tipocobro, _cantidadcobro, _idtienda, _moneda))
        If _total = 0 Then
            _clsdatos = _aDatos.Item(siguienteupc)
        End If
        _total += 1
    End Sub

    'Se obtienen los datos del siguiente producto,,
    'Si retorna false quiere decir que ya no existen productos pendientes...

    Public Function siguiente() As Boolean
        If siguienteupc < (_total - 1) Then
            siguienteupc += 1
            _clsdatos = _aDatos.Item(siguienteupc)
            Return True
        End If
    End Function



End Class
