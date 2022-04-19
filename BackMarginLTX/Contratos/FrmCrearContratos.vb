Imports MySql.Data.MySqlClient
Imports BackMarginLTX.exportacioncontratos
Imports System.Text
Imports System.IO
Imports System.Threading
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Public Class frmcontrato

#Region "VARIABLES"

    Private conexionE As New clsConexionSqlServer
    Dim cargar, cargar1 As New Clase_Cargar
    Public nocontrato1 As New FrmNocontrato
    Dim exportar As New exportacioncontratos
    Dim dtproveedores As New DataTable
    Dim dtmarcas As New DataTable
    Dim dsproductos As New DataSet
    Dim dscontactos As New DataSet
    Dim dsProveedorFact As New DataSet
    Dim dstiendas As New DataSet
    Dim dsespaciosreservadps As New DataSet
    Dim dtespacios As New DataTable
    Dim dttiposespacio As New DataTable
    Dim dtcontactos As New DataTable
    Dim dttienda As New DataTable
    Dim consulta As String = String.Empty
    Dim idproveedor As Integer = 0
    Dim productos As Integer = 0
    Dim sigUpc As Boolean = True
    Dim sigEspacio As Boolean = True
    Dim contactos As Boolean = False
    Dim detallecontrato As String = String.Empty
    '=================================================================
    Dim crearcorrelativo As String = String.Empty
    Dim unionmarcas As String = String.Empty
    Dim fecha As Date
    Dim año As String = String.Empty
    Dim tamaño As Integer = 0
    Dim numero As Integer = 0
    Dim uniontiendas As String = String.Empty
    Dim consulta2 As String = String.Empty
    Dim fechaenviar As String
    Dim direccion As String
    Dim mes1, mes2 As Integer
    Dim numfilas As Integer
    Dim mensaje As String = String.Empty
#End Region
   
    Private Sub frmcontrato_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CargarConexionContratos()
        FrmMenuContratos.WindowState = FormWindowState.Normal
    End Sub

    Private Sub frmcontrato_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'COLOCAMOS EL ID DEL CONTRATO
        Try
        nocontrato1 = New FrmNocontrato
        nocontrato1.ShowDialog()
        If estadocontrato = 1 Then
            lblnocontrato.Text = Nocontratofrm
            lblnocontrato1.Text = Nocontratofrm
            lblnocontrato2.Text = Nocontratofrm
        Else
            fecha = Now
            año = Year(fecha)
            tamaño = año.Length
            añoingresar = año.Substring((tamaño - 2), 2)
            numero = conexion.EjecutarEscalar("SELECT COUNT(nocorrelativo)FROM correlativo_contratosbmltx")

            If numero = 0 Then
                correlativo = 1
            Else
                correlativo = conexion.EjecutarEscalar("SELECT MAX(Nocorrelativo)FROM correlativo_contratosbmltx")
                correlativo = correlativo + 1
            End If

            nocontrato = Format(Val(correlativo), "0000")
            crearcorrelativo = "LTX-" & nocontrato & "-" & añoingresar
            lblnocontrato.Text = crearcorrelativo
            lblnocontrato1.Text = crearcorrelativo
            lblnocontrato2.Text = crearcorrelativo

            End If
            
            'AQUI LLENAMOS EL DATASET DE LAS TIENDAS EXPRESSS
            consulta = ""
            consulta = "SELECT id,nombre FROM `sucursales_bmltx` WHERE Idempresas=5"
            Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
            adap.Fill(DataSet4, "tienda")
            dgvtiendas.DataSource = DataSet4.Tables("tienda")
            For Each fila2 As DataRow In DataSet4.Tables("tienda").Rows
                cmbtienda.Items.Add(fila2.Item("Nombre"))
            Next
            'LLENAMOS LOS ESPACIOS.
            dtespacios = conexion.llenarDataTable("SELECT Nombre FROM tipoespacio_bmltx")
            For Each fila1 As DataRow In dtespacios.Rows
                cmbespacio.Items.Add(fila1.Item("Nombre"))
            Next

            'CAMBIAMOS LA CONEXION PARA TRAER A LOS PROVEEDORES A LOS QUE SE VA FACTURAR EN EXACTUS
            ' conexion.CerrarConexion()
            conexionE.BuscarConexionEnXml(Application.StartupPath + "\" & ("arconexionExactus.xml"))
            conexionE.AbrirConexion()
            Dim dsFac As New DataSet
            dsFac.Clear()
            dsFac = conexion.llenarDataSet("SELECT * FROM parametro_factura WHERE estado='1'")

            Dim Sval As String = String.Empty
            Dim pFac As String = String.Empty

            For Each rowFac As DataRow In dsFac.Tables(0).Rows
                Sval &= "'" & rowFac("descripcion") & "'" & ","
            Next

            Sval = Mid(Sval, 1, Len(Sval) - 1)
            pFac &= "(" & Sval & ")"

            dsProveedorFact = conexionE.llenarDataSet("SELECT CLIENTE,NOMBRE from UNISUP.CLIENTE where CATEGORIA_CLIENTE IN " & pFac & "")
            For Each fila4 As DataRow In dsProveedorFact.Tables(0).Rows
                cmbproveedoresFac.Items.Add(fila4.Item("Nombre"))
            Next
            conexionE.CerrarConexion()
            'AQUI CAMBIAMOS LA CONEXION PARA  TRAER LA INFORMACION DE PROVEEDORES
            '=========================================================================================================================================================
            'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
            'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
            CargarConexionProductos()
            'dtproveedores = conexion.llenarDataTable("SELECT Nombre FROM proveedores")
            dtproveedores = conexionpmm.llenarDataTable("SELECT nombre_proveedor as ""Nombre"" FROM unipmm.unisuperproveedor order by nombre_proveedor")
            For Each fila As DataRow In dtproveedores.Rows
                cmbproveedores.Items.Add(fila.Item("Nombre"))
            Next
        Catch ex As Exception

        End Try


    End Sub


    Private Sub cmbproveedores_LostFocus(sender As Object, e As EventArgs) Handles cmbproveedores.LostFocus
        If Trim(cmbproveedores.Text.Length) > 0 Then
            If cmbproveedores.SelectedIndex >= 0 Then
                '=========================================================================================================================================================
                'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                'idproveedor = conexion.EjecutarEscalar("SELECT id FROM proveedores WHERE Nombre='" & cmbproveedores.Text & "'")
                idproveedor = conexionpmm.EjecutarEscalar("SELECT idproveedor FROM unipmm.unisuperproveedor WHERE nombre_proveedor='" & cmbproveedores.Text & "'")
                '=========================================================================================================================================================
            Else
                MessageBox.Show("El Proveedor que acaba de ingresar no existe", "Error Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbproveedores.Text = ""
            End If
        End If
       
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbproveedores.SelectedIndexChanged


        '=========================================================================================================================================================
        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
        'idproveedor = conexion.EjecutarEscalar("SELECT id FROM proveedores WHERE Nombre='" & cmbproveedores.SelectedItem & "'")
        idproveedor = conexionpmm.EjecutarEscalar("SELECT idproveedor FROM unipmm.unisuperproveedor WHERE  nombre_proveedor='" & cmbproveedores.SelectedItem & "'")
        '=========================================================================================================================================================
        '=========================================================================================================================================================
        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
        direccion = conexionpmm.EjecutarEscalar("SELECT direccion FROM unipmm.unisuperproveedor WHERE idproveedor=" & idproveedor & "")
        '=========================================================================================================================================================
        dtmarcas.Clear()
        dtmarcas.Reset()
        DataSet3.Clear()
        cmbMarcas.Items.Clear()
        cmbMarcas.ResetText()
        dgvContactos.Rows.Clear()
        dgvContactos.DataSource = Nothing
        '=========================================================================================================================================================
        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
        'dtmarcas = conexion.llenarDataTable(" SELECT DISTINCT m.Nombre FROM marcas AS m " &
        '                                    "INNER JOIN productosmaster AS p ON m.Id=p.IdMarcas " &
        '                                    "INNER JOIN proveedores AS prov ON prov.id=p.IdProveedores WHERE p.IdProveedores=" & idproveedor & "")        
        dtmarcas = conexionpmm.llenarDataTable("SELECT marca as ""Nombre"" FROM unipmm.unisuperproductosprc where idproveedor='" & idproveedor & "'  group by marca order by marca")
        '=========================================================================================================================================================
        For Each fila As DataRow In dtmarcas.Rows
            cmbMarcas.Items.Add(fila.Item("Nombre"))
        Next

    End Sub

    Private Sub btnUpcAgregar_Click(sender As Object, e As EventArgs) Handles btnUpcAgregar.Click
        Dim respuesta As String
        Dim sku As String = String.Empty
        Dim filads() As DataRow
        Dim id As String
        Dim cadena As String = String.Empty
        Dim TipoCobro1 As String = String.Empty
        Dim cantidad1 As String = String.Empty
        Dim moneda1 As String = String.Empty
        TipoCobro1 = tipocobroresutado()
        cantidad1 = cantidadcobrar()
        moneda1 = tipomoneda()

        If TipoCobro1 = "0" Then
            Exit Sub
        End If
        If txtmontofijo.Text = "" And txtporcentaje.Text = "" Then
            MessageBox.Show("Existen campos vacíos por favor revise", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If chkdolares.Checked = False And chkquetzales.Checked = False Then
            MessageBox.Show("Seleccione moneda", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        'If txtporcentaje.Text.Length = 0 Or cmbTipocobro.Text.Length = 0 Then

        'End If
        'RECORREMOS LAS TIENDAS QUE EL USUARIO SELECCIONO
        If DataSet4.Tables("tienda").Rows.Count >= 1 Then
            If DataSet4.Tables("tienda").Select("seleccion='1'").Count = 0 Then
                Exit Sub
            Else
                numfilas = DataSet4.Tables("tienda").Select("seleccion='1'").Count
                filads = DataSet4.Tables("tienda").Select("seleccion='1'")
                cargar = New Clase_Cargar
                With cargar
                    For Each fila As DataRow In filads
                        .AgregarNombre(fila("id"), fila("nombre"))
                    Next
                End With
                For i As Integer = 1 To numfilas
                    With cargar
                        id = .id
                    End With
                    sku = txtSku.Text
                    consulta = ""
                    '=========================================================================================================================================================
                    'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                    'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                    'consulta = "   SELECT sucursales.`Nombre` AS TIENDA,subcategorias.`Nombre` AS subcategoria,producto.`sku` AS Sku,producto.`DescLarga` AS descripcion," &
                    '             "  FORMAT(costos.`costo`,2) AS costo,subcategorias.`Id` AS idsubcategoria,sucursales.id AS idtienda" &
                    '             "  FROM productosmaster AS producto" &
                    '             "  INNER JOIN subcategorias" &
                    '             "  ON subcategorias.`Id`=producto.`IdSubCategorias`" &
                    '             "  INNER JOIN costoscadena AS costos" &
                    '              "  ON costos.`sku`=producto.`sku`" &
                    '              "  INNER JOIN empresas" &
                    '              "  ON empresas.`Id`=costos.`idcadena`" &
                    '              "  INNER JOIN segmentaciontienda AS segmentacion " &
                    '              "  ON segmentacion.`sku`= costos.`sku`" &
                    '              "  INNER JOIN sucursales" &
                    '              "  ON empresas.`Id`=segmentacion.`idSucursal`" &
                    '              "  INNER JOIN proveedores " &
                    '              "  ON proveedores.`Id`=producto.`idproveedores`" &
                    '              "  WHERE producto.`sku`=" & txtSku.Text.Trim & "  AND  empresas.`Id`= 5  AND sucursales.`Id`='" & id & "'  AND producto.`IdProveedores`= " & idproveedor & " " &
                    '              "   ORDER BY sucursales.Id"
                    consulta = "SELECT T.NOMBRE AS ""TIENDA"",SC.NOMBRE AS ""SUBCATEGORIA"",P.SKU,P.DESCLARGA AS ""DESCRIPCION"", P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL as ""idtienda"" " &
                               "FROM unipmm.unisupersurtido S " &
                               "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU " &
                               "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD " &
                               "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                               "WHERE P.SKU=" & txtSku.Text.Trim & " And P.IDPROVEEDOR= " & idproveedor & " And T.IDSUCURSAL='" & id & "'  " &
                               "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"
                    '=========================================================================================================================================================
                    'VALIDAMOS QUE NO EXISTE EN LA TABLA...
                    validacionllenado(consulta, DataSet1, id, sku, "productos", TipoCobro1, cantidad1, moneda1)

                    If estadocontrato = True Then
                        respuesta = MessageBox.Show("El SKU que desea ingresar ya se encuentra en la tabla de datos.¿Desea actualizar los datos de ese SKU?", "Ingreso duplicado de SKU", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                        If respuesta = vbYes Then
                            Dim filaEncontrada() As DataRow = DataSet1.Tables("productos").Select("sku='" & sku & "' and idtienda='" & id & "' and cobro='" & tipocobroR & "'")
                            If filaEncontrada.Length > 0 Then
                                filaEncontrada(0)("cantidadcobrar") = cantidad1
                                filaEncontrada(0)("moneda") = moneda1
                                btnUpcAgregar.Enabled = False
                                estadocontrato = False
                                txtSku.Text = ""
                                txtdescripcion.Text = ""
                            ElseIf respuesta = vbNo Then
                                txtSku.Text = ""
                                txtdescripcion.Text = ""
                                btnUpcAgregar.Enabled = False
                                estadocontrato = False
                            End If
                           
                        End If
                        Exit Sub
                    End If
                    If cargar.siguiente = True Then
                    Else
                        Exit For
                    End If
                Next
            End If
        End If
  
        dgvProductos.DataSource = DataSet1.Tables("productos")
        txtSku.Text = ""
        txtdescripcion.Text = ""
        btnUpcAgregar.Enabled = False
        'estado1 = True

    End Sub

    Private Sub btnContagregar_Click(sender As Object, e As EventArgs) Handles btnContagregar.Click
        'VERIFICAMOS QUE LOS CAMPOS NO SE ENCUENTREN VACIOS...
        If Trim(txtCargo.Text.Length) > 0 And Trim(txtCnombre.Text.Length) > 0 And Trim(txtCorreo.Text.Length) > 0 And Trim(txtTelefono.Text.Length) > 0 Then
            'AGREGAMOS EL ELEMENTO A EL DATAGRID
            dgvContactos.Rows.Add(txtCnombre.Text, txtCargo.Text, txtTelefono.Text, txtCorreo.Text)
            txtCnombre.Text = ""
            txtCargo.Text = ""
            txtCorreo.Text = ""
            txtTelefono.Text = ""
        Else
            MessageBox.Show("No a terminado de llenar todos los campos", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub

    Private Sub btnagregarmarca_Click(sender As Object, e As EventArgs) Handles btnagregarmarca.Click

        If Trim(cmbMarcas.Text.Length) > 0 Then
            Dim filaEncontrada() As DataRow = DataSet3.Tables("marca").Select("nombre='" & cmbMarcas.Text & "'")
            If filaEncontrada.Length = 0 Then
                Try
                    If cmbMarcas.SelectedIndex >= 0 Then
                        consulta = ""
                        '=========================================================================================================================================================
                        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                        'consulta = "SELECT * FROM marcas WHERE nombre='" & cmbMarcas.Text & "'"
                        consulta = "select idmarca as id,nombre from unipmm.unisupermarcas where nombre='" & cmbMarcas.Text & "' order by nombre"
                        '=========================================================================================================================================================
                        'Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
                        Dim adap As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
                        adap.Fill(DataSet3, "Marca")
                        dgvMarcas.DataSource = DataSet3.Tables("Marca")
                    Else
                        MessageBox.Show("La marca que ingreso no existe o no pertenece a ese proveedor", "Error en Marca", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try

            Else
                MessageBox.Show("La Marca que desea ingresar ya se encuentra en la tabla de información", "Marca ya Existe", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        Else
            MessageBox.Show("El campo de Marcas se encuentra vacio", "Campo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btneliminarmarca_Click(sender As Object, e As EventArgs) Handles btneliminarmarca.Click
        eliminardataset(DataSet3, "marca")
    End Sub

    Private Sub btncrearcontrato_Click(sender As Object, e As EventArgs) Handles btncrearcontrato.Click
        Dim nit As String
        Dim idcliente As Integer
        'CAMBIAMOS LA CONEXION PARA TRAER A LOS PROVEEDORES A LOS QUE SE VA FACTURAR EN EXACTUS
        ' conexion.CerrarConexion()
        conexionE.BuscarConexionEnXml(Application.StartupPath + "\" & ("arconexionExactus.xml"))
        conexionE.AbrirConexion()
        idcliente = conexionE.EjecutarEscalar("SELECT CLIENTE from UNISUP.CLIENTE where NOMBRE = '" & cmbproveedoresFac.Text & "'")
        nit = conexionE.EjecutarEscalar("select contribuyente from unisup.cliente where CATEGORIA_CLIENTE='CPUBLI' AND NOMBRE='" & cmbproveedoresFac.Text & "'")
        conexionE.CerrarConexion()
        'CAMBIAMOS DE CONEXION YA QUE SE CREARA EN CONTRATO
        CargarConexionContratos()
        Dim agregarupc As New Clase_CargarUpc
        Dim agregarespacio As New Clase_CargarEspacios
        Dim agregarcontacto As New Clase_CargarContactos
        Dim filads(), filads2(), filads3() As DataRow
        Dim dgvcopias As New DataGridView
        Dim numfilas As Integer = 0
        Dim numMes As Integer = 0
        Dim año As Integer = 0
        Dim id_año As Integer = 0
        Dim consultacrearcontrato As String = String.Empty
        Dim fechainicial As String = String.Empty
        Dim fechafinal As String = String.Empty
        Dim formatocobro As String = String.Empty
        Dim formatocobroUpc As String = String.Empty
        Dim tipocobro As String = String.Empty
        Dim cantidad As String = String.Empty
        Dim idtienda As String = String.Empty
        Dim sku As String = String.Empty
        Dim cobro As String = String.Empty
        Dim tipocobroupc As String = String.Empty
        Dim cantidadcobrar As String = String.Empty
        Dim subcategoria1 As String = String.Empty
        Dim idsucategoria As String = String.Empty
        Dim costo As String = String.Empty
        Dim consulta1 As String = String.Empty
        Dim consulta2 As String = String.Empty
        Dim idtipo, nombretienda, total As String
        Dim nombre As String = String.Empty
        Dim cargo As String = String.Empty
        Dim correo As String = String.Empty
        Dim telefono As String = String.Empty
        Dim hora As String = String.Empty
        Dim fechaactualiza As String = String.Empty
        Dim moneda As String = String.Empty
        Dim moneda1 As String = String.Empty
        Dim tipomoneda As Integer
        Dim precio As Decimal
        Dim descripcion As String = String.Empty
        Dim duracion As Integer = 0
        Dim consultapago As String = String.Empty
        Dim tiendasautomaticas As Integer
        fecha = Now
        fechaenviar = Format(fecha, "yyyy-MM-dd")
        hora = Format(fecha, "hh:mm:ss")
        fechaactualiza = fechaenviar & "  " & hora
        unionmarcas = regresarcadena(DataSet3, "marca")
        uniontiendas = regresarcadena(DataSet4, "tienda")
        formatocobro = cmbformatodecobro.Text
        'VERIFICAMOS CUAL ES EL TIPO DE COBRO A REALIZAR PARA LOS PRODUCTOS....
        fechainicial = Format(dtpFinicial.Value, "yyyy-MM-dd")
        fechafinal = Format(dtpFfinal.Value, "yyyy-MM-dd")
        duracion = DateDiff(DateInterval.Month, dtpFinicial.Value, dtpFfinal.Value)
        detallecontrato = ""
        Try
            If DataSet1.Tables("Productos").Rows.Count = 0 And DataSet2.Tables("espacios").Rows.Count = 0 Then
                MessageBox.Show("Se necesita que se agregue un espacio o un producto para poder crear el contrato", "Productos y Espacios vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Me.Cursor = Cursors.WaitCursor
                ''===================================================================
                'SE VAN A INGRESAR LOS UPC QUE COMPONEN EL CONTRATO A LA BASE DE DATOS
                DataSet1.AcceptChanges()
                If DataSet1.Tables("productos").Rows.Count >= 1 Then
                    numfilas = DataSet1.Tables("productos").Rows.Count
                    filads = DataSet1.Tables("productos").Select()
                    agregarupc = New Clase_CargarUpc
                    With agregarupc
                        For Each fila As DataRow In filads
                            .AgregarProducto(fila("subcategoria"), fila("descripcion"), fila("sku"), fila("Costo"), fila("cobro"), fila("idsubcategoria"), fila("tipocobro"), fila("cantidadcobrar"), fila("idtienda"), fila("moneda"))
                        Next
                    End With
                    For i As Integer = 1 To numfilas
                        With agregarupc
                            sku = .sku
                            cobro = .formatocobro
                            subcategoria1 = .subcategoria
                            costo = .costo
                            idsucategoria = .idsubcategoria
                            tipocobroupc = .tipocobro
                            cantidadcobrar = .cantidadcobro
                            idtienda = .idtienda
                            moneda = .moneda
                            descripcion = .descripcion
                        End With
                        If sigUpc = True Then
                            consulta = ""
                            consulta = "INSERT INTO detalle_contratobmltx(idcontrato,IdProveedor,IdSubcategoria,subcategoria,Sku,costo,Formacobro,TipoCobro,Cantidadecobro,idTienda,moneda,descripcion)" & _
                            "VALUES('" & lblnocontrato.Text & "','" & idproveedor & "','" & idsucategoria & "','" & subcategoria1 & "','" & sku & "','" & costo & "', '" & cobro & "', '" & tipocobroupc & "', '" & cantidadcobrar & "','" & idtienda & "','" & moneda & "','" & descripcion & "')"
                            sigUpc = False
                        Else
                            consulta += ",  " & "('" & lblnocontrato.Text & "','" & idproveedor & "','" & idsucategoria & "','" & subcategoria1 & "','" & sku & "','" & costo & "', '" & cobro & "', '" & tipocobroupc & "', '" & cantidadcobrar & "','" & idtienda & "','" & moneda & "','" & descripcion & "')"
                        End If
                        'REVISAMOS SI EXISTE OTRO PRODUCTO PARA INGRESARLO
                        If agregarupc.siguiente = True Then
                        Else
                            Exit For
                        End If
                    Next

                    'REALIZAMOS LA CONSULTA..
                    consulta1 = conexion.EjecutarNonQuery(consulta)
                    detallecontrato = "PRODUCTOS"

                End If
                '======================================================================================
                ''VAMOS A INGRESAR LOS DATOS DE LOS ESPACIOS
                DataSet2.AcceptChanges()
                If DataSet2.Tables("espacios").Rows.Count >= 1 Then
                    numfilas = 0
                    numfilas = DataSet2.Tables("espacios").Rows.Count
                    filads2 = DataSet2.Tables("espacios").Select()
                    agregarespacio = New Clase_CargarEspacios
                    With agregarespacio
                        For Each fila As DataRow In filads2
                            .AgregarEspacio(fila("idtipoespacio"), fila("tienda"), fila("cantidad"), fila("precio"), fila("total"), fila("tipocobro"), fila("moneda"))
                        Next
                    End With
                    For i As Integer = 1 To numfilas
                        With agregarespacio
                            idtipo = .idtipoespacio
                            precio = .costosespacio
                            tipocobro = .tipocobro
                            cantidad = .cantidad
                            nombretienda = .idtienda
                            total = .totalpagar
                            moneda1 = .moneda
                        End With
                        tipomoneda = conexion.EjecutarEscalar("SELECT idmoneda FROM tipomoneda_bmltx WHERE tipomoneda='" & moneda & "'")
                        If sigEspacio = True Then
                            consulta = ""
                            consulta = "INSERT INTO espacio_rentado_backmarginltx(Idcontrato,Idproveedor,tienda,IdTipoespacio,Cantidad,TipoCobro,CantidaddeCobro,fechainicial,Fechafinal,moneda,costoespacio)" & _
                            "VALUES('" & lblnocontrato.Text & "','" & idproveedor & "','" & nombretienda & "','" & idtipo & "','" & cantidad & "','" & tipocobro & "','" & total & "','" & fechainicial & "','" & fechafinal & "','" & moneda1 & "'," & precio & ")"
                            sigEspacio = False

                        Else
                            consulta += ",  " & "('" & lblnocontrato.Text & "','" & idproveedor & "','" & nombretienda & "','" & idtipo & "','" & cantidad & "','" & tipocobro & "','" & total & "','" & fechainicial & "','" & fechafinal & "','" & moneda1 & "'," & precio & ")"
                        End If
                        'buscamos el siguiente espacio
                        If agregarespacio.siguiente = True Then
                        Else
                            Exit For
                        End If
                    Next
                    'ENVIAMOS LOS DATOS A LA TABLA
                    consulta1 = conexion.EjecutarNonQuery(consulta)

                End If

                ''=====================================================================================================
                ''AHORA VAMOS A INGRESAS A LOS CONTACTOS DEL PROVEEDOR

                llenartabla()
                sigUpc = True
                numfilas = 0
                If dtcontactos.Rows.Count >= 1 Then
                    numfilas = dtcontactos.Rows.Count
                    filads3 = dtcontactos.Select
                    agregarcontacto = New Clase_CargarContactos
                    With agregarcontacto
                        For Each fila As DataRow In filads3
                            .AgregarContacto(fila("nombre"), fila("cargo"), fila("telefono"), fila("correo"))
                        Next
                    End With
                    For i As Integer = 1 To numfilas
                        With agregarcontacto
                            nombre = .nombre
                            cargo = .cargo
                            telefono = .telefono
                            correo = .correo
                        End With
                        If sigUpc = True Then
                            consulta = ""
                            consulta = "INSERT INTO contactos_backmarginltx(idcontrato,idproveedor,nombre,cargo,telefono,correo)VALUES('" & lblnocontrato.Text & "','" & idproveedor & "','" & nombre & "','" & cargo & "','" & telefono & "','" & correo & "')"
                            sigUpc = False
                        Else
                            consulta += ",  " & "('" & lblnocontrato.Text & "','" & idproveedor & "','" & nombre & "','" & cargo & "','" & telefono & "','" & correo & "')"

                        End If

                        'SIGUIMOS CON EL SIGUIENTE REGISTRO...
                        If agregarcontacto.siguiente = True Then
                        Else
                            Exit For
                        End If
                    Next
                    'ENVIAMOS LOS DATOS A LA TABLA...
                    consulta1 = conexion.EjecutarNonQuery(consulta)
                End If

                '==================================================================================================
                ''AHORA VERIFICAMOS COMO SE REALIZO EL DETALLE DEL CONTRATO.
                If DataSet1.Tables("productos").Rows.Count >= 1 And DataSet2.Tables("espacios").Rows.Count >= 1 Then
                    detallecontrato = "PRODUCTOS Y ESPACIOS"
                End If
                'VERIFICAMOS SI TIENE SELECCIONADO LAS TIENDAS AUTOMATICAS..
                If chbtiendasnuevas.CheckState = CheckState.Checked Then
                    tiendasautomaticas = 1
                Else
                    tiendasautomaticas = 0
                End If

                '' ======================================================================================================
                '' AHORA VAMOS A CREAR EL CONTRATO EN LA BASE DE DATOS
                If estadocontrato = 0 Then
                    crearcorrelativo = conexion.EjecutarNonQuery("INSERT INTO correlativo_contratosbmltx(año)VALUES('" & añoingresar & "')")
                End If



                consultacrearcontrato = conexion.EjecutarNonQuery("INSERT INTO contratos_backmarginltx(idcontrato,idproveedor,IdNombreProveedor,Idmarcas,IdTiendas,fechainicio,fechafinal,formatocobro,Detallecontrato,nombreunisuper,nombreproveedor,usuario,FechaCreacionContrato,TiendasAutomaticas,NombreProveedorFac,nitProveedorFac,IdproveedorFac,direccion,estado)" &
                                                    "VALUES('" & lblnocontrato.Text & "','" & idproveedor & "','" & cmbproveedores.Text & "','" & unionmarcas & "','" & uniontiendas & "','" & fechainicial & "','" & fechafinal & "','" & formatocobro & "','" & detallecontrato & "','" & txtNunisuper.Text & "','" & txtNproveedor.Text & "','" & nombreusuario & "','" & fechaactualiza & "'," & tiendasautomaticas & ",'" & cmbproveedoresFac.Text & "','" & nit & "','" & idcliente & "','" & direccion & "',1)")

                MessageBox.Show("Se ha creado con exito el contrato", "Creacion de contrato", MessageBoxButtons.OK, MessageBoxIcon.Information)
                mensaje = MessageBox.Show("Desea Imprimir el contrato", "Back Margin LTExpress", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                If mensaje = vbYes Then
                    'CREAMOS DATASET PARA PODER ENVIARLES LOS DATOS A EL PDF
                    'YA QUE SE NECESITA ELIMINAR CIERTAS COLUMNAS DE LOS DATASET
                    Dim dstablasp As DataSet = DataSet1.Copy
                    Dim dstablaE As DataSet = DataSet2.Copy
                    Dim dstablaM As DataSet = DataSet3.Copy
                    dstablaM.Tables("marca").Columns.Remove("id")
                    dstablaM.Tables("marca").Columns.Remove("seleccion")
                    dstablaE.Tables("espacios").Columns.Remove("idtipoespacio")
                    dstablaE.Tables("espacios").Columns.Remove("seleccion")
                    dstablasp.Tables("productos").Columns.Remove("seleccion")
                    dstablasp.Tables("productos").Columns.Remove("idtienda")
                    dstablasp.Tables("productos").Columns.Remove("idsubcategoria")
                    dstablasp.Tables("productos").Columns.Remove("costo")
                    dstablaE.AcceptChanges()
                    dstablaM.AcceptChanges()
                    dstablasp.AcceptChanges()
                    dgvEspacio.DataSource = Nothing
                    dgvMarcas.DataSource = Nothing
                    dgvProductos.DataSource = Nothing
                    dgvEspacio.DataSource = dstablaE.Tables("espacios")
                    dgvMarcas.DataSource = dstablaM.Tables("marca")
                    dgvProductos.DataSource = dstablasp.Tables("productos")
                    exportar.creararchivo(lblnocontrato.Text & cmbproveedores.Text.ToUpperInvariant, fechafinal, fechainicial, cmbproveedores.Text, _
                                          lblnocontrato.Text, cmbformatodecobro.Text, dgvContactos, dgvProductos, txtNunisuper.Text, txtNproveedor.Text, dgvEspacio, dgvMarcas, cmbproveedoresFac.Text)
                    dstablaE.Clear()
                    dstablaM.Clear()
                    dstablasp.Clear()
                End If
                'VOLVEMOS A CARGAR LA VENTANA PARA QUE SE INGRESE OTRO CONTRATO
                CargarConexionProductos()
                cargarventana = True
                cerrarventas(cargarventana)
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            limpiarelementos()
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmbespacio_LostFocus(sender As Object, e As EventArgs) Handles cmbespacio.LostFocus
        If Trim(cmbespacio.Text.Length) > 0 Then
            If cmbespacio.SelectedIndex >= 0 Then
            Else
                MessageBox.Show("El espacio que acaba de ingresar no existe", "Error Espacio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtprecio.Text = ""
                Exit Sub
            End If
        End If
    End Sub

    Private Sub cmbespacio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbespacio.SelectedIndexChanged
        'AQUI CAMBIAMOS LA CONEXION PARA TRAER LOS TIPOS DE ESPACIOS
        CargarConexionContratos()
        txtprecio.Text = conexion.EjecutarEscalar("SELECT costo FROM tipoespacio_bmltx WHERE Nombre='" & cmbespacio.Text & "'")
        'AQUI CERRAMOS LA CONEXION ANTERIOR Y REGRESAMOS A LA DE PRODUCTOS
        CargarConexionProductos()
    End Sub

    Private Sub btnagregarmarcas_Click(sender As Object, e As EventArgs) Handles btnagregarmarcas.Click

        Dim filads() As DataRow
        Dim consulta, unionmarcas, uniontienda, tipocobro1, cantidad1, moneda, resultado As String
        Dim sku, tienda As String
        numfilas = 0
        unionmarcas = regresarcadena(DataSet3, "marca")
        tipocobro1 = tipocobroresutado()
        cantidad1 = cantidadcobrar()
        moneda = tipomoneda()
        uniontienda = regresarcadena(DataSet4, "tienda")
        Try
            If tipocobro1 = " 0 " Then
                Exit Sub
            End If
            'VALIDAMOS QUE EL CAMPO DE TIPO DE COBRO ESTE LLENO
            If txtmontofijo.Text.Length = 0 And cmbTipocobro.Text.Length = 0 And txtporcentaje.Text.Length = 0 Then
                MessageBox.Show("No se ha seleccionado un tipo de cobro para los productos que se van a ingresar", "No hay tipo cobro", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub

            End If
            'VALIDAMOS SI EL DATASET TIENE DATOS...
            If DataSet1.Tables("productos").Rows.Count >= 1 Then
                consulta = ""
                dsproductos.Clear()
                dsproductos.Reset()
                '=========================================================================================================================================================
                'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                'consulta = "   SELECT producto.`sku` AS Sku,sucursales.id AS id " &
                '            " FROM productosmaster AS producto " &
                '            " INNER JOIN subcategorias ON subcategorias.`Id`=producto.`IdSubCategorias` " &
                '            " INNER JOIN costoscadena AS costos ON costos.`sku`=producto.`sku` " &
                '            " INNER JOIN empresas ON empresas.`Id`=costos.`idcadena` " &
                '            " INNER JOIN segmentaciontienda AS segmentacion  ON segmentacion.`sku`= costos.`sku` " &
                '            " INNER JOIN sucursales ON empresas.`Id`=segmentacion.`idSucursal` " &
                '            " INNER JOIN proveedores   ON proveedores.`Id`=producto.`IdProveedores` " &
                '            " INNER JOIN marcas ON marcas.`Id`=producto.`IdMarcas` " &
                '            " WHERE marcas.`Id` IN(" & unionmarcas & ")  AND  empresas.`Id`= 5  AND sucursales.Id IN(" & uniontienda & ") AND producto.`IdProveedores`=" & idproveedor & "  ORDER BY sucursales.`Id`"
                consulta = "SELECT P.SKU AS Sku,T.IDSUCURSAL as id " &
                           "FROM unipmm.unisupersurtido S  " &
                           "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU  " &
                           "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD  " &
                           "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                           "WHERE P.IDMARCA IN(" & unionmarcas & ") And P.IDPROVEEDOR=" & idproveedor & "  And T.IDSUCURSAL IN(" & uniontienda & ")" &
                           "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"


                'LLENAMOS UN DATASET CON LOS SKU QUE EXISTEN PARA ESA MARCA...
                'Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
                Dim adap As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
                '=========================================================================================================================================================
                adap.Fill(dsproductos)
                'VERIFICAMOS QUE EL DATASET REGRESE LLENO....
                If dsproductos.Tables(0).Rows.Count >= 1 Then
                    numfilas = dsproductos.Tables(0).Rows.Count
                    filads = dsproductos.Tables(0).Select()
                    cargar1 = New Clase_Cargar
                    With cargar1
                        For Each fila As DataRow In filads
                            .AgregarNombre(fila("Sku"), fila("id"))
                        Next
                    End With
                    For i As Integer = 1 To numfilas
                        With cargar1
                            sku = .id
                            tienda = .nombre
                        End With
                        'VALIDAMOS QUE EL SKU NOSE ENCUENTRE EN LA TABLA Y LO INGRESAMOS..
                        consulta = String.Empty
                        '=========================================================================================================================================================
                        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                        'consulta = "   SELECT sucursales.`Nombre` AS TIENDA,subcategorias.`Nombre` AS subcategoria,producto.`sku` AS Sku,producto.`DescLarga` AS descripcion," &
                        '             " FORMAT(costos.`costo`,2) AS costo, subcategorias.`Id` AS idsubcategoria,sucursales.id AS idtienda" &
                        '             "  FROM productosmaster AS producto" &
                        '             "  INNER JOIN subcategorias" &
                        '             "  ON subcategorias.`Id`=producto.`IdSubCategorias`" &
                        '             "  INNER JOIN costoscadena AS costos" &
                        '              "  ON costos.`sku`=producto.`sku`" &
                        '              "  INNER JOIN empresas" &
                        '              "  ON empresas.`Id`=costos.`idcadena`" &
                        '              "  INNER JOIN segmentaciontienda AS segmentacion " &
                        '              "  ON segmentacion.`sku`= costos.`sku`" &
                        '              "  INNER JOIN sucursales" &
                        '              "  ON empresas.`Id`=segmentacion.`idSucursal`" &
                        '              "  INNER JOIN proveedores " &
                        '              "  ON proveedores.`Id`=producto.`idproveedores`" &
                        '              "  WHERE producto.`sku`=" & sku & "  AND  empresas.`Id`= 5  AND sucursales.`Id`='" & tienda & "'  AND producto.`idproveedores`= " & idproveedor & " " &
                        '              "   ORDER BY sucursales.Id"
                        consulta = "SELECT T.NOMBRE AS ""TIENDA"",SC.NOMBRE AS ""SUBCATEGORIA"",P.SKU,P.DESCLARGA AS ""DESCRIPCION"", P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL as ""idtienda"" " &
                                   "FROM unipmm.unisupersurtido S  " &
                                   "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU  " &
                                   "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD  " &
                                   "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                                   "WHERE P.SKU='" & sku & "' And P.IDPROVEEDOR=" & idproveedor & "  And T.IDSUCURSAL= " & tienda & "" &
                                   "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"


                        '=========================================================================================================================================================
                        validacionllenado(consulta, DataSet1, tienda, sku, "productos", tipocobro1, cantidad1, moneda)
                        'REVISAMOS SI EXISTE OTRO PRODUCTO PARA INGRESARLO
                        If cargar1.siguiente = True Then
                        Else
                            Exit For
                        End If
                    Next
                    dgvProductos.DataSource = DataSet1.Tables("productos")
                Else
                    'CUANDO EL DATASET DE MARCAS NO ENCONTRO NINGUN SKU
                    MessageBox.Show("No se encuentran Productos de la marcar que selecciono", "No se encuentran Productos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dsproductos.Clear()
                    dsproductos.Reset()
                    Exit Sub
                End If

                'AQUI CUANDO EL DATASET SE ENCUENTRA VACIO NOSOSTROS VAMOS A LLENAR EL DATASET
            Else
                '=========================================================================================================================================================
                'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                'consulta = " SELECT sucursales.`Nombre` AS TIENDA,subcategorias.`Nombre` AS subcategoria,producto.`sku` AS Sku, producto.`DescLarga` AS descripcion, " &
                '            " FORMAT(costos.`costo`,2) AS costo,subcategorias.`Id` AS idsubcategoria,sucursales.id AS idtienda" &
                '            " FROM productosmaster AS producto" &
                '            " INNER JOIN subcategorias" &
                '            " ON subcategorias.`Id`=producto.`IdSubCategorias`" &
                '            " INNER JOIN costoscadena AS costos" &
                '            " ON costos.`sku`=producto.`sku`" &
                '            " INNER JOIN empresas" &
                '            " ON empresas.`Id`=costos.`idcadena`" &
                '            " INNER JOIN segmentaciontienda AS segmentacion " &
                '            " ON segmentacion.`sku`= costos.`sku`" &
                '            " INNER JOIN sucursales" &
                '            " ON empresas.`Id`=segmentacion.`idSucursal`" &
                '            " INNER JOIN Proveedores " &
                '             "  ON proveedores.`Id`=producto.`IdProveedores`" &
                '            " INNER JOIN marcas" &
                '            " ON marcas.`Id`=producto.`IdMarcas`" &
                '            " WHERE marcas.`Id` IN(" & unionmarcas & ")  AND  empresas.`Id`= 5  AND sucursales.Id IN(" & uniontienda & ") AND producto.`IdProveedores`=" & idproveedor & " " &
                '            " ORDER BY sucursales.`Id`"

                consulta = "SELECT T.NOMBRE AS ""TIENDA"",SC.NOMBRE AS ""SUBCATEGORIA"",P.SKU,P.DESCLARGA AS ""DESCRIPCION"", P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL as ""idtienda"" " &
                           "FROM unipmm.unisupersurtido S  " &
                           "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU  " &
                           "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD  " &
                           "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                           "WHERE P.IDMARCA IN(" & unionmarcas & ") And P.IDPROVEEDOR=" & idproveedor & "  And T.IDSUCURSAL IN(" & uniontienda & ")" &
                           "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"

                'Dim adap1 As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
                Dim adap1 As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
                '=========================================================================================================================================================
                adap1.Fill(DataSet1, "productos")
                If tipocobro1 = "Monto Fijo" Then
                    resultado = "Monto Fijo"
                Else
                    resultado = cmbTipocobro.Text
                End If

                For Each fila As DataRow In DataSet1.Tables("productos").Rows
                    fila("cobro") = resultado
                    fila("Tipocobro") = tipocobro1
                    fila("cantidadcobrar") = cantidad1
                    fila("moneda") = moneda
                Next
                dgvProductos.DataSource = DataSet1.Tables("productos")
                'SALIMOS DE LA VALIDACION SI SE ENCUENTRA LLENO EL DATASET CENTRAL
            End If

        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try
        ' estado1 = True
    End Sub

    Private Sub btnagregarespacio_Click_1(sender As Object, e As EventArgs) Handles btnagregarespacio.Click
        'CAMBIAMOS A LA CONEXIN DE LOS CONTRATOS..
        CargarConexionContratos()
        Dim cantida, precio, resultado As Decimal
        Dim moneda As String = String.Empty
        Dim idtipoespacio As String
        Dim fechainicial, fechafinal As String
        fechainicial = Format(dtpFinicial.Value, "yyyy-MM-dd")
        fechafinal = Format(dtpFfinal.Value, "yyyy-MM-dd")

        Try
        If txtcantidadacobrarE.Text.Length = 0 Or txtprecio.Text.Length = 0 Or cmbespacio.Text.Length = 0 Or cmbtienda.Text.Length = 0 Then
            MessageBox.Show("Error al ingresa el espacio, se encuentran campos vacios.", "Error en el ingreso de un Espacio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim filaEncontrada() As DataRow = DataSet2.Tables("espacios").Select("espacio='" & cmbespacio.Text & "' and Tienda='" & cmbtienda.Text & "'")
        If filaEncontrada.Length = 0 Then

            'VALIDAMOS EL TIPO DE MONEDA SELECCIONADA...

            If chkquetzales1.CheckState = CheckState.Checked Then
                moneda = "Quetzales"
            ElseIf chkdolares1.CheckState = CheckState.Checked Then
                moneda = "Dolares"
            ElseIf chkquetzales1.CheckState = CheckState.Checked And chkdolares1.CheckState = CheckState.Checked Then
                MessageBox.Show("Solo Puede seleccionar un tipo de moneda", "Ambas monedas seleccionadas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf chkquetzales1.CheckState = CheckState.Unchecked And chkdolares1.CheckState = CheckState.Unchecked Then
                MessageBox.Show("Tiene que seleccionar un tipo de moneda", "Moneda no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
                End If

            cantida = txtcantidadacobrarE.Text
            precio = txtprecio.Text
            resultado = cantida * precio
            dgvEspacio.DataSource = DataSet2.Tables("espacios")
            idtipoespacio = conexion.EjecutarEscalar("SELECT IdTipoespacio FROM tipoespacio_bmltx WHERE nombre='" & cmbespacio.Text & "'")
            Dim anyRow As DataRow = DataSet2.Tables("espacios").NewRow
            anyRow(1) = cmbespacio.Text
            anyRow(2) = cmbtienda.Text
            anyRow(3) = cantida
            anyRow(4) = precio
            anyRow(5) = idtipoespacio
            anyRow(6) = resultado
            anyRow(7) = "Monto Fijo"
            anyRow(8) = moneda
            DataSet2.espacios.Rows.Add(anyRow)
        Else
            MessageBox.Show("El espacio que desea asignar a la Tienda ya existe en la tabla de Información", "Espacio ya existe", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        'REGRESAMOS A LA CONEXION DE LOS PRODUCTOS...
        CargarConexionProductos()

    End Sub

    Private Sub txtSku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSku.KeyPress
        Dim resultado As Integer
        If e.KeyChar = Chr(13) Then
            Try
                Int64.Parse(txtSku.Text)

            Catch ex As Exception
                MessageBox.Show("Solo puede ingresar números en la casilla Sku", "Sku debe ser un número", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Sub
            End Try
            resultado = obtenersku()
            'Si el resultado nos da 1 significa que si existe...
            If resultado = 0 Then
                MessageBox.Show("El Sku que ingreso no existe.", "Sku Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtSku.Text = ""
                txtdescripcion.Text = ""
            ElseIf resultado >= 1 Then
                'MessageBox.Show("El Sku que ingreso si existe", "Sku Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '=========================================================================================================================================================
                'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                'txtdescripcion.Text = conexion.EjecutarEscalar("SELECT descLarga FROM productosmaster WHERE sku='" & txtSku.Text & "'")
                txtdescripcion.Text = conexionpmm.EjecutarEscalar("SELECT DESCLARGA  FROM unipmm.unisuperproductosprc WHERE SKU='" & txtSku.Text & "'")
                '=========================================================================================================================================================
                btnUpcAgregar.Enabled = True
            End If
        End If


    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click

        eliminarelemento(DataSet1, dgvProductos, "productos")
    End Sub

    Private Sub EliminarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem1.Click

        eliminarelemento(DataSet2, dgvEspacio, "espacios")
    End Sub

#Region "FUNCIONES Y PROCECIMIENTOS"
    Public Sub eliminardataset(ByVal dataset As DataSet, ByVal nombretabla As String)
        Dim numeroceldas As Integer = 0
        Dim filads() As DataRow
        dgvProductos.EndEdit()
        If dataset.Tables(nombretabla).Rows.Count >= 1 Then
            If dataset.Tables(nombretabla).Select("seleccion='1'").Count = 0 Then
                MessageBox.Show("No ha seleccionado ningun elemento para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                numeroceldas = dataset.Tables(nombretabla).Select("seleccion='1'").Count
                filads = dataset.Tables(nombretabla).Select("seleccion='1'")
                For Each fila As DataRow In filads
                    For i As Integer = 0 To numeroceldas - 1
                        filads(i).Delete()
                    Next
                Next
                MessageBox.Show("Los elementos que seleccion fueron eliminados con exito", "Elementos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
    Public Function regresarcadena(ByVal dataset As DataSet, ByVal tnombretabla As String)

        cargar = New Clase_Cargar
        Dim filads() As DataRow
        Dim id As String
        Dim sigMarca As Boolean = True
        Dim cadena As String = String.Empty
        If dataset.Tables(tnombretabla).Rows.Count >= 1 Then
            If dataset.Tables(tnombretabla).Select("seleccion='1'").Count = 0 Then
                'MessageBox.Show("No ha seleccionado ningun Elemento  para realizar el contrato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else
                numfilas = dataset.Tables(tnombretabla).Select("seleccion='1'").Count
                filads = dataset.Tables(tnombretabla).Select("seleccion='1'")

                With cargar
                    For Each fila As DataRow In filads
                        .AgregarNombre(fila("id"), fila("nombre"))
                    Next
                End With
                For i As Integer = 1 To numfilas
                    With cargar
                        id = .id
                    End With

                    If sigMarca = True Then
                        cadena = ""
                        cadena = id.Trim
                        sigMarca = False
                    Else
                        cadena += ", " & id.Trim

                    End If
                    'REVISAMOS SI EXISTE OTRO PRODUCTO PARA INGRESARLO
                    If cargar.siguiente = True Then
                    Else
                        Exit For
                    End If
                Next


            End If
        End If

        Return cadena
    End Function

    Public Sub limpiarelementos()
        txtCargo.Text = ""
        txtCnombre.Text = ""
        txtCorreo.Text = ""
        txtdescripcion.Text = ""
        txtmontofijo.Text = ""
        txtNproveedor.Text = ""
        txtNunisuper.Text = ""
        txtcantidadacobrarE.Text = ""
        txtporcentaje.Text = ""
        txtTelefono.Text = ""
        txtSku.Text = ""
        txtprecio.Text = ""
        cmbespacio.Text = ""
        cmbformatodecobro.Text = ""
        cmbproveedores.Text = ""
        cmbproveedoresFac.Text = ""
        cmbtienda.Text = ""
        cmbMarcas.Text = ""
        cmbTipocobro.Text = ""
        cmbMarcas.Items.Clear()
        cmbTipocobro.Enabled = True
        chkmontofijo.Enabled = True
        chkporcentaje.Enabled = True
        txtporcentaje.Enabled = True
        txtmontofijo.Enabled = True
        txtcantidadacobrarE.Enabled = True
        sigUpc = True
        sigEspacio = True
        contactos = False
        dtmarcas.Clear()
        chkmontofijo.CheckState = CheckState.Unchecked
        chkporcentaje.CheckState = CheckState.Unchecked
        chkdolares.CheckState = CheckState.Unchecked
        chkquetzales.CheckState = CheckState.Unchecked
        chkdolares1.CheckState = CheckState.Unchecked
        chkquetzales1.CheckState = CheckState.Unchecked
        DataSet1.Clear()
        DataSet2.Clear()
        DataSet3.Clear()
        dttiposespacio.Clear()
        dtcontactos.Reset()
        dtcontactos.Clear()
        dsproductos.Clear()
        dgvContactos.Rows.Clear()
        dgvContactos.DataSource = Nothing
        txtcantidadacobrarE.Text = ""


    End Sub
    Public Sub llenartabla()

        Dim numceldas As Integer
        Dim fila As DataRow
        '===========================
        'crearemos los dt para enviar los datos al contrato
        If dgvContactos.Rows.Count > 0 Then
            dtcontactos.TableName = "contactos"
            dtcontactos.Columns.Add("nombre")
            dtcontactos.Columns.Add("cargo")
            dtcontactos.Columns.Add("telefono")
            dtcontactos.Columns.Add("correo")
            'dscontactos.Tables.Add("contactos")
            numceldas = dgvContactos.Rows.Count
            For i As Integer = 0 To numceldas - 1
                fila = dtcontactos.NewRow
                fila("nombre") = dgvContactos.Rows(i).Cells(0).Value
                fila("cargo") = dgvContactos.Rows(i).Cells(1).Value
                fila("telefono") = dgvContactos.Rows(i).Cells(2).Value
                fila("correo") = dgvContactos.Rows(i).Cells(3).Value
                dtcontactos.Rows.Add(fila)
            Next
        Else

        End If

    End Sub
    Public Function tipocobroresutado()

        Dim resultado As String = String.Empty

        If chkmontofijo.CheckState = CheckState.Checked Then
            resultado = "Monto Fijo"

        ElseIf chkporcentaje.CheckState = CheckState.Checked Then
            resultado = "Porcentaje"
        ElseIf chkmontofijo.CheckState = CheckState.Checked And chkporcentaje.CheckState = CheckState.Checked Then
            MessageBox.Show("Solo puede seleccionar una Opcion", "Ambas opciones seleccionadas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            resultado = "0"
        ElseIf chkmontofijo.CheckState = CheckState.Unchecked And chkporcentaje.CheckState = CheckState.Unchecked Then
            MessageBox.Show("No a seleccionado ninguna Opcion", "No hay una opcion seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Information)
            resultado = "0"
        End If
        Return resultado

    End Function
    Public Function cantidadcobrar()

        Dim resultado As String = String.Empty

        If chkmontofijo.CheckState = CheckState.Checked Then
            resultado = txtmontofijo.Text
        ElseIf chkporcentaje.CheckState = CheckState.Checked Then
            resultado = txtporcentaje.Text
        ElseIf chkmontofijo.CheckState = CheckState.Checked And chkporcentaje.CheckState = CheckState.Checked Then

        End If
        Return resultado

    End Function
    Public Function tipomoneda()
        Dim moneda As String = String.Empty

        If chkmontofijo.CheckState = CheckState.Checked Then
            If chkquetzales.CheckState = CheckState.Checked Then
                moneda = "Quetzales"
            ElseIf chkdolares.CheckState = CheckState.Checked Then
                moneda = "Dolares"
            End If
        Else
            moneda = "Quetzales"
        End If



        Return moneda

    End Function
    Public Function obtenersku()
        Dim _dsTiendasLTX As New DataSet
        Dim _listaTiendas As String = String.Empty
        _dsTiendasLTX = conexion.llenarDataSet("SELECT id FROM sucursales_bmltx")
        If _dsTiendasLTX.Tables(0).Rows.Count > 0 Then
            For Each fila_tienda As DataRow In _dsTiendasLTX.Tables(0).Rows
                _listaTiendas &= fila_tienda("id") & ","
            Next
            _listaTiendas = _listaTiendas.TrimEnd(",")
        End If
        If Trim(txtSku.Text.Length) > 0 Then
            '=========================================================================================================================================================
            'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
            'productos = conexion.EjecutarEscalar("SELECT COUNT(producto.sku)  FROM productosmaster AS producto  INNER JOIN segmentaciontienda AS segmentacion  " &
            '                                     "ON segmentacion.sku= producto.sku WHERE producto.sku= '" & txtSku.Text & "' AND segmentacion.idcadena=5 AND producto.Idproveedores='" & idproveedor & "'")
            productos = conexionpmm.EjecutarEscalar("SELECT COUNT(S.SKU) FROM unipmm.unisupersurtido S " &
                                                 "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU " &
                                                 "WHERE P.SKU='" & txtSku.Text & "' AND P.IDPROVEEDOR='" & idproveedor & "' " &
                                                 "AND S.TIENDACD IN(" & _listaTiendas & ") ")
            '=========================================================================================================================================================
        Else
            MessageBox.Show("No se encontro ningun producto con ese Sku", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Return productos
    End Function
    Public Sub validacionllenado(ByVal consulta As String, ByVal dataset As DataSet, ByVal tienda As Integer, ByVal sku As String, ByVal nombretabla As String, ByVal tipocobro As String, _
                                 ByVal cantidad As String, ByVal moneda As String)

        Dim resultado As String

        If tipocobro = "Monto Fijo" Then
            resultado = "Monto Fijo"
        Else
            resultado = cmbTipocobro.Text
        End If
        Dim filaencontrada1() As DataRow = dataset.Tables(nombretabla).Select("sku='" & sku & "' and idtienda='" & tienda & "' and cobro='" & resultado & "' ")
        If filaencontrada1.Length = 0 Then
            'Dim adap1 As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
            Dim adap1 As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
            adap1.Fill(dataset, nombretabla)
            Dim filaEncontrada() As DataRow = DataSet1.Tables(nombretabla).Select("sku='" & sku & "' and idtienda='" & tienda & "' and cobro= '0'")
            If filaEncontrada.Length > 0 Then
                filaEncontrada(0)("tipocobro") = tipocobro
                filaEncontrada(0)("cantidadcobrar") = cantidad
                filaEncontrada(0)("cobro") = resultado
                filaEncontrada(0)("moneda") = moneda
            End If
        Else
            estadocontrato = True
            tipocobroR = filaencontrada1(0)("cobro")

        End If
    End Sub
    Private Sub eliminarelemento(ByVal dataset As DataSet, ByVal dgv As DataGridView, ByVal nombretabla As String)
        Dim fila2 As Integer
        Try
            fila2 = dgv.CurrentRow.Index
            dataset.Tables(nombretabla).Rows(fila2).Delete()
            dataset.AcceptChanges()
            MessageBox.Show("La eliminacion del elemento seleccionado se realizo con exito", "Eliminacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Ocurrio el siguiente error " & vbCrLf & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub cerrarventas(ByVal desicion As Boolean)
        Dim contrato As New frmcontrato

        If desicion = True Then
            Me.Close()
            contrato = New frmcontrato
            contrato.ShowDialog()
        Else
            Me.Close()

        End If
    End Sub
#End Region

#Region "BOTONES QUE CAMBIAN DE PANTALLA"


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CargarConexionContratos()
        FrmMenuContratos.WindowState = FormWindowState.Normal
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'para cambiar de tabla...
        TabControl1.SelectTab(1)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'para cambiar de tabla...
        TabControl1.SelectTab(2)
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs)
        'para cambiar de tabla...
        TabControl1.SelectTab(3)
    End Sub

#End Region

    Private Sub cmbtienda_LostFocus(sender As Object, e As EventArgs) Handles cmbtienda.LostFocus
        If Trim(cmbtienda.Text.Length) > 0 Then
            If cmbtienda.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("La tienda que ingrenso no existe", "Error de Tienda", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbtienda.Text = ""
            End If
        End If

    End Sub




    Private Sub cmbproveedoresFac_LostFocus(sender As Object, e As EventArgs) Handles cmbproveedoresFac.LostFocus
        If Trim(cmbproveedoresFac.Text.Length) > 0 Then
            If cmbproveedoresFac.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El proveedor a Facturas que escribio no existe.", "Error Proveedor a Facturar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbproveedoresFac.Text = ""
            End If
        End If
    End Sub


    Private Sub cmbTipocobro_LostFocus(sender As Object, e As EventArgs) Handles cmbTipocobro.LostFocus
        If Trim(cmbTipocobro.Text.Length) > 0 Then
            If cmbTipocobro.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El Tipo de cobro que escribio no existe.", "Error Tipo Cobro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbTipocobro.Text = ""
            End If
        End If
    End Sub



    Private Sub txtmontofijo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmontofijo.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = CChar("")
        ElseIf e.KeyChar = "." Then
            If txtmontofijo.Text = "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtmontofijo.Text, ".") <> 0 Then
                    e.KeyChar = CChar("")
                End If

            End If
        ElseIf e.KeyChar = "-" Then
            If txtmontofijo.Text <> "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtmontofijo.Text, "-") <> 0 Then
                    e.KeyChar = CChar("")
                End If
            End If
        End If
    End Sub

    Private Sub txtcantidadacobrarE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcantidadacobrarE.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = CChar("")
        ElseIf e.KeyChar = "." Then
            If txtcantidadacobrarE.Text = "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtcantidadacobrarE.Text, ".") <> 0 Then
                    e.KeyChar = CChar("")
                End If

            End If
        ElseIf e.KeyChar = "-" Then
            If txtcantidadacobrarE.Text <> "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtcantidadacobrarE.Text, "-") <> 0 Then
                    e.KeyChar = CChar("")
                End If
            End If
        End If
    End Sub

    Private Sub txtporcentaje_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtporcentaje.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = CChar("")
        ElseIf e.KeyChar = "." Then
            If txtporcentaje.Text = "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtporcentaje.Text, ".") <> 0 Then
                    e.KeyChar = CChar("")
                End If

            End If
        ElseIf e.KeyChar = "-" Then
            If txtporcentaje.Text <> "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtporcentaje.Text, "-") <> 0 Then
                    e.KeyChar = CChar("")
                End If
            End If
        End If
    End Sub
    Private Sub txtprecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtprecio.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = CChar("")
        ElseIf e.KeyChar = "." Then
            If txtprecio.Text = "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtprecio.Text, ".") <> 0 Then
                    e.KeyChar = CChar("")
                End If

            End If
        ElseIf e.KeyChar = "-" Then
            If txtprecio.Text <> "" Then
                e.KeyChar = CChar("")
            Else
                If InStr(txtprecio.Text, "-") <> 0 Then
                    e.KeyChar = CChar("")
                End If
            End If
        End If
    End Sub
End Class

