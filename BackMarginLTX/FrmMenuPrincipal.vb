Imports MySql.Data.MySqlClient
Imports BackMarginLTX.exportacioncontratos
Imports System.Text
Imports System.IO
Imports System.Threading

Public Class FrmMenuPrincipal
    Public contratos As New FrmMenuContratos
    Public usuarios As New FrmMenuUsuarios
    Public items As New FrmTipodeItems
    Public reportes As New FrmReportes
    Public usuario As New Frminiciodesesion
    Dim fechaactual, fecha, fechafinal As Date
    Dim fecha1, fecha2 As String
    Private path As String = String.Empty
    Dim archivos As New ClaseGenerarTexto
    Dim dttienda As New DataTable
    Dim myType As Type
    'Public sesion As New Frminiciodesesion
    Private Sub btnContratos_Click(sender As Object, e As EventArgs) Handles btnContratos.Click
        contratos = New FrmMenuContratos
        contratos.Show()
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub btnAdministracion_Click(sender As Object, e As EventArgs) Handles btnAdministracion.Click
        usuarios = New FrmMenuUsuarios
        usuarios.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnMantemiento_Click(sender As Object, e As EventArgs) Handles btnMantemiento.Click
        items = New FrmTipodeItems
        items.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub FrmMenuPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblVersion.Text = "V" & Application.ProductVersion
        usuario = Frminiciodesesion
        usuario.ShowDialog()
        verificarprivilegios(nivelusuario)
        Dim consulta, consulta2 As String
        'CARGAMOS LA CONEXION QUE VAMOS A UTILIZAR PARA TRAER LAS SUCURSALES A UTILIZAR

        '====================================================================================================================================================================
        'YA NO SE VA A USAR LA PRICEADMIN PARA ESTAS CONSULTAS, SE CONSULTARA A PMM ENERO 2021 LBOCH
        'cargarConexionMysql1("arconexion223.xml")
        'dttienda = conexion.llenarDataTable("SELECT id,idempresas,nombre,DATE_FORMAT(FechaCreado,'%Y-%m-%d') AS FechaApertura FROM sucursales WHERE idempresas= 5")
        'dttienda = conexion.llenarDataTable("SELECT * FROM sucursales WHERE idempresas= 5")
        '====================================================================================================================================================================

        '====================================================================================================================================================================
        'YSE CONSULTARA A PMM ENERO 2021 LBOCH
        cargarConexionPmm("conexionpmm.xml")
        dttienda = conexionpmm.llenarDataTable("SELECT idsucursal AS ""id"",idempresa as ""idempresas"",nombre as ""nombre"",TO_CHAR(fechaapertura,'YYYY-MM-DD')as ""FechaApertura""  " &
                                               "FROM UNIPMM.UNISUPERSUCURSALES WHERE idempresa=5 order by idsucursal")

        '====================================================================================================================================================================

        path = Application.StartupPath & "/sucursalesExpress.csv"
        'aqui verificamos si el archivo existe..
        If File.Exists(path) Then
            File.Delete(path)
        End If
        archivos.generartexto(dttienda, path)
        'AQUI REGRESAMOS LA CONEXIMOS QUE VAMOS A UTILIZAR EN TODO EL PROYECTO
        'PARA LOS CONTRATOS Y PODER INGRESAR LA TABLA DE SUCURSALES A UTILIZAR
        conexion.CerrarConexion()
        cargarConexionMysql1("conexion2.xml")
        'ELIMINAMOS LA TABLA ANTERIOR DE LAS SUCURSALES 
        consulta2 = conexion.EjecutarNonQuery("DELETE  FROM sucursales_bmltx")
        'Insertamos la nueva tabla con los datos del archivo CSV...
        consulta = "LOAD DATA LOCAL INFILE '" & path.Replace("\", "\\") & "' IGNORE INTO TABLE  `sucursales_bmltx`  FIELDS ESCAPED BY '\\' TERMINATED BY ',' ENCLOSED BY '""' LINES TERMINATED BY '\r\n' (Id,Idempresas,Nombre,FechaApertura); "
        consulta2 = conexion.EjecutarNonQuery(consulta)

    End Sub

    Private Sub btnReportes_Click(sender As Object, e As EventArgs) Handles btnReportes.Click
        reportes = New FrmReportes
        reportes.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Public Sub verificarprivilegios(ByVal idprivilegios As String)


        Dim _dsOpciones As New DataSet
        If idprivilegios = 0 Then
            Me.Close()
        Else
            _dsOpciones = conexion.llenarDataSet("SELECT p.id_opcion,o.opcion,p.activo FROM permisos_gruposad p " &
                                             "INNER JOIN opciones_usuario o ON o.id_opcion=p.id_opcion WHERE p.id_grupoad=" & idprivilegios & " ")

            btnContratos.Enabled = _dsOpciones.Tables(0).Rows(0)("activo")
            btnReportes.Enabled = _dsOpciones.Tables(0).Rows(1)("activo")
            btnAdministracion.Enabled = _dsOpciones.Tables(0).Rows(2)("activo")
            btnMantemiento.Enabled = _dsOpciones.Tables(0).Rows(3)("activo")

            ''Select Case idprivilegios
            ''    Case 0
            ''        End

            ''    Case 1
            ''        btnReportes.Enabled = True
            ''        btnAdministracion.Enabled = True
            ''        btnContratos.Enabled = True
            ''        btnMantemiento.Enabled = True
            ''    Case 2
            ''        btnReportes.Enabled = True

            ''    Case 3
            ''        btnReportes.Enabled = True
            ''        btnContratos.Enabled = True
            ''End Select
        End If

    End Sub
End Class