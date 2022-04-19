Imports MySql.Data.MySqlClient

Public Class Frmdetalleespacio
    Dim consulta As String = String.Empty
    Dim dsespacios As New DataSet
    Dim dsventascompras As New DataSet
    Dim dsmontofijo As New DataSet
    Dim idproveedor As Integer
    Dim idMonedaD, idmonedaQ As Integer
    Dim filtro_pagocompra As String
    Dim filtro_pagotraslado As String
    Dim filtro_pagoespacios As String
    Dim filtro_pagoventa As String
    Dim filtro_pagomonto As String
    Dim _lista_sucursales
    Dim porcentaje_contrato As Decimal


    Private Sub Frmdetalleespacio_Load(sender As Object, e As EventArgs) Handles Me.Load
        _lista_sucursales = lista_tiendas
        CargarConexionContratos()
        idMonedaD = conexion.EjecutarEscalar("SELECT idmoneda FROM tipomoneda_bmltx WHERE tipomoneda='Dolares'")
        idmonedaQ = conexion.EjecutarEscalar("SELECT idmoneda FROM tipomoneda_bmltx WHERE tipomoneda='Quetzales'")

        llenartextos(txtcontrato1, txtfinal1, txtmesI1, txtproveedor1)
        llenartextos(txtcontrato, txtmesfinal, txtmesinicial, txtproveedor)
        llenartextos(txtcontrato2, txtmesf2, txtmesI2, txtproveedor2)
        Me.Cursor = Cursors.WaitCursor

        If lista_tiendas = String.Empty Then
            filtro_pagocompra = String.Empty
            filtro_pagotraslado = String.Empty
            filtro_pagoespacios = String.Empty
            filtro_pagoventa = String.Empty
            filtro_pagomonto = String.Empty
        Else
            filtro_pagocompra = " AND pagocompras.Idtienda IN (" & _lista_sucursales & ")"
            filtro_pagoespacios = " AND pagoespacio.Idtienda IN (" & _lista_sucursales & ")"
            filtro_pagoventa = " AND pagoventas.Idtienda IN (" & _lista_sucursales & ")"
            filtro_pagomonto = " AND pagomonto.Idtienda IN (" & _lista_sucursales & ")"
            filtro_pagotraslado = " AND traslados.Idtienda IN (" & _lista_sucursales & ")"

        End If




        Try
            'SE CARGARA EL DETALLE DE LOS ESPACIOS SEGUN SE HAYA INGRESADO EN EL FORMULARIO DE REPORTES PRINCIPAL
#Region "Buscar por contrato sin consolidar/consolidado"
            If txtcontrato.Text.Length > 0 And txtcontrato.Text <> "Todos los Contratos" And chcontrato = True Then



                porcentaje_contrato = conexion.EjecutarEscalar("SELECT cantidadecobro FROM detalle_contratobmltx WHERE idcontrato='" & txtcontrato.Text & "'")

                dsespacios = conexion.llenarDataSet(" SELECT pagoespacio.`Idcontrato` AS contrato,contrato.`IdNombreProveedor` AS proveedor,  sucursales_bmltx.`Nombre` AS tienda, " &
                                                    "parametros_tiempo.`nombre` AS mes, año.`valor` AS id,tipoespacio_bmltx.`Nombre` AS tipoespacio, " &
                                                    " pagoespacio.`cantidadespacio` AS cantidaespacio,  " &
                                                     " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (costoespacio) ELSE 0.00 END),2))AS tarifadolares, " &
                                                    " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (costoespacio) ELSE 0.00 END),2))AS tarifaquetzales, " &
                                                    " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales  " &
                                                    " FROM parametros_pagoespacio_bmltx AS pagoespacio  " &
                                                    " INNER JOIN tipoespacio_bmltx  " &
                                                    " ON tipoespacio_bmltx.`Idtipoespacio`=pagoespacio.`Idespacio`  " &
                                                    " INNER JOIN sucursales_bmltx  " &
                                                    " ON sucursales_bmltx.`Id`=pagoespacio.`Idtienda`  " &
                                                    " INNER JOIN parametros_año AS año  " &
                                                    " ON año.`id`=pagoespacio.`idaño`  " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo`=pagoespacio.`idmes` " &
                                                    " INNER JOIN contratos_backmarginltx AS contrato  " &
                                                    " ON contrato.`IdContrato`=pagoespacio.`Idcontrato`  " &
                                                    " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                                    " WHERE pagoespacio.`Idcontrato`='" & txtcontrato.Text & "' AND pagoespacio.`fechafinal` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' " & filtro_pagoespacios & " " &
                                                    " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idmes`,pagoespacio.`Idtienda`  " &
                                                    " ORDER BY pagoespacio.`idaño` ASC, pagoespacio.`idmes` ASC")

                dsmontofijo = conexion.llenarDataSet("SELECT  pagomonto.`Idcontrato` AS contrato, contrato.`IdNombreProveedor` AS proveedor,pagomonto.`Sku` AS sku,pagomonto.`Descripcion` AS descripcion, " &
                                                " sucursales_bmltx.`Nombre` AS tienda,parametros_tiempo.`nombre` AS mes, año.`valor`AS id, " &
                                                " CONCAT('$. ',FORMAT(SUM(CASE WHEN  pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,   " &
                                                " CONCAT('Q. ',FORMAT(SUM(CASE WHEN  pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales  " &
                                                " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                                " INNER JOIN parametros_tiempo  " &
                                                " ON parametros_tiempo.`id_tiempo` = pagomonto.`idMes` " &
                                                " INNER JOIN sucursales_bmltx " &
                                                " ON sucursales_bmltx.`Id`= pagomonto.`idTienda` " &
                                                " INNER JOIN parametros_año AS año " &
                                                " ON año.`id` = pagomonto.`idaño` " &
                                                 " INNER JOIN contratos_backmarginltx AS contrato " &
                                                " ON contrato.`IdContrato`=pagomonto.`Idcontrato` " &
                                                 " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                                " WHERE pagomonto.`fechafin` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' AND pagomonto.`Idcontrato`= '" & txtcontrato.Text & "' " & filtro_pagomonto & "  " &
                                                " GROUP BY pagomonto.`idMes`,pagomonto.`idTienda`,pagomonto.`Sku`, pagomonto.Descripcion " &
                                                " ORDER BY pagomonto.`idaño` ASC,pagomonto.`idMes` ASC,pagomonto.`Idcontrato` ASC")




                dsventascompras = conexion.llenarDataSet("SELECT contrato, " &
                                                        " sku,  " &
                                                        " descripcion, " &
                                                        " tienda,  " &
                                                        " id,  " &
                                                        " mes,    " &
                                                        " FORMAT(unidadesvendidas, 2) AS 'unidadesvendidas',  " &
                                                        " CONCAT('Q. ', FORMAT(ventas, 2)) AS 'ventas',  " &
                                                        " FORMAT(unidadcomprada,2) AS 'unidadescomprada',    " &
                                                        " CONCAT('Q. ', FORMAT(compras, 2)) AS 'compras',    " &
                                                        " FORMAT(porcentaje,2) AS 'porcentaje'  " &
                                                        " FROM   (SELECT    pagoventas.`IdContrato` AS contrato,  " &
                                                        " pagoventas.`Sku` AS sku,  pagoventas.`Descripcion` AS descripcion,   " &
                                                        " sucursales_bmltx.`Nombre` AS tienda,    " &
                                                        " parametros_tiempo.`nombre` AS mes,    " &
                                                        " año.`valor` AS id,   " &
                                                        " pagoventas.`UnidadVendida` AS unidadesvendidas, " &
                                                        " pagoventas.`Ventas` AS ventas, " &
                                                        " '' AS unidadcomprada, " &
                                                        " '' AS compras, " &
                                                        " ''AS porcentaje " &
                                                        " FROM parametros_pagoventa_bmltx AS pagoventas " &
                                                        " INNER JOIN parametros_tiempo " &
                                                        " ON parametros_tiempo.`id_tiempo` = pagoventas.`idMes`  " &
                                                        " INNER JOIN parametros_año AS año " &
                                                        " ON año.`id` = pagoventas.`idaño` " &
                                                        " INNER JOIN sucursales_bmltx " &
                                                        " ON sucursales_bmltx.`Id` = pagoventas.`IdTienda` " &
                                                        " WHERE pagoventas.`IdContrato` = '" & txtcontrato.Text & "' " & filtro_pagoventa & " " &
                                                        " AND pagoventas.`FechaFinal` BETWEEN  '" & fechainicialR & "'  AND '" & fechafinalR & "' AND pagoventas.`UnidadVendida`>0 " &
                                                        " UNION ALL    " &
                                                        " SELECT    pagocompras.idcontrato AS contrato, pagocompras.`Sku` AS sku,  " &
                                                        " pagocompras.`Descripcion` AS descripcion,   " &
                                                        "  sucursales_bmltx.`Nombre` AS tienda,    " &
                                                        "  parametros_tiempo.`nombre` AS mes,    año.`valor` AS id,           " &
                                                        " '' AS unidadvendida, '' AS ventas, " &
                                                        " pagocompras.`Unidadcomprada` AS unidadescompradas, " &
                                                        " pagocompras.`compras`, " &
                                                        " pagocompras.porcentaje AS porcentaje" &
                                                        " FROM    parametros_pagocompras_bmltx AS pagocompras " &
                                                        " INNER JOIN parametros_tiempo " &
                                                        " ON parametros_tiempo.`id_tiempo` = pagocompras.`idMes` " &
                                                        " INNER JOIN parametros_año AS año " &
                                                        " ON año.`id` = pagocompras.`idaño` " &
                                                        " INNER JOIN sucursales_bmltx " &
                                                        " ON sucursales_bmltx.`Id` = pagocompras.`Idtienda` " &
                                                        " WHERE pagocompras.`IdContrato` =  '" & txtcontrato.Text & "' " & filtro_pagocompra & "      " &
                                                        " AND pagocompras.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' AND pagocompras.`Unidadcomprada`>0 " &
                                                        " UNION ALL    " &
                                                        " SELECT    traslados.idcontrato AS contrato, traslados.`Sku` AS sku,   traslados.`Descripcion` AS descripcion,    " &
                                                        " sucursales_bmltx.`Nombre` AS tienda, parametros_tiempo.`nombre` AS mes,    año.`valor` AS id, " &
                                                        " '' AS unidadvendida,'' AS ventas,     traslados.`UnidadTrasladada` AS unidadescompradas,  " &
                                                        " traslados.`Traslado`*-1 AS compras,      " & porcentaje_contrato & " AS porcentaje FROM    parametros_pagotraslados_bmltx AS traslados " &
                                                        " INNER JOIN parametros_tiempo ON parametros_tiempo.`id_tiempo` = traslados.`idMes` " &
                                                        " INNER JOIN parametros_año AS año       ON año.`id` = traslados.`idaño` " &
                                                        " INNER JOIN sucursales_bmltx ON sucursales_bmltx.`Id` = traslados.`Idtienda` " &
                                                        " WHERE traslados.`IdContrato` =  '" & txtcontrato.Text & "' " & filtro_pagotraslado & " " &
                                                        " AND traslados.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "'   AND traslados.`UnidadTrasladada`>0 " &
                                                        " ) tmp " &
                                                        " ORDER BY ABS(sku)")



                dgvDingresos.DataSource = dsventascompras.Tables(0)
                dgvDespacio.DataSource = dsespacios.Tables(0)
                dgvDmonto.DataSource = dsmontofijo.Tables(0)
#End Region

#Region "BUSCA POR PROVEEDOR SIN CONSOLIDAD/CONSOLIDADO "
            ElseIf txtproveedor.Text.Length > 0 And txtproveedor.Text <> "TODOS LOS PROVEEDORES" And chproveedor = True Then
                '=========================================================================================================================================================
                'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                CargarConexionProductos()
                'idproveedor = conexion.EjecutarEscalar("SELECT id FROM proveedores WHERE Nombre='" & txtproveedor.Text & "'")
                idproveedor = conexionpmm.EjecutarEscalar("SELECT idproveedor as ""id"" FROM unipmm.unisuperproveedor where nombre_proveedor='" & txtproveedor.Text & "'")
                '=========================================================================================================================================================
                'REGRESAMOS A LA CONEXION DE LOS CONTRATOS..
                CargarConexionContratos()
                dsespacios.Clear()
                dsmontofijo.Clear()
                dsventascompras.Clear()
                dsespacios = conexion.llenarDataSet("SELECT pagoespacio.`Idcontrato` AS contrato,contrato.`IdNombreProveedor` AS proveedor, sucursales_bmltx.`Nombre` AS tienda, " &
                                                    " parametros_tiempo.`nombre` AS mes, año.`valor` AS id,tipoespacio_bmltx.`Nombre` AS tipoespacio, " &
                                                    " pagoespacio.`cantidadespacio` AS cantidaespacio, " &
                                                    " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (costoespacio) ELSE 0.00 END),2))AS tarifadolares, " &
                                                    " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (costoespacio) ELSE 0.00 END),2))AS tarifaquetzales, " &
                                                    " CONCAT('$. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(SUM(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales  " &
                                                    " FROM parametros_pagoespacio_bmltx AS pagoespacio " &
                                                    " INNER JOIN tipoespacio_bmltx " &
                                                    " ON tipoespacio_bmltx.`Idtipoespacio`=pagoespacio.`Idespacio` " &
                                                    " INNER JOIN sucursales_bmltx " &
                                                    " ON sucursales_bmltx.`Id`=pagoespacio.`Idtienda` " &
                                                    " INNER JOIN parametros_año AS año " &
                                                    "  ON año.`id`=pagoespacio.`idaño` " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo`=pagoespacio.`idMes` " &
                                                    " INNER JOIN contratos_backmarginltx AS contrato " &
                                                    " ON contrato.`IdContrato`=pagoespacio.`Idcontrato` " &
                                                     " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                                    " WHERE pagoespacio.`idproveedor`='" & idproveedor & "'AND contrato.`estado`=1  AND pagoespacio.`fechafinal` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' " & filtro_pagoespacios & " " &
                                                    " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idMes`,pagoespacio.`Idtienda` " &
                                                    " ORDER BY pagoespacio.`idaño` ASC, pagoespacio.`idMes` ASC,pagoespacio.`Idcontrato` ASC")

                dsmontofijo = conexion.llenarDataSet("SELECT pagomonto.`Idcontrato` AS contrato, contrato.`IdNombreProveedor` AS proveedor, " &
                                                    " pagomonto.`Sku` AS sku,pagomonto.`Descripcion` AS descripcion, " &
                                                    " sucursales_bmltx.`Nombre` AS tienda,parametros_tiempo.`nombre` AS mes, año.`valor`AS id, " &
                                                    " CONCAT('$. ',FORMAT(SUM(CASE WHEN  pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END),2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(SUM(CASE WHEN  pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END),2))AS totalquetzales  " &
                                                    " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo` = pagomonto.`idMes` " &
                                                    " INNER JOIN sucursales_bmltx " &
                                                    " ON sucursales_bmltx.`Id`= pagomonto.`idTienda` " &
                                                    " INNER JOIN parametros_año AS año " &
                                                    " ON año.`id` = pagomonto.`idaño` " &
                                                    " INNER JOIN contratos_backmarginltx AS contrato " &
                                                    " ON contrato.`IdContrato`=pagomonto.`Idcontrato` " &
                                                    " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                                    " WHERE pagomonto.`fechafin` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' AND pagomonto.`idproveedor`= '" & idproveedor & "' " & filtro_pagomonto & " " &
                                                    " AND contrato.`estado`=1 " &
                                                    " GROUP BY pagomonto.`idMes`,pagomonto.`idTienda`,pagomonto.`Sku`, pagomonto.Descripcion " &
                                                    " ORDER BY pagomonto.`idaño` ASC,pagomonto.`idMes` ASC,pagomonto.`Idcontrato` ASC")

                dsventascompras = conexion.llenarDataSet("SELECT contrato,proveedor, " &
                                                " sku,  " &
                                                " descripcion, " &
                                                " tienda,  " &
                                                " id,  " &
                                                " mes,    " &
                                                " FORMAT(unidadesvendidas, 2) AS 'unidadesvendidas',  " &
                                                " CONCAT('Q. ', FORMAT(ventas, 2)) AS 'ventas',  " &
                                                " FORMAT(unidadcomprada,2) AS 'unidadescomprada',    " &
                                                " CONCAT('Q. ', FORMAT(compras, 2)) AS 'compras',    " &
                                                " FORMAT(porcentaje,2) AS 'porcentaje' " &
                                                " FROM   (SELECT    pagoventas.`IdContrato` AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor,  " &
                                                " pagoventas.`Sku` AS sku,  pagoventas.`Descripcion` AS descripcion,   " &
                                                " sucursales_bmltx.`Nombre` AS tienda,    " &
                                                             " año.`valor` AS id,   " &
                                                " parametros_tiempo.`nombre` AS mes,    " &
                                                " pagoventas.`UnidadVendida` AS unidadesvendidas,   " &
                                                " pagoventas.`Ventas` AS ventas,        " &
                                                " '' AS unidadcomprada,         " &
                                                " '' AS compras,'' AS porcentaje    " &
                                                "  FROM    parametros_pagoventa_bmltx AS pagoventas     " &
                                                " INNER JOIN parametros_tiempo       " &
                                                " ON parametros_tiempo.`id_tiempo` = pagoventas.`idMes`  " &
                                                " INNER JOIN parametros_año AS año     " &
                                                " ON año.`id` = pagoventas.`idaño`    " &
                                                " INNER JOIN sucursales_bmltx      " &
                                                " ON sucursales_bmltx.`Id` = pagoventas.`IdTienda`     " &
                                                 " INNER JOIN contratos_backmarginltx  " &
                                                "  ON contratos_backmarginltx.`IdContrato`=pagoventas.`IdContrato`  " &
                                                " WHERE pagoventas.`IdProveedor`= " & idproveedor & "  " &
                                                " AND pagoventas.`FechaFinal` BETWEEN  '" & fechainicialR & "'  AND '" & fechafinalR & "'  " & filtro_pagoventa & " AND pagoventas.`UnidadVendida`>0    " &
                                                " UNION ALL    " &
                                                " SELECT    pagocompras.idcontrato AS contrato, contratos_backmarginltx.`IdNombreProveedor` AS proveedor, pagocompras.`Sku` AS sku,  " &
                                                " pagocompras.`Descripcion` AS descripcion,   " &
                                                "  sucursales_bmltx.`Nombre` AS tienda,    " &
                                                "  año.`valor` AS id,   parametros_tiempo.`nombre` AS mes,            " &
                                                "  '' AS unidadvendida,'' AS ventas,    " &
                                                " pagocompras.`Unidadcomprada` AS unidadescompradas,    " &
                                                " pagocompras.`compras`,    " &
                                                " pagocompras.porcentaje AS porcentaje" &
                                                " FROM    parametros_pagocompras_bmltx AS pagocompras    " &
                                                " INNER JOIN parametros_tiempo     " &
                                                " ON parametros_tiempo.`id_tiempo` = pagocompras.`idMes`     " &
                                                " INNER JOIN parametros_año AS año      " &
                                                " ON año.`id` = pagocompras.`idaño`     " &
                                                " INNER JOIN sucursales_bmltx        " &
                                                " ON sucursales_bmltx.`Id` = pagocompras.`Idtienda`     " &
                                                " INNER JOIN contratos_backmarginltx  " &
                                                "  ON contratos_backmarginltx.`IdContrato`=pagocompras.`IdContrato`   " &
                                                " WHERE   pagocompras.idproveedor=" & idproveedor & " " &
                                                " AND pagocompras.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagocompra & " AND pagocompras.`Unidadcomprada`>0 " &
                                                " UNION ALL    " &
                                                " SELECT    traslados.idcontrato AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor, traslados.`Sku` AS sku, " &
                                                " traslados.`Descripcion` AS descripcion,    " &
                                                " sucursales_bmltx.`Nombre` AS tienda, " &
                                                " año.`valor` AS id, parametros_tiempo.`nombre` AS mes," &
                                                " '' AS unidadvendida,'' AS ventas, " &
                                                " traslados.`UnidadTrasladada` AS unidadescompradas,  " &
                                                " traslados.`Traslado`*-1 AS compras, " &
                                                " 0.00 AS porcentaje " &
                                                " FROM    parametros_pagotraslados_bmltx AS traslados " &
                                                " INNER JOIN parametros_tiempo ON parametros_tiempo.`id_tiempo` = traslados.`idMes` " &
                                                " INNER JOIN parametros_año AS año       ON año.`id` = traslados.`idaño` " &
                                                " INNER JOIN sucursales_bmltx ON sucursales_bmltx.`Id` = traslados.`Idtienda` " &
                                                " INNER JOIN contratos_backmarginltx    ON contratos_backmarginltx.`IdContrato`=traslados.`IdContrato`  " &
                                                " WHERE traslados .idproveedor=   " & idproveedor & " " & filtro_pagotraslado & " " &
                                                " AND traslados.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "'   AND traslados.`UnidadTrasladada`>0 " &
                                                " ) tmp         " &
                                                " ORDER BY ABS(sku)")
                dgvDingresos.DataSource = dsventascompras.Tables(0)
                dgvDespacio.DataSource = dsespacios.Tables(0)
                dgvDmonto.DataSource = dsmontofijo.Tables(0)
#End Region
#Region "BUSCA TODOS LOS CONTRATOS SIN COSOLIDAR /CONSOLIDADOS"


            ElseIf txtcontrato.Text.Length > 0 And txtcontrato.Text = "Todos los Contratos" And chcontrato = True Then
                dsespacios.Clear()
                dsmontofijo.Clear()
                dsventascompras.Clear()
                dsespacios = conexion.llenarDataSet("SELECT pagoespacio.`Idcontrato` AS contrato,contrato.`IdNombreProveedor` AS proveedor,  sucursales_bmltx.`Nombre` AS tienda,parametros_tiempo.`nombre` AS mes, año.`valor` AS id,tipoespacio_bmltx.`Nombre` AS tipoespacio, " &
                                                    " pagoespacio.`cantidadespacio` AS cantidaespacio, " &
                                                     " CONCAT('$. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (costoespacio) ELSE 0.00 END,2))AS tarifadolares, " &
                                                    " CONCAT('Q. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (costoespacio) ELSE 0.00 END,2))AS tarifaquetzales, " &
                                                    " CONCAT('$. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END,2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END,2))AS totalquetzales  " &
                                                    " FROM parametros_pagoespacio_bmltx AS pagoespacio " &
                                                    " INNER JOIN tipoespacio_bmltx " &
                                                    " ON tipoespacio_bmltx.`Idtipoespacio`=pagoespacio.`Idespacio` " &
                                                    " INNER JOIN sucursales_bmltx " &
                                                    " ON sucursales_bmltx.`Id`=pagoespacio.`Idtienda` " &
                                                    " INNER JOIN parametros_año AS año " &
                                                    " ON año.`id`=pagoespacio.`idaño` " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo`=pagoespacio.`idMes` " &
                                                    " INNER JOIN contratos_backmarginltx AS contrato " &
                                                    " ON contrato.`IdContrato`=pagoespacio.`Idcontrato` " &
                                                    " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                                    " WHERE   contrato.`estado`=1   AND  pagoespacio.`fechafinal` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' " & filtro_pagoespacios & " " &
                                                    " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idMes`,pagoespacio.`Idtienda` " &
                                                    " ORDER BY pagoespacio.`idaño` ASC, pagoespacio.`idMes` ASC")




                dsmontofijo = conexion.llenarDataSet("SELECT  pagomonto.`Idcontrato` AS contrato, contrato.`IdNombreProveedor` AS proveedor,pagomonto.`Sku` AS sku,detalle.`Descripcion` AS descripcion, " &
                                                    " sucursales_bmltx.`Nombre` AS tienda,parametros_tiempo.`nombre` AS mes, año.`valor`AS id, " &
                                                    " CONCAT('$. ',FORMAT(CASE WHEN  pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END,2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(CASE WHEN  pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END,2))AS totalquetzales  " &
                                                    " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo` = pagomonto.`idMes` " &
                                                    " INNER JOIN sucursales_bmltx " &
                                                    " ON sucursales_bmltx.`Id`= pagomonto.`idTienda` " &
                                                    " INNER JOIN parametros_año AS año " &
                                                    " ON año.`id` = pagomonto.`idaño` " &
                                                     " INNER JOIN contratos_backmarginltx AS contrato " &
                                                    " ON contrato.`IdContrato`=pagomonto.`Idcontrato` " &
                                                    " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                                    " INNER JOIN detalle_contratobmltx AS detalle " &
                                                    " ON detalle.`IdContrato`=contrato.`IdContrato` " &
                                                    " WHERE pagomonto.`fechafin` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' " & filtro_pagomonto & "  " &
                                                    " AND contrato.`estado`=1  " &
                                                    " GROUP BY pagomonto.`idMes`,pagomonto.`idTienda`,pagomonto.`Sku`, pagomonto.Descripcion " &
                                                    " ORDER BY pagomonto.`idaño` ASC,pagomonto.`idMes` ASC,pagomonto.`Idcontrato` ASC")


                'dsventascompras = conexion.llenarDataSet("SELECT contrato, " &
                '                                        " sku,  " &
                '                                        " descripcion, " &
                '                                        " tienda,  " &
                '                                        " id,  " &
                '                                        " mes,    " &
                '                                        " FORMAT(unidadesvendidas, 2) AS 'unidadesvendidas',  " &
                '                                        " CONCAT('Q. ', FORMAT(ventas, 2)) AS 'ventas',  " &
                '                                        " FORMAT(unidadcomprada,2) AS 'unidadescomprada',    " &
                '                                        " CONCAT('Q. ', FORMAT(compras, 2)) AS 'compras',    " &
                '                                        " FORMAT(porcentaje,2) AS 'porcentaje'  " &
                '                                        " FROM   (SELECT    pagoventas.`IdContrato` AS contrato,  " &
                '                                        " pagoventas.`Sku` AS sku,  pagoventas.`Descripcion` AS descripcion,   " &
                '                                        " sucursales_bmltx.`Nombre` AS tienda,    " &
                '                                        " parametros_tiempo.`nombre` AS mes,    " &
                '                                        " año.`valor` AS id,   " &
                '                                        " pagoventas.`UnidadVendida` AS unidadesvendidas,   " &
                '                                        " pagoventas.`Ventas` AS ventas,        " &
                '                                        "                '' AS unidadcomprada,         " &
                '                                        "                '' AS compras,    " &
                '                                        "                ''AS porcentaje          " &
                '                                        "  FROM    parametros_pagoventa_bmltx AS pagoventas     " &
                '                                        " INNER JOIN parametros_tiempo       " &
                '                                        " ON parametros_tiempo.`id_tiempo` = pagoventas.`idMes`  " &
                '                                        " INNER JOIN parametros_año AS año     " &
                '                                        " ON año.`id` = pagoventas.`idaño`    " &
                '                                        " INNER JOIN sucursales_bmltx      " &
                '                                        " ON sucursales_bmltx.`Id` = pagoventas.`IdTienda`     " &
                '                                        " WHERE   pagoventas.`FechaFinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagoventa & "    " &
                '                                        "                UNION    " &
                '                                        " SELECT    pagocompras.idcontrato AS contrato, pagocompras.`Sku` AS sku,  " &
                '                                        " pagocompras.`Descripcion` AS descripcion,   " &
                '                                        "  sucursales_bmltx.`Nombre` AS tienda,    " &
                '                                        "  parametros_tiempo.`nombre` AS mes,    año.`valor` AS id,           " &
                '                                        "                '' AS unidadvendida,            '' AS ventas,    " &
                '                                        " pagocompras.`Unidadcomprada` AS unidadescompradas,    " &
                '                                        " pagocompras.`compras`,    " &
                '                                        " pagocompras.porcentaje AS porcentaje" &
                '                                        " FROM    parametros_pagocompras_bmltx AS pagocompras    " &
                '                                        " INNER JOIN parametros_tiempo     " &
                '                                        " ON parametros_tiempo.`id_tiempo` = pagocompras.`idMes`     " &
                '                                        " INNER JOIN parametros_año AS año      " &
                '                                        " ON año.`id` = pagocompras.`idaño`     " &
                '                                        " INNER JOIN sucursales_bmltx        " &
                '                                        " ON sucursales_bmltx.`Id` = pagocompras.`Idtienda`     " &
                '                                        " WHERE pagocompras.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagocompra & "      " &
                '                                        " ) tmp         " &
                '                                        " GROUP BY contrato, sku,   mes, id")
                dsventascompras = conexion.llenarDataSet("SELECT contrato, " &
                                                        " sku,  " &
                                                        " descripcion, " &
                                                        " tienda,  " &
                                                        " id,  " &
                                                        " mes,    " &
                                                        " FORMAT(unidadesvendidas, 2) AS 'unidadesvendidas',  " &
                                                        " CONCAT('Q. ', FORMAT(ventas, 2)) AS 'ventas',  " &
                                                        " FORMAT(unidadcomprada,2) AS 'unidadescomprada',    " &
                                                        " CONCAT('Q. ', FORMAT(compras, 2)) AS 'compras',    " &
                                                        " FORMAT(porcentaje,2) AS 'porcentaje'  " &
                                                        " FROM   (SELECT    pagoventas.`IdContrato` AS contrato,  " &
                                                        " pagoventas.`Sku` AS sku,  pagoventas.`Descripcion` AS descripcion,   " &
                                                        " sucursales_bmltx.`Nombre` AS tienda,    " &
                                                        " parametros_tiempo.`nombre` AS mes,    " &
                                                        " año.`valor` AS id,   " &
                                                        " pagoventas.`UnidadVendida` AS unidadesvendidas,   " &
                                                        " pagoventas.`Ventas` AS ventas,        " &
                                                        " '' AS unidadcomprada,         " &
                                                        " '' AS compras,    " &
                                                        " ''AS porcentaje          " &
                                                        "  FROM    parametros_pagoventa_bmltx AS pagoventas     " &
                                                        " INNER JOIN parametros_tiempo       " &
                                                        " ON parametros_tiempo.`id_tiempo` = pagoventas.`idMes`  " &
                                                        " INNER JOIN parametros_año AS año     " &
                                                        " ON año.`id` = pagoventas.`idaño`    " &
                                                        " INNER JOIN sucursales_bmltx      " &
                                                        " ON sucursales_bmltx.`Id` = pagoventas.`IdTienda`     " &
                                                        " WHERE pagoventas.`FechaFinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagoventa & "     " &
                                                        " UNION ALL " &
                                                        " SELECT    pagocompras.idcontrato AS contrato, pagocompras.`Sku` AS sku,  " &
                                                        " pagocompras.`Descripcion` AS descripcion,   " &
                                                        "  sucursales_bmltx.`Nombre` AS tienda,    " &
                                                        "  parametros_tiempo.`nombre` AS mes,    año.`valor` AS id,           " &
                                                        "                '' AS unidadvendida,            '' AS ventas,    " &
                                                        " pagocompras.`Unidadcomprada` AS unidadescompradas,    " &
                                                        " pagocompras.`compras`,   " &
                                                        " pagocompras.porcentaje AS porcentaje" &
                                                        " FROM    parametros_pagocompras_bmltx AS pagocompras    " &
                                                        " INNER JOIN parametros_tiempo     " &
                                                        " ON parametros_tiempo.`id_tiempo` = pagocompras.`idMes`     " &
                                                        " INNER JOIN parametros_año AS año      " &
                                                        " ON año.`id` = pagocompras.`idaño`     " &
                                                        " INNER JOIN sucursales_bmltx        " &
                                                        " ON sucursales_bmltx.`Id` = pagocompras.`Idtienda`     " &
                                                        " WHERE pagocompras.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' AND pagocompras.`Unidadcomprada`>0 " & filtro_pagocompra & "      " &
                                                         "                UNION ALL    " &
                                                        " SELECT    traslados.idcontrato AS contrato, traslados.`Sku` AS sku,   traslados.`Descripcion` AS descripcion,    " &
                                                        " sucursales_bmltx.`Nombre` AS tienda,      parametros_tiempo.`nombre` AS mes,    año.`valor` AS id, " &
                                                        " '' AS unidadvendida,'' AS ventas,     traslados.`UnidadTrasladada` AS unidadescompradas,  " &
                                                        " traslados.`Traslado`*-1 AS compras,     0.00 AS porcentaje FROM    parametros_pagotraslados_bmltx AS traslados " &
                                                        " INNER JOIN parametros_tiempo ON parametros_tiempo.`id_tiempo` = traslados.`idMes` " &
                                                        " INNER JOIN parametros_año AS año       ON año.`id` = traslados.`idaño` " &
                                                        " INNER JOIN sucursales_bmltx ON sucursales_bmltx.`Id` = traslados.`Idtienda` " &
                                                        " WHERE traslados.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "'   AND traslados.`UnidadTrasladada`>0  " & filtro_pagotraslado & " " &
                                                        " ) tmp " &
                                                        " ORDER BY ABS(sku)")
                dgvDmonto.DataSource = dsmontofijo.Tables(0)
                dgvDingresos.DataSource = dsventascompras.Tables(0)
                dgvDespacio.DataSource = dsespacios.Tables(0)
#End Region
#Region "BUSCA POR TODOS LOS PROVEEDORES SIN COSOLIDAR /CONSOLIDADOS "


            ElseIf txtproveedor.Text.Length > 0 And txtproveedor.Text = "TODOS LOS PROVEEDORES" And chproveedor = True Then
                '=========================================================================================================================================================
                'CAMBIAMOS DE CONEXION AL 223 PARA OBTENER EL ID DEL PROVEEDOR
                'YA NO SE USARA LA CONEXION AL 223 SE CAMBIA A PMM LBOCH ENERO 2021
                CargarConexionProductos()
                'idproveedor = conexion.EjecutarEscalar("SELECT id FROM proveedores WHERE Nombre='" & txtproveedor.Text & "'")
                idproveedor = conexionpmm.EjecutarEscalar("SELECT idproveedor  FROM unipmm.unisuperproveedor where nombre_proveedor='" & txtproveedor.Text & "'")
                '=========================================================================================================================================================
                'REGRESAMOS A LA CONEXION DE LOS CONTRATOS..
                CargarConexionContratos()
                dsespacios.Clear()
                dsmontofijo.Clear()
                dsventascompras.Clear()

                dsespacios = conexion.llenarDataSet("SELECT pagoespacio.`Idcontrato` AS contrato,contrato.`IdNombreProveedor` AS proveedor, sucursales_bmltx.`Nombre` AS tienda, " &
                                                    " parametros_tiempo.`nombre` AS mes, año.`valor` AS id,tipoespacio_bmltx.`Nombre` AS tipoespacio, " &
                                                    " pagoespacio.`cantidadespacio` AS cantidaespacio, " &
                                                     " CONCAT('$. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (costoespacio) ELSE 0.00 END,2))AS tarifadolares, " &
                                                    " CONCAT('Q. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (costoespacio) ELSE 0.00 END,2))AS tarifaquetzales, " &
                                                    " CONCAT('$. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END,2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(CASE WHEN pagoespacio.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END,2))AS totalquetzales  " &
                                                    " FROM parametros_pagoespacio_bmltx AS pagoespacio " &
                                                    " INNER JOIN tipoespacio_bmltx " &
                                                    " ON tipoespacio_bmltx.`Idtipoespacio`=pagoespacio.`Idespacio` " &
                                                    " INNER JOIN sucursales_bmltx " &
                                                    " ON sucursales_bmltx.`Id`=pagoespacio.`Idtienda` " &
                                                    " INNER JOIN parametros_año AS año " &
                                                    "  ON año.`id`=pagoespacio.`idaño` " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo`=pagoespacio.`idMes` " &
                                                    " INNER JOIN contratos_backmarginltx AS contrato " &
                                                    " ON contrato.`IdContrato`=pagoespacio.`Idcontrato` " &
                                                     " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagoespacio.`idtipomoneda` " &
                                                    " WHERE  contrato.`estado`=1  AND   pagoespacio.`fechafinal` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' " & filtro_pagoespacios & " " &
                                                    " GROUP BY pagoespacio.`Idespacio`,pagoespacio.`idMes`,pagoespacio.`Idtienda` " &
                                                    " ORDER BY pagoespacio.`idaño` ASC, pagoespacio.`idMes` ASC,pagoespacio.`Idcontrato` ASC")

                dsmontofijo = conexion.llenarDataSet("SELECT pagomonto.`Idcontrato` AS contrato, contrato.`IdNombreProveedor` AS proveedor, " &
                                                    " pagomonto.`Sku` AS sku,pagomonto.`Descripcion` AS descripcion, " &
                                                    " sucursales_bmltx.`Nombre` AS tienda,parametros_tiempo.`nombre` AS mes, año.`valor`AS id, " &
                                                   " CONCAT('$. ',FORMAT(CASE WHEN  pagomonto.`idtipomoneda`=" & idMonedaD & " THEN (Total) ELSE 0.00 END,2))AS totaldolares,   " &
                                                    " CONCAT('Q. ',FORMAT(CASE WHEN  pagomonto.`idtipomoneda`=" & idmonedaQ & " THEN (Total) ELSE 0.00 END,2))AS totalquetzales  " &
                                                    " FROM parametros_pagomontofijo_bmltx AS pagomonto " &
                                                    " INNER JOIN parametros_tiempo  " &
                                                    " ON parametros_tiempo.`id_tiempo` = pagomonto.`idMes` " &
                                                    " INNER JOIN sucursales_bmltx " &
                                                    " ON sucursales_bmltx.`Id`= pagomonto.`idTienda` " &
                                                    " INNER JOIN parametros_año AS año " &
                                                    " ON año.`id` = pagomonto.`idaño` " &
                                                    " INNER JOIN contratos_backmarginltx AS contrato " &
                                                    " ON contrato.`IdContrato`=pagomonto.`Idcontrato` " &
                                                    " INNER JOIN tipomoneda_bmltx AS moneda " &
                                                    " ON moneda.`Idmoneda`=pagomonto.`idtipomoneda` " &
                                                    " WHERE pagomonto.`fechafin` BETWEEN '" & fechainicialR & "' AND '" & fechafinalR & "' " & filtro_pagomonto & " " &
                                                    " AND  contrato.`estado`=1 " &
                                                    " GROUP BY pagomonto.`idMes`,pagomonto.`idTienda`,pagomonto.`Sku`, pagomonto.Descripcion " &
                                                    " ORDER BY pagomonto.`idaño` ASC,pagomonto.`idMes` ASC,pagomonto.`Idcontrato` ASC")


                'dsventascompras = conexion.llenarDataSet("SELECT contrato,proveedor, " &
                '                                " sku,  " &
                '                                " descripcion, " &
                '                                " tienda,  " &
                '                                " id,  " &
                '                                " mes,    " &
                '                                " FORMAT(unidadesvendidas, 2) AS 'unidadesvendidas',  " &
                '                                " CONCAT('Q. ', FORMAT(ventas, 2)) AS 'ventas',  " &
                '                                " FORMAT(unidadcomprada,2) AS 'unidadescomprada',    " &
                '                                " CONCAT('Q. ', FORMAT(compras, 2)) AS 'compras'    " &
                '                                " FROM   (SELECT    pagoventas.`IdContrato` AS contrato,contratos_backmarginltx.`IdNombreProveedor` AS proveedor,  " &
                '                                " pagoventas.`Sku` AS sku,  pagoventas.`Descripcion` AS descripcion,   " &
                '                                " sucursales_bmltx.`Nombre` AS tienda,    " &
                '                                " parametros_tiempo.`nombre` AS mes,    " &
                '                                " año.`valor` AS id,   " &
                '                                " pagoventas.`UnidadVendida` AS unidadesvendidas,   " &
                '                                " pagoventas.`Ventas` AS ventas,        " &
                '                                "                '' AS unidadcomprada,         " &
                '                                "                '' AS compras    " &
                '                                "  FROM    parametros_pagoventa_bmltx AS pagoventas     " &
                '                                " INNER JOIN parametros_tiempo       " &
                '                                " ON parametros_tiempo.`id_tiempo` = pagoventas.`idMes`  " &
                '                                " INNER JOIN parametros_año AS año     " &
                '                                " ON año.`id` = pagoventas.`idaño`    " &
                '                                " INNER JOIN sucursales_bmltx      " &
                '                                " ON sucursales_bmltx.`Id` = pagoventas.`IdTienda`     " &
                '                                 " INNER JOIN contratos_backmarginltx  " &
                '                                "  ON contratos_backmarginltx.`IdContrato`=pagoventas.`IdContrato`  " &
                '                                " WHERE  pagoventas.`FechaFinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagoventa & "    " &
                '                                "                UNION    " &
                '                                " SELECT    pagocompras.idcontrato AS contrato, contratos_backmarginltx.`IdNombreProveedor` AS proveedor, pagocompras.`Sku` AS sku,  " &
                '                                " pagocompras.`Descripcion` AS descripcion,   " &
                '                                "  sucursales_bmltx.`Nombre` AS tienda,    " &
                '                                "  parametros_tiempo.`nombre` AS mes,    año.`valor` AS id,           " &
                '                                "                '' AS unidadvendida,            '' AS ventas,    " &
                '                                " pagocompras.`Unidadcomprada` AS unidadescompradas,    " &
                '                                " pagocompras.`compras`    " &
                '                                " FROM    parametros_pagocompras_bmltx AS pagocompras    " &
                '                                " INNER JOIN parametros_tiempo     " &
                '                                " ON parametros_tiempo.`id_tiempo` = pagocompras.`idMes`     " &
                '                                " INNER JOIN parametros_año AS año      " &
                '                                " ON año.`id` = pagocompras.`idaño`     " &
                '                                " INNER JOIN sucursales_bmltx        " &
                '                                " ON sucursales_bmltx.`Id` = pagocompras.`Idtienda`     " &
                '                                " INNER JOIN contratos_backmarginltx  " &
                '                                "  ON contratos_backmarginltx.`IdContrato`=pagocompras.`IdContrato`   " &
                '                                " WHERE   pagocompras.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagocompra & "     " &
                '                                " ) tmp         " &
                '                                " GROUP BY contrato, sku,   mes, id")


                dsventascompras = conexion.llenarDataSet("SELECT contrato, " &
                                                        " sku,  " &
                                                        " descripcion, " &
                                                        " tienda,  " &
                                                        " id,  " &
                                                        " mes,    " &
                                                        " FORMAT(unidadesvendidas, 2) AS 'unidadesvendidas',  " &
                                                        " CONCAT('Q. ', FORMAT(ventas, 2)) AS 'ventas',  " &
                                                        " FORMAT(unidadcomprada,2) AS 'unidadescomprada',    " &
                                                        " CONCAT('Q. ', FORMAT(compras, 2)) AS 'compras',    " &
                                                        " FORMAT(porcentaje,2) AS 'porcentaje'  " &
                                                        " FROM   (SELECT    pagoventas.`IdContrato` AS contrato,  " &
                                                        " pagoventas.`Sku` AS sku,  pagoventas.`Descripcion` AS descripcion,   " &
                                                        " sucursales_bmltx.`Nombre` AS tienda,    " &
                                                        " parametros_tiempo.`nombre` AS mes,    " &
                                                        " año.`valor` AS id,   " &
                                                        " pagoventas.`UnidadVendida` AS unidadesvendidas,   " &
                                                        " pagoventas.`Ventas` AS ventas,        " &
                                                        "                '' AS unidadcomprada,         " &
                                                        "                '' AS compras,    " &
                                                        "                ''AS porcentaje          " &
                                                        "  FROM    parametros_pagoventa_bmltx AS pagoventas     " &
                                                        " INNER JOIN parametros_tiempo       " &
                                                        " ON parametros_tiempo.`id_tiempo` = pagoventas.`idMes`  " &
                                                        " INNER JOIN parametros_año AS año     " &
                                                        " ON año.`id` = pagoventas.`idaño`    " &
                                                        " INNER JOIN sucursales_bmltx      " &
                                                        " ON sucursales_bmltx.`Id` = pagoventas.`IdTienda`     " &
                                                        " WHERE pagoventas.`FechaFinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagoventa & "     " &
                                                        "                UNION ALL    " &
                                                        " SELECT    pagocompras.idcontrato AS contrato, pagocompras.`Sku` AS sku,  " &
                                                        " pagocompras.`Descripcion` AS descripcion,   " &
                                                        "  sucursales_bmltx.`Nombre` AS tienda,    " &
                                                        "  parametros_tiempo.`nombre` AS mes,    año.`valor` AS id,           " &
                                                        "                '' AS unidadvendida,            '' AS ventas,    " &
                                                        " pagocompras.`Unidadcomprada` AS unidadescompradas,    " &
                                                        " pagocompras.`compras`,   " &
                                                        " pagocompras.porcentaje AS porcentaje" &
                                                        " FROM    parametros_pagocompras_bmltx AS pagocompras    " &
                                                        " INNER JOIN parametros_tiempo     " &
                                                        " ON parametros_tiempo.`id_tiempo` = pagocompras.`idMes`     " &
                                                        " INNER JOIN parametros_año AS año      " &
                                                        " ON año.`id` = pagocompras.`idaño`     " &
                                                        " INNER JOIN sucursales_bmltx        " &
                                                        " ON sucursales_bmltx.`Id` = pagocompras.`Idtienda`     " &
                                                        " WHERE pagocompras.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' AND pagocompras.`Unidadcomprada`>0 " & filtro_pagocompra & "      " &
                                                         "                UNION ALL    " &
                                                        " SELECT    traslados.idcontrato AS contrato, traslados.`Sku` AS sku,   traslados.`Descripcion` AS descripcion,    " &
                                                        " sucursales_bmltx.`Nombre` AS tienda,      parametros_tiempo.`nombre` AS mes,    año.`valor` AS id, " &
                                                        " '' AS unidadvendida,'' AS ventas,     traslados.`UnidadTrasladada` AS unidadescompradas,  " &
                                                        " traslados.`Traslado`*-1 AS compras,    " & porcentaje_contrato & " AS porcentaje FROM    parametros_pagotraslados_bmltx AS traslados " &
                                                        " INNER JOIN parametros_tiempo ON parametros_tiempo.`id_tiempo` = traslados.`idMes` " &
                                                        " INNER JOIN parametros_año AS año       ON año.`id` = traslados.`idaño` " &
                                                        " INNER JOIN sucursales_bmltx ON sucursales_bmltx.`Id` = traslados.`Idtienda` " &
                                                        " WHERE traslados.`Fechafinal` BETWEEN  '" & fechainicialR & "'     AND '" & fechafinalR & "' " & filtro_pagotraslado & " " &
                                                        " AND traslados.`UnidadTrasladada`>0 " &
                                                        " ) tmp " &
                                                        " ORDER BY ABS(sku)")
                dgvDingresos.DataSource = dsventascompras.Tables(0)
                dgvDespacio.DataSource = dsespacios.Tables(0)
                dgvDmonto.DataSource = dsmontofijo.Tables(0)
#End Region
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error BackMargin", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub



    Public Sub llenartextos(ByVal txt1 As TextBox, ByVal txt2 As TextBox, ByVal txt3 As TextBox, ByVal txt4 As TextBox)
        txt1.Text = contratoR
        txt2.Text = mesfinalR
        txt3.Text = mesinicialR
        txt4.Text = proveedorR
    End Sub
   



End Class