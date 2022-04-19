<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcontrato
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcontrato))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpgdatosbasicos = New System.Windows.Forms.TabPage()
        Me.cmbproveedoresFac = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblnocontrato = New System.Windows.Forms.Label()
        Me.btneliminarmarca = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnagregarmarca = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.dgvMarcas = New System.Windows.Forms.DataGridView()
        Me.seleccion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Marca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbMarcas = New System.Windows.Forms.ComboBox()
        Me.cmbproveedores = New System.Windows.Forms.ComboBox()
        Me.dgvContactos = New System.Windows.Forms.DataGridView()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cargo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Telefono = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Correo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnContagregar = New System.Windows.Forms.Button()
        Me.txtCorreo = New System.Windows.Forms.TextBox()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.txtCargo = New System.Windows.Forms.TextBox()
        Me.txtCnombre = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNproveedor = New System.Windows.Forms.TextBox()
        Me.txtNunisuper = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbformatodecobro = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFfinal = New System.Windows.Forms.DateTimePicker()
        Me.dtpFinicial = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tpgproductos = New System.Windows.Forms.TabPage()
        Me.chbtiendasnuevas = New System.Windows.Forms.CheckBox()
        Me.chkdolares = New System.Windows.Forms.CheckBox()
        Me.chkquetzales = New System.Windows.Forms.CheckBox()
        Me.dgvtiendas = New System.Windows.Forms.DataGridView()
        Me.seleccion1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NombreT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtmontofijo = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtporcentaje = New System.Windows.Forms.TextBox()
        Me.chkporcentaje = New System.Windows.Forms.CheckBox()
        Me.chkmontofijo = New System.Windows.Forms.CheckBox()
        Me.cmbTipocobro = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dgvProductos = New System.Windows.Forms.DataGridView()
        Me.estado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.idtienda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tienda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.subcategoria = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sku = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.moneda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Costo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cobro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipocobro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idsubcategoria = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtdescripcion = New System.Windows.Forms.TextBox()
        Me.txtSku = New System.Windows.Forms.TextBox()
        Me.lbdescripcion = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnagregarmarcas = New System.Windows.Forms.Button()
        Me.btnUpcAgregar = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblnocontrato1 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.tpgespacios = New System.Windows.Forms.TabPage()
        Me.chkdolares1 = New System.Windows.Forms.CheckBox()
        Me.chkquetzales1 = New System.Windows.Forms.CheckBox()
        Me.txtprecio = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbtienda = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtcantidadacobrarE = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbespacio = New System.Windows.Forms.ComboBox()
        Me.dgvEspacio = New System.Windows.Forms.DataGridView()
        Me.seleccionEsp = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Espacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoCobro1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.moneda1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tienda1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantidad1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idtipoespacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EliminarToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnagregarespacio = New System.Windows.Forms.Button()
        Me.btncrearcontrato = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblnocontrato2 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.MarcasBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet3 = New BackMarginLTX.DataSet3()
        Me.TiendasBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet4 = New BackMarginLTX.DataSet4()
        Me.UpcBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet1 = New BackMarginLTX.DataSet1()
        Me.espaciosBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet2 = New BackMarginLTX.DataSet2()
        Me.TabControl1.SuspendLayout()
        Me.tpgdatosbasicos.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgvMarcas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvContactos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpgproductos.SuspendLayout()
        CType(Me.dgvtiendas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tpgespacios.SuspendLayout()
        CType(Me.dgvEspacio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.MarcasBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TiendasBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UpcBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.espaciosBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpgdatosbasicos)
        Me.TabControl1.Controls.Add(Me.tpgproductos)
        Me.TabControl1.Controls.Add(Me.tpgespacios)
        Me.TabControl1.Location = New System.Drawing.Point(31, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(867, 680)
        Me.TabControl1.TabIndex = 0
        '
        'tpgdatosbasicos
        '
        Me.tpgdatosbasicos.Controls.Add(Me.cmbproveedoresFac)
        Me.tpgdatosbasicos.Controls.Add(Me.Label23)
        Me.tpgdatosbasicos.Controls.Add(Me.Panel1)
        Me.tpgdatosbasicos.Controls.Add(Me.btneliminarmarca)
        Me.tpgdatosbasicos.Controls.Add(Me.Button1)
        Me.tpgdatosbasicos.Controls.Add(Me.btnagregarmarca)
        Me.tpgdatosbasicos.Controls.Add(Me.Panel4)
        Me.tpgdatosbasicos.Controls.Add(Me.dgvMarcas)
        Me.tpgdatosbasicos.Controls.Add(Me.cmbMarcas)
        Me.tpgdatosbasicos.Controls.Add(Me.cmbproveedores)
        Me.tpgdatosbasicos.Controls.Add(Me.dgvContactos)
        Me.tpgdatosbasicos.Controls.Add(Me.btnContagregar)
        Me.tpgdatosbasicos.Controls.Add(Me.txtCorreo)
        Me.tpgdatosbasicos.Controls.Add(Me.txtTelefono)
        Me.tpgdatosbasicos.Controls.Add(Me.txtCargo)
        Me.tpgdatosbasicos.Controls.Add(Me.txtCnombre)
        Me.tpgdatosbasicos.Controls.Add(Me.Label12)
        Me.tpgdatosbasicos.Controls.Add(Me.Label11)
        Me.tpgdatosbasicos.Controls.Add(Me.Label10)
        Me.tpgdatosbasicos.Controls.Add(Me.Label9)
        Me.tpgdatosbasicos.Controls.Add(Me.txtNproveedor)
        Me.tpgdatosbasicos.Controls.Add(Me.txtNunisuper)
        Me.tpgdatosbasicos.Controls.Add(Me.Label7)
        Me.tpgdatosbasicos.Controls.Add(Me.Label6)
        Me.tpgdatosbasicos.Controls.Add(Me.cmbformatodecobro)
        Me.tpgdatosbasicos.Controls.Add(Me.Label5)
        Me.tpgdatosbasicos.Controls.Add(Me.dtpFfinal)
        Me.tpgdatosbasicos.Controls.Add(Me.dtpFinicial)
        Me.tpgdatosbasicos.Controls.Add(Me.Label4)
        Me.tpgdatosbasicos.Controls.Add(Me.Label3)
        Me.tpgdatosbasicos.Controls.Add(Me.Label2)
        Me.tpgdatosbasicos.Controls.Add(Me.Label1)
        Me.tpgdatosbasicos.Location = New System.Drawing.Point(4, 22)
        Me.tpgdatosbasicos.Name = "tpgdatosbasicos"
        Me.tpgdatosbasicos.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgdatosbasicos.Size = New System.Drawing.Size(859, 654)
        Me.tpgdatosbasicos.TabIndex = 0
        Me.tpgdatosbasicos.Text = "Crear Contrato"
        Me.tpgdatosbasicos.UseVisualStyleBackColor = True
        '
        'cmbproveedoresFac
        '
        Me.cmbproveedoresFac.FormattingEnabled = True
        Me.cmbproveedoresFac.Location = New System.Drawing.Point(607, 47)
        Me.cmbproveedoresFac.Name = "cmbproveedoresFac"
        Me.cmbproveedoresFac.Size = New System.Drawing.Size(236, 21)
        Me.cmbproveedoresFac.TabIndex = 37
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(491, 56)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(110, 13)
        Me.Label23.TabIndex = 36
        Me.Label23.Text = "Proveedor a Facturar:"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.lblnocontrato)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(863, 36)
        Me.Panel1.TabIndex = 35
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label22.Location = New System.Drawing.Point(5, 2)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(177, 20)
        Me.Label22.TabIndex = 15
        Me.Label22.Text = "Datos  del Proveedor"
        '
        'lblnocontrato
        '
        Me.lblnocontrato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblnocontrato.AutoSize = True
        Me.lblnocontrato.BackColor = System.Drawing.Color.Transparent
        Me.lblnocontrato.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnocontrato.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblnocontrato.Location = New System.Drawing.Point(735, 3)
        Me.lblnocontrato.Name = "lblnocontrato"
        Me.lblnocontrato.Size = New System.Drawing.Size(0, 20)
        Me.lblnocontrato.TabIndex = 29
        '
        'btneliminarmarca
        '
        Me.btneliminarmarca.Image = CType(resources.GetObject("btneliminarmarca.Image"), System.Drawing.Image)
        Me.btneliminarmarca.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btneliminarmarca.Location = New System.Drawing.Point(455, 78)
        Me.btneliminarmarca.Name = "btneliminarmarca"
        Me.btneliminarmarca.Size = New System.Drawing.Size(78, 36)
        Me.btneliminarmarca.TabIndex = 34
        Me.btneliminarmarca.Text = "Eliminar"
        Me.btneliminarmarca.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btneliminarmarca.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(755, 606)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 33)
        Me.Button1.TabIndex = 33
        Me.Button1.Text = "Siguiente"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnagregarmarca
        '
        Me.btnagregarmarca.Image = CType(resources.GetObject("btnagregarmarca.Image"), System.Drawing.Image)
        Me.btnagregarmarca.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnagregarmarca.Location = New System.Drawing.Point(365, 78)
        Me.btnagregarmarca.Name = "btnagregarmarca"
        Me.btnagregarmarca.Size = New System.Drawing.Size(84, 36)
        Me.btnagregarmarca.TabIndex = 32
        Me.btnagregarmarca.Text = "Agregar"
        Me.btnagregarmarca.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnagregarmarca.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Location = New System.Drawing.Point(-4, 334)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(877, 36)
        Me.Panel4.TabIndex = 31
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label8.Location = New System.Drawing.Point(10, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(206, 20)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Contactos del Proveedor"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Location = New System.Drawing.Point(4, 29)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(854, 3)
        Me.Panel7.TabIndex = 38
        '
        'dgvMarcas
        '
        Me.dgvMarcas.AllowUserToAddRows = False
        Me.dgvMarcas.AllowUserToDeleteRows = False
        Me.dgvMarcas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMarcas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.seleccion, Me.id, Me.Marca})
        Me.dgvMarcas.Location = New System.Drawing.Point(19, 111)
        Me.dgvMarcas.Name = "dgvMarcas"
        Me.dgvMarcas.Size = New System.Drawing.Size(325, 92)
        Me.dgvMarcas.TabIndex = 30
        '
        'seleccion
        '
        Me.seleccion.DataPropertyName = "seleccion"
        Me.seleccion.FalseValue = "0"
        Me.seleccion.HeaderText = ""
        Me.seleccion.Name = "seleccion"
        Me.seleccion.TrueValue = "1"
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'Marca
        '
        Me.Marca.DataPropertyName = "Nombre"
        Me.Marca.HeaderText = "Marca"
        Me.Marca.Name = "Marca"
        Me.Marca.ReadOnly = True
        '
        'cmbMarcas
        '
        Me.cmbMarcas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbMarcas.FormattingEnabled = True
        Me.cmbMarcas.Location = New System.Drawing.Point(88, 75)
        Me.cmbMarcas.Name = "cmbMarcas"
        Me.cmbMarcas.Size = New System.Drawing.Size(236, 21)
        Me.cmbMarcas.TabIndex = 28
        '
        'cmbproveedores
        '
        Me.cmbproveedores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbproveedores.FormattingEnabled = True
        Me.cmbproveedores.Location = New System.Drawing.Point(88, 47)
        Me.cmbproveedores.Name = "cmbproveedores"
        Me.cmbproveedores.Size = New System.Drawing.Size(236, 21)
        Me.cmbproveedores.TabIndex = 27
        '
        'dgvContactos
        '
        Me.dgvContactos.AllowUserToAddRows = False
        Me.dgvContactos.AllowUserToDeleteRows = False
        Me.dgvContactos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvContactos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvContactos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre, Me.Cargo, Me.Telefono, Me.Correo})
        Me.dgvContactos.Location = New System.Drawing.Point(24, 489)
        Me.dgvContactos.Name = "dgvContactos"
        Me.dgvContactos.ReadOnly = True
        Me.dgvContactos.Size = New System.Drawing.Size(650, 150)
        Me.dgvContactos.TabIndex = 25
        '
        'Nombre
        '
        Me.Nombre.DataPropertyName = "Nombre"
        Me.Nombre.HeaderText = "Nombre"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'Cargo
        '
        Me.Cargo.DataPropertyName = "Cargo"
        Me.Cargo.HeaderText = "Cargo"
        Me.Cargo.Name = "Cargo"
        Me.Cargo.ReadOnly = True
        '
        'Telefono
        '
        Me.Telefono.DataPropertyName = "Telefono"
        Me.Telefono.HeaderText = "Telefono"
        Me.Telefono.Name = "Telefono"
        Me.Telefono.ReadOnly = True
        '
        'Correo
        '
        Me.Correo.DataPropertyName = "Correo"
        Me.Correo.HeaderText = "Correo Electronico"
        Me.Correo.Name = "Correo"
        Me.Correo.ReadOnly = True
        '
        'btnContagregar
        '
        Me.btnContagregar.Image = CType(resources.GetObject("btnContagregar.Image"), System.Drawing.Image)
        Me.btnContagregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContagregar.Location = New System.Drawing.Point(585, 433)
        Me.btnContagregar.Name = "btnContagregar"
        Me.btnContagregar.Size = New System.Drawing.Size(89, 40)
        Me.btnContagregar.TabIndex = 24
        Me.btnContagregar.Text = "Agregar"
        Me.btnContagregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnContagregar.UseVisualStyleBackColor = True
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(197, 453)
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Size = New System.Drawing.Size(363, 20)
        Me.txtCorreo.TabIndex = 23
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(197, 425)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(363, 20)
        Me.txtTelefono.TabIndex = 22
        '
        'txtCargo
        '
        Me.txtCargo.Location = New System.Drawing.Point(197, 399)
        Me.txtCargo.Name = "txtCargo"
        Me.txtCargo.Size = New System.Drawing.Size(363, 20)
        Me.txtCargo.TabIndex = 21
        '
        'txtCnombre
        '
        Me.txtCnombre.Location = New System.Drawing.Point(197, 372)
        Me.txtCnombre.Name = "txtCnombre"
        Me.txtCnombre.Size = New System.Drawing.Size(363, 20)
        Me.txtCnombre.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 460)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Correo Electronico:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(21, 433)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Telefono:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 406)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Cargo:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(21, 379)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Nombre:"
        '
        'txtNproveedor
        '
        Me.txtNproveedor.Location = New System.Drawing.Point(274, 305)
        Me.txtNproveedor.Name = "txtNproveedor"
        Me.txtNproveedor.Size = New System.Drawing.Size(425, 20)
        Me.txtNproveedor.TabIndex = 14
        '
        'txtNunisuper
        '
        Me.txtNunisuper.Location = New System.Drawing.Point(274, 271)
        Me.txtNunisuper.Name = "txtNunisuper"
        Me.txtNunisuper.Size = New System.Drawing.Size(425, 20)
        Me.txtNunisuper.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 305)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(256, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Nombre Completo del Representante del Proveedor: "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 274)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(239, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Nombre Completo del representante de Unisuper:"
        '
        'cmbformatodecobro
        '
        Me.cmbformatodecobro.FormattingEnabled = True
        Me.cmbformatodecobro.Items.AddRange(New Object() {"Mensual", "Bimensual", "Trimestral", "Semestral", "Anual"})
        Me.cmbformatodecobro.Location = New System.Drawing.Point(117, 245)
        Me.cmbformatodecobro.Name = "cmbformatodecobro"
        Me.cmbformatodecobro.Size = New System.Drawing.Size(121, 21)
        Me.cmbformatodecobro.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 248)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Formato de Cobro:"
        '
        'dtpFfinal
        '
        Me.dtpFfinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFfinal.Location = New System.Drawing.Point(274, 213)
        Me.dtpFfinal.Name = "dtpFfinal"
        Me.dtpFfinal.Size = New System.Drawing.Size(110, 20)
        Me.dtpFfinal.TabIndex = 8
        '
        'dtpFinicial
        '
        Me.dtpFinicial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFinicial.Location = New System.Drawing.Point(91, 214)
        Me.dtpFinicial.Name = "dtpFinicial"
        Me.dtpFinicial.Size = New System.Drawing.Size(101, 20)
        Me.dtpFinicial.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(211, 220)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Fecha Fin:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 220)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Fecha Inicio:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Marca:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Proveedor:"
        '
        'tpgproductos
        '
        Me.tpgproductos.Controls.Add(Me.chbtiendasnuevas)
        Me.tpgproductos.Controls.Add(Me.chkdolares)
        Me.tpgproductos.Controls.Add(Me.chkquetzales)
        Me.tpgproductos.Controls.Add(Me.dgvtiendas)
        Me.tpgproductos.Controls.Add(Me.Label17)
        Me.tpgproductos.Controls.Add(Me.Button2)
        Me.tpgproductos.Controls.Add(Me.txtmontofijo)
        Me.tpgproductos.Controls.Add(Me.Label15)
        Me.tpgproductos.Controls.Add(Me.txtporcentaje)
        Me.tpgproductos.Controls.Add(Me.chkporcentaje)
        Me.tpgproductos.Controls.Add(Me.chkmontofijo)
        Me.tpgproductos.Controls.Add(Me.cmbTipocobro)
        Me.tpgproductos.Controls.Add(Me.Label14)
        Me.tpgproductos.Controls.Add(Me.dgvProductos)
        Me.tpgproductos.Controls.Add(Me.txtdescripcion)
        Me.tpgproductos.Controls.Add(Me.txtSku)
        Me.tpgproductos.Controls.Add(Me.lbdescripcion)
        Me.tpgproductos.Controls.Add(Me.Label13)
        Me.tpgproductos.Controls.Add(Me.btnagregarmarcas)
        Me.tpgproductos.Controls.Add(Me.btnUpcAgregar)
        Me.tpgproductos.Controls.Add(Me.Panel2)
        Me.tpgproductos.Location = New System.Drawing.Point(4, 22)
        Me.tpgproductos.Name = "tpgproductos"
        Me.tpgproductos.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgproductos.Size = New System.Drawing.Size(859, 654)
        Me.tpgproductos.TabIndex = 1
        Me.tpgproductos.Text = "Productos"
        Me.tpgproductos.UseVisualStyleBackColor = True
        '
        'chbtiendasnuevas
        '
        Me.chbtiendasnuevas.AutoSize = True
        Me.chbtiendasnuevas.Location = New System.Drawing.Point(425, 197)
        Me.chbtiendasnuevas.Name = "chbtiendasnuevas"
        Me.chbtiendasnuevas.Size = New System.Drawing.Size(214, 17)
        Me.chbtiendasnuevas.TabIndex = 41
        Me.chbtiendasnuevas.Text = "Agregar las Tiendas Nuevas al Contrato"
        Me.chbtiendasnuevas.UseVisualStyleBackColor = True
        '
        'chkdolares
        '
        Me.chkdolares.AutoSize = True
        Me.chkdolares.Location = New System.Drawing.Point(295, 317)
        Me.chkdolares.Name = "chkdolares"
        Me.chkdolares.Size = New System.Drawing.Size(35, 17)
        Me.chkdolares.TabIndex = 40
        Me.chkdolares.Text = "$."
        Me.chkdolares.UseVisualStyleBackColor = True
        '
        'chkquetzales
        '
        Me.chkquetzales.AutoSize = True
        Me.chkquetzales.Location = New System.Drawing.Point(252, 317)
        Me.chkquetzales.Name = "chkquetzales"
        Me.chkquetzales.Size = New System.Drawing.Size(37, 17)
        Me.chkquetzales.TabIndex = 39
        Me.chkquetzales.Text = "Q."
        Me.chkquetzales.UseVisualStyleBackColor = True
        '
        'dgvtiendas
        '
        Me.dgvtiendas.AllowUserToAddRows = False
        Me.dgvtiendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvtiendas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.seleccion1, Me.NombreT, Me.id1})
        Me.dgvtiendas.Location = New System.Drawing.Point(34, 197)
        Me.dgvtiendas.Name = "dgvtiendas"
        Me.dgvtiendas.Size = New System.Drawing.Size(353, 105)
        Me.dgvtiendas.TabIndex = 37
        '
        'seleccion1
        '
        Me.seleccion1.DataPropertyName = "seleccion"
        Me.seleccion1.FalseValue = "0"
        Me.seleccion1.HeaderText = ""
        Me.seleccion1.Name = "seleccion1"
        Me.seleccion1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.seleccion1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.seleccion1.TrueValue = "1"
        '
        'NombreT
        '
        Me.NombreT.DataPropertyName = "nombre"
        Me.NombreT.HeaderText = "Nombre de las Tiendas"
        Me.NombreT.Name = "NombreT"
        Me.NombreT.ReadOnly = True
        Me.NombreT.Width = 200
        '
        'id1
        '
        Me.id1.DataPropertyName = "id"
        Me.id1.HeaderText = "id"
        Me.id1.Name = "id1"
        Me.id1.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(31, 161)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 13)
        Me.Label17.TabIndex = 36
        Me.Label17.Text = "Tiendas"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.Location = New System.Drawing.Point(755, 615)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(87, 33)
        Me.Button2.TabIndex = 35
        Me.Button2.Text = "Siguiente"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtmontofijo
        '
        Me.txtmontofijo.Location = New System.Drawing.Point(111, 317)
        Me.txtmontofijo.Name = "txtmontofijo"
        Me.txtmontofijo.Size = New System.Drawing.Size(121, 20)
        Me.txtmontofijo.TabIndex = 30
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(411, 356)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(21, 18)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "%"
        '
        'txtporcentaje
        '
        Me.txtporcentaje.Location = New System.Drawing.Point(335, 353)
        Me.txtporcentaje.Name = "txtporcentaje"
        Me.txtporcentaje.Size = New System.Drawing.Size(70, 20)
        Me.txtporcentaje.TabIndex = 12
        '
        'chkporcentaje
        '
        Me.chkporcentaje.AutoSize = True
        Me.chkporcentaje.Location = New System.Drawing.Point(252, 356)
        Me.chkporcentaje.Name = "chkporcentaje"
        Me.chkporcentaje.Size = New System.Drawing.Size(77, 17)
        Me.chkporcentaje.TabIndex = 11
        Me.chkporcentaje.Text = "Porcentaje"
        Me.chkporcentaje.UseVisualStyleBackColor = True
        '
        'chkmontofijo
        '
        Me.chkmontofijo.AutoSize = True
        Me.chkmontofijo.Location = New System.Drawing.Point(34, 320)
        Me.chkmontofijo.Name = "chkmontofijo"
        Me.chkmontofijo.Size = New System.Drawing.Size(75, 17)
        Me.chkmontofijo.TabIndex = 10
        Me.chkmontofijo.Text = "Monto Fijo"
        Me.chkmontofijo.UseVisualStyleBackColor = True
        '
        'cmbTipocobro
        '
        Me.cmbTipocobro.FormattingEnabled = True
        Me.cmbTipocobro.Items.AddRange(New Object() {"Ingreso ", "Ventas"})
        Me.cmbTipocobro.Location = New System.Drawing.Point(111, 352)
        Me.cmbTipocobro.Name = "cmbTipocobro"
        Me.cmbTipocobro.Size = New System.Drawing.Size(121, 21)
        Me.cmbTipocobro.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(31, 360)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Tipo de Cobro"
        '
        'dgvProductos
        '
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToDeleteRows = False
        Me.dgvProductos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.estado, Me.idtienda, Me.Tienda, Me.subcategoria, Me.Sku, Me.Descripcion, Me.moneda, Me.Costo, Me.Cobro, Me.Tipocobro, Me.Cantidad, Me.idsubcategoria})
        Me.dgvProductos.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgvProductos.Location = New System.Drawing.Point(43, 392)
        Me.dgvProductos.Name = "dgvProductos"
        Me.dgvProductos.Size = New System.Drawing.Size(799, 217)
        Me.dgvProductos.TabIndex = 7
        '
        'estado
        '
        Me.estado.ContextMenuStrip = Me.ContextMenuStrip1
        Me.estado.DataPropertyName = "seleccion"
        Me.estado.FalseValue = "0"
        Me.estado.HeaderText = ""
        Me.estado.Name = "estado"
        Me.estado.TrueValue = "1"
        Me.estado.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EliminarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 26)
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        '
        'idtienda
        '
        Me.idtienda.DataPropertyName = "idtienda"
        Me.idtienda.HeaderText = "idtienda"
        Me.idtienda.Name = "idtienda"
        Me.idtienda.Visible = False
        '
        'Tienda
        '
        Me.Tienda.DataPropertyName = "Tienda"
        Me.Tienda.HeaderText = "Tienda"
        Me.Tienda.Name = "Tienda"
        Me.Tienda.ReadOnly = True
        '
        'subcategoria
        '
        Me.subcategoria.DataPropertyName = "Subcategoria"
        Me.subcategoria.HeaderText = "SubCategoria"
        Me.subcategoria.Name = "subcategoria"
        Me.subcategoria.ReadOnly = True
        '
        'Sku
        '
        Me.Sku.DataPropertyName = "Sku"
        Me.Sku.HeaderText = "Plu"
        Me.Sku.Name = "Sku"
        Me.Sku.ReadOnly = True
        '
        'Descripcion
        '
        Me.Descripcion.DataPropertyName = "Descripcion"
        Me.Descripcion.HeaderText = "Descripción"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        Me.Descripcion.Width = 180
        '
        'moneda
        '
        Me.moneda.DataPropertyName = "moneda"
        Me.moneda.HeaderText = "Moneda"
        Me.moneda.Name = "moneda"
        Me.moneda.ReadOnly = True
        '
        'Costo
        '
        Me.Costo.DataPropertyName = "Costo"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = "0.00"
        Me.Costo.DefaultCellStyle = DataGridViewCellStyle1
        Me.Costo.HeaderText = "Costo"
        Me.Costo.Name = "Costo"
        Me.Costo.ReadOnly = True
        Me.Costo.Visible = False
        '
        'Cobro
        '
        Me.Cobro.DataPropertyName = "cobro"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0.00"
        Me.Cobro.DefaultCellStyle = DataGridViewCellStyle2
        Me.Cobro.HeaderText = "Cobro"
        Me.Cobro.Name = "Cobro"
        Me.Cobro.ReadOnly = True
        '
        'Tipocobro
        '
        Me.Tipocobro.DataPropertyName = "tipocobro"
        Me.Tipocobro.HeaderText = "Tipo Cobro"
        Me.Tipocobro.Name = "Tipocobro"
        Me.Tipocobro.ReadOnly = True
        '
        'Cantidad
        '
        Me.Cantidad.DataPropertyName = "cantidadcobrar"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0.00"
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle3
        Me.Cantidad.HeaderText = "Cantidad a cobrar"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        '
        'idsubcategoria
        '
        Me.idsubcategoria.DataPropertyName = "IdSubcategoria"
        Me.idsubcategoria.HeaderText = "idsubcategoria"
        Me.idsubcategoria.Name = "idsubcategoria"
        Me.idsubcategoria.Visible = False
        '
        'txtdescripcion
        '
        Me.txtdescripcion.Enabled = False
        Me.txtdescripcion.Location = New System.Drawing.Point(114, 80)
        Me.txtdescripcion.Multiline = True
        Me.txtdescripcion.Name = "txtdescripcion"
        Me.txtdescripcion.Size = New System.Drawing.Size(254, 70)
        Me.txtdescripcion.TabIndex = 3
        '
        'txtSku
        '
        Me.txtSku.Location = New System.Drawing.Point(114, 51)
        Me.txtSku.Name = "txtSku"
        Me.txtSku.Size = New System.Drawing.Size(138, 20)
        Me.txtSku.TabIndex = 2
        '
        'lbdescripcion
        '
        Me.lbdescripcion.AutoSize = True
        Me.lbdescripcion.Location = New System.Drawing.Point(31, 85)
        Me.lbdescripcion.Name = "lbdescripcion"
        Me.lbdescripcion.Size = New System.Drawing.Size(63, 13)
        Me.lbdescripcion.TabIndex = 1
        Me.lbdescripcion.Text = "Descripcion"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(31, 54)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(22, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Plu"
        '
        'btnagregarmarcas
        '
        Me.btnagregarmarcas.Image = CType(resources.GetObject("btnagregarmarcas.Image"), System.Drawing.Image)
        Me.btnagregarmarcas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnagregarmarcas.Location = New System.Drawing.Point(621, 339)
        Me.btnagregarmarcas.Name = "btnagregarmarcas"
        Me.btnagregarmarcas.Size = New System.Drawing.Size(156, 36)
        Me.btnagregarmarcas.TabIndex = 38
        Me.btnagregarmarcas.Text = "Agregar Productos de las Marcas"
        Me.btnagregarmarcas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnagregarmarcas.UseVisualStyleBackColor = True
        '
        'btnUpcAgregar
        '
        Me.btnUpcAgregar.Enabled = False
        Me.btnUpcAgregar.Image = CType(resources.GetObject("btnUpcAgregar.Image"), System.Drawing.Image)
        Me.btnUpcAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpcAgregar.Location = New System.Drawing.Point(532, 339)
        Me.btnUpcAgregar.Name = "btnUpcAgregar"
        Me.btnUpcAgregar.Size = New System.Drawing.Size(83, 36)
        Me.btnUpcAgregar.TabIndex = 5
        Me.btnUpcAgregar.Text = "Agregar"
        Me.btnUpcAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUpcAgregar.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblnocontrato1)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Location = New System.Drawing.Point(-8, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(867, 40)
        Me.Panel2.TabIndex = 26
        '
        'lblnocontrato1
        '
        Me.lblnocontrato1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblnocontrato1.AutoSize = True
        Me.lblnocontrato1.BackColor = System.Drawing.Color.Transparent
        Me.lblnocontrato1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnocontrato1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblnocontrato1.Location = New System.Drawing.Point(751, 3)
        Me.lblnocontrato1.Name = "lblnocontrato1"
        Me.lblnocontrato1.Size = New System.Drawing.Size(0, 20)
        Me.lblnocontrato1.TabIndex = 30
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label19.Location = New System.Drawing.Point(13, 9)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(90, 20)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "Productos"
        '
        'tpgespacios
        '
        Me.tpgespacios.Controls.Add(Me.chkdolares1)
        Me.tpgespacios.Controls.Add(Me.chkquetzales1)
        Me.tpgespacios.Controls.Add(Me.txtprecio)
        Me.tpgespacios.Controls.Add(Me.Label21)
        Me.tpgespacios.Controls.Add(Me.cmbtienda)
        Me.tpgespacios.Controls.Add(Me.Label18)
        Me.tpgespacios.Controls.Add(Me.Label24)
        Me.tpgespacios.Controls.Add(Me.txtcantidadacobrarE)
        Me.tpgespacios.Controls.Add(Me.Label16)
        Me.tpgespacios.Controls.Add(Me.cmbespacio)
        Me.tpgespacios.Controls.Add(Me.dgvEspacio)
        Me.tpgespacios.Controls.Add(Me.btnagregarespacio)
        Me.tpgespacios.Controls.Add(Me.btncrearcontrato)
        Me.tpgespacios.Controls.Add(Me.Button3)
        Me.tpgespacios.Controls.Add(Me.Panel3)
        Me.tpgespacios.Location = New System.Drawing.Point(4, 22)
        Me.tpgespacios.Name = "tpgespacios"
        Me.tpgespacios.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgespacios.Size = New System.Drawing.Size(859, 654)
        Me.tpgespacios.TabIndex = 3
        Me.tpgespacios.Text = "Espacios"
        Me.tpgespacios.UseVisualStyleBackColor = True
        '
        'chkdolares1
        '
        Me.chkdolares1.AutoSize = True
        Me.chkdolares1.Location = New System.Drawing.Point(413, 155)
        Me.chkdolares1.Name = "chkdolares1"
        Me.chkdolares1.Size = New System.Drawing.Size(32, 17)
        Me.chkdolares1.TabIndex = 52
        Me.chkdolares1.Text = "$"
        Me.chkdolares1.UseVisualStyleBackColor = True
        '
        'chkquetzales1
        '
        Me.chkquetzales1.AutoSize = True
        Me.chkquetzales1.Location = New System.Drawing.Point(370, 155)
        Me.chkquetzales1.Name = "chkquetzales1"
        Me.chkquetzales1.Size = New System.Drawing.Size(37, 17)
        Me.chkquetzales1.TabIndex = 51
        Me.chkquetzales1.Text = "Q."
        Me.chkquetzales1.UseVisualStyleBackColor = True
        '
        'txtprecio
        '
        Me.txtprecio.Location = New System.Drawing.Point(224, 149)
        Me.txtprecio.Name = "txtprecio"
        Me.txtprecio.Size = New System.Drawing.Size(121, 20)
        Me.txtprecio.TabIndex = 50
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(96, 156)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(95, 13)
        Me.Label21.TabIndex = 49
        Me.Label21.Text = "Precio del Espacio"
        '
        'cmbtienda
        '
        Me.cmbtienda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbtienda.FormattingEnabled = True
        Me.cmbtienda.Location = New System.Drawing.Point(224, 87)
        Me.cmbtienda.Name = "cmbtienda"
        Me.cmbtienda.Size = New System.Drawing.Size(243, 21)
        Me.cmbtienda.TabIndex = 48
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(96, 95)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(40, 13)
        Me.Label18.TabIndex = 47
        Me.Label18.Text = "Tienda"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(96, 188)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(109, 13)
        Me.Label24.TabIndex = 45
        Me.Label24.Text = "Cantidad de espacios"
        '
        'txtcantidadacobrarE
        '
        Me.txtcantidadacobrarE.Location = New System.Drawing.Point(224, 180)
        Me.txtcantidadacobrarE.Name = "txtcantidadacobrarE"
        Me.txtcantidadacobrarE.Size = New System.Drawing.Size(89, 20)
        Me.txtcantidadacobrarE.TabIndex = 44
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(96, 126)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Espacio"
        '
        'cmbespacio
        '
        Me.cmbespacio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbespacio.FormattingEnabled = True
        Me.cmbespacio.Location = New System.Drawing.Point(224, 118)
        Me.cmbespacio.Name = "cmbespacio"
        Me.cmbespacio.Size = New System.Drawing.Size(243, 21)
        Me.cmbespacio.TabIndex = 38
        '
        'dgvEspacio
        '
        Me.dgvEspacio.AllowUserToAddRows = False
        Me.dgvEspacio.AllowUserToDeleteRows = False
        Me.dgvEspacio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEspacio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEspacio.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.seleccionEsp, Me.Espacio, Me.TipoCobro1, Me.moneda1, Me.Tienda1, Me.cantidad1, Me.Precio, Me.total, Me.idtipoespacio})
        Me.dgvEspacio.ContextMenuStrip = Me.ContextMenuStrip2
        Me.dgvEspacio.Location = New System.Drawing.Point(7, 246)
        Me.dgvEspacio.Name = "dgvEspacio"
        Me.dgvEspacio.Size = New System.Drawing.Size(791, 292)
        Me.dgvEspacio.TabIndex = 37
        '
        'seleccionEsp
        '
        Me.seleccionEsp.DataPropertyName = "seleccion"
        Me.seleccionEsp.FalseValue = "0"
        Me.seleccionEsp.HeaderText = ""
        Me.seleccionEsp.Name = "seleccionEsp"
        Me.seleccionEsp.TrueValue = "1"
        Me.seleccionEsp.Visible = False
        '
        'Espacio
        '
        Me.Espacio.DataPropertyName = "espacio"
        Me.Espacio.HeaderText = "Espacio"
        Me.Espacio.Name = "Espacio"
        Me.Espacio.ReadOnly = True
        Me.Espacio.Width = 150
        '
        'TipoCobro1
        '
        Me.TipoCobro1.DataPropertyName = "tipocobro"
        Me.TipoCobro1.HeaderText = "Tipo de Cobro"
        Me.TipoCobro1.Name = "TipoCobro1"
        Me.TipoCobro1.Visible = False
        '
        'moneda1
        '
        Me.moneda1.DataPropertyName = "moneda"
        Me.moneda1.HeaderText = "Moneda"
        Me.moneda1.Name = "moneda1"
        '
        'Tienda1
        '
        Me.Tienda1.DataPropertyName = "tienda"
        Me.Tienda1.HeaderText = "Tienda"
        Me.Tienda1.Name = "Tienda1"
        Me.Tienda1.ReadOnly = True
        Me.Tienda1.Width = 200
        '
        'cantidad1
        '
        Me.cantidad1.DataPropertyName = "cantidad"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.cantidad1.DefaultCellStyle = DataGridViewCellStyle4
        Me.cantidad1.HeaderText = "Cantidad"
        Me.cantidad1.Name = "cantidad1"
        Me.cantidad1.ReadOnly = True
        '
        'Precio
        '
        Me.Precio.DataPropertyName = "precio"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0.00"
        Me.Precio.DefaultCellStyle = DataGridViewCellStyle5
        Me.Precio.HeaderText = "Costo Espacio"
        Me.Precio.Name = "Precio"
        Me.Precio.ReadOnly = True
        '
        'total
        '
        Me.total.DataPropertyName = "total"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0.00"
        Me.total.DefaultCellStyle = DataGridViewCellStyle6
        Me.total.HeaderText = "Total"
        Me.total.Name = "total"
        Me.total.ReadOnly = True
        '
        'idtipoespacio
        '
        Me.idtipoespacio.DataPropertyName = "idtipoespacio"
        Me.idtipoespacio.HeaderText = "idtipoespacio"
        Me.idtipoespacio.Name = "idtipoespacio"
        Me.idtipoespacio.Visible = False
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EliminarToolStripMenuItem1})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(118, 26)
        '
        'EliminarToolStripMenuItem1
        '
        Me.EliminarToolStripMenuItem1.Name = "EliminarToolStripMenuItem1"
        Me.EliminarToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
        Me.EliminarToolStripMenuItem1.Text = "Eliminar"
        '
        'btnagregarespacio
        '
        Me.btnagregarespacio.BackColor = System.Drawing.SystemColors.MenuBar
        Me.btnagregarespacio.Image = CType(resources.GetObject("btnagregarespacio.Image"), System.Drawing.Image)
        Me.btnagregarespacio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnagregarespacio.Location = New System.Drawing.Point(551, 188)
        Me.btnagregarespacio.Name = "btnagregarespacio"
        Me.btnagregarespacio.Size = New System.Drawing.Size(90, 36)
        Me.btnagregarespacio.TabIndex = 40
        Me.btnagregarespacio.Text = "Agregar"
        Me.btnagregarespacio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnagregarespacio.UseVisualStyleBackColor = True
        '
        'btncrearcontrato
        '
        Me.btncrearcontrato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncrearcontrato.Image = CType(resources.GetObject("btncrearcontrato.Image"), System.Drawing.Image)
        Me.btncrearcontrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btncrearcontrato.Location = New System.Drawing.Point(585, 603)
        Me.btncrearcontrato.Name = "btncrearcontrato"
        Me.btncrearcontrato.Size = New System.Drawing.Size(110, 39)
        Me.btncrearcontrato.TabIndex = 36
        Me.btncrearcontrato.Text = "Crear Contrato"
        Me.btncrearcontrato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncrearcontrato.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(716, 603)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(101, 39)
        Me.Button3.TabIndex = 35
        Me.Button3.Text = "Salir"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.lblnocontrato2)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(867, 40)
        Me.Panel3.TabIndex = 46
        '
        'lblnocontrato2
        '
        Me.lblnocontrato2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblnocontrato2.AutoSize = True
        Me.lblnocontrato2.BackColor = System.Drawing.Color.Transparent
        Me.lblnocontrato2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnocontrato2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblnocontrato2.Location = New System.Drawing.Point(751, 3)
        Me.lblnocontrato2.Name = "lblnocontrato2"
        Me.lblnocontrato2.Size = New System.Drawing.Size(0, 20)
        Me.lblnocontrato2.TabIndex = 47
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label20.Location = New System.Drawing.Point(3, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(82, 20)
        Me.Label20.TabIndex = 25
        Me.Label20.Text = "Espacios"
        '
        'MarcasBindingSource1
        '
        Me.MarcasBindingSource1.DataMember = "Marca"
        Me.MarcasBindingSource1.DataSource = Me.DataSet3
        '
        'DataSet3
        '
        Me.DataSet3.DataSetName = "DataSet3"
        Me.DataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TiendasBindingSource1
        '
        Me.TiendasBindingSource1.DataMember = "Tienda"
        Me.TiendasBindingSource1.DataSource = Me.DataSet4
        '
        'DataSet4
        '
        Me.DataSet4.DataSetName = "DataSet4"
        Me.DataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'UpcBindingSource1
        '
        Me.UpcBindingSource1.DataMember = "productos"
        Me.UpcBindingSource1.DataSource = Me.DataSet1
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "DataSet1"
        Me.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'espaciosBindingSource1
        '
        Me.espaciosBindingSource1.DataMember = "espacios"
        Me.espaciosBindingSource1.DataSource = Me.DataSet2
        '
        'DataSet2
        '
        Me.DataSet2.DataSetName = "DataSet2"
        Me.DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmcontrato
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 733)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmcontrato"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BackMargin  LTExpress"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.tpgdatosbasicos.ResumeLayout(False)
        Me.tpgdatosbasicos.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgvMarcas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvContactos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpgproductos.ResumeLayout(False)
        Me.tpgproductos.PerformLayout()
        CType(Me.dgvtiendas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tpgespacios.ResumeLayout(False)
        Me.tpgespacios.PerformLayout()
        CType(Me.dgvEspacio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.MarcasBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TiendasBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UpcBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.espaciosBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpgdatosbasicos As System.Windows.Forms.TabPage
    Friend WithEvents tpgproductos As System.Windows.Forms.TabPage
    Friend WithEvents dtpFfinal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFinicial As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvContactos As System.Windows.Forms.DataGridView
    Friend WithEvents btnContagregar As System.Windows.Forms.Button
    Friend WithEvents txtCorreo As System.Windows.Forms.TextBox
    Friend WithEvents txtTelefono As System.Windows.Forms.TextBox
    Friend WithEvents txtCargo As System.Windows.Forms.TextBox
    Friend WithEvents txtCnombre As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNproveedor As System.Windows.Forms.TextBox
    Friend WithEvents txtNunisuper As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbformatodecobro As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtdescripcion As System.Windows.Forms.TextBox
    Friend WithEvents txtSku As System.Windows.Forms.TextBox
    Friend WithEvents lbdescripcion As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnUpcAgregar As System.Windows.Forms.Button
    Friend WithEvents dgvProductos As System.Windows.Forms.DataGridView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtporcentaje As System.Windows.Forms.TextBox
    Friend WithEvents chkporcentaje As System.Windows.Forms.CheckBox
    Friend WithEvents chkmontofijo As System.Windows.Forms.CheckBox
    Friend WithEvents cmbTipocobro As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbproveedores As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMarcas As System.Windows.Forms.ComboBox
    Friend WithEvents lblnocontrato As System.Windows.Forms.Label
    Friend WithEvents lblnocontrato1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents dgvMarcas As System.Windows.Forms.DataGridView
    Friend WithEvents btnagregarmarca As System.Windows.Forms.Button
    Friend WithEvents UpcBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet1 As BackMarginLTX.DataSet1
    Friend WithEvents espaciosBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet2 As BackMarginLTX.DataSet2
    Friend WithEvents txtmontofijo As System.Windows.Forms.TextBox
    Friend WithEvents tpgespacios As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btneliminarmarca As System.Windows.Forms.Button
    Friend WithEvents MarcasBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet3 As BackMarginLTX.DataSet3
    Friend WithEvents TiendasBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet4 As BackMarginLTX.DataSet4
    Friend WithEvents seleccion As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Marca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cargo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Telefono As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Correo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btncrearcontrato As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents dgvtiendas As System.Windows.Forms.DataGridView
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtcantidadacobrarE As System.Windows.Forms.TextBox
    Friend WithEvents btnagregarespacio As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbespacio As System.Windows.Forms.ComboBox
    Friend WithEvents dgvEspacio As System.Windows.Forms.DataGridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblnocontrato2 As System.Windows.Forms.Label
    Friend WithEvents cmbtienda As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtprecio As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnagregarmarcas As System.Windows.Forms.Button
    Friend WithEvents chkdolares As System.Windows.Forms.CheckBox
    Friend WithEvents chkquetzales As System.Windows.Forms.CheckBox
    Friend WithEvents chkdolares1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkquetzales1 As System.Windows.Forms.CheckBox
    Friend WithEvents seleccion1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NombreT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EliminarToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents chbtiendasnuevas As System.Windows.Forms.CheckBox
    Friend WithEvents seleccionEsp As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Espacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TipoCobro1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moneda1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tienda1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantidad1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Precio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents idtipoespacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbproveedoresFac As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents estado As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents idtienda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tienda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents subcategoria As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sku As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Costo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cobro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tipocobro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents idsubcategoria As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
