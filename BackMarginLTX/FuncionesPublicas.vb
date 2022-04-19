Imports Cifrado.ClaseCifrarConexion
Module FuncionesPublicas

    Public estadoconexion As Boolean = False
    Public estadovalidacion As Boolean = False
    Public estado As Boolean = True
    Public nombreusuario As String
    Public fechainicialR As String
    Public fechafinalR As String
    Public tipocobroR As String
    Public cargarventana As Boolean = False
    Public cargarventanaM As Boolean = False
    Public mesinicialR As String
    Public mesfinalR As String
    Public contratoR As String
    Public proveedorR As String
    Public chcontrato As Boolean = False
    Public chproveedor As Boolean = False
    Public estadocontrato As Integer = 0
    Public Nocontratofrm As String = String.Empty
    Public añoingresar As String = String.Empty
    Public nocontrato As String = String.Empty
    Public correlativo As String = String.Empty
    Public cadenaregreso As String = String.Empty
    Public porcentajeV As Decimal
    Public porcentajeC As Decimal
    Public tipo As Integer = 0
    Public tasadecambio As Decimal
    Public PreguntaTasaC As Boolean = False
    Public nivelusuario As Integer
    Public ContContratos As Integer = 0
    Public lista_tiendas As String
    Public listaCaracteresEspeciales As String()
    Public Function ValidaNulos(Of tipo)(ByVal valor As Object, ByVal ReturnIfNull As tipo) As tipo
        If IsDBNull(valor) Or valor Is Nothing Then 'si es nulo retornamos el valor deseado...
            Return ReturnIfNull
        End If
        Return valor 'si no es nulo retornamos el valor original...
    End Function

    Public conexion As New claseConexion
    ' Public cifrado As New Cifrado.ClaseCifrarConexion
    ''''''''''''Public Sub cargarConexionMysql()
    ''''''''''''    conexion.BuscarConexionEnXml("cnSPS.xml")
    ''''''''''''End Sub
    Public Sub cargarConexionMysql1(ByVal cadena As String)
        conexion.BuscarConexionEnXml(cadena)
    End Sub


    'CONEXION QUE UTILIZAMOS PARA EL 223 YA NO SE USARA LA PRICEADMIN
    Public Sub CargarConexionProductos()
        '==============================================================================================
        'CONSULTAS  A PMM ENERO 2021
        'conexion.CerrarConexion()
        'cargarConexionMysql1("arconexion223.xml")
        'cargarConexionMysql1("conexionJ.xml")
        cargarConexionPmm("conexionpmm.xml")
    End Sub
    Public Sub CargarConexionContratos()
        conexion.CerrarConexion()
        cargarConexionMysql1("conexion2.xml")
    End Sub
    '==============================================================================================
    'PARA HACER LA CONEXION A PMM ENERO 2021
    Public conexionpmm As New ClaseConexionPMM
    Public Sub cargarConexionPmm(ByVal cadena As String)
        conexionpmm.BuscarConexionEnXml(cadena)
    End Sub
    '==============================================================================================



    Public Sub llenardatosparahistorico(ByVal caso As String, ByVal cadena As String)
        Select Case caso
            Case 1
                cadenaregreso = "Se acaba de modificar un elemento de la Bases de datos, el elemento que fue modificado es" &
                                  " " & cadena & " "

            Case 2
                cadenaregreso = "Se acaba de eliminar un elemento de la base de datos, el elemento que fue eliminado es  " & cadena & ""

            Case 3
                cadenaregreso = "Se acaba de agregar un elemento de la base de datos, el elemento que fue agregado es " & cadena & ""
        End Select

    End Sub
    '================================================================================================================
    'SE AGREGAR PARA LIMPIAR CARACTERES Y PREVEER SQL INJECTION LBOCH ENERO 2021
    Public Function limpiarCadenaTexto(cadenaTexto As String, sustituirPor As String) As String
        Dim cadenaResultado As String
        cadenaResultado = cadenaTexto
        For Each elemento As String In listaCaracteresEspeciales
            cadenaResultado = cadenaResultado.Replace(elemento, sustituirPor)

        Next
        Return cadenaResultado
    End Function
    '================================================================================================================
End Module
