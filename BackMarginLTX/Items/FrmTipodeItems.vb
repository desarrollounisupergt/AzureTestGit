Public Class FrmTipodeItems
    Dim dttipos As New DataTable
    Dim dstipos As New DataSet
    Dim consultas As String
    Dim IdItem As Integer
    Dim nombreTE As String
    Dim ancho1, alto1 As String
    Dim Cverificacionexistencia As String


    Private Sub FrmTipodeItems_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        FrmMenuPrincipal.WindowState = FormWindowState.Normal
    End Sub
    Private Sub FrmTipodeItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dttipos = conexion.llenarDataTable("SELECT Nombre FROM tipoespacio_bmltx")
        For Each fila As DataRow In dttipos.Rows
            cmbitems.Items.Add(fila.Item("Nombre"))

        Next
    End Sub


    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        dstipos.Clear()
        dstipos.Reset()
        dstipos = conexion.llenarDataSet("SELECT Nombre,Ancho,Alto,FORMAT(Costo,2) AS costo FROM tipoespacio_bmltx  WHERE Nombre='" & cmbitems.Text & "'")

        dgvtiposdeespacio.DataSource = dstipos.Tables(0)
    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If Trim(txtcosto.Text.Length) > 0 Then
            nombreTE = ""
            alto1 = ""
            ancho1 = ""
            If Trim(txtnombre.Text.Length) = 0 Then
                nombreTE = ""
                nombreTE = txtalto.Text & "X" & txtancho.Text
            Else
                nombreTE = txtnombre.Text
            End If
            If Trim(txtalto.Text.Length) = 0 Then
                alto1 = 0
            Else
                alto1 = txtalto.Text
            End If
            If Trim(txtancho.Text.Length) = 0 Then
                ancho1 = 0
            Else
                ancho1 = txtancho.Text
            End If
            Cverificacionexistencia = conexion.EjecutarEscalar("SELECT COUNT(Nombre) FROM tipoespacio_bmltx WHERE Nombre='" & nombreTE & "'")
            If Cverificacionexistencia = 0 Then
                consultas = conexion.EjecutarNonQuery("INSERT INTO tipoespacio_bmltx(Nombre,ancho,alto,Costo)VALUES('" & nombreTE & "','" & ancho1 & "','" & alto1 & "','" & txtcosto.Text & "')")
                MessageBox.Show("Se ingreso con exito el espacio ", "Tipos de Espacio", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtalto.Text = ""
                txtancho.Text = ""
                txtcosto.Text = ""
                txtnombre.Text = ""
                cmbitems.Items.Clear()
                cmbitems.ResetText()
                dttipos = conexion.llenarDataTable("SELECT Nombre FROM tipoespacio_bmltx")
                For Each fila As DataRow In dttipos.Rows
                    cmbitems.Items.Add(fila.Item("Nombre"))

                Next
            Else
                MessageBox.Show("El Espacios que desea crear ya existe", "Error de Creacion de Espacio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtalto.Text = ""
                txtancho.Text = ""
                txtcosto.Text = ""
                txtnombre.Text = ""
            End If
        Else
            MessageBox.Show("Asegurese que todos los campos se encuentren llenos", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If

    End Sub

    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()
        FrmMenuPrincipal.WindowState = FormWindowState.Normal
    End Sub
End Class