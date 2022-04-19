Public Class FrmMenuUsuarios
    Dim dtusuarios As New DataTable
    Dim dsusuario As New DataSet
    Dim dtprivilegios As New DataTable
    Dim consulta As String
    Dim contusuario As Integer
    Dim nuevoid As Integer
    Dim contraseña As Integer
    Dim consulta1 As String
    Dim contraseñaE As String

    Private Sub FrmMenuUsuarios_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        FrmMenuPrincipal.WindowState = FormWindowState.Normal
    End Sub
    Private Sub FrmMenuUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtusuarios = conexion.llenarDataTable("SELECT usuario FROM usuarios_backmarginltx")
        For Each fila As DataRow In dtusuarios.Rows
            cmbusurios.Items.Add(fila.Item("Usuario"))
        Next
        dtprivilegios = conexion.llenarDataTable(" SELECT nombre FROM privilegios_bmltx")

        For Each fila As DataRow In dtprivilegios.Rows
            cmbprivilegios.Items.Add(fila.Item("Nombre"))
            cmbprivilegios2.Items.Add(fila.Item("Nombre"))
        Next
    End Sub



    Private Sub cmbusurios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbusurios.SelectedIndexChanged

        dsusuario.Clear()
        dsusuario.Reset()
        txtapellidousuario.Text = ""
        txtnombrecontable2.Text = ""
        txtnombreusuario.Text = ""
        txtcodigoUsuario.Text = ""
        dsusuario = conexion.llenarDataSet("SELECT Nombres,apellidos,CodigoContable,Nombrecontable,Privilegios,Estado FROM usuarios_backmarginltx WHERE usuario='" & cmbusurios.Text & "'")
        txtnombreusuario.Text = dsusuario.Tables(0).Rows(0).Item(0)
        txtapellidousuario.Text = dsusuario.Tables(0).Rows(0).Item(1)
        txtcodigoUsuario.Text = dsusuario.Tables(0).Rows(0).Item(2)
        txtnombrecontable2.Text = dsusuario.Tables(0).Rows(0).Item(3)
        cmbprivilegios2.SelectedIndex = dsusuario.Tables(0).Rows(0).Item(4) - 1
        cmbestado.SelectedIndex = dsusuario.Tables(0).Rows(0).Item(5)
    End Sub

    Private Sub cmbestado_LostFocus(sender As Object, e As EventArgs) Handles cmbestado.LostFocus
        If Trim(cmbestado.Text.Length) > 0 Then
            If cmbestado.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El estado que ingrenso no existe", "Error de Estado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub cmbprivilegios2_LostFocus(sender As Object, e As EventArgs) Handles cmbprivilegios2.LostFocus, cmbprivilegios.LostFocus

        If Trim(cmbprivilegios2.Text.Length) > 0 Or Trim(cmbprivilegios.Text.Length) > 0 Then
            If cmbprivilegios.SelectedIndex >= 0 Or cmbprivilegios2.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El Privilegio que ingrenso no existe", "Error de Privilegios", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub cmbusuarios_LostFocus(sender As Object, e As EventArgs) Handles cmbusurios.LostFocus
        If Trim(cmbusurios.Text.Length) > 0 Then
            If cmbusurios.SelectedIndex >= 0 Then

            Else
                MessageBox.Show("El estado que ingrenso no existe", "Error de Estado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim contraseña As New Frmcambiarcontraseña
        contraseña = New Frmcambiarcontraseña
        contraseña.ShowDialog()
    End Sub


    Private Sub btnguardarMod_Click(sender As Object, e As EventArgs) Handles btnguardarMod.Click
        Dim idusuario As Integer
        If Trim(txtnombrecontable2.Text.Length) > 0 And Trim(txtnombreusuario.Text.Length) > 0 And Trim(txtapellidousuario.Text.Length) > 0 And Trim(txtcodigoUsuario.Text.Length) > 0 Then
            consulta = ""
            contusuario = conexion.EjecutarEscalar("SELECT COUNT(IdUsuarios) FROM usuarios_backmarginltx WHERE usuario='" & cmbusurios.Text & "'")

            If contusuario > 0 Then

                idusuario = conexion.EjecutarEscalar("SELECT idusuarios FROM usuarios_backmarginltx WHERE usuario='" & cmbusurios.Text & "'")
                consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET nombres='" & txtnombreusuario.Text & "' WHERE IdUsuarios=" & idusuario & "")
                consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET apellidos='" & txtapellidousuario.Text & "' WHERE IdUsuarios=" & idusuario & "")
                consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET Codigocontable='" & txtcodigoUsuario.Text & "' WHERE IdUsuarios=" & idusuario & "")
                consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET nombrecontable='" & txtnombrecontable2.Text & "' WHERE IdUsuarios=" & idusuario & "")
                consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET privilegios='" & cmbprivilegios2.SelectedIndex + 1 & "' WHERE IdUsuarios=" & idusuario & "")
                consulta = conexion.EjecutarNonQuery("UPDATE usuarios_backmarginltx SET estado='" & cmbestado.SelectedIndex & "' WHERE IdUsuarios=" & idusuario & "")
                MessageBox.Show("Se realizaron los cambios con exito", "Modificacion de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtapellidousuario.Text = ""
                txtnombreCusuario.Text = ""
                txtnombreusuario.Text = ""
                txtcodigoUsuario.Text = ""
                txtnombrecontable2.Text = ""
                cmbprivilegios2.Text = ""

            Else
                MessageBox.Show("El Usuario no existe ", "Error de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtapellidousuario.Text = ""
                txtnombreCusuario.Text = ""
                txtnombreusuario.Text = ""
                txtcodigoUsuario.Text = ""
                txtnombrecontable2.Text = ""
                cmbprivilegios2.Text = ""
            End If
        Else

            MessageBox.Show("Tiene algun campo vacio", "Error campos vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtapellidousuario.Text = ""
            txtnombreCusuario.Text = ""
            txtnombreusuario.Text = ""
            txtcodigoUsuario.Text = ""
        End If



    End Sub
    Public Sub limpiartext(ByVal txt1 As TextBox, ByVal txt2 As TextBox, ByVal txt3 As TextBox, ByVal txt4 As TextBox, ByVal txt5 As TextBox, ByVal txt6 As TextBox, ByVal cmb As ComboBox)

        txt1.Text = ""
        txt2.Text = ""
        txt3.Text = ""
        txt4.Text = ""
        txt5.Text = ""
        txt6.Text = ""
        cmb.Text = ""

    End Sub

    Private Sub btnGuardar_Click_1(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
        consulta = conexion.EjecutarNonQuery("SELECT COUNT(IdUsuarios) FROM usuarios_backmarginltx WHERE usuario='" & txtusuario.Text & "' and CodigoContable= '" & txtcodigocontable.Text & "' ")
        nuevoid = 0
        If consulta = 1 Then
            MessageBox.Show("El nombre de Usuario que ingreso ya existe en la base de datos", "Error al crear Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error)  
                limpiartext(txtNombre, txtapellido, txtcodigocontable, txtnombrecontable, txtcontraseña, txtusuario, cmbprivilegios)
        Else
            nuevoid = conexion.EjecutarEscalar("SELECT MAX(IDusuarios) FROM usuarios_backmarginltx")
            nuevoid += 1
                contraseñaE = conexion.EjecutarEscalar("SELECT CAST(SHA1('" & txtcontraseña.Text.ToUpper & "') AS CHAR)")
                consulta1 = conexion.EjecutarNonQuery("INSERT INTO usuarios_backmarginltx(IdUsuarios,Nombres,Apellidos,Usuario,Contraseña,CodigoContable,Nombrecontable,NivelUsuario,Privilegios,Estado) " & _
                                                      " VALUES(" & nuevoid & ",'" & txtNombre.Text.ToUpper & "','" & txtapellido.Text.ToUpper & "','" & txtusuario.Text.ToUpper & "','" & contraseñaE & "'," & txtcodigocontable.Text & ",'" & txtnombrecontable.Text.ToUpper & "',1,'" & cmbprivilegios.SelectedIndex + 1 & "',1)")
            If consulta1 = 1 Then
                MessageBox.Show("El Usuario se a creado con exitó", "Creacion de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    limpiartext(txtNombre, txtapellido, txtcodigocontable, txtnombrecontable, txtcontraseña, txtusuario, cmbprivilegios)
                dtusuarios.Clear()
                dtusuarios.Reset()
                cmbusurios.Items.Clear()
                dtusuarios = conexion.llenarDataTable("SELECT usuario FROM usuarios_backmarginltx")
                For Each fila As DataRow In dtusuarios.Rows
                    cmbusurios.Items.Add(fila.Item("Usuario"))
                Next

            Else
                MessageBox.Show("Error al Crear usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    limpiartext(txtNombre, txtapellido, txtcodigocontable, txtnombrecontable, txtcontraseña, txtusuario, cmbprivilegios)
            End If
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio el Error:" & vbCrLf & ex.Message, "Back Margin Express", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try
    End Sub



    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        Me.Close()
        FrmMenuPrincipal.WindowState = FormWindowState.Normal
    End Sub

End Class