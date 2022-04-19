Imports MySql.Data.MySqlClient
Imports BackMarginLTX.exportacioncontratos
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Public Class Frmmodificardetalle

    Private conexionE As New clsConexionSqlServer
    Dim cargar, cargar1 As New Clase_Cargar
    Dim cargarelemento As New Clase_CargarEliminados
    Public nocontrato1 As New FrmNocontrato
    Dim exportar As New exportacioncontratos
    Dim agregarupc As New Clase_CargarUpc
    Dim dtproveedores As New DataTable
    Dim dtmarcas As New DataTable
    Dim dsproductos As New DataSet
    Dim dscontactos As New DataSet
    Dim dsProveedorFact As New DataSet
    Dim dsinformacion As New DataSet
    Dim dsespaciosreservadps As New DataSet
    Dim dtespacios As New DataTable
    Dim dttiposespacio As New DataTable
    Dim dtcontactos As New DataTable
    Dim dscomparacion As New DataSet
    Dim dsproductoseliminados As New DataSet
    Dim dsespacioseliminados As New DataSet
    Dim dshistorico As New DataSet
    Dim numfilas As Integer
    Dim nitBD As String
    Dim direccionBD As String
    Dim idclienteBD As Integer
    Dim NproveedorFact As String
    Dim tipocobroA As String
    Dim idproveedor As String = 0
    Dim siguiente1 As Boolean = True
    Dim sigUpc As Boolean = True
    Dim sigEspacio As Boolean = True
    Dim contactos As Boolean = False
    Dim consulta As String
    Dim FinicialBD, FfinalBD As String
    Dim estadoTA As Integer
    Dim marcas As String = String.Empty
    Dim nombreU, nombreP As String
    Dim productos As Integer = 0

    Private Sub Frmmodificardetalle_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CargarConexionContratos()
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
    Private Sub Frmmodificardetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nocontrato1 = New FrmNocontrato
        nocontrato1.ShowDialog()
        If estadocontrato = 1 Then
            lblnocontrato.Text = Nocontratofrm
            lblnocontrato1.Text = Nocontratofrm
            lblnocontrato2.Text = Nocontratofrm
        ElseIf estadocontrato = 0 Then
            MessageBox.Show("Se necesita que ingrese un numero de contrato para poder hacer la modificacion", "Numero de Contrato", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' nocontrato1.ShowDialog()
            Me.Close()
            Exit Sub
        End If
        dsinformacion = conexion.llenarDataSet("SELECT nitProveedorFac,IdproveedorFac,Direccion FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        nitBD = dsinformacion.Tables(0).Rows(0).Item(0)
        idproveedor = dsinformacion.Tables(0).Rows(0).Item(1)
        direccionBD = dsinformacion.Tables(0).Rows(0).Item(2)

        idproveedor = conexion.EjecutarEscalar("SELECT Idproveedor FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        txtproveedor.Text = conexion.EjecutarEscalar("SELECT IdNombreProveedor FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        marcas = conexion.EjecutarEscalar("SELECT idmarcas FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        txtNunisuper.Text = conexion.EjecutarEscalar("SELECT nombreunisuper FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        txtNproveedor.Text = conexion.EjecutarEscalar("SELECT NombreProveedor FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        nombreU = txtNunisuper.Text
        nombreP = txtNproveedor.Text
        estadoTA = conexion.EjecutarEscalar("SELECT TiendasAutomaticas FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        chbtiendasnuevas.CheckState = estadoTA
        consulta = "SELECT subcategoria ,sku,costo,formacobro AS cobro,FORMAT(cantidadecobro,2) AS cantidadcobrar,tipocobro, " & _
                    " moneda,sucursal.nombre AS tienda,idsubcategoria,idtienda,descripcion  " & _
                    " FROM detalle_contratobmltx AS detallet " & _
                    " INNER JOIN sucursales_bmltx AS sucursal " & _
                    " ON sucursal.id=detallet.idtienda " & _
                    " WHERE idcontrato='" & lblnocontrato.Text & "'"
        llenardataset(consulta, DataSet11, "productos", dgvProductos)
        dsespacioseliminados = conexion.llenarDataSet(" SELECT idtipoespacio,nombre ,nombre AS tienda FROM tipoespacio_bmltx")
        dsespacioseliminados.Clear()
        dsproductoseliminados = conexion.llenarDataSet("  SELECT '' sku,''AS idtienda,'' AS tipocobro FROM tiempocobro_backmarginltx")
        dsproductoseliminados.Clear()
        ''CONSULTA PARA LLENAR LA TABLA DE ESPACIOS...
        consulta = ""
        consulta = "SELECT tipo.`Nombre` as espacio, " & _
                    " espacio.tienda," & _
                    " espacio.cantidad," & _
                    " espacio.cantidaddecobro AS total," & _
                    " espacio.moneda," & _
                    " espacio.`costoespacio` AS precio," & _
                    " tipo.`Idtipoespacio` AS idtipoespacio," & _
                    " espacio.`TipoCobro` " & _
                    " FROM espacio_rentado_backmarginltx AS espacio  " & _
                    " INNER JOIN tipoespacio_bmltx AS tipo " & _
                    " ON tipo.`Idtipoespacio`=espacio.`IdTipoespacio` WHERE espacio.`Idcontrato`='" & lblnocontrato.Text & "'"

        llenardataset(consulta, DataSet21, "espacios", dgvEspacio)
        'BUSCAMOS EL NOMBRE DEL PROVEEDOR AL QUE SE FACTURO
        NproveedorFact = conexion.EjecutarEscalar(" SELECT NombreProveedorFac FROM contratos_backmarginltx WHERE IdContrato='" & lblnocontrato.Text & "'")
        ''AHORA REALIZAMOS LA CONSULTA QUE TRAERA LOS CONTACTOS DEL PROVEEDOR...
        consulta = ""
        consulta = "SELECT nombre,cargo,telefono,correo FROM contactos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'"
        dscontactos = conexion.llenarDataSet(consulta)
        dgvContactos.DataSource = dscontactos.Tables(0)
        consulta = ""
        consulta = "SELECT id,nombre FROM sucursales_bmltx WHERE Idempresas=5"
        Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
        adap.Fill(DataSet41, "tienda")
        dgvtiendas.DataSource = DataSet41.Tables("tienda")
        For Each fila2 As DataRow In DataSet41.Tables("tienda").Rows
            cmbtienda.Items.Add(fila2.Item("Nombre"))
        Next
        dtespacios = conexion.llenarDataTable("SELECT Nombre FROM tipoespacio_bmltx")
        cmbformatodecobro.Text = conexion.EjecutarEscalar("SELECT formatocobro  FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        dtpFinicial.Text = conexion.EjecutarEscalar("SELECT fechainicio FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        dtpFfinal.Text = conexion.EjecutarEscalar("SELECT fechafinal FROM contratos_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        'ASIGNAMOS LOS DATOS PARA COMPARARLOS A LA HORA DE MODIFICAR
        FinicialBD = Format(dtpFinicial.Value, "yyyy-MM-dd")
        FfinalBD = Format(dtpFfinal.Value, "yyyy-MM-dd")
        tipocobroA = cmbformatodecobro.Text
        'CARGAMOS EL HISTORICO..

        dshistorico = conexion.llenarDataSet(" SELECT fechamodificado, usuario,Tipo_modificacion,Elemento_modificado,Detalle_ElementoM,Detalle_NElemento FROM historico_bmltx  WHERE nocontrato='" & lblnocontrato.Text & "'")
        dgvhistorico.DataSource = dshistorico.Tables(0)
        'CAMBIAMOS LA CONEXION PARA TRAER A LOS PROVEEDORES A LOS QUE SE VA FACTURAR EN EXACTUS

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
        'AQUI LE ASIGNAMOS EL VALOR QUE TENIAS EN LA BD..
        cmbproveedoresFac.Text = NproveedorFact

        '=========================================================================================================================================================
        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
        'AQUI CAMBIAMOS LA CONEXION PARA  TRAER LA INFORMACION DE PROVEEDORES
        CargarConexionProductos()

        For Each fila1 As DataRow In dtespacios.Rows
            cmbespacio.Items.Add(fila1.Item("Nombre"))
        Next
        'dtmarcas = conexion.llenarDataTable("SELECT DISTINCT m.Nombre FROM marcas AS m " &
        '                                   "INNER JOIN productosmaster AS p ON m.Id=p.IdMarcas " &
        '                                   "INNER JOIN proveedores AS prov ON prov.id=p.IdProveedores WHERE p.IdProveedores='" & idproveedor & "'")
        dtmarcas = conexionpmm.llenarDataTable("SELECT marca as ""Nombre"" FROM unipmm.unisuperproductosprc where idproveedor='" & idproveedor & "' group by marca order by marca")

        '=========================================================================================================================================================
        For Each fila As DataRow In dtmarcas.Rows
            cmbMarcas.Items.Add(fila.Item("Nombre"))
        Next
        consulta = ""
        consulta = "select idmarca as id ,nombre from unipmm.unisupermarcas where idmarca IN(" & marcas & ") group by idmarca,nombre"
        llenardatasetpmm(consulta, DataSet31, "marca", dgvMarcas)
        'llenardataset(consulta, DataSet31, "marca", dgvMarcas)

    End Sub
    Private Sub btnagregarmarca_Click(sender As Object, e As EventArgs) Handles btnagregarmarca.Click

        If Trim(cmbMarcas.Text.Length) > 0 Then
            Dim filaEncontrada() As DataRow = DataSet31.Tables("marca").Select("nombre='" & cmbMarcas.Text & "'")
            If filaEncontrada.Length = 0 Then
                Try
                    If cmbMarcas.SelectedIndex >= 0 Then
                        consulta = ""
                        'consulta = "SELECT * FROM marcas WHERE nombre='" & cmbMarcas.Text & "'"
                        consulta = "select idmarca as id ,nombre from unipmm.unisupermarcas where nombre='" & cmbMarcas.Text & "' group by idmarca,nombre"
                        'Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
                        Dim adap As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
                        adap.Fill(DataSet31, "Marca")
                        dgvMarcas.DataSource = DataSet31.Tables("Marca")
                        'CAMBIAMOS LA CONEXION A CONTRATOS
                        CargarConexionContratos()
                        llenardatosparahistorico(3, "la marca " & cmbMarcas.Text & "")
                        registrohistorico(cadenaregreso, "Agregamos una nueva marca", " marca ", "Agregar una nueva marca")
                    Else
                        MessageBox.Show("La marca que ingreso no existe o no pertenece a ese proveedor", "Error en Marca", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cmbMarcas.Text = ""
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
        'REGRESAMOS A LA CONEXION DE LOS PRODUCTOS.
        CargarConexionProductos()
    End Sub

    Private Sub btneliminarmarca_Click(sender As Object, e As EventArgs) Handles btneliminarmarca.Click
        eliminardataset(DataSet31, "marca")
    End Sub

    Private Sub btnContagregar_Click(sender As Object, e As EventArgs) Handles btnContagregar.Click
        Dim consultacontactos As String
        If Trim(txtCargo.Text.Length) > 0 And Trim(txtCnombre.Text.Length) > 0 And Trim(txtCorreo.Text.Length) > 0 And Trim(txtTelefono.Text.Length) > 0 Then
            'AGREGAMOS EL ELEMENTO A EL DATAGRID
            Dim anyRow As DataRow = dscontactos.Tables(0).NewRow
            anyRow(0) = txtCnombre.Text
            anyRow(1) = txtCargo.Text
            anyRow(2) = txtTelefono.Text
            anyRow(3) = txtCorreo.Text
            dscontactos.Tables(0).Rows.Add(anyRow)
            'CAMBIAMOS LA CONEXION PARA LOS CONTRATOS..
            CargarConexionContratos()
            consultacontactos = conexion.EjecutarNonQuery("INSERT INTO contactos_backmarginltx(idcontrato,idproveedor,nombre,cargo,telefono,correo)" & _
                                                          " VALUES('" & lblnocontrato.Text & "','" & idproveedor & "', '" & txtCnombre.Text & "','" & txtTelefono.Text & "','" & txtCorreo.Text & "')")
            txtCnombre.Text = ""
            txtCargo.Text = ""
            txtCorreo.Text = ""
            txtTelefono.Text = ""
        Else
            MessageBox.Show("No a terminado de llenar todos los campos", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        'CAMBIAMOS LA CONEXION PARA LA DE PRODUCTOS
        CargarConexionProductos()
    End Sub

    Private Sub btnagregarespacio_Click(sender As Object, e As EventArgs) Handles btnagregarespacio.Click
        'CAMBIAMOS A LA CONEXIN DE LOS CONTRATOS..
        CargarConexionContratos()
        Dim cantida, precio, resultado As Decimal
        Dim moneda As String = String.Empty
        Dim idtipoespacio As String

        Try
            If txtcantidadacobrarE.Text.Length = 0 Or txtprecio.Text.Length = 0 Or cmbespacio.Text.Length = 0 Or cmbtienda.Text.Length = 0 Then
                MessageBox.Show("Error al ingresa el espacio, se encuentran campos vacios.", "Error en el ingreso de un Espacio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim filaEncontrada() As DataRow = DataSet21.Tables("espacios").Select("espacio='" & cmbespacio.Text & "' and Tienda='" & cmbtienda.Text & "'")
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
                dgvEspacio.DataSource = DataSet21.Tables("espacios")
                idtipoespacio = conexion.EjecutarEscalar("SELECT IdTipoespacio FROM tipoespacio_bmltx WHERE nombre='" & cmbespacio.Text & "'")
                Dim anyRow As DataRow = DataSet21.Tables("espacios").NewRow
                anyRow(1) = cmbespacio.Text
                anyRow(2) = cmbtienda.Text
                anyRow(3) = cantida
                anyRow(4) = precio
                anyRow(5) = idtipoespacio
                anyRow(6) = resultado
                anyRow(7) = "Monto Fijo"
                anyRow(8) = moneda
                DataSet21.espacios.Rows.Add(anyRow)
                llenardatosparahistorico(2, "el espacio  " & cmbespacio.Text & " de la tienda " & cmbtienda.Text & "")
                registrohistorico(cadenaregreso, "Agregamos un nuevo espacio ", " Espacios ", "Agregar")
            Else
                MessageBox.Show("El espacio que desea asignar a la Tienda ya existe en la tabla de Información", "Espacio ya existe", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        'REGRESAMOS A LA CONEXION DE LOS PRODUCTOS...
        CargarConexionProductos()
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

        If TipoCobro1 = " 0" Then
            Exit Sub
        End If

        If txtporcentaje.Text.Length = 0 Or cmbTipocobro.Text.Length = 0 Then

        End If
        'RECORREMOS LAS TIENDAS QUE EL USUARIO SELECCIONO
        If DataSet41.Tables("tienda").Rows.Count >= 1 Then
            If DataSet41.Tables("tienda").Select("seleccion='1'").Count = 0 Then
                Exit Sub
            Else
                numfilas = DataSet41.Tables("tienda").Select("seleccion='1'").Count
                filads = DataSet41.Tables("tienda").Select("seleccion='1'")
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
                    '              "  INNER JOIN Proveedores " &
                    '              "  ON proveedores.`Id`=producto.`IdProveedores`" &
                    '              "  WHERE producto.`sku`=" & txtSku.Text.Trim & "  AND  empresas.`Id`= 5  AND sucursales.`Id`='" & id & "'  AND producto.`IdProveedores`= " & idproveedor & " " &
                    '              "   ORDER BY sucursales.Id"
                    consulta = "SELECT T.NOMBRE AS ""TIENDA"",SC.NOMBRE AS ""SUBCATEGORIA"",P.SKU,P.DESCLARGA AS ""DESCRIPCION"", P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL as ""idtienda"" " &
                               "FROM unipmm.unisupersurtido S " &
                               "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU " &
                               "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD " &
                               "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                               "WHERE P.SKU=" & txtSku.Text.Trim & " And P.IDPROVEEDOR= " & idproveedor & " And T.IDSUCURSAL='" & id & "'  " &
                               "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"
                    'VALIDAMOS QUE NO EXISTE EN LA TABLA...
                    validacionllenado(consulta, DataSet11, id, sku, "productos", TipoCobro1, cantidad1, moneda1)
                    '=========================================================================================================================================================
                    If estadocontrato = True Then
                        respuesta = MessageBox.Show("El SKU que desea ingresar ya se encuentra en la tabla de datos.¿Desea actualizar los datos de ese SKU?", "Ingreso duplicado de SKU", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                        If respuesta = vbYes Then
                            Dim filaEncontrada() As DataRow = DataSet11.Tables("productos").Select("sku='" & sku & "' and idtienda='" & id & "' and cobro='" & tipocobroR & "'")
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

        dgvProductos.DataSource = DataSet11.Tables("productos")
        txtSku.Text = ""
        txtdescripcion.Text = ""
        btnUpcAgregar.Enabled = False
        'estado1 = True

    End Sub

    Private Sub btnagregarmarcas_Click(sender As Object, e As EventArgs) Handles btnagregarmarcas.Click

        Dim filads() As DataRow
        Dim consulta, unionmarcas, uniontienda, tipocobro1, cantidad1, moneda, resultado As String
        Dim sku, tienda As String
        numfilas = 0
        unionmarcas = regresarcadena(DataSet31, "marca")
        tipocobro1 = tipocobroresutado()
        cantidad1 = cantidadcobrar()
        moneda = tipomoneda()
        uniontienda = regresarcadena(DataSet41, "tienda")
        Try
            If tipocobro1 = " 0 " Then
                Exit Sub
            End If
            'VALIDAMOS QUE EL CAMPO DE TIPO DE COBRO ESTE LLENO
            'VALIDAMOS SI EL DATASET TIENE DATOS...
            Button1.Enabled = False
            If DataSet11.Tables("productos").Rows.Count >= 1 Then
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
                '=========================================================================================================================================================
                'LLENAMOS UN DATASET CON LOS SKU QUE EXISTEN PARA ESA MARCA...
                'Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
                Dim adap As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
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
                        '=========================================================================================================================================================
                        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                        consulta = ""
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
                        '              "  ON proveedores.`Id`=producto.`IdProveedores`" &
                        '              "  WHERE producto.`sku`=" & sku & "  AND  empresas.`Id`= 5  AND sucursales.`Id`='" & tienda & "'  AND producto.`IdProveedores`= " & idproveedor & " " &
                        '              "   ORDER BY sucursales.Id"
                        consulta = "SELECT T.NOMBRE AS ""TIENDA"",SC.NOMBRE AS ""SUBCATEGORIA"",P.SKU,P.DESCLARGA AS ""DESCRIPCION"", P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL as ""idtienda"" " &
                                   "FROM unipmm.unisupersurtido S " &
                                   "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU " &
                                   "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD " &
                                   "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                                   "WHERE P.SKU=" & sku & " And P.IDPROVEEDOR=" & idproveedor & " And T.IDSUCURSAL='" & tienda & "' " &
                                   "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"
                        validacionllenado(consulta, DataSet11, tienda, sku, "productos", tipocobro1, cantidad1, moneda)
                        '=========================================================================================================================================================
                        'REVISAMOS SI EXISTE OTRO PRODUCTO PARA INGRESARLO...
                        If cargar1.siguiente = True Then
                        Else
                            Exit For
                        End If
                    Next
                    dgvProductos.DataSource = DataSet11.Tables("productos")
                    Button1.Enabled = True
                    MessageBox.Show("Productos cargados correctamente", "No se encuentran Productos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'CUANDO EL DATASET DE MARCAS NO ENCONTRO NINGUN SKU...
                    MessageBox.Show("No se encuentran Productos de la marcar que selecciono", "No se encuentran Productos", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                '            " INNER JOIN proveedores " &
                '             "  ON proveedores.`Id`=producto.`IdProveedores`" &
                '            " INNER JOIN marcas" &
                '            " ON marcas.`Id`=producto.`IdMarcas`" &
                '            " WHERE marcas.`Id` IN(" & unionmarcas & ")  AND  empresas.`Id`= 5  AND sucursales.Id IN(" & uniontienda & ") AND producto.`IdProveedores`=" & idproveedor & " " &
                '            " ORDER BY sucursales.`Id`"
                consulta = "SELECT P.SKU AS Sku,T.IDSUCURSAL as id " &
                           "FROM unipmm.unisupersurtido S  " &
                           "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU  " &
                           "INNER JOIN UNIPMM.UNISUPERSUCURSALES T ON T.IDSUCURSAL=S.TIENDACD  " &
                           "INNER JOIN unipmm.unisupersubcategorias SC ON SC.IDSUBCATEGORIA=P.IDSUBCATEGORIA " &
                           "WHERE P.IDMARCA IN(" & unionmarcas & ") And P.IDPROVEEDOR=" & idproveedor & "  And T.IDSUCURSAL IN(" & uniontienda & ")" &
                           "GROUP BY T.NOMBRE,SC.NOMBRE,P.SKU,P.DESCLARGA,P.COSTO,P.IDSUBCATEGORIA,T.IDSUCURSAL ORDER BY T.IDSUCURSAL"
                'Dim adap1 As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
                Dim adap1 As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
                '=========================================================================================================================================================
                adap1.Fill(DataSet11, "productos")
                If tipocobro1 = "Monto Fijo" Then
                    resultado = "Monto Fijo"
                Else
                    resultado = cmbTipocobro.Text
                End If

                For Each fila As DataRow In DataSet11.Tables("productos").Rows
                    fila("cobro") = resultado
                    fila("Tipocobro") = tipocobro1
                    fila("cantidadcobrar") = cantidad1
                    fila("moneda") = moneda
                Next
                dgvProductos.DataSource = DataSet11.Tables("productos")
                Button1.Enabled = True
                MessageBox.Show("Productos cargados correctamente", "No se encuentran Productos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'SALIMOS DE LA VALIDACION SI SE ENCUENTRA LLENO EL DATASET CENTRAL
            End If

        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try
        ' estado1 = True
    End Sub

    Private Sub cmbespacio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbespacio.SelectedIndexChanged
        'AQUI CAMBIAMOS LA CONEXION PARA TRAER LOS TIPOS DE ESPACIOS
        CargarConexionContratos()
        txtprecio.Text = conexion.EjecutarEscalar("SELECT costo FROM tipoespacio_bmltx WHERE Nombre='" & cmbespacio.Text & "'")
        'AQUI CERRAMOS LA CONEXION ANTERIOR Y REGRESAMOS A LA DE PRODUCTOS
        CargarConexionProductos()
    End Sub
    Private Sub txtSku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSku.KeyPress
        Dim resultado As Integer
        If e.KeyChar = Chr(13) Then
            Try
                Int64.Parse(txtSku.Text)

            Catch ex As Exception
                MessageBox.Show("Solo puede ingresar números en la casilla Sku", "Sku debe ser un número", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
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
                txtdescripcion.Text = conexionpmm.EjecutarEscalar("select desclarga from unipmm.unisuperproductosprc WHERE sku='" & txtSku.Text & "'")
                '=========================================================================================================================================================
                btnUpcAgregar.Enabled = True
            End If
        End If


    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        eliminarelemento(DataSet11, dgvProductos, "productos")

    End Sub

    Private Sub EliminarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem1.Click
        eliminarelemento(DataSet21, dgvEspacio, "espacios")
    End Sub

    Private Sub btnModificarContrato_Click(sender As Object, e As EventArgs) Handles btnModificarContrato.Click
        'AQUI CAMBIAMOS LA CONEXION PARA Tener la de productos...
 

        Dim agregarupc As New Clase_CargarUpc
        Dim agregarespacio As New Clase_CargarEspacios
        Dim filads(), filads2() As DataRow
        Dim dgvcopias As New DataGridView
        Dim numfilas As Integer = 0
        Dim consultacrearcontrato As String = String.Empty
        Dim fechainicial As String = String.Empty
        Dim fechafinal As String = String.Empty
        Dim formatocobro As String = String.Empty
        Dim formatocobroUpc As String = String.Empty
        Dim tipocobro As String = String.Empty
        Dim cantidad As String = String.Empty
        Dim idtienda As String
        Dim barra As String = String.Empty
        Dim sku As String = String.Empty
        Dim cobro As String = String.Empty
        Dim tipocobroupc As String = String.Empty
        Dim cantidadcobrar1 As String = String.Empty
        Dim subcategoria1 As String = String.Empty
        Dim idsucategoria2 As String = String.Empty
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
        Dim precio, monton As Decimal
        Dim descripcion As String = String.Empty
        Dim duracion As Integer = 0
        Dim consultapago As String = String.Empty
        Dim tiendasautomaticas As Integer
        Dim consultaactualizacion As String = String.Empty
        Dim mensaje As String
        Dim unionmarcaActual As String = String.Empty
        Dim consultaeliminacion As String = String.Empty
        Dim consultaeliminacionTiempo As String = String.Empty
        Dim fechaenviar As String
        Dim nit As String
        Dim idcliente As String
        Dim fecha As Date
        Dim duracion2, duracionModificacion As Integer
        Dim contadorInsertar As Integer = 0

        Try
            Me.Cursor = Cursors.WaitCursor

            'CAMBIAMOS LA CONEXION PARA TRAER A LOS PROVEEDORES A LOS QUE SE VA FACTURAR EN EXACTUS
            ' conexion.CerrarConexion()
            conexionE.BuscarConexionEnXml(Application.StartupPath + "\" & ("arconexionExactus.xml"))
            conexionE.AbrirConexion()
            idcliente = conexionE.EjecutarEscalar("SELECT CLIENTE from UNISUP.CLIENTE where NOMBRE = '" & cmbproveedoresFac.Text & "'")
            nit = conexionE.EjecutarEscalar("select contribuyente from unisup.cliente where CATEGORIA_CLIENTE='CPUBLI' AND NOMBRE ='" & cmbproveedoresFac.Text & "'")
            conexionE.CerrarConexion()
            'REGRESAMOS A A LA CONEXION DE LOS CONTRATOS
            fecha = Now
            fechaenviar = Format(fecha, "yyyy-MM-dd")
            hora = Format(fecha, "hh:mm:ss")
            fechaactualiza = fechaenviar & "  " & hora
            unionmarcaActual = regresarcadena(DataSet31, "marca")
            fechainicial = Format(dtpFinicial.Value, "yyyy-MM-dd")
            fechafinal = Format(dtpFfinal.Value, "yyyy-MM-dd")
            tiendasautomaticas = chbtiendasnuevas.CheckState
            'BUSCAMOS LA DURACION DEL CONTRATO..
            duracion = DateDiff(DateInterval.Month, dtpFinicial.Value, dtpFfinal.Value)
            duracion2 = DateDiff(DateInterval.Month, dtpFinicial.Value, dtpFfinal.Value)
            duracionModificacion = DateDiff(DateInterval.Month, Now, dtpFfinal.Value)
            'CAMBIAMOS LA CONEXION PARA TENER LA DE CONTRATOS..
            CargarConexionContratos()
            'ACTUALIZAMOS LA FECHA INICIO Y FECHA FINAL

            comparaciondatos(fechainicial, FinicialBD, "Fechainicio")
            comparaciondatos(fechafinal, FfinalBD, "Fechafinal")
            comparaciondatos(cmbformatodecobro.Text, tipocobroA, "formatocobro")
            comparaciondatos(txtNunisuper.Text, nombreU, "NombreUnisuper")
            comparaciondatos(txtNproveedor.Text, nombreP, "NombreProveedor")
            comparaciondatos(unionmarcaActual, marcas, "IdMarcas")
            comparaciondatos(tiendasautomaticas, estadoTA, "TiendasAutomaticas")
            comparaciondatos(cmbproveedoresFac.Text, NproveedorFact, "NombreProveedorFac")
            comparaciondatos(nit, nitBD, "nitProveedorFac")
            comparaciondatos(idcliente, idclienteBD, "IdproveedorFac")


            'AHORA VAMOS A CARGAR LOS DATOS DE LOS SKU..
            'PRIMERO VAMOS A ELIMINAR LOS ELEMENTOS ANTERIORES DE LA TABLAS SEGUN EL CONTRATO.
            consultaeliminacion = conexion.EjecutarNonQuery(" DELETE FROM detalle_contratobmltx WHERE idcontrato='" & lblnocontrato.Text & "'")
            'AHORA SE CARGAN LOS UPC QUE ELLA TIENE ACTUALMENTE..
            DataSet11.AcceptChanges()
            If DataSet11.Tables("productos").Rows.Count >= 1 Then
                numfilas = DataSet11.Tables("productos").Rows.Count
                filads = DataSet11.Tables("productos").Select()
                With agregarupc
                    For Each fila As DataRow In filads
                        .AgregarProducto(fila("subcategoria"), fila("descripcion"), fila("sku"), fila("Costo"), fila("cobro"), fila("idsubcategoria"), fila("tipocobro"), fila("cantidadcobrar"), fila("idtienda"), fila("moneda"))
                    Next
                End With
                conexion.AbrirConexion()
                conexion.IniciarTransaccion()

                For i As Integer = 1 To numfilas
                    With agregarupc
                        sku = .sku
                        cobro = .formatocobro
                        subcategoria1 = .subcategoria
                        costo = .costo
                        idsucategoria2 = .idsubcategoria
                        tipocobroupc = .tipocobro
                        cantidadcobrar1 = .cantidadcobro
                        idtienda = .idtienda
                        moneda = .moneda
                        descripcion = .descripcion
                    End With
                    If sigUpc = True Then
                        consulta = String.Empty
                        consulta = "INSERT INTO detalle_contratobmltx(idcontrato,IdProveedor,IdSubcategoria,subcategoria,Sku,costo,Formacobro,TipoCobro,Cantidadecobro,idTienda,moneda,descripcion)" & vbCr &
                        "VALUES('" & lblnocontrato.Text & "','" & idproveedor & "','" & idsucategoria2 & "','" & subcategoria1 & "','" & sku & "','" & costo & "', '" & cobro & "', '" & tipocobroupc & "', '" & cantidadcobrar1 & "','" & idtienda & "','" & moneda & "','" & descripcion & "')" & vbCr
                        sigUpc = False
                    Else
                        contadorInsertar += 1
                        consulta += ",  " & "('" & lblnocontrato.Text & "','" & idproveedor & "','" & idsucategoria2 & "','" & subcategoria1 & "','" & sku & "','" & costo & "', '" & cobro & "', '" & tipocobroupc & "', '" & cantidadcobrar1 & "','" & idtienda & "','" & moneda & "','" & descripcion & "')" & vbCr
                        If contadorInsertar = 500 Then
                            consulta1 = conexion.EjecutarNonQuery(consulta)
                            consulta = String.Empty
                            sigUpc = True
                            contadorInsertar = 0
                        End If
                    End If
                    'REVISAMOS SI EXISTE OTRO PRODUCTO PARA INGRESARLO
                    If agregarupc.siguiente = True Then
                    Else
                        Exit For
                    End If
                Next
                'REALIZAMOS LA CONSULTA..
                consulta1 = conexion.EjecutarNonQuery(consulta)
                conexion.FinalizarTransaccion()
                conexion.CerrarConexion()
            End If

            'AHORA VAMOS A ELIMINAR LOS ESPACIOS QUE SE ENCUENTRAN EN LOS CONTRATOS..
            conexion.AbrirConexion()
            conexion.IniciarTransaccion()
            consultaeliminacion = conexion.EjecutarNonQuery(" DELETE FROM espacio_rentado_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
            If DataSet21.Tables("espacios").Rows.Count >= 1 Then
                numfilas = 0
                numfilas = DataSet21.Tables("espacios").Rows.Count
                filads2 = DataSet21.Tables("espacios").Select()
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
                    ' tipomoneda = conexion.EjecutarEscalar("SELECT idmoneda FROM tipomoneda_bmltx WHERE tipomoneda='" & moneda1 & "'")
                    consulta = ""
                    monton = 0
                    consulta = "INSERT INTO espacio_rentado_backmarginltx(Idcontrato,Idproveedor,tienda,IdTipoespacio,Cantidad,TipoCobro,CantidaddeCobro,fechainicial,Fechafinal,moneda,costoespacio)" &
                            "VALUES('" & lblnocontrato.Text & "','" & idproveedor & "','" & nombretienda & "','" & idtipo & "','" & cantidad & "','" & tipocobro & "','" & total & "','" & fechainicial & "','" & fechafinal & "','" & moneda1 & "'," & precio & ")"
                    'REALIZAMOS LA CONSULTA.
                    consulta1 = conexion.EjecutarNonQuery(consulta)

                    'buscamos el siguiente espacio
                    If agregarespacio.siguiente = True Then
                    Else
                        Exit For
                    End If
                Next
            End If
            conexion.FinalizarTransaccion()
            conexion.CerrarConexion()

            MessageBox.Show("Se ha Modificado con exito el contrato", "Modificacion de contrato", MessageBoxButtons.OK, MessageBoxIcon.Information)
            mensaje = MessageBox.Show("Desea Imprimir el contrato", "Back Margin LTExpress", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            cargarventanaM = True
            If mensaje = vbYes Then
                'CREAMOS DATASET PARA PODER ENVIARLES LOS DATOS A EL PDF
                'YA QUE SE NECESITA ELIMINAR CIERTAS COLUMNAS DE LOS DATASET
                Dim dstablasp As DataSet = DataSet11.Copy
                Dim dstablaE As DataSet = DataSet21.Copy
                Dim dstablaM As DataSet = DataSet31.Copy
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
                exportar.creararchivo("Modificacion del contrato No. " & lblnocontrato.Text & txtNproveedor.Text.ToUpperInvariant, fechafinal, fechainicial, txtNproveedor.Text,
                                      lblnocontrato.Text, cmbformatodecobro.Text, dgvContactos, dgvProductos, txtNunisuper.Text, txtNproveedor.Text, dgvEspacio, dgvMarcas, cmbproveedoresFac.Text)
                elimimarelementosEspacios()
                elimimarelementosProductos()
                dstablaE.Clear()
                dstablaM.Clear()
                dstablasp.Clear()
            End If
            CargarConexionProductos()

            'limpiarelementos()
            cerrarventanas(cargarventanaM)
            dscomparacion = conexion.llenarDataSet(" SELECT idtipoespacio,CantidaddeCobro,Cantidad, tienda FROM espacio_rentado_backmarginltx WHERE idcontrato='" & lblnocontrato.Text & "'")
        Catch ex As Exception
            conexion.ForzarErrorTransaccion()
            conexion.FinalizarTransaccion()
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' limpiarelementos()
            Exit Sub
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub cmbtienda_LostFocus(sender As Object, e As EventArgs) Handles cmbtienda.LostFocus
        If Trim(cmbtienda.Text.Length) > 0 Then
            If cmbtienda.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("La tienda que ingrenso no existe", "Error de Tienda", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbtienda.Text = ""
            End If
        End If

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
#Region "FUNCIONES Y PROCECIMIENTOS"
    Public Sub eliminardataset(ByVal dataset As DataSet, ByVal nombretabla As String)
        Dim numeroceldas As Integer = 0
        Dim filads() As DataRow
        Dim nombreelemento As String
        Dim idelemento
        dgvProductos.EndEdit()


        If dataset.Tables(nombretabla).Rows.Count >= 1 Then
            If dataset.Tables(nombretabla).Select("seleccion='1'").Count = 0 Then
                MessageBox.Show("No ha seleccionado ningun Elemento  para realizar el contrato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else
                numeroceldas = dataset.Tables(nombretabla).Select("seleccion='1'").Count
                filads = dataset.Tables(nombretabla).Select("seleccion='1'")

                With cargar
                    For Each fila As DataRow In filads
                        .AgregarNombre(fila("id"), fila("nombre"))
                    Next
                End With
                For i As Integer = 1 To numeroceldas
                    With cargar
                        idelemento = .id
                        nombreelemento = .nombre
                    End With
                    llenardatosparahistorico(2, "la marca " & nombreelemento & "")
                    registrohistorico(cadenaregreso, "Eliminacion de  una  marca en el contrato " & lblnocontrato.Text & "", " marca ", "Eliminacion")

                    'REVISAMOS SI EXISTE OTRO PRODUCTO PARA INGRESARLO
                    If cargar.siguiente = True Then
                    Else
                        Exit For
                    End If
                Next

                'AHORA ELIMINAMOS TODOS LOS ELEMENTOS SELECCIONADOS

                For Each fila As DataRow In filads
                    For i As Integer = 0 To numeroceldas - 1
                        filads(i).Delete()
                    Next
                Next

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
                        cadena = id.TrimEnd
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

    'Public Sub limpiarelementos()
    '    txtCargo.Text = ""
    '    txtCnombre.Text = ""
    '    txtCorreo.Text = ""
    '    txtdescripcion.Text = ""
    '    txtmontofijo.Text = ""
    '    txtNproveedor.Text = ""
    '    txtNunisuper.Text = ""
    '    txtcantidadacobrarE.Text = ""
    '    txtporcentaje.Text = ""
    '    txtTelefono.Text = ""
    '    txtSku.Text = ""
    '    cmbespacio.Text = ""
    '    cmbformatodecobro.Text = ""
    '    txtNproveedor.Text = ""
    '    cmbtienda.Text = ""
    '    cmbMarcas.Text = ""
    '    cmbTipocobro.Text = ""
    '    cmbMarcas.Items.Clear()
    '    cmbTipocobro.Enabled = True
    '    chkmontofijo.Enabled = True
    '    chkporcentaje.Enabled = True
    '    txtporcentaje.Enabled = True
    '    txtmontofijo.Enabled = True
    '    txtcantidadacobrarE.Enabled = True
    '    siguiente1 = True
    '    sigUpc = True
    '    sigEspacio = True
    '    contactos = False
    '    dtmarcas.Clear()
    '    dtmarcas.Reset()
    '    chkmontofijo.CheckState = CheckState.Unchecked
    '    chkporcentaje.CheckState = CheckState.Unchecked
    '    chkdolares.CheckState = CheckState.Unchecked
    '    chkquetzales.CheckState = CheckState.Unchecked
    '    chkdolares1.CheckState = CheckState.Unchecked
    '    chkquetzales1.CheckState = CheckState.Unchecked
    '    DataSet11.Clear()
    '    DataSet21.Clear()
    '    DataSet31.Clear()
    '    dtcontactos.Reset()
    '    dtcontactos.Clear()
    '    dsproductos.Clear()
    '    dsproductos.Reset()
    '    dscontactos.Clear()

    '    txtcantidadacobrarE.Text = ""


    'End Sub
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
            MessageBox.Show("Solo puede seleccionar una Opcion", "Ambas opciones seleccionadas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            resultado = "0"
        ElseIf chkmontofijo.CheckState = CheckState.Unchecked And chkporcentaje.CheckState = CheckState.Unchecked Then
            MessageBox.Show("No a seleccionado ninguna Opcion", "No hay una opcion seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                _listaTiendas &= _dsTiendasLTX.Tables(0).Rows(0)("id") & ","
            Next
            _listaTiendas = _listaTiendas.TrimEnd(",")
        End If

        If Trim(txtSku.Text.Length) > 0 Then
            '=========================================================================================================================================================
            'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
            'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
            'productos = conexion.EjecutarEscalar("SELECT COUNT(producto.sku)  FROM productosmaster AS producto  INNER JOIN segmentaciontienda AS segmentacion  " &
            '                                     "ON segmentacion.sku= producto.sku WHERE producto.sku= '" & txtSku.Text & "' " &
            '                                     "AND segmentacion.idcadena=5 AND producto.Idproveedores='" & idproveedor & "'")

            productos = conexionpmm.EjecutarEscalar("SELECT COUNT(S.SKU) FROM unipmm.unisupersurtido S " &
                                                 "INNER JOIN unipmm.unisuperproductosprc P ON S.SKU=P.SKU " &
                                                 "WHERE P.SKU= '" & txtSku.Text & "' AND P.IDPROVEEDOR='" & idproveedor & "' AND S.TIENDACD IN(" & _listaTiendas & ")")
            '=========================================================================================================================================================
        Else
            MessageBox.Show("No se encontro ningun producto con ese Sku", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Return productos
    End Function
    Public Sub validacionllenado(ByVal consulta As String, ByVal dataset As DataSet, ByVal tienda As Integer, ByVal sku As String, ByVal nombretabla As String, ByVal tipocobro As String, _
                                 ByVal cantidad As String, ByVal moneda As String)
        'CAMBIAMOS A LA CONEXION DE PRODUCTOS
        CargarConexionProductos()

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
            Dim filaEncontrada() As DataRow = dataset.Tables(nombretabla).Select("sku='" & sku & "' and idtienda='" & tienda & "' and cobro= '0'")
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
        Dim tipocobro As String
        Dim idespacio, espacio As String
        Dim sku, tienda As String
        Dim idtienda As Integer
        Dim tipomoneda As String
        Try

            fila2 = dgv.CurrentRow.Index
            If nombretabla = "productos" Then
                sku = dataset.Tables(nombretabla).Rows(fila2).Item(1)
                tienda = dataset.Tables(nombretabla).Rows(fila2).Item(8)
                idtienda = dataset.Tables(nombretabla).Rows(fila2).Item(10)
                tipocobro = dataset.Tables(nombretabla).Rows(fila2).Item(7)
                tipomoneda = dataset.Tables(nombretabla).Rows(fila2).Item(11)
                Dim anyRow1 As DataRow = dsproductoseliminados.Tables(0).NewRow
                anyRow1(0) = sku
                anyRow1(1) = idtienda
                anyRow1(2) = tipocobro
                dsproductoseliminados.Tables(0).Rows.Add(anyRow1)
                dataset.Tables(nombretabla).Rows(fila2).Delete()
                dataset.AcceptChanges()
            ElseIf nombretabla = "espacios" Then

                idespacio = dataset.Tables(nombretabla).Rows(fila2).Item(5)
                espacio = dataset.Tables(nombretabla).Rows(fila2).Item(1)
                tienda = dataset.Tables(nombretabla).Rows(fila2).Item("tienda")
                Dim anyRow As DataRow = dsespacioseliminados.Tables(0).NewRow
                anyRow(0) = idespacio
                anyRow(2) = tienda
                anyRow(1) = espacio
                dsespacioseliminados.Tables(0).Rows.Add(anyRow)
                dataset.Tables(nombretabla).Rows(fila2).Delete()
                dataset.AcceptChanges()
            End If

            MessageBox.Show("La eliminacion del elemento seleccionado se realizo con exito", "Eliminacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Ocurrio el siguiente error " & vbCrLf & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub llenardataset(ByVal consulta As String, ByVal dataset As DataSet, ByVal nombretabla As String, ByVal dgv As DataGridView)
        Dim adap As New MySqlDataAdapter(consulta, conexion.ObtenerConexion)
        adap.Fill(dataset, nombretabla)
        dgv.DataSource = dataset.Tables(nombretabla)
    End Sub

    Public Sub llenardatasetpmm(ByVal consulta As String, ByVal dataset As DataSet, ByVal nombretabla As String, ByVal dgv As DataGridView)
        Dim adap As New OracleDataAdapter(consulta, conexionpmm.ObtenerConexion)
        adap.Fill(dataset, nombretabla)
        dgv.DataSource = dataset.Tables(nombretabla)
    End Sub
    Public Sub registrohistorico(ByVal dato As String, ByVal datocambiado As String, ByVal razon As String, ByVal tipomodificacion As String)
        ''CAMBIAMOS LA CONEXION PARA PODER GUARDAR LOS DATOS EN EL HISTORICO
        CargarConexionContratos()
        Dim consulta As String = String.Empty
        Dim fecha As Date
        Dim fechaenviar As String
        Dim hora As String
        Dim fechaactualizada As String
        'cambiar el nombre de usuario
        fecha = Now
        fechaenviar = Format(fecha, "yyyy-MM-dd")
        hora = Format(fecha, "hh:mm:ss")
        fechaactualizada = fechaenviar & "  " & hora
        consulta = conexion.EjecutarNonQuery("INSERT INTO historico_bmltx(fechamodificado,usuario,Tipo_modificacion,Elemento_modificado,Detalle_ElementoM,Detalle_NElemento,Nocontrato)" & _
                    "  VALUES('" & fechaactualizada & "','" & nombreusuario & "','" & tipomodificacion & "','" & razon & "','" & dato & "','" & datocambiado & "','" & lblnocontrato.Text & "')")

        ''CAMBIAMOS LA CONEXION PARA LA DE PRODUCTOS
        'CargarConexionProductos()
    End Sub
    Public Sub comparaciondatos(ByVal datoinicial As String, ByVal datoanterior As String, ByVal tipodato As String)

        Dim consulta As String
        Dim resultado As Boolean = True

        'COMPARAMOS SI LOS DATOS SON IGUALES..
        resultado = String.Equals(datoinicial, datoanterior)
        If resultado = True Then
            Exit Sub
        Else
            consulta = conexion.EjecutarNonQuery("UPDATE contratos_backmarginltx SET " & tipodato & "='" & datoinicial & "' WHERE idcontrato='" & lblnocontrato.Text & "'")
            llenardatosparahistorico(1, tipodato)
            registrohistorico(cadenaregreso, "Modificacion de la " & tipodato & " del contrato " & lblnocontrato.Text & "", tipodato, "Modificacion")
        End If



    End Sub

    Public Sub cerrarventanas(ByVal desicion As Boolean)
        Dim contrato As New Frmmodificardetalle

        If desicion = True Then
            Me.Close()
            contrato = New Frmmodificardetalle
            contrato.ShowDialog()
        Else
            Me.Close()

        End If
    End Sub

    Public Sub elimimarelementosProductos()

        Dim filads() As DataRow
        Dim barra As String = String.Empty
        'Dim consulta1 As String
        Dim sku As String
        Dim idtienda As String
        Dim tipocobro As String
        Dim consulta As String = String.Empty
        Dim fecha As Date
        Dim tienda As String
        Dim fechacontrato As String
        'Dim consultaEliminar As String
        fecha = Now
        fechacontrato = Format(fecha, "yyyy-MM-dd")
        If dsproductoseliminados.Tables(0).Rows.Count >= 1 Then
            numfilas = dsproductoseliminados.Tables(0).Rows.Count
            filads = dsproductoseliminados.Tables(0).Select()
            cargarelemento = New Clase_CargarEliminados
            With cargarelemento
                For Each fila As DataRow In filads
                    .Agregarelemento(fila("Sku"), fila("idtienda"), fila("tipocobro"))
                Next
            End With
            For i As Integer = 1 To numfilas
                With cargarelemento
                    sku = .idelemento
                    idtienda = .tienda
                    tipocobro = .tipoelemento

                End With
                'AQUI VERIFICAMOS SI ES MONTO FIJO LO INGRESAMOS A LA TABLA DE MONTO FIJO
                tienda = conexion.EjecutarEscalar("SELECT nombre FROM sucursales_bmltx WHERE id=" & idtienda & "")
                llenardatosparahistorico(2, "el PLU " & sku & " de la tienda " & tienda & "")
                registrohistorico(cadenaregreso, "Eliminamos un PLU", " Eliminacion ", "Eliminamos el PLU " & sku & "")
                If cargarelemento.siguiente = True Then
                Else
                    Exit For
                End If
            Next

        End If
    End Sub

    Public Sub elimimarelementosEspacios()

        Dim filads() As DataRow
        Dim barra As String = String.Empty
        Dim idespacio As String
        Dim tienda As String
        Dim espacio As String
        Dim consulta As String = String.Empty
        Dim fecha As Date
        Dim fechacontrato As String

        fecha = Now
        fechacontrato = Format(fecha, "yyyy-MM-dd")
        If dsespacioseliminados.Tables(0).Rows.Count >= 1 Then
            numfilas = dsespacioseliminados.Tables(0).Rows.Count
            filads = dsespacioseliminados.Tables(0).Select()
            cargarelemento = New Clase_CargarEliminados
            With cargarelemento
                For Each fila As DataRow In filads
                    .Agregarelemento(fila("idtipoespacio"), fila("tienda"), fila("nombre"))
                Next
            End With
            For i As Integer = 1 To numfilas
                With cargarelemento
                    idespacio = .idelemento
                    tienda = .tienda
                    espacio = .tipoelemento
                End With
                llenardatosparahistorico(2, "el espacio " & espacio & " de la tienda " & tienda & "")
                registrohistorico(cadenaregreso, "Eliminamos el espacio", " Eliminacion ", "Eliminamos el espacio " & espacio & "")
                If cargarelemento.siguiente = True Then
                Else
                    Exit For
                End If
            Next

        End If
    End Sub

#End Region

    Private Sub cmbTipocobro_LostFocus(sender As Object, e As EventArgs) Handles cmbTipocobro.LostFocus
        If Trim(cmbTipocobro.Text.Length) > 0 Then
            If cmbTipocobro.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El Tipo de cobro que escribio no existe.", "Error Tipo Cobro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbTipocobro.Text = ""
            End If
        End If
    End Sub




End Class