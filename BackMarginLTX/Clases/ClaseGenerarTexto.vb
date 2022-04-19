Imports System.IO
Imports System.Text

Public Class ClaseGenerarTexto
    Private _textodelimatador As String
    Private _textoencapulado As String

    Public Sub New()
        textodelimitador = ","
        textoencapsulado = """"

    End Sub

    Public Property textodelimitador() As String
        Get
            Return _textodelimatador
        End Get
        Set(ByVal value As String)
            _textodelimatador = value
        End Set
    End Property
    Public Property textoencapsulado() As String
        Get
            Return _textoencapulado
        End Get
        Set(ByVal value As String)
            _textoencapulado = value
        End Set
    End Property

    Public Sub generartexto(ByVal tabla As DataTable, ByVal path As String)

        Dim txtBuilder As New StringBuilder()
        Dim contColumnas As Integer = tabla.Columns.Count - 1
        Dim contador As Integer = 0
        Dim tmpstring As String = String.Empty

        Dim fs As FileStream = Nothing
        fs = New FileStream(path, FileMode.CreateNew)
        Using writer As StreamWriter = New StreamWriter(fs, Encoding.Default, 512)
            For Each fila As DataRow In tabla.Rows

                For i As Integer = 0 To contColumnas
                    If IsDBNull(fila(i)) Then
                        txtBuilder.Append("\N")
                    Else
                        tmpstring = fila(i)
                        tmpstring = tmpstring.Replace("""", "\""")
                        txtBuilder.Append(textoencapsulado & tmpstring & textoencapsulado)
                    End If
                    If i < contColumnas Then
                        txtBuilder.Append(textodelimitador)

                    End If
                Next
                txtBuilder.AppendLine()
                contador += 1

                If contador >= 50000 Then
                    contador = 0
                    writer.Write(txtBuilder.ToString())
                    'txtBuilder.clear()
                    txtBuilder = New StringBuilder()

                End If

            Next

            If txtBuilder.Length > 0 Then
                writer.Write(txtBuilder.ToString())

            End If

        End Using


    End Sub



End Class
