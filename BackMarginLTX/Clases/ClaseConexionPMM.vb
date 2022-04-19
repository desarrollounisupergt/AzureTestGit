Imports Oracle.ManagedDataAccess.Client
Imports Cifrado
Public Class ClaseConexionPMM
    Private conexion As OracleConnection
    Private sConexion As String 'el string de conexion que se esta utilizando
    Private query As OracleCommand
    Private queryStoredProc As New OracleCommand 'se crea una nueva variable para no entorpecer el utilizado
    Private daDatos As OracleDataAdapter


    Private transaccion As OracleTransaction
    'Esto es para saber en que estado esta la transaccion
    Private EstadoTransaccion As Integer
    Private Enum Estado As Integer
        Iniciado = 1
        Finalizado = 0
        ConError = 2
    End Enum

    Private _timeOutCommand As Integer
    ''' <summary>
    ''' Esto es para modificar el time out de los querys, esto para la variable command....
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeOutCommand() As Integer
        Get
            Return _timeOutCommand
        End Get
        Set(ByVal value As Integer)
            _timeOutCommand = value
            query.CommandTimeout = _timeOutCommand
        End Set
    End Property

    Private _timeOutStoredProc As Integer = 1800 'en teoria tiene 30mins para ejecutar ese query...
    ''' <summary>
    ''' Para asignar el time out de un stored Proc, esto porque se puede tardar mas la ejecucion de un 
    ''' stored proc. El tiempo por default es de 1800 segundos...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeOutStoredProc() As Integer
        Get
            Return _timeOutStoredProc
        End Get
        Set(ByVal value As Integer)
            _timeOutStoredProc = value
            queryStoredProc.CommandTimeout = _timeOutStoredProc
        End Set
    End Property

    'variable utilizada para cuando insertamos datos ...
    Dim _UltimoIdInsertado As Long
    Public ReadOnly Property UltimoIdInsertado() As Long
        Get
            Return _UltimoIdInsertado
        End Get
    End Property

    Public Sub New()
        conexion = New OracleConnection
        InstanciarQuery()
    End Sub

#Region "--- Propiedades de la conexion ..."

    Private _sServer As String = String.Empty
    Private _sInitialCatalog As String = String.Empty
    Private _sUserId As String = String.Empty
    Private _sPassword As String = String.Empty
    Private _iPuerto As Integer
    Private _bPooling As Boolean
    'Private _bAllowZeroDateTime As Boolean = False

    ''' <summary>
    ''' Aqui va la ip del server al cual se va a conectar...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Server() As String
        Get
            Return _sServer
        End Get
        Set(ByVal value As String)
            _sServer = value
        End Set
    End Property

    ''' <summary>
    ''' A que base se va a conectar ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialCatalog() As String
        Get
            Return _sInitialCatalog
        End Get
        Set(ByVal value As String)
            _sInitialCatalog = value
        End Set
    End Property

    ''' <summary>
    ''' Usuario que se utiliza para la conexion ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserId() As String
        Get
            Return _sUserId
        End Get
        Set(ByVal value As String)
            _sUserId = value
        End Set
    End Property

    ''' <summary>
    ''' Clave a utilizarse en la conexion ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Password() As String
        Get
            Return _sPassword
        End Get
        Set(ByVal value As String)
            _sPassword = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de puerto que va a utilizar la conexion ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Puerto() As Integer
        Get
            Return _iPuerto
        End Get
        Set(ByVal value As Integer)
            _iPuerto = value
        End Set
    End Property

    ''' <summary>
    ''' Configuracion de la conexion si va a utilizar pooling
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pooling() As Boolean
        Get
            Return _bPooling
        End Get
        Set(ByVal value As Boolean)
            _bPooling = value
        End Set
    End Property

    ''' <summary>
    ''' Configuracion de la conexion si va a aceptar fechas tipo string...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Property AllowZeroDateTime() As Boolean
    '    Get
    '        Return _bAllowZeroDateTime
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _bAllowZeroDateTime = value
    '    End Set
    'End Property

#End Region

#Region "--- Funciones utilizados para inicializar la conexion hacia la base de datos ..."

    ''' <summary>
    ''' String con la direccion de mysql a utilizar en la conexion...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StringDeConexion() As String
        Get
            Return sConexion
        End Get
        Set(ByVal value As String)
            sConexion = value
            conexion.ConnectionString = sConexion
            descomponerStringDeConexion() 'Agregado version 1.5
        End Set
    End Property

    Private Sub descomponerStringDeConexion()

        'LO QUE SE BUSCA AQUI ES,,, ASIGNARLE LOS VALORES A LAS VARIABLES A PARTIR DEL STRING DE CONEXION
        'En teoria solo se esta obteniendo un estring pero se neceista saber los valores del servidor, clave, etc...

        'primero generamos la matriz con los valores separados por la ;
        Dim sValoresConexion() As String = sConexion.Split(";")

        If sValoresConexion.Length > 0 Then

            Dim svariables() As String

            For Each svalor As String In sValoresConexion

                svariables = svalor.Split("=")

                Select Case svariables(0).ToString.Trim.ToLower
                    Case "server"
                        _sServer = svariables(1).ToString.Trim
                    Case "initial catalog"
                        _sInitialCatalog = svariables(1).ToString.Trim
                    Case "user id"
                        _sUserId = svariables(1).ToString.Trim
                    Case "password"
                        _sPassword = svariables(1).ToString.Trim
                    Case "port"
                        _iPuerto = svariables(1).ToString.Trim
                    Case "pooling"
                        _bPooling = svariables(1).ToString.Trim
                        'Case "allow zero datetime"
                        '    _bAllowZeroDateTime = svariables(1).ToString.Trim
                End Select
            Next
        Else
            Throw New InvalidOperationException("La cadena de conexion hacia la base de datos es incorrecta")
        End If

    End Sub

    ''' <summary>
    ''' Abrir la conexion con la base de datos...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirConexion()
        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If
    End Sub

    ''' <summary>
    ''' Cerrar la conexion con la base de datos...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CerrarConexion()
        conexion.Close()
    End Sub

    ''' <summary>
    ''' Se obtiene la variable de conexion que se esta utilizando...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerConexion() As OracleConnection
        Return conexion
    End Function

    ''' <summary>
    ''' Se obtiene que string de conexion se esta utilizando...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerStringDeConexion() As String
        Return sConexion
    End Function

    ''' <summary>
    ''' Por si se quiere instanciar nuevamente el command...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InstanciarQuery()

        query = New OracleCommand
        query.Connection = conexion

        queryStoredProc = New OracleCommand
        queryStoredProc.Connection = conexion

        daDatos = New OracleDataAdapter
    End Sub

    ''' <summary>
    ''' Con las propiedades de la conexion ingresados de antemano se genera el estring de conexion...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GenerarConexion()
        StringDeConexion = "Data Source =(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" & _sServer & ") " &
                            "(PORT= " & _iPuerto & "))) (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" & _sInitialCatalog & ")));" &
                            " User Id = " & _sUserId & ";" &
                            " Password = " & _sPassword & ";"


    End Sub

    ''' <summary>
    ''' Se busca un xml con la informacion de la conexion en la ubicacion del exe...
    ''' </summary>
    ''' <param name="nombrexml"></param>
    ''' <remarks></remarks>
    Public Sub BuscarConexionEnXml(ByVal nombrexml As String)

        Try
            Dim dsconexion As New DataSet

            dsconexion.ReadXml(nombrexml)

            'AHORA asignamos a las propiedades de la conexion la info...
            AsignarDatosDConexion(dsconexion)

            GenerarConexion()

        Catch ex As Exception

            Throw New InvalidOperationException("Error en el archivo de conexion " & nombrexml & " : " & ex.Message)

            Throw ex

        End Try

    End Sub

    ''' <summary>
    ''' Se busca un xml con la informacion de la conexion en la ubicacion especificada...
    ''' </summary>
    ''' <param name="nombrexml"></param>
    ''' <param name="ubicacionxml"></param>
    ''' <remarks></remarks>
    Public Sub BuscarConexionEnXml(ByVal nombrexml As String, ByVal ubicacionxml As String)
        Try
            Dim dsconexion As New DataSet

            dsconexion.ReadXml(ubicacionxml & "\" & nombrexml)

            'AHORA asignamos a las propiedades de la conexion la info...
            AsignarDatosDConexion(dsconexion)

            GenerarConexion()

        Catch ex As Exception

            Throw New InvalidOperationException("Error en el archivo de conexion " & nombrexml & " : " & ex.Message)

            Throw ex

        End Try


    End Sub

    Private Sub AsignarDatosDConexion(ByVal dsconexion As DataSet)
        _sServer = dsconexion.Tables(0).Rows(0)(0)
        _sInitialCatalog = dsconexion.Tables(0).Rows(0)(1)
        _sUserId = dsconexion.Tables(0).Rows(0)(2)
        _sPassword = Cifrado.Conexion.IniciarDesencriptacion(dsconexion.Tables(0).Rows(0)(3))

        ' encriptacion.desencriptar.desencripcadena(StringType.FromObject(DataSet.Tables(0).Rows(0)(2)), codigo)
        If dsconexion.Tables(0).Columns.Count >= 5 Then
            'no todos los archivos de conexion tienen las 5 columnas ...
            _iPuerto = dsconexion.Tables(0).Rows(0)(4)
        End If
    End Sub

    Private Function descubrirpassword(ByVal spentrada As String) As String

        Dim caracteres As String
        Dim codigo As Integer
        Dim stemp As String
        Dim cadenadesencriptada As String = ""
        Dim j As Integer

        For j = 0 To (spentrada.Length - 1) Step 3

            caracteres = spentrada.Chars(j + 0) & spentrada.Chars(j + 1) & spentrada.Chars(j + 2)

            codigo = (caracteres)

            stemp = Chr(codigo)

            cadenadesencriptada = cadenadesencriptada & stemp

            If j + 3 > spentrada.Length - 1 Then

                j = spentrada.Length

            End If
        Next

        Return cadenadesencriptada

    End Function

#End Region

#Region "--- Funciones utilizadas cuando hay una transaccion ..."

    ''' <summary>
    ''' se inicia con la transaccion en los querys de Mysql...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub IniciarTransaccion()
        Try
            AbrirConexion()
            transaccion = conexion.BeginTransaction
            'le asignamos al comand la transaccion ...
            query.Transaction = transaccion
            'indicamos que hemos iniciado la transaccion...
            EstadoTransaccion = Estado.Iniciado
        Catch ex As Exception
            Throw New InvalidOperationException(" Iniciando transaccion: " & ex.Message)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Se finaliza la transaccion, si no existe error ejecuta Commit, de lo contrario Rollback...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FinalizarTransaccion()
        Try
            If EstadoTransaccion = Estado.ConError Then
                transaccion.Rollback()
            Else
                'aquiere decir que no hay error...
                If EstadoTransaccion = Estado.Iniciado Then
                    transaccion.Commit()
                End If
            End If
        Catch ex As Exception
            'AQUI PUEDE DAR UN ERROR, si es que no hay conexion, pero de ser asi mysql hace rollback...
        Finally
            'indicamos que finalizo la transaccion...
            EstadoTransaccion = Estado.Finalizado
            'indicamos que el query ya no tenga ninguna transaccion 
            query.Transaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Cambia el estado de la transaccion, para realizar el rollback al Finalizar la Transaccion...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ForzarErrorTransaccion()
        EstadoTransaccion = Estado.ConError
    End Sub

#End Region

#Region "--- Funciones utilizados para ejecutar querys y traer informacion ..."

    Private Sub LimpiarQuery_CommandText()
        query.CommandText = String.Empty
    End Sub
    Private Sub LimpiarQuery_Parametros()
        query.Parameters.Clear()
        LimpiarParametrosCommand()
    End Sub

#Region "--- Querys sin parametros ---"

    ''' <summary>
    ''' Ejecuta el query deseado, y nos devuelve el numero de lineas afectadas ...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarNonQuery(ByVal _squery As String) As Integer
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery

            'ahora ejecutamos el query...
            EjecutarNonQuery = query.ExecuteNonQuery

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve un dato ...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarEscalar(ByVal _squery As String) As Object
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            'traemos el dato solicitado...
            query.CommandText = _squery
            EjecutarEscalar = query.ExecuteScalar

        Catch ex As Exception

            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' Esta funcion nos sirve para cuando insertamos un datos, y asi saber el id nuevo, 
    ''' Para eso se utilizo la variabla UltimoIdInsertado. SIEMPRE Y CUANDO EL ID SEA NUMERICO ...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertarDato(ByVal _squery As String) As Integer
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            'limpiamos nuestro ultimo id insertado...
            _UltimoIdInsertado = 0

            query.CommandText = _squery
            'ahora ejecutamos el query...
            InsertarDato = query.ExecuteNonQuery

            'aqui es donde sabemos que id utilizo el query al final...
            ''  _UltimoIdInsertado = query.LastInsertedId

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' LLENA UN DATASET POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO
    ''' SI LA CONEXION NO ESTA ABIERTA ESTA FUNCION LA ABRE EJECUTA EL 
    ''' LLENADO DEL DATASET Y LA CIERRA
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataSet(ByVal _squery As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(datasettemporal)

            Return datasettemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' LLENA UN DATASET POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO, SE PUEDE ESPECIFICAR EL NOMBRE QUE VA A TENER 
    ''' LA TABLA EN EL DATASET
    ''' SI LA CONEXION NO ESTA ABIERTA ESTA FUNCION LA ABRE EJECUTA EL 
    ''' LLENADO DEL DATASET Y LA CIERRA
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <param name="nombretabla"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataSet(ByVal _squery As String, ByVal nombretabla As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(datasettemporal, nombretabla)

            Return datasettemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Este no retorna dataset, sino que agrega la info. al dataset enviado por referencia...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <param name="nombretabla"></param>
    ''' <param name="DsDatos"></param>
    ''' <remarks></remarks>
    Public Sub llenarDataSet(ByVal _squery As String, ByVal nombretabla As String, ByRef DsDatos As DataSet)

        Dim tmpEstadoConexion As Integer = 0

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(DsDatos, nombretabla)

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try

    End Sub

    'Incluido en la version 1.7
    ''' <summary>
    ''' LLENA UN DATATABLE POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataTable(ByVal _squery As String) As DataTable

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(datasettemporal)

            Return datasettemporal.Tables(0)

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function




#End Region


#Region "--- Querys CON parametros ---"

#Region "--- Codigo para agregarle parametros a los querys --- "

    ''' <summary>
    ''' Clase que almacena nombre y valor del parametro. Para un command normal...
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cParametrosCommand
        Public nombre As String = String.Empty
        Public valor As Object
        Public Sub New(ByVal _nombre As String, ByVal _valor As Object)
            nombre = _nombre
            valor = _valor
        End Sub
    End Class
    Private _listaParametrosCommand As New ArrayList
    Private Function buscarParametro(ByVal _nombre As String, ByVal _valor As Object) As Boolean
        For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
            If String.Compare(tmpParametro.nombre, _nombre) = 0 Then
                tmpParametro.valor = _valor
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Agrega un nuevo parametro, si ya existe ese parametro entonces se actualiza su valor
    ''' </summary>
    ''' <param name="_nombre"></param>
    ''' <param name="_valor"></param>
    ''' <remarks></remarks>
    Public Sub AgregarParametrosCommand(ByVal _nombre As String, ByVal _valor As Object)
        _nombre = _nombre.Trim
        'Primero buscamos que no exista ese parametro...
        If buscarParametro(_nombre, _valor) = False Then
            'si no existiera entonces, se agrega el nuevo parametro...
            _listaParametrosCommand.Add(New cParametrosCommand(_nombre, _valor))
        End If
    End Sub
    Public Sub LimpiarParametrosCommand()
        _listaParametrosCommand.Clear()
    End Sub

#End Region


    ''' <summary>
    ''' Ejecuta el query deseado, y nos devuelve el numero de lineas afectadas.
    ''' Se utilizan los parametros ingresados...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarNonQueryParametros(ByVal _squery As String) As Integer
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            'Recorremos todos los parametros, para ingresarlos al query...
            If _listaParametrosCommand.Count > 0 Then
                For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
                    ''query.Parameters.AddWithValue(tmpParametro.nombre, tmpParametro.valor)

                    query.Parameters.Add(tmpParametro.nombre, tmpParametro.valor)
                Next
            Else
                'error ...
                Throw New InvalidOperationException("No existen parametros validos.")
            End If

            'ahora ejecutamos el query...
            EjecutarNonQueryParametros = query.ExecuteNonQuery

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
            'De ultimo eliminamos los parametros...
            LimpiarQuery_Parametros()
        End Try

    End Function

    ''' <summary>
    ''' Devuelve un dato, Utiliza Parametros...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarEscalarParametros(ByVal _squery As String) As Object
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            'traemos el dato solicitado...
            query.CommandText = _squery
            'Recorremos todos los parametros, para ingresarlos al query...
            If _listaParametrosCommand.Count > 0 Then
                For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
                    ''query.Parameters.AddWithValue(tmpParametro.nombre, tmpParametro.valor)
                    query.Parameters.Add(tmpParametro.nombre, tmpParametro.valor)
                Next
            Else
                'error ...
                Throw New InvalidOperationException("No existen parametros validos.")
            End If

            EjecutarEscalarParametros = query.ExecuteScalar

        Catch ex As Exception

            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
            'De ultimo eliminamos los parametros...
            LimpiarQuery_Parametros()
        End Try
    End Function

    ''' <summary>
    ''' Esta funcion nos sirve para cuando insertamos un datos, y asi saber el id nuevo, 
    ''' Para eso se utilizo la variabla UltimoIdInsertado. SIEMPRE Y CUANDO EL ID SEA NUMERICO.
    ''' Utilizando los parametros ingresados...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertarDatoParametros(ByVal _squery As String) As Integer
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            'limpiamos nuestro ultimo id insertado...
            _UltimoIdInsertado = 0

            query.CommandText = _squery

            'Recorremos todos los parametros, para ingresarlos al query...
            If _listaParametrosCommand.Count > 0 Then
                For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
                    query.Parameters.Add(tmpParametro.nombre, tmpParametro.valor)
                    ''query.Parameters.AddWithValue(tmpParametro.nombre, tmpParametro.valor)
                Next
            Else
                'error ...
                Throw New InvalidOperationException("No existen parametros validos.")
            End If

            'ahora ejecutamos el query...
            InsertarDatoParametros = query.ExecuteNonQuery

            'aqui es donde sabemos que id utilizo el query al final...
            '''_UltimoIdInsertado = query.LastInsertedId

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
            'De ultimo eliminamos los parametros...
            LimpiarQuery_Parametros()
        End Try
    End Function

    ''' <summary>
    ''' LLENA UN DATASET POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO
    ''' SI LA CONEXION NO ESTA ABIERTA ESTA FUNCION LA ABRE EJECUTA EL 
    ''' LLENADO DEL DATASET Y LA CIERRA.
    ''' Utiliza los parametros ingresados.
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataSetParametros(ByVal _squery As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            'Recorremos todos los parametros, para ingresarlos al query...
            If _listaParametrosCommand.Count > 0 Then
                For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
                    ''query.Parameters.AddWithValue(tmpParametro.nombre, tmpParametro.valor)
                    query.Parameters.Add(tmpParametro.nombre, tmpParametro.valor)
                Next
            Else
                'error ...
                Throw New InvalidOperationException("No existen parametros validos.")
            End If

            daDatos.SelectCommand = query
            daDatos.Fill(datasettemporal)

            Return datasettemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
            'De ultimo eliminamos los parametros...
            LimpiarQuery_Parametros()
        End Try
    End Function

    ''' <summary>
    ''' LLENA UN DATASET POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO, SE PUEDE ESPECIFICAR EL NOMBRE QUE VA A TENER 
    ''' LA TABLA EN EL DATASET
    ''' SI LA CONEXION NO ESTA ABIERTA ESTA FUNCION LA ABRE EJECUTA EL 
    ''' LLENADO DEL DATASET Y LA CIERRA.
    ''' Utiliza los parametros ingresados...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <param name="nombretabla"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataSetParametros(ByVal _squery As String, ByVal nombretabla As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            'Recorremos todos los parametros, para ingresarlos al query...
            If _listaParametrosCommand.Count > 0 Then
                For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
                    ''query.Parameters.AddWithValue(tmpParametro.nombre, tmpParametro.valor)
                    query.Parameters.Add(tmpParametro.nombre, tmpParametro.valor)
                Next
            Else
                'error ...
                Throw New InvalidOperationException("No existen parametros validos.")
            End If

            daDatos.SelectCommand = query
            daDatos.Fill(datasettemporal, nombretabla)

            Return datasettemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
            'De ultimo eliminamos los parametros...
            LimpiarQuery_Parametros()
        End Try

    End Function

    ''' <summary>
    ''' Este no retorna dataset, sino que agrega la info. al dataset enviado por referencia.
    ''' Utiliza los parametros ingresados...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <param name="nombretabla"></param>
    ''' <param name="DsDatos"></param>
    ''' <remarks></remarks>
    Public Sub llenarDataSetParametros(ByVal _squery As String, ByVal nombretabla As String, ByRef DsDatos As DataSet)

        Dim tmpEstadoConexion As Integer = 0

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            'Recorremos todos los parametros, para ingresarlos al query...
            If _listaParametrosCommand.Count > 0 Then
                For Each tmpParametro As cParametrosCommand In _listaParametrosCommand
                    ''query.Parameters.AddWithValue(tmpParametro.nombre, tmpParametro.valor)
                    query.Parameters.Add(tmpParametro.nombre, tmpParametro.valor)
                Next
            Else
                'error ...
                Throw New InvalidOperationException("No existen parametros validos.")
            End If

            daDatos.SelectCommand = query
            daDatos.Fill(DsDatos, nombretabla)

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
            'De ultimo eliminamos los parametros...
            LimpiarQuery_Parametros()
        End Try

    End Sub

#End Region



#End Region

#Region "--- Funciones de los stored Procs"

    ''' <summary>
    ''' Esta clase sirve para enviar los parametros al momento de utilizar un stored proc...
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ParametrosStoredP
        Public nombreParametro As String = String.Empty
        Public valorParametro As Object
        Public direccionParametro As ParameterDirection
        Public tipoValor As DbType
        Public Sub New(ByVal _nombreParametro As String,
                       ByVal _valorParametro As Object,
                       ByVal _direccionParametro As ParameterDirection,
                       ByVal _tipoValor As DbType)
            nombreParametro = _nombreParametro
            valorParametro = _valorParametro
            direccionParametro = _direccionParametro
            tipoValor = _tipoValor
        End Sub
    End Class

    Private Sub LimpiarQueryStored_CommandText()
        query.CommandText = String.Empty
    End Sub

    ''' <summary>
    ''' Se ejecuta un stored proc..., falta mejorar esta funcion hay que sobrecargarlo x si hay parametros...
    ''' esto en la proxima version de la clase.
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarStoredProc(ByVal _nombreStoredProc As String) As Integer

        Dim tmpEstadoConexion As Integer = 0

        Try

            'regularmente...
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc
            EjecutarStoredProc = queryStoredProc.ExecuteNonQuery

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Ejecuta un stored proc en la base de datos, pero utiliza parametros...
    ''' </summary>
    ''' <param name="_nombreStoredProc">Nombre del stored procedure</param>
    ''' <param name="parametros">Los parametros que necesita el stored procedure</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarStoredProc(ByVal _nombreStoredProc As String,
                                       ByVal ParamArray parametros() As ParametrosStoredP) As Integer

        Dim tmpEstadoConexion As Integer = 0

        Try

            'regularmente...
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            Dim tmpParametro As OracleParameter
            'agregamos todos los parametros 
            For Each elemento As ParametrosStoredP In parametros
                With elemento
                    tmpParametro = New OracleParameter(.nombreParametro, .valorParametro)
                    tmpParametro.Direction = .direccionParametro
                    tmpParametro.DbType = .tipoValor
                End With
                queryStoredProc.Parameters.Add(tmpParametro)
            Next

            EjecutarStoredProc = queryStoredProc.ExecuteNonQuery

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Se obtiene un dato de la base de datos utilizando un stored procedure...
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarEscalarStoredProc(ByVal _nombreStoredProc As String) As Object

        Dim tmpEstadoConexion As Integer = 0

        Try

            'regularmente...
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc
            EjecutarEscalarStoredProc = queryStoredProc.ExecuteScalar

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Se obtiene un dato de la base de datos utilizando un stored procedure con parametros...
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <param name="parametros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarEscalarStoredProc(ByVal _nombreStoredProc As String,
                                       ByVal ParamArray parametros() As ParametrosStoredP) As Object

        Dim tmpEstadoConexion As Integer = 0

        Try

            'regularmente...
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            Dim tmpParametro As OracleParameter
            'agregamos todos los parametros 
            For Each elemento As ParametrosStoredP In parametros
                With elemento
                    tmpParametro = New OracleParameter(.nombreParametro, .valorParametro)
                    tmpParametro.Direction = .direccionParametro
                    tmpParametro.DbType = .tipoValor
                End With
                queryStoredProc.Parameters.Add(tmpParametro)
            Next

            EjecutarEscalarStoredProc = queryStoredProc.ExecuteScalar

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Se llena un dataset utilizando como recurso un stored procedure...
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LlenarDatasetStoredProc(ByVal _nombreStoredProc As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0

        Try

            Dim dstemporal As New DataSet
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            'le asignamos el query de seleccion y llenamos el dataset...
            daDatos.SelectCommand = queryStoredProc
            daDatos.Fill(dstemporal)

            Return dstemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Se llena un dataset utilizando como recurso un stored procedura, se genera con el nombre de tabla especificado...
    ''' </summary>
    ''' <param name="_nombreStoredProc">Nombre del stored procedure</param>
    ''' <param name="_nombreTabla">con que nombre se quiere que se graba la consulta en el dataset</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LlenarDatasetStoredProc(ByVal _nombreStoredProc As String, ByVal _nombreTabla As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0

        Try

            Dim dstemporal As New DataSet
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            'le asignamos el query de seleccion y llenamos el dataset...
            daDatos.SelectCommand = queryStoredProc
            daDatos.Fill(dstemporal, _nombreTabla)

            Return dstemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Se llena un dataset utilizando un stored procedure, pero con parametros...
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <param name="parametros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LlenarDatasetStoredProc(ByVal _nombreStoredProc As String, ByVal ParamArray parametros() As ParametrosStoredP) As DataSet

        Dim tmpEstadoConexion As Integer = 0

        Try

            Dim dstemporal As New DataSet
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            Dim tmpParametro As OracleParameter
            'agregamos todos los parametros 
            For Each elemento As ParametrosStoredP In parametros
                With elemento
                    tmpParametro = New OracleParameter(.nombreParametro, .valorParametro)
                    tmpParametro.Direction = .direccionParametro
                    tmpParametro.DbType = .tipoValor
                End With
                queryStoredProc.Parameters.Add(tmpParametro)
            Next

            'le asignamos el query de seleccion y llenamos el dataset...
            daDatos.SelectCommand = queryStoredProc
            daDatos.Fill(dstemporal)

            Return dstemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Se llena un dataset utilizando un stored procedure, con nombre de tabla para el dataset y parametros...
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <param name="_nombreTabla"></param>
    ''' <param name="parametros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LlenarDatasetStoredProc(ByVal _nombreStoredProc As String,
                                            ByVal _nombreTabla As String,
                                            ByVal ParamArray parametros() As ParametrosStoredP) As DataSet

        Dim tmpEstadoConexion As Integer = 0

        Try

            Dim dstemporal As New DataSet
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            Dim tmpParametro As OracleParameter
            'agregamos todos los parametros 
            For Each elemento As ParametrosStoredP In parametros
                With elemento
                    tmpParametro = New OracleParameter(.nombreParametro, .valorParametro)
                    tmpParametro.Direction = .direccionParametro
                    tmpParametro.DbType = .tipoValor
                End With
                queryStoredProc.Parameters.Add(tmpParametro)
            Next

            'le asignamos el query de seleccion y llenamos el dataset...
            daDatos.SelectCommand = queryStoredProc
            daDatos.Fill(dstemporal, _nombreTabla)

            Return dstemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' No retorna ningun valor,,, se llena un dataset enviado por referencia utilizando el stored procedure
    ''' especificado.
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <param name="_nombreTabla"></param>
    ''' <param name="_dsDatos"></param>
    ''' <remarks></remarks>
    Public Sub LlenarDatasetStoredProc(ByVal _nombreStoredProc As String,
                                       ByVal _nombreTabla As String,
                                       ByRef _dsDatos As DataSet)

        Dim tmpEstadoConexion As Integer = 0

        Try

            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            'le asignamos el query de seleccion y llenamos el dataset...
            daDatos.SelectCommand = queryStoredProc
            daDatos.Fill(_dsDatos, _nombreTabla)

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Sub

    ''' <summary>
    ''' No retorna ningun valor,,, se llena un dataset enviado por referencia utilizando un stored procedure,
    ''' utilizando utilizando un stored procedures como recurso y com parametros...
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <param name="_nombreTabla"></param>
    ''' <param name="_dsDatos"></param>
    ''' <param name="parametros"></param>
    ''' <remarks></remarks>
    Public Sub LlenarDatasetStoredProc(ByVal _nombreStoredProc As String,
                                            ByVal _nombreTabla As String,
                                            ByVal _dsDatos As DataSet,
                                            ByVal ParamArray parametros() As ParametrosStoredP)

        Dim tmpEstadoConexion As Integer = 0

        Try

            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            Dim tmpParametro As OracleParameter
            'agregamos todos los parametros 
            For Each elemento As ParametrosStoredP In parametros
                With elemento
                    tmpParametro = New OracleParameter(.nombreParametro, .valorParametro)
                    tmpParametro.Direction = .direccionParametro
                    tmpParametro.DbType = .tipoValor
                End With
                queryStoredProc.Parameters.Add(tmpParametro)
            Next

            'le asignamos el query de seleccion y llenamos el dataset...
            daDatos.SelectCommand = queryStoredProc
            daDatos.Fill(_dsDatos, _nombreTabla)

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQueryStored_CommandText()
        End Try

    End Sub

#End Region

#Region "--- Funciones de verificacion de conexion"

    ''' <summary>
    ''' Para ver si hay ping con el servidor especificado...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Ping() As Boolean
        Try
            'se hacen varios intentos de ping...
            For i As Integer = 0 To 4
                If My.Computer.Network.Ping(_sServer) Then
                    'si logra hacer ping al servidor entonces retorna verdadero...
                    'Return True
                    'ahora abrimos la conexion!
                    'En teoria si no tira erros al abrir la conexion entonces se puede usar la conexion...
                    Try
                        AbrirConexion()
                        Return True
                    Catch ex As Exception
                        'no vamos a mostrar error porque solo se esta intentando establecer si se puede
                        'insertar informacion...
                    Finally
                        CerrarConexion()
                    End Try
                End If
                'se espera antes de intentar otra vez...
                System.Threading.Thread.Sleep(2000)
            Next
            Return False
        Catch ex As Exception
            Throw New InvalidOperationException("Error buscando servidor. " & ex.Message)
            Throw ex
        End Try
    End Function

#End Region

#Region "--- Funciones para ejecutar el DataReader"

    Private _drLector As OracleDataReader
    Public ReadOnly Property drLector() As OracleDataReader
        Get
            Return _drLector
        End Get
    End Property

    ''' <summary>
    ''' Esta funcion se utiliza para cuando se ejecutan varios datareaders y se sabe que se va a tardar un 
    ''' tiempo considerable utilizando esas conexiones. Si se utiliza deberia ser despues de abrir la
    ''' conexion y antes de ejecutar el reader.
    ''' Despues debe cerrarse la conexion....
    ''' </summary>
    ''' <param name="tiempo_escritura">Tiempo maximo que se puede tener la informacion para escritura</param>
    ''' <param name="tiempo_lectura">Tiempo maximo que se puede tener la informacion para lectura</param>
    ''' <remarks></remarks>
    Public Sub AumentarTiempoServerReader(ByVal tiempo_escritura As Integer, ByVal tiempo_lectura As Integer)
        'la idea principal de esto es que se pueda mantener en memoria el mayor tiempo posible
        'nuestra consulta de datareader, que como se sabe es un select que se queda en la memoria de
        'nuestro server...
        EjecutarNonQuery("set net_write_timeout=" & tiempo_escritura & "; " &
          "set net_read_timeout=" & tiempo_lectura)
    End Sub

    ''' <summary>
    ''' Se inicia el dataReader con el query necesario...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <remarks></remarks>
    Public Sub iniciarDataReader(ByVal _squery As String)
        Try
            query.CommandText = _squery

            'ya deberia estar abierta la conexion...
            _drLector = query.ExecuteReader

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException("Error inicializando el DataReader. " & ex.Message)
            Throw ex
        End Try
    End Sub

    'ESTAS FUNCIONES SON LAS UNICAS QUE PERTENECEN A LOS STORES PROCEDURES... PERO ES PARA TENER JUNTO LOS
    'READERS.....
    ''' <summary>
    ''' Se inicia un dataReader utilizando como recurso un stored procedures, el stored procedure debera 
    ''' devolver varios registros para que sea funcional.
    ''' </summary>
    ''' <param name="_nombreStoredProc">Nombre del stored procedure</param>
    ''' <remarks></remarks>
    Public Sub iniciarDataReaderStoredProc(ByVal _nombreStoredProc As String)

        Try

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            'ya deberia estar abierta la conexion...
            _drLector = queryStoredProc.ExecuteReader

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException("Error inicializando el DataReader. " & ex.Message)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Se inicia un datareader utilizando como recurso un datareader, utilizando parametros.
    ''' </summary>
    ''' <param name="_nombreStoredProc"></param>
    ''' <param name="parametros"></param>
    ''' <remarks></remarks>
    Public Sub iniciarDataReaderStoredProc(ByVal _nombreStoredProc As String,
                                            ByVal ParamArray parametros() As ParametrosStoredP)

        Try

            queryStoredProc.CommandText = _nombreStoredProc 'le enviamos el nombre del stored proc....
            queryStoredProc.CommandType = CommandType.StoredProcedure 'Asignamos el tipo stored proc...

            'ahora ejecutamos el query...
            queryStoredProc.CommandTimeout = _timeOutStoredProc

            Dim tmpParametro As OracleParameter
            'agregamos todos los parametros 
            For Each elemento As ParametrosStoredP In parametros
                With elemento
                    tmpParametro = New OracleParameter(.nombreParametro, .valorParametro)
                    tmpParametro.Direction = .direccionParametro
                    tmpParametro.DbType = .tipoValor
                End With
                queryStoredProc.Parameters.Add(tmpParametro)
            Next

            'ya deberia estar abierta la conexion...
            _drLector = queryStoredProc.ExecuteReader

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException("Error inicializando el DataReader. " & ex.Message)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' se cierra el datareader, para poder ejecutar otros querys...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub cerrarDataReader()
        _drLector.Close()
    End Sub

#End Region

End Class

