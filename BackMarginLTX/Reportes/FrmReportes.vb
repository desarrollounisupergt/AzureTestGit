Imports MySql.Data.MySqlClient
Imports BackMarginLTX.exportacionreportes
Public Class FrmReportes
#Region "Variables"
    Dim dtcontratos As New DataTable
    Dim dtproveedores As New DataTable
    Private conexionE As New clsConexionSqlServer
    Dim dtaño As New DataTable
    Dim ds1tienda As New DataSet
    Dim dscontratos As New DataSet
    Dim dscontratosanidados As New DataSet
    Dim frmtasa As New frmtasadecambio
    Dim fechainicio As String
    Dim fechafinal1 As String
    Dim fechainicioBD As Date
    Dim fechafinalBD As Date
    Dim dia As Integer
    Dim idproveedor As Integer
    Dim consultaventas As Decimal = 0.0
    Dim consultacompras As Decimal = 0.0
    Dim consultaespaciosQ As Decimal = 0.0
    Dim consultaespaciosD As Decimal = 0.0
    Dim consultamontofijoQ As Decimal = 0.0
    Dim consultamontofijoD As Decimal = 0.0
    Dim consultageneral As String
    Dim validacion As Boolean = True
    Dim id_año, id_año2 As Integer
    Dim idtienda As String
    Dim moneda As String
    Dim numeromeses, numeromeses1, numerototalmeses As Integer
    Dim reporte As New exportacionreportes
    Dim idespacio, idingresos, idventas, idmontofijo As Integer
    Dim idMonedaD, idmonedaQ As Integer
    Private exportarcsv As New ClaseExportar

    Private _ds_resultado As New DataSet


    Public Property ds_resultado As DataSet
        Get
            Return _ds_resultado
        End Get
        Set(value As DataSet)
            _ds_resultado = value
        End Set
    End Property

    Private _lista_sucursales As String
    Public Property lista_sucursales As String
        Get
            Return _lista_sucursales
        End Get
        Set(value As String)
            _lista_sucursales = value
        End Set
    End Property

    Private _lista_id_sucursales As String
    Public Property lista_id_sucursales As String
        Get
            Return _lista_id_sucursales
        End Get
        Set(value As String)
            _lista_id_sucursales = value
        End Set
    End Property



#End Region

    'Dim dgvTotales As New DataGridView
    Private Sub FrmReportes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FrmMenuPrincipal.WindowState = FormWindowState.Normal
        CargarConexionContratos()
    End Sub

    Private Sub FrmReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bloquear_controles_panel()
        dtcontratos = conexion.llenarDataTable("SELECT idcontrato FROM contratos_backmarginltx WHERE estado=1")
        For Each fila As DataRow In dtcontratos.Rows
            cmbcontrato.Items.Add(fila.Item("idcontrato"))
        Next

        dtaño = conexion.llenarDataTable(" SELECT valor FROM parametros_año")
        For Each fila3 As DataRow In dtaño.Rows
            cmbañofinal.Items.Add(fila3.Item("valor"))
            cmbañoinicial.Items.Add(fila3.Item("valor"))
        Next
        idespacio = conexion.EjecutarEscalar("SELECT idformacobro FROM formacobro_bmltx WHERE Nombre_formacobro='Espacio'")
        idventas = conexion.EjecutarEscalar("SELECT idformacobro FROM formacobro_bmltx WHERE Nombre_formacobro='Ventas'")
        idingresos = conexion.EjecutarEscalar("SELECT idformacobro FROM formacobro_bmltx WHERE Nombre_formacobro='Ingreso'")
        idmontofijo = conexion.EjecutarEscalar("SELECT idformacobro FROM formacobro_bmltx WHERE Nombre_formacobro='Monto Fijo'")
        idMonedaD = conexion.EjecutarEscalar("SELECT idmoneda FROM tipomoneda_bmltx WHERE tipomoneda='Dolares'")
        idmonedaQ = conexion.EjecutarEscalar("SELECT idmoneda FROM tipomoneda_bmltx WHERE tipomoneda='Quetzales'")

        '=========================================================================================================================================================
        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
        CargarConexionProductos()
        'dtproveedores = conexion.llenarDataTable("SELECT Nombre FROM proveedores")
        dtproveedores = conexionpmm.llenarDataTable("select nombre_proveedor as ""Nombre"" FROM unipmm.unisuperproveedor order by nombre_proveedor")
        For Each fila1 As DataRow In dtproveedores.Rows
            cmbproveedores.Items.Add(fila1.Item("Nombre"))
        Next
        'BUSCAMOS LOS ID DE LAS FORMAS DE COBRO PARA LOS CONTRATOS

    End Sub


    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        ContContratos = 0
        CargarConexionContratos()
        ds1tienda.Clear()
        ds1tienda.Reset()

        'VALIDAMOS LOS DATOS ANTES DE INGRESARLOS A LA CONSULTA
        If ckbcontrato.CheckState = CheckState.Unchecked And chkproveedor.CheckState = CheckState.Unchecked Then
            MessageBox.Show("Tiene que seleccionar uno de los dos filtros", "Error en Filtros", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf ckbcontrato.CheckState = CheckState.Checked And chkproveedor.CheckState = CheckState.Checked Then
            MessageBox.Show("No pueden seleccionar los dos filtros.", "Error en Filtros", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If cmbañoinicial.Text.Length = 0 And cmbañofinal.Text.Length = 0 Then
            MessageBox.Show("Tiene que seleccionar un año inicial y final para poder hacer el reporte", "Error en el año", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf cmbañoinicial.Text.Length = 0 Then
            MessageBox.Show("Tiene que seleccionar un año inicial para pocer hacer el contrato", "Error en el año Inicial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf cmbañofinal.Text.Length = 0 Then
            MessageBox.Show("Tiene que seleccionar un año final para pocer hacer el contrato", "Error en el año Inicial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If


        If cmbMesfinal.Text.Length = 0 And cmbMesinicial.Text.Length = 0 Then
            MessageBox.Show("Tiene que seleccionar un Mes inicial y final para poder hacer el reporte", "Error en Mes", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf cmbMesinicial.Text.Length = 0 Then
            MessageBox.Show("Tiene que seleccionar un Mes inicial para poder hacer el contrato", "Error en el Mes Inicial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf cmbMesfinal.Text.Length = 0 Then
            MessageBox.Show("Tiene que seleccionar un Mes final para poder hacer el contrato", "Error en el Mes Inicial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub


        End If



        ' BSUCAMOS LOS ID DE LOS MESES A TRABAJAR
        numeromeses = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & cmbMesinicial.Text & "'")
        numeromeses1 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & cmbMesfinal.Text & "'")
        id_año = conexion.EjecutarEscalar("SELECT id FROM parametros_año WHERE valor=" & cmbañoinicial.Text & "")
        id_año2 = conexion.EjecutarEscalar("SELECT id FROM parametros_año WHERE valor=" & cmbañofinal.Text & "")
        'VALIDAMOS EL AÑO Y EL MES..
        If id_año > id_año2 Then
            MessageBox.Show("El año inicial no puede ser mayor al año final ", "Error en Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf id_año2 < id_año Then
            MessageBox.Show("El año final no puede ser menor al año inicial ", "Error en Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf id_año = id_año2 Then
            If numeromeses1 < numeromeses Then
                MessageBox.Show("El mes Final no puede ser menor al Mes inicial ", "Error en Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        'ASIGNSMOS YA LOS DATOS DESPUES DE VALIDARLOS
        fechainicio = cmbañoinicial.Text & "-" & numeromeses & "-" & "01"
        dia = validacionmesfinal(numeromeses1)
        fechafinal1 = cmbañofinal.Text & "-" & numeromeses1 & "-" & "31"
        fechainicialR = fechainicio
        fechafinalR = fechafinal1
        contratoR = cmbcontrato.Text
        proveedorR = cmbproveedores.Text
        mesinicialR = cmbMesinicial.Text
        mesfinalR = cmbMesfinal.Text

        'SI EL FILTRO DE LOS CONTRATOS ESTA ACTIVADO....



        If ckbcontrato.CheckState = CheckState.Checked And chkconsololidadoC.CheckState = CheckState.Checked Then
            If cmbcontrato.Text <> "Todos los Contratos" Then
                limpiarconsultas()

                'AHORA VAMOS A REALIZAR LA CONSULTA... 
                dgvreportes.Columns("proveedor").Visible = False
                dscontratosanidados = conexion.llenarDataSet("SELECT resumen.idcontrato AS contrato,  " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.`idtipomoneda`=" & idMonedaD & " AND resumen.`idformacobro`=" & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espaciodolares,  " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR)  AS espacioQuetzales,   " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontofijoQuetzales,   " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idMonedaD & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontoDolares,   " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN  resumen.`idformacobro`= " & idventas & " OR resumen.`idformacobro`=" & idingresos & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS variable,procesado, ''as Impresion1  " &
                                                            " FROM resumen_bmltx AS resumen  " &
                                                            " INNER JOIN formacobro_bmltx AS formacobro  " &
                                                            " ON formacobro.idformacobro=resumen.idformacobro " &
                                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                            " ON moneda.idmoneda=resumen.idtipomoneda " &
                                                            " WHERE resumen.idcontrato='" & cmbcontrato.Text & "'  AND resumen.Fechafin BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "'" &
                                                             " GROUP BY resumen.idcontrato")

                'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                dgvreportes.Visible = True
                dgvreportenormal.Visible = False
                verificacionDataset(dscontratosanidados, dgvreportes)

            ElseIf cmbcontrato.Text = "Todos los Contratos" And chkconsololidadoC.CheckState = CheckState.Checked Then
                limpiarconsultas()
                dgvreportes.Columns("proveedor").Visible = False
                dscontratosanidados = conexion.llenarDataSet("SELECT resumen.idcontrato AS contrato,  " &
                                                              " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.`idtipomoneda`=" & idMonedaD & " AND resumen.`idformacobro`=" & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espaciodolares,  " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espacioQuetzales,   " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontofijoQuetzales,   " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idMonedaD & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontoDolares,   " &
                                                            " CAST(IFNULL(FORMAT(SUM(CASE WHEN  resumen.`idformacobro`= " & idventas & " OR resumen.`idformacobro`=" & idingresos & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS variable,procesado,''as Impresion1  " &
                                                             " FROM resumen_bmltx AS resumen " &
                                                             " INNER JOIN formacobro_bmltx AS formacobro  " &
                                                             " ON formacobro.idformacobro=resumen.idformacobro  " &
                                                             " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                             " ON moneda.idmoneda=resumen.idtipomoneda  " &
                                                             " WHERE resumen.Fechafin BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "'" &
                                                             " GROUP BY resumen.idcontrato")
                'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                dgvreportes.Visible = True
                dgvreportenormal.Visible = False
                verificacionDataset(dscontratosanidados, dgvreportes)
            End If

            '=========================================================================================
            'AHORA VERIFICAMOS LOS CONTRATOS CUANDO NO SON CONSOLIDADOS...
        ElseIf ckbcontrato.CheckState = CheckState.Checked Then
            If cmbcontrato.Text <> "Todos los Contratos" Then
                'dscontratosanidados.Clear()
                'dscontratos.Clear()
                limpiarconsultas()
                dgvreportenormal.Columns(1).Visible = True
                dscontratos = conexion.llenarDataSet("SELECT resumen_bmltx.`IdContrato` AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS Proveedor," &
                                                    " parametros_tiempo.`nombre` AS mesinicial, parametros_año.`valor` AS idinicial, tiempo.`nombre` AS mesfinal, aniosfinal.`valor` AS idfinal,  " &
                                                    " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idMonedaD & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidaddolares,  " &
                                                    " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idmonedaQ & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidadquetzales,procesado,'' AS imprimir     " &
                                                    "  FROM resumen_bmltx " &
                                                    " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.idmoneda=resumen_bmltx.`idtipomoneda` " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo`=resumen_bmltx.`idmesinicial`  " &
                                                    " INNER JOIN parametros_tiempo AS tiempo " &
                                                    " ON tiempo.`id_tiempo`=resumen_bmltx.`idmesfinal` " &
                                                    " INNER JOIN parametros_año  " &
                                                    " ON parametros_año.`id`= resumen_bmltx.`idañoinicial`  " &
                                                    " INNER JOIN parametros_año AS aniosfinal  " &
                                                    " ON aniosfinal.`id`=resumen_bmltx.`idañofinal` " &
                                                    " INNER JOIN contratos_backmarginltx  " &
                                                    " ON contratos_backmarginltx.`IdContrato`=resumen_bmltx.`IdContrato`  " &
                                                    " WHERE resumen_bmltx.`IdContrato`= '" & cmbcontrato.Text & "' AND resumen_bmltx.`Fechafin` BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "'" &
                                                    " GROUP BY resumen_bmltx.`idmesfinal`,resumen_bmltx.`idañoinicial`  " &
                                                    " ORDER BY resumen_bmltx.`idmesinicial` ASC,resumen_bmltx.`idañoinicial` ASC")

                'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                dgvreportes.Visible = False
                dgvreportenormal.Visible = True
                verificacionDataset(dscontratos, dgvreportenormal)
                ContContratos = dscontratos.Tables(0).Select("procesado='0'").Count

            ElseIf cmbcontrato.Text = "Todos los Contratos" Then
                ' dscontratos.Clear()
                limpiarconsultas()
                dgvreportenormal.Columns(1).Visible = True
                dscontratos = conexion.llenarDataSet(" SELECT resumen_bmltx.`IdContrato` AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS Proveedor, " &
                                                     " parametros_tiempo.`nombre` AS mesinicial, parametros_año.`valor` AS idinicial, tiempo.`nombre` AS mesfinal, aniosfinal.`valor` AS idfinal,  " &
                                                     " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idMonedaD & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidaddolares,  " &
                                                     " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idmonedaQ & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidadquetzales,procesado,'' AS imprimir     " &
                                                     " FROM resumen_bmltx  " &
                                                     " INNER JOIN tipomoneda_bmltx AS moneda  " &
                                                     " ON moneda.idmoneda=resumen_bmltx.`idtipomoneda` " &
                                                     " INNER JOIN parametros_tiempo  " &
                                                     " ON parametros_tiempo.`id_tiempo`=resumen_bmltx.`idmesinicial`  " &
                                                     " INNER JOIN parametros_tiempo AS tiempo " &
                                                     " ON tiempo.`id_tiempo`=resumen_bmltx.`idmesfinal` " &
                                                     " INNER JOIN parametros_año  " &
                                                     " ON parametros_año.`id`= resumen_bmltx.`idañoinicial`  " &
                                                     " INNER JOIN parametros_año AS aniosfinal " &
                                                     " ON aniosfinal.`id`=resumen_bmltx.`idañofinal` " &
                                                     " INNER JOIN contratos_backmarginltx  " &
                                                     " ON contratos_backmarginltx.`IdContrato`=resumen_bmltx.`IdContrato`  " &
                                                     " WHERE resumen_bmltx.`Fechafin` BETWEEN  '" & fechainicio & "' AND '" & fechafinal1 & "'  " &
                                                     " GROUP BY resumen_bmltx.`idmesinicial`,resumen_bmltx.`idañoinicial`,resumen_bmltx.`IdContrato`  " &
                                                     " ORDER BY resumen_bmltx.`idmesinicial` ASC,resumen_bmltx.`idañoinicial` ASC")

                dgvreportes.Visible = False
                dgvreportenormal.Visible = True
                verificacionDataset(dscontratos, dgvreportenormal)
                ContContratos = dscontratos.Tables(0).Select("procesado='0'").Count
            End If
        End If

        '====================================================================================================
        'AHORA HACEMOS EL REPORTE PARA LOS PROVEEDORES..

        If Trim(cmbproveedores.Text.Length) > 0 Then

            Try
                If cmbproveedores.Text <> "TODOS LOS PROVEEDORES" Then
                    '=========================================================================================================================================================
                    'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                    'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                    'CAMBIAMOS LA CONEXION PARA EL 223
                    CargarConexionProductos()
                    'idproveedor = conexion.EjecutarEscalar("SELECT id FROM proveedores WHERE nombre='" & cmbproveedores.Text & "'")
                    idproveedor = conexionpmm.EjecutarEscalar("SELECT idproveedor FROM unipmm.unisuperproveedor where nombre_proveedor='" & cmbproveedores.Text & "'")
                    '=========================================================================================================================================================
                End If

            Catch ex As Exception
                MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try


            'CAMBIAMOS LA CONEXION PARA LOS CONTRATOS
            CargarConexionContratos()

            'CUANDO ESTA LA CASILLA DE CONSOLOLIDADO SELECCIONADA..
            If chkproveedor.CheckState = CheckState.Checked And chkconsoproveedor.CheckState = CheckState.Checked Then
                'CUANDO NO SON TODOS LOS PROVEEDORES..
                If cmbproveedores.Text <> "TODOS LOS PROVEEDORES" Then
                    limpiarconsultas()
                    'dscontratos.Clear()
                    'dscontratosanidados.Clear()
                    'AHORA VAMOS A REALIZAR LA CONSULTA...
                    dgvreportes.Columns("proveedor").Visible = True
                    dscontratosanidados = conexion.llenarDataSet("SELECT resumen.idcontrato AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor,  " &
                                                                     " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.`idtipomoneda`=" & idMonedaD & " AND resumen.`idformacobro`=" & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espaciodolares,  " &
                                                                     " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espacioQuetzales,   " &
                                                                     " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS  CHAR) AS MontofijoQuetzales,   " &
                                                                     " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idMonedaD & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontoDolares,   " &
                                                                      " CAST(IFNULL(FORMAT(SUM(CASE WHEN  resumen.`idformacobro`= " & idventas & " OR resumen.`idformacobro`=" & idingresos & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS variable,procesado, '' as Impresion1  " &
                                                                    " FROM resumen_bmltx AS resumen  " &
                                                                    " INNER JOIN formacobro_bmltx AS formacobro  " &
                                                                    " ON formacobro.idformacobro=resumen.idformacobro  " &
                                                                    " INNER JOIN tipomoneda_bmltx AS moneda  " &
                                                                    " ON moneda.idmoneda=resumen.idtipomoneda  " &
                                                                    " INNER JOIN contratos_backmarginltx  " &
                                                                    " ON contratos_backmarginltx.`IdProveedor`=resumen.`IdProveedor`  " &
                                                                    " WHERE resumen.`IdProveedor`= '" & idproveedor & "'  AND contratos_backmarginltx.`estado`=1  AND resumen.Fechafin BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "'   " &
                                                                    " GROUP BY resumen.idcontrato")


                    'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                    dgvreportes.Visible = True
                    dgvreportenormal.Visible = False
                    verificacionDataset(dscontratosanidados, dgvreportes)

                ElseIf cmbproveedores.Text = "TODOS LOS PROVEEDORES" Then
                    limpiarconsultas()
                    'dscontratos.Clear()
                    'dscontratosanidados.Clear()
                    dgvreportes.Columns("proveedor").Visible = True
                    dscontratosanidados = conexion.llenarDataSet(" SELECT resumen.`IdContrato` AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor , " &
                                                                    " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.`idtipomoneda`=" & idMonedaD & " AND resumen.`idformacobro`=" & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espaciodolares,  " &
                                                                    " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idespacio & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS espacioQuetzales,   " &
                                                                     " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idmonedaQ & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontofijoQuetzales,   " &
                                                                    " CAST(IFNULL(FORMAT(SUM(CASE WHEN resumen.idtipomoneda=" & idMonedaD & " AND resumen.`idformacobro`= " & idmontofijo & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS MontoDolares,   " &
                                                                    " CAST(IFNULL(FORMAT(SUM(CASE WHEN  resumen.`idformacobro`= " & idventas & " OR resumen.`idformacobro`=" & idingresos & " THEN (resumen.TotalContrato) ELSE 0.00 END),2),0.00) AS CHAR) AS variable,procesado, '' as Impresion1 " &
                                                                    " FROM resumen_bmltx AS resumen" &
                                                                    " INNER JOIN formacobro_bmltx AS formacobro  " &
                                                                    " ON formacobro.idformacobro=resumen.idformacobro  " &
                                                                    " INNER JOIN tipomoneda_bmltx AS moneda  " &
                                                                    " ON moneda.idmoneda=resumen.idtipomoneda  " &
                                                                    " INNER JOIN contratos_backmarginltx " &
                                                                    " ON contratos_backmarginltx.`IdContrato`=resumen.`IdContrato` " &
                                                                    " WHERE  Fechafin BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "' " &
                                                                    " GROUP BY resumen.`IdContrato` ORDER BY resumen.`IdContrato` ASC ")
                    'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                    dgvreportes.Visible = True
                    dgvreportenormal.Visible = False
                    verificacionDataset(dscontratosanidados, dgvreportes)

                End If
                'AHORA REALIZAREMOS LAS CONSULTAS CUANDO NO SON CONSOLIDADOS..

            ElseIf chkproveedor.CheckState = CheckState.Checked Then
                'CUANDO NO SON TODOS LOS PROVEEDORES..
                If cmbproveedores.Text <> "TODOS LOS PROVEEDORES" Then
                    'dscontratosanidados.Clear()
                    'dscontratos.Clear()
                    limpiarconsultas()
                    dgvreportenormal.Columns(1).Visible = True
                    dscontratos = conexion.llenarDataSet("  SELECT resumen_bmltx.`IdContrato` as contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor,  " &
                                                        " parametros_tiempo.`nombre` AS mesinicial, parametros_año.`valor` AS idinicial, tiempo.`nombre` AS mesfinal, aniosfinal.`valor` AS idfinal,  " &
                                                         " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idMonedaD & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidaddolares,  " &
                                                         " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idmonedaQ & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidadquetzales,procesado,'' AS imprimir     " &
                                                         "  FROM resumen_bmltx " &
                                                          " INNER JOIN tipomoneda_bmltx AS moneda  " &
                                                        " ON moneda.idmoneda=resumen_bmltx.`idtipomoneda` " &
                                                         " INNER JOIN parametros_tiempo  " &
                                                         " ON parametros_tiempo.`id_tiempo`=resumen_bmltx.`idmesinicial`  " &
                                                         " INNER JOIN parametros_tiempo AS tiempo " &
                                                         " ON tiempo.`id_tiempo`=resumen_bmltx.`idmesfinal` " &
                                                         " INNER JOIN parametros_año  " &
                                                         " ON parametros_año.`id`= resumen_bmltx.`idañoinicial`  " &
                                                         " INNER JOIN parametros_año AS aniosfinal " &
                                                         " ON aniosfinal.`id`=resumen_bmltx.`idañofinal` " &
                                                         "  INNER JOIN contratos_backmarginltx " &
                                                         "  ON contratos_backmarginltx.`IdContrato`=resumen_bmltx.`IdContrato` " &
                                                         "  WHERE  resumen_bmltx.`IdProveedor`='" & idproveedor & "' AND resumen_bmltx.`Fechafin` BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "' " &
                                                         "  GROUP BY resumen_bmltx.`idMesinicial`,resumen_bmltx.`idañoinicial`,resumen_bmltx.`IdContrato` " &
                                                         "  ORDER BY resumen_bmltx.`idañoinicial` ASC,resumen_bmltx.`idMesinicial` ASC ")


                    'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                    dgvreportes.Visible = False
                    dgvreportenormal.Visible = True
                    verificacionDataset(dscontratos, dgvreportenormal)
                    ContContratos = dscontratos.Tables(0).Select("procesado='0'").Count

                ElseIf cmbproveedores.Text = "TODOS LOS PROVEEDORES" Then
                    'dscontratosanidados.Clear()
                    'dscontratos.Clear()
                    limpiarconsultas()
                    dgvreportenormal.Columns(1).Visible = True
                    dscontratos = conexion.llenarDataSet("  SELECT resumen_bmltx.`IdContrato` as contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor, " &
                                                         " parametros_tiempo.`nombre` AS mesinicial, parametros_año.`valor` AS idinicial, tiempo.`nombre` AS mesfinal, aniosfinal.`valor` AS idfinal,  " &
                                                         " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idMonedaD & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidaddolares,  " &
                                                        " FORMAT(SUM(CASE WHEN resumen_bmltx.`idtipomoneda`=" & idmonedaQ & " THEN (TotalContrato) ELSE 0.00 END),2)AS cantidadquetzales,procesado,'' AS imprimir     " &
                                                         "  FROM resumen_bmltx " &
                                                        " INNER JOIN tipomoneda_bmltx AS moneda  " &
                                                        " ON moneda.idmoneda=resumen_bmltx.`idtipomoneda` " &
                                                        "  INNER JOIN contratos_backmarginltx " &
                                                         "  ON contratos_backmarginltx.`IdContrato`=resumen_bmltx.`IdContrato` " &
                                                        " INNER JOIN parametros_tiempo  " &
                                                         " ON parametros_tiempo.`id_tiempo`=resumen_bmltx.`idmesinicial`  " &
                                                            " INNER JOIN parametros_tiempo AS tiempo " &
                                                          " ON tiempo.`id_tiempo`=resumen_bmltx.`idmesfinal` " &
                                                          " INNER JOIN parametros_año  " &
                                                         " ON parametros_año.`id`= resumen_bmltx.`idañoinicial`  " &
                                                         " INNER JOIN parametros_año AS aniosfinal " &
                                                         " ON aniosfinal.`id`=resumen_bmltx.`idañofinal` " &
                                                         "  WHERE  resumen_bmltx.`Fechafin` BETWEEN '" & fechainicio & "' AND '" & fechafinal1 & "' " &
                                                         "  GROUP BY resumen_bmltx.`idMesinicial`,resumen_bmltx.`idañoinicial`,resumen_bmltx.`IdContrato` " &
                                                         "  ORDER BY resumen_bmltx.`idañoinicial` ASC,resumen_bmltx.`idMesinicial` ASC ")

                    'AHORA VERIFICAMOS SI EL DATASET REGRESO CON INFORMACION
                    dgvreportes.Visible = False
                    dgvreportenormal.Visible = True
                    verificacionDataset(dscontratos, dgvreportenormal)
                    ContContratos = dscontratos.Tables(0).Select("procesado='0'").Count
                End If

            End If

        Else
            'MessageBox.Show("No se ha seleccionado un proveedor para hacer el reporte", "Error en reporte", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'Exit Sub
        End If
        lblconteocontratos.Text = ContContratos
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs)
        CargarConexionContratos()
        Me.Close()
        FrmMenuPrincipal.WindowState = FormWindowState.Normal

    End Sub

    Private Sub VerDetalleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerDetalleToolStripMenuItem.Click

        chcontrato = ckbcontrato.CheckState
        chproveedor = chkproveedor.CheckState
        If chcontrato = True And chproveedor = True Then
            MessageBox.Show("Tienen activado los dos filtros, para poder realizar el reporte detallado tiene que utilizar un filtro.", "Error en Filtros", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            lista_tiendas = _lista_id_sucursales



            Frmdetalleespacio.ShowDialog()
        End If

    End Sub




#Region "FUNCIONES "
    Private Sub limpiarconsultas()
        consultacompras = 0.0
        consultaespaciosQ = 0.0
        consultamontofijoQ = 0.0
        consultaespaciosD = 0.0
        consultamontofijoD = 0.0
        consultaventas = 0.0
        dscontratos.Clear()
        dscontratos.Reset()
        dscontratosanidados.Clear()
        dscontratosanidados.Reset()
    End Sub

    Public Function validacionmesfinal(ByVal mes As Integer)
        Dim dia As Integer

        If mes = 1 Or mes = 3 Or mes = 5 Or mes = 7 Or mes = 8 Or mes = 10 Or mes = 12 Then
            dia = 31
        ElseIf mes = 4 Or mes = 6 Or mes = 9 Or mes = 11 Then
            dia = 30
        Else
            dia = 28
        End If
        Return dia
    End Function
    Public Sub verificacionDataset(ByVal dataset As DataSet, ByVal dgv As DataGridView)

        If dataset.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("El contrato que ingreso no tiene cobros en el rango de mes que coloco", "No encontro cobros", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dataset.Clear()
            dataset.Clear()
            Exit Sub
        Else
            dgv.DataSource = dataset.Tables(0)
        End If
    End Sub
    Public Sub envioDatos(ByVal mes1 As TextBox, ByVal mes2 As TextBox, ByVal proveedor As TextBox, ByVal contrato As TextBox)
        mes1.Text = cmbMesinicial.Text
        mes2.Text = cmbMesfinal.Text
        proveedor.Text = cmbproveedores.Text
        contrato.Text = cmbcontrato.Text

    End Sub
    Public Sub generarreporte(ByVal contrato As String, ByVal mesincial As String, ByVal añoinicial As String,
                              ByVal mesfinal As String, ByVal añofinal As String, ByVal tasacambio As Decimal)

        'CARGAMOS LA CONEXION DE LOS CONTRATOS...
        CargarConexionContratos()

        Dim dsmontofijo As New DataSet
        Dim totalespacioD As Decimal = 0.0
        Dim totalespacioQ As Decimal
        Dim totalmontoD As Decimal
        Dim totalmontoQ As Decimal
        Dim totalmontoCon As Decimal
        Dim totalespacioCon As Decimal
        Dim totaldolares As Decimal
        Dim totalquetzales As Decimal
        Dim dsventas As New DataSet
        Dim dscompras As New DataSet
        Dim dstraslados As New DataSet
        Dim dscompras_traslados As New DataSet
        Dim nuevafila As DataRow
        Dim dsespacios As New DataSet
        Dim totalventas As Decimal
        Dim totalcompras As Decimal
        Dim totaltraslados As Decimal
        Dim totalconvertido As Decimal
        Dim totalcontrato As Decimal
        Dim fechaI, fechaF As String
        Dim fecha As Date
        Dim fechaenviar, hora, fechaactualizada As String
        Dim nit As String
        Dim mes1, mes2 As Integer
        Dim dia1, dia2 As Integer
        Dim añoIngresar, añoingresar2 As Integer
        Dim proveedor As String
        Dim direccion As String
        Dim nombreproveedor As String
        Dim filtro_pagocompra As String
        Dim filtro_pagotralados As String
        Dim filtro_pagoespacios As String
        Dim filtro_pagoventa As String
        Dim filtro_pagomonto As String
        'para dibujar el grid de los totales

        'DIBUJAMOS EL GRID

        totalespacioD = 0.0
        totalespacioQ = 0.0
        totalmontoD = 0.0
        totalmontoQ = 0.0
        totalmontoCon = 0.0
        totalespacioCon = 0.0
        totaldolares = 0.0
        totalquetzales = 0.0
        totalventas = 0.0
        totalcompras = 0.0
        totaltraslados = 0.0
        totalconvertido = 0.0
        totalcontrato = 0.0
        fecha = Now
        fechaenviar = Format(fecha, "yyyy-MM-dd")
        hora = Format(fecha, "hh:mm:ss")
        fechaactualizada = fechaenviar & "  " & hora
        mes1 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & mesincial & "'")
        mes2 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & mesfinal & "'")
        añoIngresar = conexion.EjecutarEscalar(" SELECT id FROM parametros_año WHERE valor= " & añoinicial & "")
        añoingresar2 = conexion.EjecutarEscalar(" SELECT id FROM parametros_año WHERE valor= " & añofinal & "")
        dia1 = validacionmesfinal(mes2)
        fechaI = añoinicial & "-" & mes1 & "-" & "01"
        fechaF = añofinal & "-" & mes2 & "-" & dia1


        'AHORA BUSCAMOS LA INFORMACION DE LOS PROVEEDORES..

        proveedor = conexion.EjecutarEscalar("SELECT nombreproveedorFAC FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        nombreproveedor = conexion.EjecutarEscalar("SELECT nombreproveedor FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        nit = conexion.EjecutarEscalar("SELECT CAST(IFNULL(nitProveedorFac,0) AS CHAR) FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        direccion = conexion.EjecutarEscalar("SELECT CAST(IFNULL(direccion,0) AS CHAR)  FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")

        'SE AGREGA PARA FILTRO DE TIENDAS OCT2017 JBOCH
        If txtLista_Tiendas.Text.Length > 0 Then
            filtro_pagocompra = " AND pagocompras.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagotralados = " AND traslados.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagoespacios = " AND pagoespacio.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagoventa = " AND pagoventa.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagomonto = " AND pagomonto .Idtienda IN (" & _lista_id_sucursales & ")"
        Else

            filtro_pagocompra = String.Empty
            filtro_pagotralados = String.Empty
            filtro_pagoespacios = String.Empty
            filtro_pagoventa = String.Empty
            filtro_pagomonto = String.Empty
        End If

        dsespacios = conexion.llenarDataSet("  Select espacio.`Nombre` As nombreespacio,pagoespacio.`cantidadespacio` As cantidadrentada,  " &
                                            " FORMAT(pagoespacio.`costoespacio`,2) As costoespacio, " &
                                            " tienda.`Nombre` As Tienda, " &
                                            " CONCAT('$. ',FORMAT(SUM(CASE WHEN  pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,  " &
                                            " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales   " &
                                            " FROM tipoespacio_bmltx AS espacio  " &
                                            " INNER JOIN parametros_pagoespacio_bmltx AS pagoespacio  " &
                                            " ON espacio.`Idtipoespacio`=pagoespacio.`Idespacio`  " &
                                            " INNER JOIN parametros_tiempo AS mes  " &
                                            " ON mes.`id_tiempo`=pagoespacio.`idmes` " &
                                            " INNER JOIN parametros_año AS año  " &
                                            "  ON año.`id`=pagoespacio.`idaño`  " &
                                            " INNER JOIN sucursales_bmltx AS tienda  " &
                                            " ON tienda.`Id`=pagoespacio.`Idtienda`  " &
                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                            " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                            " WHERE  pagoespacio.`Idcontrato`='" & contrato & "' AND pagoespacio.`fechafinal` BETWEEN '" & fechaI & "' AND '" & fechaF & "'  " &
                                            " " & filtro_pagoespacios & "" &
                                            " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idmes`,pagoespacio.Idtienda " &
                                            " HAVING totaldolares <> '$. 0.00' " &
                                            " OR totalquetzales <> 'Q. 0.00' " &
                                            " ORDER BY pagoespacio.`Idtienda`,pagoespacio.`idmes` ")

        dsventas = conexion.llenarDataSet(" SELECT pagoventa.sku AS sku, " &
                                            " tienda.nombre AS tienda,tiempo.nombre AS mes,pagoventa.`Descripcion` AS descripcion, " &
                                            " CONCAT('Q. ',FORMAT((Ventas),2))AS total " &
                                            " FROM parametros_pagoventa_bmltx AS pagoventa " &
                                            " INNER JOIN parametros_año AS año " &
                                            " ON año.id= pagoventa.idaño " &
                                            " INNER JOIN parametros_tiempo AS tiempo " &
                                            " ON tiempo.id_tiempo= pagoventa.idmes " &
                                            " INNER JOIN sucursales_bmltx AS tienda " &
                                            " ON tienda.id=pagoventa.idtienda " &
                                            " WHERE pagoventa.idcontrato='" & contrato & "' AND pagoventa.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                            " " & filtro_pagoventa & "" &
                                            " GROUP BY pagoventa.`idMes`,pagoventa.`idaño`,pagoventa.`Sku` ,pagoventa.`Descripcion`,pagoventa.idtienda " &
                                            " HAVING total <> 'Q. 0.00' " &
                                            " ORDER BY pagoventa.`idMes`ASC ,pagoventa.`idaño` ASC")

        dscompras = conexion.llenarDataSet(" SELECT  pagocompras.sku AS sku," &
                                           " tienda.nombre As tienda," &
                                           " tiempo.nombre AS mes," &
                                           " pagocompras.Unidadcomprada AS Unidades, " &
                                           " pagocompras.`porcentaje` AS Cobro,   " &
                                           " pagocompras.`Descripcion` AS descripcion, " &
                                           " CONCAT('Q. ',FORMAT((compras),2))AS total " &
                                           " From parametros_pagocompras_bmltx As pagocompras " &
                                           " INNER Join parametros_año AS año " &
                                           " On año.id= pagocompras.idaño " &
                                           " INNER Join parametros_tiempo AS tiempo " &
                                           " On tiempo.id_tiempo= pagocompras.idmes " &
                                           " INNER Join sucursales_bmltx AS tienda " &
                                           " On tienda.id=pagocompras.idtienda " &
                                           " WHERE pagocompras.idcontrato ='" & contrato & "' " &
                                           " And pagocompras.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                           " " & filtro_pagocompra & "" &
                                           " GROUP BY pagocompras.idmes, pagocompras.`idaño`, pagocompras.`Sku` , pagocompras.`Descripcion`,pagocompras.idtienda" &
                                           " HAVING total <> 'Q. 0.00' " &
                                           " ORDER BY ABS(pagocompras.`Sku`), pagocompras.`Idtienda`,  pagocompras.`idmes`")

        dstraslados = conexion.llenarDataSet(" SELECT  traslados.sku AS sku, " &
                                             "tienda.nombre As tienda, " &
                                             "tiempo.nombre AS mes, " &
                                             "traslados.`UnidadTrasladada` AS Unidades, " &
                                             "'' AS Cobro,  " &
                                             "traslados.`Descripcion` AS descripcion, " &
                                             "CONCAT('Q. ',FORMAT((`Traslado`),2))AS total " &
                                             "FROM `parametros_pagotraslados_bmltx` As traslados  " &
                                             "INNER JOIN parametros_año AS año  ON año.id= traslados.idaño " &
                                             "INNER Join parametros_tiempo AS tiempo  ON tiempo.id_tiempo= traslados.idmes  " &
                                             "INNER JOIN sucursales_bmltx AS tienda  ON tienda.id=traslados.idtienda  " &
                                             "WHERE traslados.idcontrato ='" & contrato & "'  " &
                                             "And traslados.fechafinal " &
                                             "BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                             "GROUP BY traslados.idmes, traslados.`idaño`, traslados.`Sku` , traslados.`Descripcion`, traslados.idtienda " &
                                             " " & filtro_pagotralados & "" &
                                             "HAVING total <> 'Q. 0.00'  " &
                                             "ORDER BY ABS(traslados.`Sku`), traslados.`Idtienda`, traslados.`idmes`")



        dscompras_traslados = dscompras.Clone


        If dscompras.Tables(0).Rows.Count > 0 Then
            Dim busqueda() As DataRow
            Dim cobrobusqueda As String
            Dim unidadestrasladas As Decimal
            Dim unidadescompradas As Decimal

            Dim cobrofila As String
            Dim total_cobro As Decimal

            ' Dim saldo_final As Decimal
            Dim saldo_parcial As Decimal
            Dim saldo_unidades As Decimal
            Dim control As Integer = 0
            Dim control_tienda As Integer = 0
            '   Dim control As Integer
            Dim porcentaje_compras As Decimal
            Dim tienda_compra As String
            Dim ultima_tienda_compra As String
            Dim tienda_traslado As String
            Dim fila_sku As String
            Dim fila_tienda As String
            Dim total_lineas As Integer


            For Each fila In dscompras.Tables(0).Rows

                unidadescompradas = 0.00
                unidadestrasladas = 0.00
                saldo_unidades = 0.00
                cobrobusqueda = 0.00
                cobrofila = 0.00
                porcentaje_compras = 0.0
                total_cobro = 0.0

                fila_sku = String.Empty
                fila_tienda = String.Empty

                tienda_compra = String.Empty
                tienda_traslado = String.Empty



                fila_sku = fila("sku")
                fila_tienda = fila("tienda")


                busqueda = dstraslados.Tables(0).Select("sku='" & fila("sku") & "' AND tienda='" & fila("tienda") & "' ", String.Empty)

                If dscompras_traslados.Tables(0).Rows.Count > 0 Then
                    total_lineas = dscompras_traslados.Tables(0).Rows.Count - 1
                    ultima_tienda_compra = dscompras_traslados.Tables(0).Rows(total_lineas)("tienda")
                End If

                'porcentaje_compras = dscompras.Tables(0).Rows(0)("cobro") / 100
                porcentaje_compras = fila("cobro") / 100
                'lo encontro


                If busqueda.Length > 0 Then
                    tienda_compra = fila("tienda")
                    tienda_traslado = busqueda(0)(1)

                    If ultima_tienda_compra <> tienda_compra Then
                        control_tienda = 0
                    End If

                    If tienda_compra = tienda_traslado Then

                        control_tienda += 1

                        unidadescompradas = fila("unidades")
                        unidadestrasladas = busqueda(0)("unidades")
                        saldo_unidades = unidadescompradas - unidadestrasladas
                        cobrobusqueda = busqueda(0)("total")
                        cobrofila = fila("total")
                        cobrobusqueda = cobrobusqueda.Remove(0, 3)
                        cobrofila = cobrofila.Remove(0, 3)
                        'saldo_parcial = (cobrobusqueda * porcentaje_compras) * unidadestrasladas
                        saldo_parcial = (cobrobusqueda * porcentaje_compras)
                        total_cobro = cobrofila - saldo_parcial
                        total_cobro = Format(total_cobro, “##,##0.000”)

                        If total_cobro > 0 And busqueda.Length > 0 And control_tienda = 1 Then
                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = busqueda(0)("sku")
                            nuevafila("tienda") = busqueda(0)("tienda")
                            nuevafila("mes") = busqueda(0)("mes")
                            nuevafila("Unidades") = saldo_unidades
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = busqueda(0)("descripcion")
                            nuevafila("total") = "Q. " & total_cobro

                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)
                        ElseIf total_cobro > 0 And busqueda.Length > 0 And tienda_compra = tienda_traslado And control_tienda > 1 Then

                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = fila("sku")
                            nuevafila("tienda") = fila("tienda")
                            nuevafila("mes") = fila("mes")
                            nuevafila("Unidades") = fila("unidades")
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = fila("descripcion")
                            nuevafila("total") = fila("total")
                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)

                        ElseIf total_cobro <= 0 And busqueda.Length > 0 Then

                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = fila("sku")
                            nuevafila("tienda") = fila("tienda")
                            nuevafila("mes") = fila("mes")
                            nuevafila("Unidades") = fila("unidades")
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = fila("descripcion")
                            nuevafila("total") = fila("total")
                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)


                        End If


                    End If
                Else
                    control = 0
                    nuevafila = dscompras_traslados.Tables(0).NewRow
                    nuevafila("sku") = fila("sku")
                    nuevafila("tienda") = fila("tienda")
                    nuevafila("mes") = fila("mes")
                    nuevafila("Unidades") = fila("unidades")
                    nuevafila("Cobro") = fila("cobro")
                    nuevafila("descripcion") = fila("descripcion")
                    nuevafila("total") = fila("total")
                    dscompras_traslados.Tables(0).Rows.Add(nuevafila)
                    control_tienda = 0
                End If
                'saldo_parcial = dscompras_traslados.Tables(0).Rows(0)("total").Remove(0, 4)
                'totalcompras &= saldo_parcial
            Next
            Dim cont As Integer
            cont = dscompras_traslados.Tables(0).Rows.Count()
            For Each fila In dscompras_traslados.Tables(0).Rows
                saldo_parcial = 0.00
                saldo_parcial = fila("total").Remove(0, 3)
                totalcompras += saldo_parcial

            Next
            totalcompras = Format(totalcompras, “##,##0.00”)
        End If

        dsmontofijo = conexion.llenarDataSet("Select  pagomonto.sku As sku, " &
                                            " tienda.`Nombre` As tienda, " &
                                            " mes.nombre As mes, " &
                                            " pagomonto.`Descripcion`, " &
                                            " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares, " &
                                            " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales   " &
                                            " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                            " INNER JOIN parametros_tiempo AS mes " &
                                            " ON mes.id_tiempo=pagomonto.idmes " &
                                            " INNER JOIN parametros_año AS año " &
                                            " ON año.id=pagomonto.idaño " &
                                            " INNER JOIN sucursales_bmltx AS tienda " &
                                            " ON tienda.`Id`=pagomonto.`idTienda` " &
                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                            " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                            " WHERE pagomonto.idcontrato='" & contrato & "' AND pagomonto.fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                            " " & filtro_pagomonto & "" &
                                            " GROUP BY pagomonto.`idTienda`,pagomonto.`idMes`, pagomonto.`Sku`,pagomonto.idTienda, pagomonto.`Descripcion` " &
                                            " HAVING totaldolares <> '$. 0.00' " &
                                            " OR totalquetzales <> 'Q. 0.00' " &
                                            " ORDER BY pagomonto.`idTienda`")

        dgvespacios.DataSource = dsespacios.Tables(0)
        dgvproductosC.DataSource = dscompras_traslados.Tables(0)
        dgvproductosM.DataSource = dsmontofijo.Tables(0)
        dgvproductosV.DataSource = dsventas.Tables(0)
        totalventas = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Ventas),2),0.00) AS CHAR)  " &
                                                "  FROM parametros_pagoventa_bmltx " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")

        ''totalcompras = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(compras),2),0.00) AS CHAR) " &
        ''                                        " FROM parametros_pagocompras_bmltx " &
        ''                                        " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")


        totaltraslados = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(traslado),2),0.00) AS CHAR) " &
                                                " FROM parametros_pagotraslados_bmltx " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")

        totalespacioQ = conexion.EjecutarEscalar(" SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total  " &
                                                " FROM parametros_pagoespacio_bmltx  " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagoespacio_bmltx.`idtipomoneda` " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagoespacio_bmltx.`idtipomoneda`=" & idmonedaQ & " ")

        totalespacioD = conexion.EjecutarEscalar(" SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total  " &
                                                " FROM parametros_pagoespacio_bmltx  " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagoespacio_bmltx.`idtipomoneda` " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND  parametros_pagoespacio_bmltx.`idtipomoneda`=" & idMonedaD & " ")

        totalmontoD = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total " &
                                                "  FROM parametros_pagomontofijo_bmltx " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagomontofijo_bmltx.`idtipomoneda` " &
                                                " WHERE  idcontrato='" & contrato & "' AND fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagomontofijo_bmltx.`idtipomoneda`=" & idMonedaD & " ")

        totalmontoQ = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total " &
                                                "  FROM parametros_pagomontofijo_bmltx " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagomontofijo_bmltx.`idtipomoneda` " &
                                                " WHERE  idcontrato='" & contrato & "' AND fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagomontofijo_bmltx.`idtipomoneda`=" & idmonedaQ & "")

        totalmontoCon = Format(totalmontoD * tasacambio, " ##,##0.00")
        totalespacioCon = Format(totalespacioD * tasacambio, "##,##0.00")
        totaldolares = Format(totalmontoD + totalespacioD, "##,##0.00")
        totalquetzales = Format(totalespacioQ + totalcompras + totalventas + totalmontoQ, "##,##0.00")
        totalconvertido = Format(totalmontoCon + totalespacioCon, "##,##0.00")
        totalcontrato = Format(totalconvertido + totalquetzales, "##,##0.00")


        If totaldolares = 0 Then
            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()


            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "TotalenQuetzales"
            col3.HeaderText = "Total en Quetzales"
            dgvTotales.Columns.Add(col3)

            If totalespacioQ > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "Q. " & totalespacioQ)
            End If
            If totalmontoQ > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "Q. " & totalmontoQ)
            End If
            If totalcompras > 0 Then
                dgvTotales.Rows.Add("Total de Compras:", "Q. " & totalcompras)
            End If
            If totalventas > 0 Then
                dgvTotales.Rows.Add("Total de Ventas:", "Q. " & totalventas)
            End If
            dgvTotales.Rows.Add("Total: ", "Q. " & totalquetzales)
            llenartablaquetzales()

        ElseIf totalquetzales = 0 Then

            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()

            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "TotalenDolares"
            col2.HeaderText = "Total en Dolares"
            dgvTotales.Columns.Add(col2)


            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            If totalespacioQ > 0 Or totalespacioD > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "$. " & totalespacioD)
            End If
            If totalmontoQ > 0 And totalmontoD > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "$. " & totalmontoD)
            End If
            dgvTotales.Rows.Add("Total: ", "$. " & totaldolares)
            llenartabladolares()

        Else

            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()

            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "TotalenQuetzales"
            col3.HeaderText = "Total en Quetzales"
            dgvTotales.Columns.Add(col3)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "TotalenDolares"
            col2.HeaderText = "Total en Dolares"
            dgvTotales.Columns.Add(col2)

            If totalespacioQ > 0 Or totalespacioD > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "Q. " & totalespacioQ, "$. " & totalespacioD)
            End If
            If totalmontoQ > 0 Or totalmontoD > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "Q. " & totalmontoQ, "$. " & totalmontoD)
            End If
            If totalcompras > 0 Then
                dgvTotales.Rows.Add("Total de Compras:", "Q. " & totalcompras, "$. 0.0")
            End If
            If totalventas > 0 Then
                dgvTotales.Rows.Add("Total de Ventas:", "Q. " & totalventas, "$ 0.00")
            End If
            dgvTotales.Rows.Add("Total: ", "Q. " & totalquetzales, "$. " & totaldolares)

            llenartabla()
        End If


        reporte.creararchivo("REPORTE DE PAGO DEL CONTRATO No." & contrato & " DEL MES DE " & mesincial & " al MES DE " & mesfinal & "", nit, mesincial & "  AL MES  DE  " & mesfinal, proveedor, dgvespacios,
                              dgvproductosC, dgvproductosV, dgvproductosM, nombreproveedor, direccion, tasacambio, totalespacioD, totalespacioQ,
                               totalcompras, totalventas, totalmontoD, totalmontoQ, totalquetzales, totaldolares, totalconvertido, totalcontrato, contrato, dgvTotales)

        actualizacion(contrato, fechaactualizada, mes1, mes2, añoIngresar, añoingresar2)


        Dim filaencontrada1() As DataRow = dscontratos.Tables(0).Select("contrato='" & contrato & "' and mesinicial='" & mesincial & "' and idinicial='" & añoinicial & "'  and mesfinal='" & mesfinal & "' and idfinal='" & añofinal & "'  ")
        If filaencontrada1.Length <> 0 Then
            filaencontrada1(0)("procesado") = 1
        End If
        ContContratos = ContContratos - 1
        lblconteocontratos.Text = ContContratos
        dgvreportenormal.Refresh()


    End Sub

    'SE AGREGA OCT2017 JBOCH
    Public Sub generarCSV(ByVal contrato As String, ByVal mesincial As String, ByVal añoinicial As String,
                              ByVal mesfinal As String, ByVal añofinal As String, ByVal tasacambio As Decimal)

        'CARGAMOS LA CONEXION DE LOS CONTRATOS...
        CargarConexionContratos()

        Dim dsmontofijo As New DataSet
        Dim totalespacioD As Decimal = 0.0
        Dim totalespacioQ As Decimal
        Dim totalmontoD As Decimal
        Dim totalmontoQ As Decimal
        Dim totalmontoCon As Decimal
        Dim totalespacioCon As Decimal
        Dim totaldolares As Decimal
        Dim totalquetzales As Decimal
        Dim dsventas As New DataSet
        Dim dscompras As New DataSet
        Dim dstraslados As New DataSet
        Dim dscompras_traslados As New DataSet
        Dim nuevafila As DataRow
        Dim dsespacios As New DataSet
        Dim totalventas As Decimal
        Dim totalcompras As Decimal
        Dim totalconvertido As Decimal
        Dim totalcontrato As Decimal
        Dim fechaI, fechaF As String
        Dim fecha As Date
        Dim fechaenviar, hora, fechaactualizada As String
        Dim nit As String
        Dim mes1, mes2 As Integer
        Dim dia1, dia2 As Integer
        Dim añoIngresar, añoingresar2 As Integer
        Dim proveedor As String
        Dim direccion As String
        Dim nombreproveedor As String
        Dim filtro_pagocompra As String
        Dim filtro_pagotralados As String
        Dim filtro_pagoespacios As String
        Dim filtro_pagoventa As String
        Dim filtro_pagomonto As String
        'para dibujar el grid de los totales

        'DIBUJAMOS EL GRID

        totalespacioD = 0.0
        totalespacioQ = 0.0
        totalmontoD = 0.0
        totalmontoQ = 0.0
        totalmontoCon = 0.0
        totalespacioCon = 0.0
        totaldolares = 0.0
        totalquetzales = 0.0
        totalventas = 0.0
        totalcompras = 0.0
        totalconvertido = 0.0
        totalcontrato = 0.0
        fecha = Now
        fechaenviar = Format(fecha, "yyyy-MM-dd")
        hora = Format(fecha, "hh:mm:ss")
        fechaactualizada = fechaenviar & "  " & hora
        mes1 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & mesincial & "'")
        mes2 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & mesfinal & "'")
        añoIngresar = conexion.EjecutarEscalar(" SELECT id FROM parametros_año WHERE valor= " & añoinicial & "")
        añoingresar2 = conexion.EjecutarEscalar(" SELECT id FROM parametros_año WHERE valor= " & añofinal & "")
        dia1 = validacionmesfinal(mes2)
        fechaI = añoinicial & "-" & mes1 & "-" & "01"
        fechaF = añofinal & "-" & mes2 & "-" & dia1

        'SE AGREGA PARA FILTRO DE TIENDAS nov2017 JBOCH
        If txtLista_Tiendas.Text.Length > 0 Then
            filtro_pagocompra = " AND pagocompras.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagotralados = " AND traslados.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagoespacios = " AND pagoespacio.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagoventa = " AND pagoventa.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagomonto = " AND pagomonto .Idtienda IN (" & _lista_id_sucursales & ")"
        Else

            filtro_pagocompra = String.Empty
            filtro_pagotralados = String.Empty
            filtro_pagoespacios = String.Empty
            filtro_pagoventa = String.Empty
            filtro_pagomonto = String.Empty
        End If
        'AHORA BUSCAMOS LA INFORMACION DE LOS PROVEEDORES..

        proveedor = conexion.EjecutarEscalar("SELECT nombreproveedorFAC FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        nombreproveedor = conexion.EjecutarEscalar("SELECT nombreproveedor FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        nit = conexion.EjecutarEscalar("SELECT CAST(IFNULL(nitProveedorFac,0) AS CHAR) FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        direccion = conexion.EjecutarEscalar("SELECT CAST(IFNULL(direccion,0) AS CHAR)  FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")

        dsespacios = conexion.llenarDataSet("  SELECT espacio.`Nombre` AS nombreespacio,pagoespacio.`cantidadespacio` AS cantidadrentada,  " &
                                            " FORMAT(pagoespacio.`costoespacio`,2) AS costoespacio, " &
                                            " tienda.`Nombre` AS Tienda, " &
                                            " CONCAT('$. ',FORMAT(SUM(CASE WHEN  pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,  " &
                                            " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales   " &
                                            " FROM tipoespacio_bmltx AS espacio  " &
                                            " INNER JOIN parametros_pagoespacio_bmltx AS pagoespacio  " &
                                            " ON espacio.`Idtipoespacio`=pagoespacio.`Idespacio`  " &
                                            " INNER JOIN parametros_tiempo AS mes  " &
                                            " ON mes.`id_tiempo`=pagoespacio.`idmes` " &
                                            " INNER JOIN parametros_año AS año  " &
                                            "  ON año.`id`=pagoespacio.`idaño`  " &
                                            " INNER JOIN sucursales_bmltx AS tienda  " &
                                            " ON tienda.`Id`=pagoespacio.`Idtienda`  " &
                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                            " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                            " WHERE  pagoespacio.`Idcontrato`='" & contrato & "' AND pagoespacio.`fechafinal` BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                              " " & filtro_pagoespacios & "" &
                                            " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idmes`,pagoespacio.Idtienda " &
                                            " HAVING totaldolares <> '$. 0.00' " &
                                            " OR totalquetzales <> 'Q. 0.00' " &
                                            " ORDER BY pagoespacio.`Idtienda`,pagoespacio.`idmes` ")

        dsventas = conexion.llenarDataSet(" SELECT pagoventa.sku AS sku, " &
                                            " tienda.nombre AS tienda,tiempo.nombre AS mes,pagoventa.`Descripcion` AS descripcion, " &
                                            " CONCAT('Q. ',FORMAT((Ventas),2))AS total " &
                                            " FROM parametros_pagoventa_bmltx AS pagoventa " &
                                            " INNER JOIN parametros_año AS año " &
                                            " ON año.id= pagoventa.idaño " &
                                            " INNER JOIN parametros_tiempo AS tiempo " &
                                            " ON tiempo.id_tiempo= pagoventa.idmes " &
                                            " INNER JOIN sucursales_bmltx AS tienda " &
                                            " ON tienda.id=pagoventa.idtienda " &
                                            " WHERE pagoventa.idcontrato='" & contrato & "' AND pagoventa.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                             " " & filtro_pagoventa & "" &
                                            " GROUP BY pagoventa.`idMes`,pagoventa.`idaño`,pagoventa.`Sku` ,pagoventa.`Descripcion`,pagoventa.idtienda " &
                                            " HAVING total <> 'Q. 0.00' " &
                                            " ORDER BY pagoventa.`idMes`ASC ,pagoventa.`idaño` ASC")
        'dscompras = conexion.llenarDataSet(" SELECT  pagocompras.sku AS sku," &
        '                                   " tienda.nombre As tienda," &
        '                                   " tiempo.nombre AS mes," &
        '                                   " pagocompras.Unidadcomprada AS Unidades, " &
        '                                   " pagocompras.`porcentaje` AS Cobro,   " &
        '                                   " pagocompras.`Descripcion` AS descripcion, " &
        '                                   " CONCAT('Q. ',FORMAT((compras),2))AS total " &
        '                                   " From parametros_pagocompras_bmltx As pagocompras " &
        '                                   " INNER Join parametros_año AS año " &
        '                                   " On año.id= pagocompras.idaño " &
        '                                   " INNER Join parametros_tiempo AS tiempo " &
        '                                   " On tiempo.id_tiempo= pagocompras.idmes " &
        '                                   " INNER Join sucursales_bmltx AS tienda " &
        '                                   " On tienda.id=pagocompras.idtienda " &
        '                                   " WHERE pagocompras.idcontrato ='" & contrato & "' " &
        '                                   " " & filtro_pagocompra & "" &
        '                                   " And pagocompras.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
        '                                   " GROUP BY pagocompras.idmes, pagocompras.`idaño`, pagocompras.`Sku` , pagocompras.`Descripcion`,pagocompras.idtienda" &
        '                                   " HAVING total <> 'Q. 0.00' " &
        '                                   " ORDER BY pagocompras.`Idtienda`, pagocompras.`Sku`, pagocompras.`idmes`")

        dsmontofijo = conexion.llenarDataSet("Select  pagomonto.sku As sku, " &
                                            " tienda.`Nombre` As tienda, " &
                                            " mes.nombre As mes, " &
                                            " pagomonto.`Descripcion`," &
                                            " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares, " &
                                            " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales   " &
                                            " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                            " INNER JOIN parametros_tiempo AS mes " &
                                            " ON mes.id_tiempo=pagomonto.idmes " &
                                            " INNER JOIN parametros_año AS año " &
                                            " ON año.id=pagomonto.idaño " &
                                            " INNER JOIN sucursales_bmltx AS tienda " &
                                            " ON tienda.`Id`=pagomonto.`idTienda` " &
                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                            " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                            " WHERE pagomonto.idcontrato='" & contrato & "' AND pagomonto.fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                            " " & filtro_pagomonto & "" &
                                            " GROUP BY pagomonto.`idTienda`,pagomonto.`idMes`, pagomonto.`Sku`,pagomonto.idTienda, pagomonto.`Descripcion` " &
                                            " HAVING totaldolares <> '$. 0.00' " &
                                            " OR totalquetzales <> 'Q. 0.00' " &
                                            " ORDER BY pagomonto.`idTienda`")


        'dstraslados = conexion.llenarDataSet(" SELECT  traslados.sku AS sku, " &
        '                                     "tienda.nombre As tienda, " &
        '                                     "tiempo.nombre AS mes, " &
        '                                     "traslados.`UnidadTrasladada` AS Unidades, " &
        '                                     "'' AS Cobro,  " &
        '                                     "traslados.`Descripcion` AS descripcion, " &
        '                                     "CONCAT('Q. ',FORMAT((`Traslado`),2))AS total " &
        '                                     "FROM `parametros_pagotraslados_bmltx` As traslados  " &
        '                                     "INNER JOIN parametros_año AS año  ON año.id= traslados.idaño " &
        '                                     "INNER Join parametros_tiempo AS tiempo  ON tiempo.id_tiempo= traslados.idmes  " &
        '                                     "INNER JOIN sucursales_bmltx AS tienda  ON tienda.id=traslados.idtienda  " &
        '                                     "WHERE traslados.idcontrato ='" & contrato & "'  " &
        '                                     "And traslados.fechafinal " &
        '                                     "BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
        '                                     "GROUP BY traslados.idmes, traslados.`idaño`, traslados.`Sku` , traslados.`Descripcion`, traslados.idtienda " &
        '                                     " " & filtro_pagotralados & "" &
        '                                     "HAVING total <> 'Q. 0.00'  " &
        '                                     "ORDER BY traslados.`Idtienda`, ABS(traslados.`Sku`), traslados.`idmes`")


        dscompras = conexion.llenarDataSet(" SELECT  pagocompras.sku AS sku," &
                                           " tienda.nombre As tienda," &
                                           " tiempo.nombre AS mes," &
                                           " pagocompras.Unidadcomprada AS Unidades, " &
                                           " pagocompras.`porcentaje` AS Cobro,   " &
                                           " pagocompras.`Descripcion` AS descripcion, " &
                                           " CONCAT('Q. ',FORMAT((compras),2))AS total " &
                                           " From parametros_pagocompras_bmltx As pagocompras " &
                                           " INNER Join parametros_año AS año " &
                                           " On año.id= pagocompras.idaño " &
                                           " INNER Join parametros_tiempo AS tiempo " &
                                           " On tiempo.id_tiempo= pagocompras.idmes " &
                                           " INNER Join sucursales_bmltx AS tienda " &
                                           " On tienda.id=pagocompras.idtienda " &
                                           " WHERE pagocompras.idcontrato ='" & contrato & "' " &
                                           " And pagocompras.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                           " " & filtro_pagocompra & "" &
                                           " GROUP BY pagocompras.idmes, pagocompras.`idaño`, pagocompras.`Sku` , pagocompras.`Descripcion`,pagocompras.idtienda" &
                                           " HAVING total <> 'Q. 0.00' " &
                                           " ORDER BY ABS(pagocompras.`Sku`), pagocompras.`Idtienda`,  pagocompras.`idmes`")

        dstraslados = conexion.llenarDataSet(" SELECT  traslados.sku AS sku, " &
                                             "tienda.nombre As tienda, " &
                                             "tiempo.nombre AS mes, " &
                                             "traslados.`UnidadTrasladada` AS Unidades, " &
                                             "'' AS Cobro,  " &
                                             "traslados.`Descripcion` AS descripcion, " &
                                             "CONCAT('Q. ',FORMAT((`Traslado`),2))AS total " &
                                             "FROM `parametros_pagotraslados_bmltx` As traslados  " &
                                             "INNER JOIN parametros_año AS año  ON año.id= traslados.idaño " &
                                             "INNER Join parametros_tiempo AS tiempo  ON tiempo.id_tiempo= traslados.idmes  " &
                                             "INNER JOIN sucursales_bmltx AS tienda  ON tienda.id=traslados.idtienda  " &
                                             "WHERE traslados.idcontrato ='" & contrato & "'  " &
                                             "And traslados.fechafinal " &
                                             "BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                             "GROUP BY traslados.idmes, traslados.`idaño`, traslados.`Sku` , traslados.`Descripcion`, traslados.idtienda " &
                                             " " & filtro_pagotralados & "" &
                                             "HAVING total <> 'Q. 0.00'  " &
                                             "ORDER BY ABS(traslados.`Sku`), traslados.`Idtienda`, traslados.`idmes`")

        dscompras_traslados = dscompras.Clone


        If dscompras.Tables(0).Rows.Count > 0 Then
            Dim busqueda() As DataRow
            Dim cobrobusqueda As String
            Dim unidadestrasladas As Decimal
            Dim unidadescompradas As Decimal

            Dim cobrofila As String
            Dim total_cobro As Decimal

            ' Dim saldo_final As Decimal
            Dim saldo_parcial As Decimal
            Dim saldo_unidades As Decimal
            Dim control As Integer = 0
            Dim control_tienda As Integer = 0
            '   Dim control As Integer
            Dim porcentaje_compras As Decimal
            Dim tienda_compra As String
            Dim ultima_tienda_compra As String
            Dim tienda_traslado As String
            Dim fila_sku As String
            Dim fila_tienda As String
            Dim total_lineas As Integer


            For Each fila In dscompras.Tables(0).Rows

                unidadescompradas = 0.00
                unidadestrasladas = 0.00
                saldo_unidades = 0.00
                cobrobusqueda = 0.00
                cobrofila = 0.00
                porcentaje_compras = 0.0
                total_cobro = 0.0

                fila_sku = String.Empty
                fila_tienda = String.Empty

                tienda_compra = String.Empty
                tienda_traslado = String.Empty



                fila_sku = fila("sku")
                fila_tienda = fila("tienda")


                busqueda = dstraslados.Tables(0).Select("sku='" & fila("sku") & "' AND tienda='" & fila("tienda") & "' ", String.Empty)
                If dscompras_traslados.Tables(0).Rows.Count > 0 Then
                    total_lineas = dscompras_traslados.Tables(0).Rows.Count - 1
                    ultima_tienda_compra = dscompras_traslados.Tables(0).Rows(total_lineas)("tienda")
                End If

                'porcentaje_compras = dscompras.Tables(0).Rows(0)("cobro") / 100
                porcentaje_compras = fila("cobro") / 100
                'lo encontro

                If busqueda.Length > 0 Then
                    tienda_compra = fila("tienda")
                    tienda_traslado = busqueda(0)(1)

                    If ultima_tienda_compra <> tienda_compra Then
                        control_tienda = 0
                    End If

                    If tienda_compra = tienda_traslado Then

                        control_tienda += 1

                        unidadescompradas = fila("unidades")
                        unidadestrasladas = busqueda(0)("unidades")
                        saldo_unidades = unidadescompradas - unidadestrasladas
                        cobrobusqueda = busqueda(0)("total")
                        cobrofila = fila("total")
                        cobrobusqueda = cobrobusqueda.Remove(0, 3)
                        cobrofila = cobrofila.Remove(0, 3)
                        'saldo_parcial = (cobrobusqueda * porcentaje_compras) * unidadestrasladas
                        saldo_parcial = (cobrobusqueda * porcentaje_compras)
                        total_cobro = cobrofila - saldo_parcial
                        total_cobro = Format(total_cobro, “##,##0.000”)

                        If total_cobro > 0 And busqueda.Length > 0 And control_tienda = 1 Then
                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = busqueda(0)("sku")
                            nuevafila("tienda") = busqueda(0)("tienda")
                            nuevafila("mes") = busqueda(0)("mes")
                            nuevafila("Unidades") = saldo_unidades
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = busqueda(0)("descripcion")
                            nuevafila("total") = "Q. " & total_cobro

                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)
                        ElseIf total_cobro > 0 And busqueda.Length > 0 And tienda_compra = tienda_traslado And control_tienda > 1 Then

                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = fila("sku")
                            nuevafila("tienda") = fila("tienda")
                            nuevafila("mes") = fila("mes")
                            nuevafila("Unidades") = fila("unidades")
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = fila("descripcion")
                            nuevafila("total") = fila("total")
                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)

                        ElseIf total_cobro <= 0 And busqueda.Length > 0 Then

                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = fila("sku")
                            nuevafila("tienda") = fila("tienda")
                            nuevafila("mes") = fila("mes")
                            nuevafila("Unidades") = fila("unidades")
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = fila("descripcion")
                            nuevafila("total") = fila("total")
                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)


                        End If


                    End If
                Else
                    control = 0
                    nuevafila = dscompras_traslados.Tables(0).NewRow
                    nuevafila("sku") = fila("sku")
                    nuevafila("tienda") = fila("tienda")
                    nuevafila("mes") = fila("mes")
                    nuevafila("Unidades") = fila("unidades")
                    nuevafila("Cobro") = fila("cobro")
                    nuevafila("descripcion") = fila("descripcion")
                    nuevafila("total") = fila("total")
                    dscompras_traslados.Tables(0).Rows.Add(nuevafila)
                    control_tienda = 0



                End If

                'saldo_parcial = dscompras_traslados.Tables(0).Rows(0)("total").Remove(0, 4)
                'totalcompras &= saldo_parcial
            Next
            'Dim cont As Integer
            'cont = dscompras_traslados.Tables(0).Rows.Count()
            'For Each fila In dscompras_traslados.Tables(0).Rows
            '    saldo_parcial = 0.00
            '    saldo_parcial = fila("total").Remove(0, 3)
            '    totalcompras += saldo_parcial

            'Next
            'totalcompras = Format(totalcompras, “##,##0.00”)
            Dim cont As Integer
            cont = dscompras_traslados.Tables(0).Rows.Count()
            For Each fila In dscompras_traslados.Tables(0).Rows
                saldo_parcial = 0.00
                saldo_parcial = fila("total").Remove(0, 3)
                totalcompras += saldo_parcial

            Next
            totalcompras = Format(totalcompras, “##,##0.00”)
        End If

















        dgvespacios.DataSource = dsespacios.Tables(0)
        dgvproductosC.DataSource = dscompras_traslados.Tables(0)
        dgvproductosM.DataSource = dsmontofijo.Tables(0)
        dgvproductosV.DataSource = dsventas.Tables(0)




        totalventas = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Ventas),2),0.00) AS CHAR)  " &
                                                "  FROM parametros_pagoventa_bmltx " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")

        'totalcompras = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(compras),2),0.00) AS CHAR) " &
        '                                        " FROM parametros_pagocompras_bmltx " &
        '                                        " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")

        totalespacioQ = conexion.EjecutarEscalar(" SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total  " &
                                                " FROM parametros_pagoespacio_bmltx  " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagoespacio_bmltx.`idtipomoneda` " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagoespacio_bmltx.`idtipomoneda`=" & idmonedaQ & " ")

        totalespacioD = conexion.EjecutarEscalar(" SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total  " &
                                                " FROM parametros_pagoespacio_bmltx  " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagoespacio_bmltx.`idtipomoneda` " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND  parametros_pagoespacio_bmltx.`idtipomoneda`=" & idMonedaD & " ")

        totalmontoD = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total " &
                                                "  FROM parametros_pagomontofijo_bmltx " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagomontofijo_bmltx.`idtipomoneda` " &
                                                " WHERE  idcontrato='" & contrato & "' AND fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagomontofijo_bmltx.`idtipomoneda`=" & idMonedaD & "")

        totalmontoQ = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total " &
                                                "  FROM parametros_pagomontofijo_bmltx " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagomontofijo_bmltx.`idtipomoneda` " &
                                                " WHERE  idcontrato='" & contrato & "' AND fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagomontofijo_bmltx.`idtipomoneda`=" & idmonedaQ & "")

        totalmontoCon = Format(totalmontoD * tasacambio, " ##,##0.00")
        totalespacioCon = Format(totalespacioD * tasacambio, "##,##0.00")
        totaldolares = Format(totalmontoD + totalespacioD, "##,##0.00")
        totalquetzales = Format(totalespacioQ + totalcompras + totalventas + totalmontoQ, "##,##0.00")
        totalconvertido = Format(totalmontoCon + totalespacioCon, "##,##0.00")
        totalcontrato = Format(totalconvertido + totalquetzales, "##,##0.00")


        If totaldolares = 0 Then
            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()


            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "TotalenQuetzales"
            col3.HeaderText = "Total en Quetzales"
            dgvTotales.Columns.Add(col3)

            If totalespacioQ > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "Q. " & totalespacioQ)
            End If
            If totalmontoQ > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "Q. " & totalmontoQ)
            End If
            If totalcompras > 0 Then
                dgvTotales.Rows.Add("Total de Compras:", "Q. " & totalcompras)
            End If
            If totalventas > 0 Then
                dgvTotales.Rows.Add("Total de Ventas:", "Q. " & totalventas)
            End If
            dgvTotales.Rows.Add("Total: ", "Q. " & totalquetzales)
            llenartablaquetzales()

        ElseIf totalquetzales = 0 Then

            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()

            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "TotalenDolares"
            col2.HeaderText = "Total en Dolares"
            dgvTotales.Columns.Add(col2)


            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            If totalespacioQ > 0 Or totalespacioD > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "$. " & totalespacioD)
            End If
            If totalmontoQ > 0 And totalmontoD > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "$. " & totalmontoD)
            End If
            dgvTotales.Rows.Add("Total: ", "$. " & totaldolares)
            llenartabladolares()

        Else

            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()

            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "TotalenQuetzales"
            col3.HeaderText = "Total en Quetzales"
            dgvTotales.Columns.Add(col3)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "TotalenDolares"
            col2.HeaderText = "Total en Dolares"
            dgvTotales.Columns.Add(col2)

            If totalespacioQ > 0 Or totalespacioD > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "Q. " & totalespacioQ, "$. " & totalespacioD)
            End If
            If totalmontoQ > 0 Or totalmontoD > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "Q. " & totalmontoQ, "$. " & totalmontoD)
            End If
            If totalcompras > 0 Then
                dgvTotales.Rows.Add("Total de Compras:", "Q. " & totalcompras, "$. 0.0")
            End If
            If totalventas > 0 Then
                dgvTotales.Rows.Add("Total de Ventas:", "Q. " & totalventas, "$ 0.00")
            End If
            dgvTotales.Rows.Add("Total: ", "Q. " & totalquetzales, "$. " & totaldolares)

            llenartabla()
        End If


        reporte.crearcsv("REPORTE DE PAGO DEL CONTRATO No." & contrato & " DEL MES DE " & mesincial & " al MES DE " & mesfinal & "", nit, mesincial & "  AL MES  DE  " & mesfinal, proveedor, dgvespacios,
                              dgvproductosC, dgvproductosV, dgvproductosM, nombreproveedor, direccion, tasacambio, totalespacioD, totalespacioQ,
                               totalcompras, totalventas, totalmontoD, totalmontoQ, totalquetzales, totaldolares, totalconvertido, totalcontrato, contrato, dgvTotales)

        actualizacion(contrato, fechaactualizada, mes1, mes2, añoIngresar, añoingresar2)


        Dim filaencontrada1() As DataRow = dscontratos.Tables(0).Select("contrato='" & contrato & "' and mesinicial='" & mesincial & "' and idinicial='" & añoinicial & "'  and mesfinal='" & mesfinal & "' and idfinal='" & añofinal & "'  ")
        If filaencontrada1.Length <> 0 Then
            filaencontrada1(0)("procesado") = 1
        End If
        ContContratos = ContContratos - 1
        lblconteocontratos.Text = ContContratos
        dgvreportenormal.Refresh()


    End Sub
#End Region

    Private Sub bloquear_controles_panel()
        pnlFiltros.Visible = False
    End Sub

    Private Sub cmbcontrato_LostFocus(sender As Object, e As EventArgs) Handles cmbcontrato.LostFocus
        If Trim(cmbcontrato.Text.Length) > 0 Then
            If cmbcontrato.SelectedIndex >= 0 Then
            Else
                MessageBox.Show("El Contrato que escribio no existe.", "Error en el Contrato", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbcontrato.Text = ""
            End If
        End If
    End Sub

    Private Sub cmbMesfinal_LostFocus(sender As Object, e As EventArgs) Handles cmbMesfinal.LostFocus
        If Trim(cmbMesfinal.Text.Length) > 0 Then
            If cmbMesfinal.SelectedIndex >= 0 Then
            Else
                MessageBox.Show("El mes que escribio no existe.", "Error en el Mes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbMesfinal.Text = ""
            End If
        End If
    End Sub


    Private Sub cmbañoinicial_LostFocus(sender As Object, e As EventArgs) Handles cmbañoinicial.LostFocus
        If Trim(cmbañoinicial.Text.Length) > 0 Then
            If cmbañoinicial.SelectedIndex >= 0 Then
            Else
                MessageBox.Show("El año que seleccion no existe.", "Error en el año ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbañoinicial.Text = ""
            End If
        End If
    End Sub

    Private Sub cmbañofinal_LostFocus(sender As Object, e As EventArgs) Handles cmbañofinal.LostFocus
        If Trim(cmbañofinal.Text.Length) > 0 Then
            If cmbañofinal.SelectedIndex >= 0 Then
            Else
                MessageBox.Show("El año que seleccion no existe.", "Error en el año ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbañofinal.Text = ""
            End If
        End If
    End Sub


    Private Sub cmbMesinicial_LostFocus(sender As Object, e As EventArgs) Handles cmbMesinicial.LostFocus
        If Trim(cmbMesinicial.Text.Length) > 0 Then
            If cmbMesinicial.SelectedIndex >= 0 Then
            Else
                MessageBox.Show("El mes que escribio no existe.", "Error en el Mes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbMesinicial.Text = ""
            End If
        End If
    End Sub

    Private Sub cmbproveedores_LostFocus(sender As Object, e As EventArgs) Handles cmbproveedores.LostFocus
        If Trim(cmbproveedores.Text.Length) > 0 Then
            If cmbproveedores.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El proveedor a Facturas que escribio no existe.", "Error Proveedor a Facturar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbproveedores.Text = ""
            End If
        End If
    End Sub


    Private Sub cmbproveedores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbproveedores.SelectedIndexChanged
        '=========================================================================================================================================================
        'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
        'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021

        CargarConexionProductos()
        Try
            'idproveedor = conexion.EjecutarEscalar("SELECT id FROM proveedores WHERE nombre='" & cmbproveedores.Text & "'")
            idproveedor = conexionpmm.EjecutarEscalar("SELECT idproveedor  FROM unipmm.unisuperproveedor where nombre_proveedor='" & cmbproveedores.Text & "'")
        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        'CAMBIAMOS LA CONEXION PARA LOS CONTRATOS
        CargarConexionContratos()
    End Sub

    Private Sub dgvreportes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvreportes.CellContentClick
        Dim numerofila As Integer
        Dim contrato As String
        Dim mesinicial As String
        Dim añoinicial As String
        Dim mesfinal As String
        Dim añofinal As String
        Dim procesado As Integer

        If Me.dgvreportes.Columns(e.ColumnIndex).Name = "impresion1" Then
            numerofila = e.RowIndex
            ' MsgBox("se oprimio" & e.RowIndex)
            If PreguntaTasaC = False Then
                frmtasa = New frmtasadecambio
                frmtasa.ShowDialog()

                If estado = True Then
                    'VERIFICAMOS SI SE INGRESO UNA TASA DE CAMBIO
                    If tasadecambio.ToString.Length > 0 Then
                        'BUSCAMOS LOS DATOS DEL CONTRATO SEGUN LAS PISICINES DESIGNADAS 
                        contrato = Me.dgvreportes.CurrentRow.Cells.Item("Nocontrato").Value


                        'mesinicial = Me.dgvreportes.CurrentRow.Cells.Item(3).Value
                        'añoinicial = Me.dgvreportes.CurrentRow.Cells.Item(4).Value
                        'mesfinal = Me.dgvreportes.CurrentRow.Cells.Item(5).Value
                        'añofinal = Me.dgvreportes.CurrentRow.Cells.Item(6).Value
                        'procesado = Me.dgvreportes.CurrentRow.Cells.Item(9).Value

                        generarreporteconsolidado(contrato, cmbMesinicial.Text, cmbañoinicial.Text, cmbMesfinal.Text, cmbañofinal.Text, tasadecambio)
                    End If
                Else
                    Exit Sub
                End If

            Else
                contrato = Me.dgvreportes.CurrentRow.Cells.Item(2).Value
                mesinicial = Me.dgvreportes.CurrentRow.Cells.Item(3).Value
                añoinicial = Me.dgvreportes.CurrentRow.Cells.Item(4).Value
                mesfinal = Me.dgvreportes.CurrentRow.Cells.Item(5).Value
                añofinal = Me.dgvreportes.CurrentRow.Cells.Item(6).Value
                procesado = Me.dgvreportes.CurrentRow.Cells.Item(9).Value
                generarreporte(contrato, mesinicial, añoinicial, mesfinal, añofinal, tasadecambio)
            End If
        End If
    End Sub

    Public Sub generarreporteconsolidado(ByVal contrato As String, ByVal mesincial As String, ByVal añoinicial As String,
                              ByVal mesfinal As String, ByVal añofinal As String, ByVal tasacambio As Decimal)

        'CARGAMOS LA CONEXION DE LOS CONTRATOS...
        CargarConexionContratos()

        Dim dsmontofijo As New DataSet
        Dim totalespacioD As Decimal = 0.0
        Dim totalespacioQ As Decimal
        Dim totalmontoD As Decimal
        Dim totalmontoQ As Decimal
        Dim totalmontoCon As Decimal
        Dim totalespacioCon As Decimal
        Dim totaldolares As Decimal
        Dim totalquetzales As Decimal
        Dim dsventas As New DataSet
        Dim dscompras As New DataSet
        Dim dstraslados As New DataSet
        Dim dscompras_traslados As New DataSet
        Dim nuevafila As DataRow
        Dim dsespacios As New DataSet
        Dim totalventas As Decimal
        Dim totalcompras As Decimal
        Dim totalconvertido As Decimal
        Dim totalcontrato As Decimal
        Dim fechaI, fechaF As String
        Dim fecha As Date
        Dim fechaenviar, hora, fechaactualizada As String
        Dim nit As String
        Dim mes1, mes2 As Integer
        Dim dia1, dia2 As Integer
        Dim añoIngresar, añoingresar2 As Integer
        Dim proveedor As String
        Dim direccion As String
        Dim nombreproveedor As String
        Dim filtro_pagocompra As String
        Dim filtro_pagotralados As String
        Dim filtro_pagoespacios As String
        Dim filtro_pagoventa As String
        Dim filtro_pagomonto As String
        'para dibujar el grid de los totales

        'DIBUJAMOS EL GRID

        totalespacioD = 0.0
        totalespacioQ = 0.0
        totalmontoD = 0.0
        totalmontoQ = 0.0
        totalmontoCon = 0.0
        totalespacioCon = 0.0
        totaldolares = 0.0
        totalquetzales = 0.0
        totalventas = 0.0
        totalcompras = 0.0
        totalconvertido = 0.0
        totalcontrato = 0.0
        fecha = Now
        fechaenviar = Format(fecha, "yyyy-MM-dd")
        hora = Format(fecha, "hh:mm:ss")
        fechaactualizada = fechaenviar & "  " & hora
        mes1 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & mesincial & "'")
        mes2 = conexion.EjecutarEscalar("SELECT id_tiempo FROM parametros_tiempo WHERE nombre='" & mesfinal & "'")
        añoIngresar = conexion.EjecutarEscalar(" SELECT id FROM parametros_año WHERE valor= " & añoinicial & "")
        añoingresar2 = conexion.EjecutarEscalar(" SELECT id FROM parametros_año WHERE valor= " & añofinal & "")
        dia1 = validacionmesfinal(mes2)
        fechaI = añoinicial & "-" & mes1 & "-" & "01"
        fechaF = añofinal & "-" & mes2 & "-" & dia1


        'AHORA BUSCAMOS LA INFORMACION DE LOS PROVEEDORES..

        proveedor = conexion.EjecutarEscalar("SELECT nombreproveedorFAC FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        nombreproveedor = conexion.EjecutarEscalar("SELECT nombreproveedor FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        nit = conexion.EjecutarEscalar("SELECT CAST(IFNULL(nitProveedorFac,0) AS CHAR) FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")
        direccion = conexion.EjecutarEscalar("SELECT CAST(IFNULL(direccion,0) AS CHAR)  FROM contratos_backmarginltx WHERE idcontrato='" & contrato & "'")

        'SE AGREGA PARA FILTRO DE TIENDAS nov2017 JBOCH
        If txtLista_Tiendas.Text.Length > 0 Then
            filtro_pagocompra = " AND pagocompras.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagotralados = " AND traslados.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagoespacios = " AND pagoespacio.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagoventa = " AND pagoventa.Idtienda IN (" & _lista_id_sucursales & ")"
            filtro_pagomonto = " AND pagomonto .Idtienda IN (" & _lista_id_sucursales & ")"
        Else

            filtro_pagocompra = String.Empty
            filtro_pagotralados = String.Empty
            filtro_pagoespacios = String.Empty
            filtro_pagoventa = String.Empty
            filtro_pagomonto = String.Empty
        End If

        dsespacios = conexion.llenarDataSet("  Select espacio.`Nombre` As nombreespacio,pagoespacio.`cantidadespacio` As cantidadrentada,  " &
                                            " FORMAT(pagoespacio.`costoespacio`,2) As costoespacio, " &
                                            " tienda.`Nombre` As Tienda, " &
                                            " CONCAT('$. ',FORMAT(SUM(CASE WHEN  pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,  " &
                                            " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales   " &
                                            " FROM tipoespacio_bmltx AS espacio  " &
                                            " INNER JOIN parametros_pagoespacio_bmltx AS pagoespacio  " &
                                            " ON espacio.`Idtipoespacio`=pagoespacio.`Idespacio`  " &
                                            " INNER JOIN parametros_tiempo AS mes  " &
                                            " ON mes.`id_tiempo`=pagoespacio.`idmes` " &
                                            " INNER JOIN parametros_año AS año  " &
                                            "  ON año.`id`=pagoespacio.`idaño`  " &
                                            " INNER JOIN sucursales_bmltx AS tienda  " &
                                            " ON tienda.`Id`=pagoespacio.`Idtienda`  " &
                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                            " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                            " WHERE  pagoespacio.`Idcontrato`='" & contrato & "' AND pagoespacio.`fechafinal` BETWEEN '" & fechaI & "' AND '" & fechaF & "'  " &
                                            " " & filtro_pagoespacios & "" &
                                            " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idmes`,pagoespacio.Idtienda " &
                                            " HAVING totaldolares <> '$. 0.00' " &
                                            " OR totalquetzales <> 'Q. 0.00' " &
                                            " ORDER BY pagoespacio.`Idtienda`,pagoespacio.`idmes` ")

        dsventas = conexion.llenarDataSet(" SELECT pagoventa.sku AS sku, " &
                                            " tienda.nombre AS tienda,tiempo.nombre AS mes,pagoventa.`Descripcion` AS descripcion, " &
                                            " CONCAT('Q. ',FORMAT((Ventas),2))AS total " &
                                            " FROM parametros_pagoventa_bmltx AS pagoventa " &
                                            " INNER JOIN parametros_año AS año " &
                                            " ON año.id= pagoventa.idaño " &
                                            " INNER JOIN parametros_tiempo AS tiempo " &
                                            " ON tiempo.id_tiempo= pagoventa.idmes " &
                                            " INNER JOIN sucursales_bmltx AS tienda " &
                                            " ON tienda.id=pagoventa.idtienda " &
                                            " WHERE pagoventa.idcontrato='" & contrato & "' AND pagoventa.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                            " " & filtro_pagoventa & "" &
                                            " GROUP BY pagoventa.`idMes`,pagoventa.`idaño`,pagoventa.`Sku` ,pagoventa.`Descripcion`,pagoventa.idtienda " &
                                            " HAVING total <> 'Q. 0.00' " &
                                            " ORDER BY pagoventa.`idMes`ASC ,pagoventa.`idaño` ASC")

        dsmontofijo = conexion.llenarDataSet("Select  pagomonto.sku As sku, " &
                                            " tienda.`Nombre` As tienda, " &
                                            " mes.nombre As mes, " &
                                            " pagomonto.`Descripcion`," &
                                            " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares, " &
                                            " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales   " &
                                            " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                            " INNER JOIN parametros_tiempo AS mes " &
                                            " ON mes.id_tiempo=pagomonto.idmes " &
                                            " INNER JOIN parametros_año AS año " &
                                            " ON año.id=pagomonto.idaño " &
                                            " INNER JOIN sucursales_bmltx AS tienda " &
                                            " ON tienda.`Id`=pagomonto.`idTienda` " &
                                            " INNER JOIN tipomoneda_bmltx AS moneda " &
                                            " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                            " WHERE pagomonto.idcontrato='" & contrato & "' AND pagomonto.fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                            " " & filtro_pagomonto & "" &
                                            " GROUP BY pagomonto.`idTienda`,pagomonto.`idMes`, pagomonto.`Sku`,pagomonto.idTienda, pagomonto.`Descripcion` " &
                                            " HAVING totaldolares <> '$. 0.00' " &
                                            " OR totalquetzales <> 'Q. 0.00' " &
                                            " ORDER BY pagomonto.`idTienda`")

        dscompras = conexion.llenarDataSet(" SELECT  pagocompras.sku AS sku," &
                                           " tienda.nombre As tienda," &
                                           " tiempo.nombre AS mes," &
                                           " pagocompras.Unidadcomprada AS Unidades, " &
                                           " pagocompras.`porcentaje` AS Cobro,   " &
                                           " pagocompras.`Descripcion` AS descripcion, " &
                                           " CONCAT('Q. ',FORMAT((compras),2))AS total " &
                                           " From parametros_pagocompras_bmltx As pagocompras " &
                                           " INNER Join parametros_año AS año " &
                                           " On año.id= pagocompras.idaño " &
                                           " INNER Join parametros_tiempo AS tiempo " &
                                           " On tiempo.id_tiempo= pagocompras.idmes " &
                                           " INNER Join sucursales_bmltx AS tienda " &
                                           " On tienda.id=pagocompras.idtienda " &
                                           " WHERE pagocompras.idcontrato ='" & contrato & "' " &
                                           " And pagocompras.fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                           " " & filtro_pagocompra & "" &
                                           " GROUP BY pagocompras.idmes, pagocompras.`idaño`, pagocompras.`Sku` , pagocompras.`Descripcion`,pagocompras.idtienda" &
                                           " HAVING total <> 'Q. 0.00' " &
                                           " ORDER BY ABS(pagocompras.`Sku`), pagocompras.`Idtienda`,  pagocompras.`idmes`")


        dstraslados = conexion.llenarDataSet(" SELECT  traslados.sku AS sku, " &
                                             "tienda.nombre As tienda, " &
                                             "tiempo.nombre AS mes, " &
                                             "traslados.`UnidadTrasladada` AS Unidades, " &
                                             "'' AS Cobro,  " &
                                             "traslados.`Descripcion` AS descripcion, " &
                                             "CONCAT('Q. ',FORMAT((`Traslado`),2))AS total " &
                                             "FROM `parametros_pagotraslados_bmltx` As traslados  " &
                                             "INNER JOIN parametros_año AS año  ON año.id= traslados.idaño " &
                                             "INNER Join parametros_tiempo AS tiempo  ON tiempo.id_tiempo= traslados.idmes  " &
                                             "INNER JOIN sucursales_bmltx AS tienda  ON tienda.id=traslados.idtienda  " &
                                             "WHERE traslados.idcontrato ='" & contrato & "'  " &
                                             "And traslados.fechafinal " &
                                             "BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                             "GROUP BY traslados.idmes, traslados.`idaño`, traslados.`Sku` , traslados.`Descripcion`, traslados.idtienda " &
                                             " " & filtro_pagotralados & "" &
                                             "HAVING total <> 'Q. 0.00'  " &
                                             "ORDER BY ABS(traslados.`Sku`), traslados.`Idtienda`, traslados.`idmes`")



        dscompras_traslados = dscompras.Clone


        If dscompras.Tables(0).Rows.Count > 0 Then
            Dim busqueda() As DataRow
            Dim cobrobusqueda As String
            Dim unidadestrasladas As Decimal
            Dim unidadescompradas As Decimal

            Dim cobrofila As String
            Dim total_cobro As Decimal

            ' Dim saldo_final As Decimal
            Dim saldo_parcial As Decimal
            Dim saldo_unidades As Decimal
            Dim control As Integer = 0
            Dim control_tienda As Integer = 0
            '   Dim control As Integer
            Dim porcentaje_compras As Decimal
            Dim tienda_compra As String
            Dim ultima_tienda_compra As String
            Dim tienda_traslado As String
            Dim fila_sku As String
            Dim fila_tienda As String
            Dim total_lineas As Integer


            For Each fila In dscompras.Tables(0).Rows

                unidadescompradas = 0.00
                unidadestrasladas = 0.00
                saldo_unidades = 0.00
                cobrobusqueda = 0.00
                cobrofila = 0.00
                porcentaje_compras = 0.0
                total_cobro = 0.0

                fila_sku = String.Empty
                fila_tienda = String.Empty

                tienda_compra = String.Empty
                tienda_traslado = String.Empty



                fila_sku = fila("sku")
                fila_tienda = fila("tienda")


                busqueda = dstraslados.Tables(0).Select("sku='" & fila("sku") & "' AND tienda='" & fila("tienda") & "' ", String.Empty)


                If dscompras_traslados.Tables(0).Rows.Count > 0 Then
                    total_lineas = dscompras_traslados.Tables(0).Rows.Count - 1
                    ultima_tienda_compra = dscompras_traslados.Tables(0).Rows(total_lineas)("tienda")
                End If

                'porcentaje_compras = dscompras.Tables(0).Rows(0)("cobro") / 100
                porcentaje_compras = fila("cobro") / 100
                'lo encontro



                If busqueda.Length > 0 Then
                    tienda_compra = fila("tienda")
                    tienda_traslado = busqueda(0)(1)

                    If ultima_tienda_compra <> tienda_compra Then
                        control_tienda = 0
                    End If

                    If tienda_compra = tienda_traslado Then

                        control_tienda += 1

                        unidadescompradas = fila("unidades")
                        unidadestrasladas = busqueda(0)("unidades")
                        saldo_unidades = unidadescompradas - unidadestrasladas
                        cobrobusqueda = busqueda(0)("total")
                        cobrofila = fila("total")
                        cobrobusqueda = cobrobusqueda.Remove(0, 3)
                        cobrofila = cobrofila.Remove(0, 3)
                        'saldo_parcial = (cobrobusqueda * porcentaje_compras) * unidadestrasladas
                        saldo_parcial = (cobrobusqueda * porcentaje_compras)
                        total_cobro = cobrofila - saldo_parcial
                        total_cobro = Format(total_cobro, “##,##0.000”)

                        If total_cobro > 0 And busqueda.Length > 0 And control_tienda = 1 Then
                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = busqueda(0)("sku")
                            nuevafila("tienda") = busqueda(0)("tienda")
                            nuevafila("mes") = busqueda(0)("mes")
                            nuevafila("Unidades") = saldo_unidades
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = busqueda(0)("descripcion")
                            nuevafila("total") = "Q. " & total_cobro

                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)
                        ElseIf total_cobro > 0 And busqueda.Length > 0 And tienda_compra = tienda_traslado And control_tienda > 1 Then

                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = fila("sku")
                            nuevafila("tienda") = fila("tienda")
                            nuevafila("mes") = fila("mes")
                            nuevafila("Unidades") = fila("unidades")
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = fila("descripcion")
                            nuevafila("total") = fila("total")
                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)

                        ElseIf total_cobro <= 0 And busqueda.Length > 0 Then

                            nuevafila = dscompras_traslados.Tables(0).NewRow
                            nuevafila("sku") = fila("sku")
                            nuevafila("tienda") = fila("tienda")
                            nuevafila("mes") = fila("mes")
                            nuevafila("Unidades") = fila("unidades")
                            nuevafila("Cobro") = fila("cobro")
                            nuevafila("descripcion") = fila("descripcion")
                            nuevafila("total") = fila("total")
                            dscompras_traslados.Tables(0).Rows.Add(nuevafila)


                        End If


                    End If
                Else
                    control = 0
                    nuevafila = dscompras_traslados.Tables(0).NewRow
                    nuevafila("sku") = fila("sku")
                    nuevafila("tienda") = fila("tienda")
                    nuevafila("mes") = fila("mes")
                    nuevafila("Unidades") = fila("unidades")
                    nuevafila("Cobro") = fila("cobro")
                    nuevafila("descripcion") = fila("descripcion")
                    nuevafila("total") = fila("total")
                    dscompras_traslados.Tables(0).Rows.Add(nuevafila)
                    control_tienda = 0
                End If
                'saldo_parcial = dscompras_traslados.Tables(0).Rows(0)("total").Remove(0, 4)
                'totalcompras &= saldo_parcial
            Next
            Dim cont As Integer
            cont = dscompras_traslados.Tables(0).Rows.Count()
            For Each fila In dscompras_traslados.Tables(0).Rows
                saldo_parcial = 0.00
                saldo_parcial = fila("total").Remove(0, 3)
                totalcompras += saldo_parcial

            Next
            totalcompras = Format(totalcompras, “##,##0.00”)
        End If
        dgvespacios.DataSource = dsespacios.Tables(0)
        dgvproductosC.DataSource = dscompras_traslados.Tables(0)
        dgvproductosM.DataSource = dsmontofijo.Tables(0)
        dgvproductosV.DataSource = dsventas.Tables(0)
        totalventas = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Ventas),2),0.00) AS CHAR)  " &
                                                "  FROM parametros_pagoventa_bmltx " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")

        'totalcompras = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(compras),2),0.00) AS CHAR) " &
        '                                        " FROM parametros_pagocompras_bmltx " &
        '                                        " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "'")

        totalespacioQ = conexion.EjecutarEscalar(" SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total  " &
                                                " FROM parametros_pagoespacio_bmltx  " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagoespacio_bmltx.`idtipomoneda` " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagoespacio_bmltx.`idtipomoneda`=" & idmonedaQ & " ")

        totalespacioD = conexion.EjecutarEscalar(" SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total  " &
                                                " FROM parametros_pagoespacio_bmltx  " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagoespacio_bmltx.`idtipomoneda` " &
                                                " WHERE idcontrato='" & contrato & "' AND fechafinal BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND  parametros_pagoespacio_bmltx.`idtipomoneda`=" & idMonedaD & " ")

        totalmontoD = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total " &
                                                "  FROM parametros_pagomontofijo_bmltx " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagomontofijo_bmltx.`idtipomoneda` " &
                                                " WHERE  idcontrato='" & contrato & "' AND fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagomontofijo_bmltx.`idtipomoneda`=" & idMonedaD & "")

        totalmontoQ = conexion.EjecutarEscalar("SELECT CAST(IFNULL(FORMAT(SUM(Total),2),0.00) AS CHAR) AS total " &
                                                "  FROM parametros_pagomontofijo_bmltx " &
                                                " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=parametros_pagomontofijo_bmltx.`idtipomoneda` " &
                                                " WHERE  idcontrato='" & contrato & "' AND fechafin BETWEEN '" & fechaI & "' AND '" & fechaF & "' " &
                                                " AND parametros_pagomontofijo_bmltx.`idtipomoneda`=" & idmonedaQ & "")

        totalmontoCon = Format(totalmontoD * tasacambio, " ##,##0.00")
        totalespacioCon = Format(totalespacioD * tasacambio, "##,##0.00")
        totaldolares = Format(totalmontoD + totalespacioD, "##,##0.00")
        totalquetzales = Format(totalespacioQ + totalcompras + totalventas + totalmontoQ, "##,##0.00")
        totalconvertido = Format(totalmontoCon + totalespacioCon, "##,##0.00")
        totalcontrato = Format(totalconvertido + totalquetzales, "##,##0.00")


        If totaldolares = 0 Then
            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()


            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "TotalenQuetzales"
            col3.HeaderText = "Total en Quetzales"
            dgvTotales.Columns.Add(col3)

            If totalespacioQ > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "Q. " & totalespacioQ)
            End If
            If totalmontoQ > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "Q. " & totalmontoQ)
            End If
            If totalcompras > 0 Then
                dgvTotales.Rows.Add("Total de Compras:", "Q. " & totalcompras)
            End If
            If totalventas > 0 Then
                dgvTotales.Rows.Add("Total de Ventas:", "Q. " & totalventas)
            End If
            dgvTotales.Rows.Add("Total: ", "Q. " & totalquetzales)
            llenartablaquetzales()

        ElseIf totalquetzales = 0 Then

            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()

            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "TotalenDolares"
            col2.HeaderText = "Total en Dolares"
            dgvTotales.Columns.Add(col2)


            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            If totalespacioQ > 0 Or totalespacioD > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "$. " & totalespacioD)
            End If
            If totalmontoQ > 0 And totalmontoD > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "$. " & totalmontoD)
            End If
            dgvTotales.Rows.Add("Total: ", "$. " & totaldolares)
            llenartabladolares()

        Else

            dgvTotales.Rows.Clear() 'jotzoy...070317. limpiamos por si imprimen mas de 1
            dgvTotales.Columns.Clear()

            Dim col1 As New DataGridViewTextBoxColumn
            col1.Name = "Resumen"
            col1.HeaderText = "Resumen"
            dgvTotales.Columns.Add(col1)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "TotalenQuetzales"
            col3.HeaderText = "Total en Quetzales"
            dgvTotales.Columns.Add(col3)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "TotalenDolares"
            col2.HeaderText = "Total en Dolares"
            dgvTotales.Columns.Add(col2)

            If totalespacioQ > 0 Or totalespacioD > 0 Then
                dgvTotales.Rows.Add("Total de espacios:", "Q. " & totalespacioQ, "$. " & totalespacioD)
            End If
            If totalmontoQ > 0 Or totalmontoD > 0 Then
                dgvTotales.Rows.Add("Total de productos con Monto Fijo:", "Q. " & totalmontoQ, "$. " & totalmontoD)
            End If
            If totalcompras > 0 Then
                dgvTotales.Rows.Add("Total de Compras:", "Q. " & totalcompras, "$. 0.0")
            End If
            If totalventas > 0 Then
                dgvTotales.Rows.Add("Total de Ventas:", "Q. " & totalventas, "$ 0.00")
            End If
            dgvTotales.Rows.Add("Total: ", "Q. " & totalquetzales, "$. " & totaldolares)

            llenartabla()
        End If


        reporte.creararchivo("REPORTE DE PAGO DEL CONTRATO No." & contrato & " DEL MES DE " & mesincial & " al MES DE " & mesfinal & "", nit, mesincial & "  AL MES  DE  " & mesfinal, proveedor, dgvespacios,
                              dgvproductosC, dgvproductosV, dgvproductosM, nombreproveedor, direccion, tasacambio, totalespacioD, totalespacioQ,
                               totalcompras, totalventas, totalmontoD, totalmontoQ, totalquetzales, totaldolares, totalconvertido, totalcontrato, contrato, dgvTotales)

        actualizacion(contrato, fechaactualizada, mes1, mes2, añoIngresar, añoingresar2)


        Dim filaencontrada1() As DataRow = dscontratosanidados.Tables(0).Select("contrato='" & contrato & "'")
        If filaencontrada1.Length <> 0 Then
            filaencontrada1(0)("procesado") = 1
        End If
        ContContratos = ContContratos - 1
        lblconteocontratos.Text = ContContratos
        dgvreportenormal.Refresh()


    End Sub
    Private _valorGrid As Integer
    Public Property valorGrid() As Integer
        Get
            Return _valorGrid
        End Get
        Set(ByVal value As Integer)
            _valorGrid = value
        End Set
    End Property

    Private Sub dgvreportenormal_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvreportenormal.CellContentClick
        Dim numerofila As Integer
        Dim contrato As String
        Dim mesinicial As String
        Dim añoinicial As String
        Dim mesfinal As String
        Dim añofinal As String
        Dim procesado As Integer
        Dim valor As Integer = 0

        If dgvreportenormal.Rows.Count > 0 Then

            If Me.dgvreportenormal.Columns(e.ColumnIndex).Name = "impresion" Then
                numerofila = e.RowIndex
                ' MsgBox("se oprimio" & e.RowIndex)
                If PreguntaTasaC = False Then
                    frmtasa = New frmtasadecambio
                    frmtasa.ShowDialog()

                    If estado = True Then
                        'VERIFICAMOS SI SE INGRESO UNA TASA DE CAMBIO
                        If tasadecambio.ToString.Length > 0 Then
                            'BUSCAMOS LOS DATOS DEL CONTRATO SEGUN LAS POSICIONES DESIGNADAS 
                            contrato = Me.dgvreportenormal.CurrentRow.Cells.Item("colcontrato").Value
                            mesinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(3).Value
                            añoinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(4).Value
                            mesfinal = Me.dgvreportenormal.CurrentRow.Cells.Item(5).Value
                            añofinal = Me.dgvreportenormal.CurrentRow.Cells.Item(6).Value
                            procesado = Me.dgvreportenormal.CurrentRow.Cells.Item(9).Value
                            generarreporte(contrato, mesinicial, añoinicial, mesfinal, añofinal, tasadecambio)
                        End If
                    Else
                        Exit Sub
                    End If

                Else
                    contrato = Me.dgvreportenormal.CurrentRow.Cells.Item("colcontrato").Value
                    mesinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(3).Value
                    añoinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(4).Value
                    mesfinal = Me.dgvreportenormal.CurrentRow.Cells.Item(5).Value
                    añofinal = Me.dgvreportenormal.CurrentRow.Cells.Item(6).Value
                    procesado = Me.dgvreportenormal.CurrentRow.Cells.Item(9).Value
                    generarreporte(contrato, mesinicial, añoinicial, mesfinal, añofinal, tasadecambio)
                End If


            End If



            If Me.dgvreportenormal.Columns(e.ColumnIndex).Name = "CSV" Then
                numerofila = e.RowIndex
                ' MsgBox("se oprimio" & e.RowIndex)
                If PreguntaTasaC = False Then
                    frmtasa = New frmtasadecambio
                    frmtasa.ShowDialog()

                    If estado = True Then
                        'VERIFICAMOS SI SE INGRESO UNA TASA DE CAMBIO
                        If tasadecambio.ToString.Length > 0 Then
                            'BUSCAMOS LOS DATOS DEL CONTRATO SEGUN LAS PISICINES DESIGNADAS 
                            contrato = Me.dgvreportenormal.CurrentRow.Cells.Item("colcontrato").Value
                            mesinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(3).Value
                            añoinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(4).Value
                            mesfinal = Me.dgvreportenormal.CurrentRow.Cells.Item(5).Value
                            añofinal = Me.dgvreportenormal.CurrentRow.Cells.Item(6).Value
                            procesado = Me.dgvreportenormal.CurrentRow.Cells.Item(9).Value
                            generarCSV(contrato, mesinicial, añoinicial, mesfinal, añofinal, tasadecambio)
                        End If
                    Else
                        Exit Sub
                    End If

                Else
                    contrato = Me.dgvreportenormal.CurrentRow.Cells.Item("colcontrato").Value
                    mesinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(3).Value
                    añoinicial = Me.dgvreportenormal.CurrentRow.Cells.Item(4).Value
                    mesfinal = Me.dgvreportenormal.CurrentRow.Cells.Item(5).Value
                    añofinal = Me.dgvreportenormal.CurrentRow.Cells.Item(6).Value
                    procesado = Me.dgvreportenormal.CurrentRow.Cells.Item(9).Value
                    generarCSV(contrato, mesinicial, añoinicial, mesfinal, añofinal, tasadecambio)
                End If


            End If
        Else
            Exit Sub
        End If

    End Sub


    Public Sub actualizacion(ByVal contrato As String, ByVal fechaactualizada As String, ByVal mes1 As Integer, ByVal mes2 As Integer, ByVal id1 As Integer, ByVal id2 As Integer)
        Dim consulta As String
        Try
            'CARGAMOS LA CONEXION DE LOS CONTRATOS
            CargarConexionContratos()
            'consulta = conexion.EjecutarNonQuery(" UPDATE  resumen_bmltx   SET procesado = 1 WHERE idcontrato='" & contrato & "' AND idmesinicial =" & mes1 & "   AND   idmesfinal=" & mes2 & "     AND idañoinicial= " & id1 & "   AND idañofinal= " & id2 & "")
            consulta = conexion.EjecutarNonQuery("UPDATE resumen_bmltx   SET UsuarioModificacion = '" & nombreusuario & "'  WHERE  idcontrato='" & contrato & "'   AND  idmesinicial =" & mes1 & "  AND idmesfinal=" & mes2 & "  AND idañoinicial= " & id1 & "  AND idañofinal=" & id2 & "")
            consulta = conexion.EjecutarNonQuery("UPDATE resumen_bmltx   SET FechaModificacion = '" & fechaactualizada & "'  WHERE  idcontrato='" & contrato & "'   AND  idmesinicial =" & mes1 & " AND idmesfinal=" & mes2 & "  AND idañoinicial= " & id1 & "  AND idañofinal=" & id2 & "")

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Public Sub llenartabla()
        Dim dttotales As New DataTable

        Dim numceldas As Integer = 0.0
        Dim fila As DataRow
        dttotales.TableName = "Total"
        dttotales.Columns.Add("Resumen")
        dttotales.Columns.Add("Total en Quetzales")
        dttotales.Columns.Add("Total en Dolares")
        numceldas = dgvTotales.Rows.Count
        For i As Integer = 0 To numceldas - 1
            fila = dttotales.NewRow
            fila("Resumen") = dgvTotales.Rows(i).Cells(0).Value
            fila("Total en Quetzales") = dgvTotales.Rows(i).Cells(1).Value
            fila("Total en Dolares") = dgvTotales.Rows(i).Cells(2).Value
            'fila("correo") = dgvTotales.Rows(i).Cells(3).Value
            dttotales.Rows.Add(fila)
        Next
        'Else

        ' End If

    End Sub
    Public Sub llenartablaquetzales()

        Dim dttotales As New DataTable
        Dim numceldas As Integer
        Dim fila As DataRow
        dttotales.TableName = "Total"
        dttotales.Columns.Add("Resumen")
        dttotales.Columns.Add("Total en Quetzales")
        'dscontactos.Tables.Add("contactos")
        numceldas = dgvTotales.Rows.Count
        For i As Integer = 0 To numceldas - 1
            fila = dttotales.NewRow
            fila("Resumen") = dgvTotales.Rows(i).Cells(0).Value
            fila("Total en Quetzales") = dgvTotales.Rows(i).Cells(1).Value
            'fila("correo") = dgvTotales.Rows(i).Cells(3).Value
            dttotales.Rows.Add(fila)
        Next
    End Sub



    Public Sub llenartabladolares()

        Dim dttotales As New DataTable
        Dim numceldas As Integer
        Dim fila As DataRow
        dttotales.TableName = "Total"
        dttotales.Columns.Add("Resumen")
        dttotales.Columns.Add("Total en Dolares")
        'dscontactos.Tables.Add("contactos")
        numceldas = dgvTotales.Rows.Count
        For i As Integer = 0 To numceldas - 1
            fila = dttotales.NewRow
            fila("Resumen") = dgvTotales.Rows(i).Cells(0).Value
            fila("Total en Dolares") = dgvTotales.Rows(i).Cells(1).Value
            'fila("correo") = dgvTotales.Rows(i).Cells(3).Value
            dttotales.Rows.Add(fila)
        Next
    End Sub

    Private Sub chb_Tienda_Click(sender As Object, e As EventArgs)
        Try
            btnElegir_Tienda.Enabled = True
            _lista_sucursales = Nothing
            _lista_id_sucursales = Nothing
            txtLista_Tiendas.Text = String.Empty

        Catch ex As Exception
            Throw New InvalidOperationException("Error " & vbCrLf & ex.Message)
        End Try
    End Sub



    Private mouseLocation1 As DataGridViewCellEventArgs
    Private Sub dgv_Filtros_CellEnter(sender As Object, Location As DataGridViewCellEventArgs) Handles dgv_Filtros.CellEnter
        mouseLocation1 = Location
    End Sub

    Private Sub dgv_Filtros_ControlAdded(sender As Object, e As ControlEventArgs) Handles dgv_Filtros.ControlAdded
        If dgv_Filtros.IsCurrentCellDirty Then
            dgv_Filtros.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgv_Filtros_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Filtros.CellContentClick
        Dim _dsActivos As New DataSet
        Dim contador As Integer = 0
        Try
            _lista_id_sucursales = String.Empty
            If e.RowIndex = -1 Or mouseLocation1.ColumnIndex <= -1 Then
                Return
            End If
            Dim row As DataGridViewRow = dgv_Filtros.Rows(e.RowIndex)

            Dim col As Integer
            col = mouseLocation1.ColumnIndex
            Dim ncol As String = String.Empty
            ncol = dgv_Filtros.Columns(col).Name
            If ncol = "nombre" Or ncol = "id" Then
                Exit Sub
            End If
            Dim cellSelecion As DataGridViewCheckBoxCell = TryCast(row.Cells(ncol), DataGridViewCheckBoxCell)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'SE AGREGA OCT2017 JBOCH
    Private Sub btnElegir_Tienda_Click(sender As Object, e As EventArgs) Handles btnElegir_Tienda.Click
        Dim colid As DataGridViewColumn = New DataGridViewTextBoxColumn
        Dim coltienda As DataGridViewColumn = New DataGridViewTextBoxColumn
        Dim colactivo As DataGridViewColumn = New DataGridViewCheckBoxColumn

        cargarConexionMysql1("conexion2.xml")


        Try
            Dim contador As Integer = 0
            Dim _TotalFilas As Integer
            _TotalFilas = dgv_Filtros.RowCount

            For x = 0 To _TotalFilas - 1
                    If dgv_Filtros.Item("colActivo", contador).Value = True Then
                        _lista_id_sucursales = dgv_Filtros.Item("id", contador).Value & "," & _lista_id_sucursales
                    contador = contador + 1
                End If

            Next
            If _TotalFilas > 0 Then
                If _TotalFilas = contador Then
                    chTodos.Checked = True
                Else
                    chTodos.Checked = False
                End If
            End If


            If chTodos.Checked = True Then
                Actualizar_todo(2)

            End If
            '_lista_sucursales = String.Empty
            '_lista_id_sucursales = String.Empty

            If txtLista_Tiendas.Text.Length = 0 Then
                _lista_sucursales = String.Empty
                _lista_id_sucursales = String.Empty


                _ds_resultado = conexion.llenarDataSet("SELECT id,nombre FROM sucursales_bmltx ORDER BY id")
                dgv_Filtros.DataSource = _ds_resultado.Tables(0)

                dgv_Filtros.Columns(1).HeaderText = "ID"
                dgv_Filtros.Columns(1).Width = 50
                colid.Name = "colid_codigo"
                colid.DataPropertyName = "id"

                dgv_Filtros.Columns(2).HeaderText = "Tienda"
                dgv_Filtros.Columns(2).Width = 220
                coltienda.Name = "colsucursal"
                coltienda.DataPropertyName = "nombre"

                dgv_Filtros.Columns("nombre").ReadOnly = True
                dgv_Filtros.Columns("id").ReadOnly = True


                dgv_Filtros.Columns("nombre").Frozen = True
                dgv_Filtros.Columns("id").Frozen = True

                pnlFiltros.Visible = True
            Else
                pnlFiltros.Visible = True
            End If

        Catch ex As Exception
            Throw New InvalidOperationException("Error " & vbCrLf & ex.Message)
        End Try
    End Sub


    Private Sub btn_Aceptar_Click(sender As Object, e As EventArgs) Handles btn_Aceptar.Click
        _lista_sucursales = Nothing
        _lista_id_sucursales = Nothing
        Dim contador As Integer = 0
        Dim _TotalFilas As Integer
        _TotalFilas = dgv_Filtros.RowCount
        Try
            For x = 0 To _TotalFilas - 1
                If dgv_Filtros.Item("colActivo", contador).Value = True Then
                    _lista_id_sucursales = dgv_Filtros.Item("id", contador).Value & "," & _lista_id_sucursales

                End If
                contador = contador + 1
            Next


            If _lista_id_sucursales = Nothing Then
                pnlFiltros.Visible = False
                txtLista_Tiendas.Text = _lista_id_sucursales
                Exit Sub
            Else
                _lista_id_sucursales = _lista_id_sucursales.TrimEnd(",")
                txtLista_Tiendas.Text = _lista_id_sucursales
                pnlFiltros.Visible = False
            End If
            pnlFiltros.Visible = False
        Catch ex As Exception
            Throw New InvalidOperationException("No fue posible agregar" & ex.Message)
        End Try
    End Sub

    Private Sub chTodos_Click(sender As Object, e As EventArgs) Handles chTodos.Click
        Dim parametro As Integer
        If chTodos.Checked = True Then
            parametro = 1
        Else
            parametro = 0
        End If
        Actualizar_todo(parametro)
    End Sub
    Private Sub Actualizar_todo(ByVal parametro As Integer)
        Try
            Dim _TotalFilas As Integer
            Dim columnaSeleccionada As Integer = dgv_Filtros.CurrentCell.ColumnIndex
            Dim filaSeleccionada As Integer = dgv_Filtros.CurrentRow.Index
            Dim contador As Integer = 0
            _TotalFilas = dgv_Filtros.RowCount


            If parametro = 0 Then
                For x = 0 To _TotalFilas - 1
                    If dgv_Filtros.Item("colActivo", contador).Value = False Then
                        dgv_Filtros.Item("colActivo", contador).Value = True
                    Else
                        dgv_Filtros.Item("colActivo", contador).Value = False

                    End If
                    contador = contador + 1
                Next
            End If


            If parametro = 1 Then
                For x = 0 To _TotalFilas - 1
                    If dgv_Filtros.Item("colActivo", contador).Value = False Or dgv_Filtros.Item("colActivo", contador).Value = True Then
                        dgv_Filtros.Item("colActivo", contador).Value = True
                    Else
                        dgv_Filtros.Item("colActivo", contador).Value = False

                    End If
                    contador = contador + 1
                Next
            End If

            If parametro = 2 Then
                For x = 0 To _TotalFilas - 1
                    If dgv_Filtros.Item("colActivo", contador).Value = True Then
                        dgv_Filtros.Item("colActivo", contador).Value = True

                    End If
                    contador = contador + 1
                Next

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class