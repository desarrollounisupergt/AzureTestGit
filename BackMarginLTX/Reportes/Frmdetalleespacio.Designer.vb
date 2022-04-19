<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frmdetalleespacio
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmdetalleespacio))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtmesfinal = New System.Windows.Forms.TextBox()
        Me.txtproveedor = New System.Windows.Forms.TextBox()
        Me.txtcontrato = New System.Windows.Forms.TextBox()
        Me.txtmesinicial = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvDespacio = New System.Windows.Forms.DataGridView()
        Me.contrato = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Proveedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tienda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.año = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tipoespacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.noespacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tarifaquetzales = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tarifadolares = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Totalq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totald = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtfinal1 = New System.Windows.Forms.TextBox()
        Me.txtproveedor1 = New System.Windows.Forms.TextBox()
        Me.txtcontrato1 = New System.Windows.Forms.TextBox()
        Me.txtmesI1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgvDingresos = New System.Windows.Forms.DataGridView()
        Me.contrato2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sku = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.año2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mes1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tienda2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ucompradas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPorcentaje = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.compras = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Uvendidas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ventas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.txtmesf2 = New System.Windows.Forms.TextBox()
        Me.txtproveedor2 = New System.Windows.Forms.TextBox()
        Me.txtcontrato2 = New System.Windows.Forms.TextBox()
        Me.txtmesI2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dgvDmonto = New System.Windows.Forms.DataGridView()
        Me.contrato1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.proveedor1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sku1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.notiendas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mes2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.año1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totald1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvDespacio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvDingresos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvDmonto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(930, 680)
        Me.TabControl1.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtmesfinal)
        Me.TabPage1.Controls.Add(Me.txtproveedor)
        Me.TabPage1.Controls.Add(Me.txtcontrato)
        Me.TabPage1.Controls.Add(Me.txtmesinicial)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.dgvDespacio)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(922, 654)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detalle Espacio"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtmesfinal
        '
        Me.txtmesfinal.Enabled = False
        Me.txtmesfinal.Location = New System.Drawing.Point(402, 15)
        Me.txtmesfinal.Name = "txtmesfinal"
        Me.txtmesfinal.Size = New System.Drawing.Size(176, 20)
        Me.txtmesfinal.TabIndex = 8
        '
        'txtproveedor
        '
        Me.txtproveedor.Enabled = False
        Me.txtproveedor.Location = New System.Drawing.Point(79, 52)
        Me.txtproveedor.Name = "txtproveedor"
        Me.txtproveedor.Size = New System.Drawing.Size(176, 20)
        Me.txtproveedor.TabIndex = 7
        '
        'txtcontrato
        '
        Me.txtcontrato.Enabled = False
        Me.txtcontrato.Location = New System.Drawing.Point(402, 52)
        Me.txtcontrato.Name = "txtcontrato"
        Me.txtcontrato.Size = New System.Drawing.Size(176, 20)
        Me.txtcontrato.TabIndex = 6
        '
        'txtmesinicial
        '
        Me.txtmesinicial.Enabled = False
        Me.txtmesinicial.Location = New System.Drawing.Point(79, 15)
        Me.txtmesinicial.Name = "txtmesinicial"
        Me.txtmesinicial.Size = New System.Drawing.Size(176, 20)
        Me.txtmesinicial.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(310, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Contrato:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Proveedor:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Mes Final :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Mes Inicial:"
        '
        'dgvDespacio
        '
        Me.dgvDespacio.AllowUserToAddRows = False
        Me.dgvDespacio.AllowUserToDeleteRows = False
        Me.dgvDespacio.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDespacio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDespacio.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.contrato, Me.Proveedor, Me.Tienda, Me.mes, Me.año, Me.tipoespacio, Me.noespacio, Me.tarifaquetzales, Me.tarifadolares, Me.Totalq, Me.totald})
        Me.dgvDespacio.Location = New System.Drawing.Point(17, 109)
        Me.dgvDespacio.Name = "dgvDespacio"
        Me.dgvDespacio.ReadOnly = True
        Me.dgvDespacio.Size = New System.Drawing.Size(884, 517)
        Me.dgvDespacio.TabIndex = 0
        '
        'contrato
        '
        Me.contrato.DataPropertyName = "contrato"
        Me.contrato.HeaderText = "Contrato"
        Me.contrato.Name = "contrato"
        Me.contrato.ReadOnly = True
        '
        'Proveedor
        '
        Me.Proveedor.DataPropertyName = "proveedor"
        Me.Proveedor.HeaderText = "Proveedor"
        Me.Proveedor.Name = "Proveedor"
        Me.Proveedor.ReadOnly = True
        '
        'Tienda
        '
        Me.Tienda.DataPropertyName = "tienda"
        Me.Tienda.HeaderText = "Tienda"
        Me.Tienda.Name = "Tienda"
        Me.Tienda.ReadOnly = True
        '
        'mes
        '
        Me.mes.DataPropertyName = "mes"
        Me.mes.HeaderText = "Mes"
        Me.mes.Name = "mes"
        Me.mes.ReadOnly = True
        '
        'año
        '
        Me.año.DataPropertyName = "id"
        Me.año.HeaderText = "Año"
        Me.año.Name = "año"
        Me.año.ReadOnly = True
        '
        'tipoespacio
        '
        Me.tipoespacio.DataPropertyName = "tipoespacio"
        Me.tipoespacio.HeaderText = "Tipo Espacio"
        Me.tipoespacio.Name = "tipoespacio"
        Me.tipoespacio.ReadOnly = True
        '
        'noespacio
        '
        Me.noespacio.DataPropertyName = "cantidaespacio"
        Me.noespacio.HeaderText = "No. Espacio"
        Me.noespacio.Name = "noespacio"
        Me.noespacio.ReadOnly = True
        '
        'tarifaquetzales
        '
        Me.tarifaquetzales.DataPropertyName = "tarifaquetzales"
        Me.tarifaquetzales.HeaderText = "Tarifa Quetzales"
        Me.tarifaquetzales.Name = "tarifaquetzales"
        Me.tarifaquetzales.ReadOnly = True
        '
        'tarifadolares
        '
        Me.tarifadolares.DataPropertyName = "tarifadolares"
        Me.tarifadolares.HeaderText = "Tarifa Dolares"
        Me.tarifadolares.Name = "tarifadolares"
        Me.tarifadolares.ReadOnly = True
        '
        'Totalq
        '
        Me.Totalq.DataPropertyName = "totalquetzales"
        Me.Totalq.HeaderText = "Total Quetzales"
        Me.Totalq.Name = "Totalq"
        Me.Totalq.ReadOnly = True
        '
        'totald
        '
        Me.totald.DataPropertyName = "totaldolares"
        Me.totald.HeaderText = "Total Dolares"
        Me.totald.Name = "totald"
        Me.totald.ReadOnly = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtfinal1)
        Me.TabPage2.Controls.Add(Me.txtproveedor1)
        Me.TabPage2.Controls.Add(Me.txtcontrato1)
        Me.TabPage2.Controls.Add(Me.txtmesI1)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.dgvDingresos)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(922, 654)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Detalle Ingreso/Ventas"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtfinal1
        '
        Me.txtfinal1.Enabled = False
        Me.txtfinal1.Location = New System.Drawing.Point(402, 15)
        Me.txtfinal1.Name = "txtfinal1"
        Me.txtfinal1.Size = New System.Drawing.Size(176, 20)
        Me.txtfinal1.TabIndex = 16
        '
        'txtproveedor1
        '
        Me.txtproveedor1.Enabled = False
        Me.txtproveedor1.Location = New System.Drawing.Point(79, 52)
        Me.txtproveedor1.Name = "txtproveedor1"
        Me.txtproveedor1.Size = New System.Drawing.Size(176, 20)
        Me.txtproveedor1.TabIndex = 15
        '
        'txtcontrato1
        '
        Me.txtcontrato1.Enabled = False
        Me.txtcontrato1.Location = New System.Drawing.Point(402, 52)
        Me.txtcontrato1.Name = "txtcontrato1"
        Me.txtcontrato1.Size = New System.Drawing.Size(176, 20)
        Me.txtcontrato1.TabIndex = 14
        '
        'txtmesI1
        '
        Me.txtmesI1.Enabled = False
        Me.txtmesI1.Location = New System.Drawing.Point(79, 15)
        Me.txtmesI1.Name = "txtmesI1"
        Me.txtmesI1.Size = New System.Drawing.Size(176, 20)
        Me.txtmesI1.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(310, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Contrato:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Proveedor:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(310, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Mes Final :"
        Me.Label7.UseWaitCursor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Mes Inicial:"
        '
        'dgvDingresos
        '
        Me.dgvDingresos.AllowUserToAddRows = False
        Me.dgvDingresos.AllowUserToDeleteRows = False
        Me.dgvDingresos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDingresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDingresos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.contrato2, Me.sku, Me.Descripcion, Me.año2, Me.mes1, Me.tienda2, Me.Ucompradas, Me.colPorcentaje, Me.compras, Me.Uvendidas, Me.ventas})
        Me.dgvDingresos.Location = New System.Drawing.Point(17, 112)
        Me.dgvDingresos.Name = "dgvDingresos"
        Me.dgvDingresos.ReadOnly = True
        Me.dgvDingresos.Size = New System.Drawing.Size(884, 517)
        Me.dgvDingresos.TabIndex = 1
        '
        'contrato2
        '
        Me.contrato2.DataPropertyName = "contrato"
        Me.contrato2.HeaderText = "Contrato"
        Me.contrato2.Name = "contrato2"
        Me.contrato2.ReadOnly = True
        '
        'sku
        '
        Me.sku.DataPropertyName = "sku"
        Me.sku.HeaderText = "Plu"
        Me.sku.Name = "sku"
        Me.sku.ReadOnly = True
        '
        'Descripcion
        '
        Me.Descripcion.DataPropertyName = "descripcion"
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        '
        'año2
        '
        Me.año2.DataPropertyName = "id"
        Me.año2.HeaderText = "Año"
        Me.año2.Name = "año2"
        Me.año2.ReadOnly = True
        '
        'mes1
        '
        Me.mes1.DataPropertyName = "mes"
        Me.mes1.HeaderText = "Mes"
        Me.mes1.Name = "mes1"
        Me.mes1.ReadOnly = True
        '
        'tienda2
        '
        Me.tienda2.DataPropertyName = "tienda"
        Me.tienda2.HeaderText = "Tienda"
        Me.tienda2.Name = "tienda2"
        Me.tienda2.ReadOnly = True
        '
        'Ucompradas
        '
        Me.Ucompradas.DataPropertyName = "unidadescomprada"
        Me.Ucompradas.HeaderText = "Unidades Compradas"
        Me.Ucompradas.Name = "Ucompradas"
        Me.Ucompradas.ReadOnly = True
        '
        'colPorcentaje
        '
        Me.colPorcentaje.DataPropertyName = "porcentaje"
        Me.colPorcentaje.HeaderText = "Porcentaje Cobro"
        Me.colPorcentaje.Name = "colPorcentaje"
        Me.colPorcentaje.ReadOnly = True
        Me.colPorcentaje.Width = 90
        '
        'compras
        '
        Me.compras.DataPropertyName = "compras"
        Me.compras.HeaderText = "Compras"
        Me.compras.Name = "compras"
        Me.compras.ReadOnly = True
        '
        'Uvendidas
        '
        Me.Uvendidas.DataPropertyName = "unidadesvendidas"
        Me.Uvendidas.HeaderText = "Unidades Vendidas"
        Me.Uvendidas.Name = "Uvendidas"
        Me.Uvendidas.ReadOnly = True
        '
        'ventas
        '
        Me.ventas.DataPropertyName = "ventas"
        Me.ventas.HeaderText = "Ventas"
        Me.ventas.Name = "ventas"
        Me.ventas.ReadOnly = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.txtmesf2)
        Me.TabPage3.Controls.Add(Me.txtproveedor2)
        Me.TabPage3.Controls.Add(Me.txtcontrato2)
        Me.TabPage3.Controls.Add(Me.txtmesI2)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.dgvDmonto)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(922, 654)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Detalle Monto Fijo"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'txtmesf2
        '
        Me.txtmesf2.Enabled = False
        Me.txtmesf2.Location = New System.Drawing.Point(402, 15)
        Me.txtmesf2.Name = "txtmesf2"
        Me.txtmesf2.Size = New System.Drawing.Size(176, 20)
        Me.txtmesf2.TabIndex = 16
        '
        'txtproveedor2
        '
        Me.txtproveedor2.Enabled = False
        Me.txtproveedor2.Location = New System.Drawing.Point(79, 52)
        Me.txtproveedor2.Name = "txtproveedor2"
        Me.txtproveedor2.Size = New System.Drawing.Size(176, 20)
        Me.txtproveedor2.TabIndex = 15
        '
        'txtcontrato2
        '
        Me.txtcontrato2.Enabled = False
        Me.txtcontrato2.Location = New System.Drawing.Point(402, 52)
        Me.txtcontrato2.Name = "txtcontrato2"
        Me.txtcontrato2.Size = New System.Drawing.Size(176, 20)
        Me.txtcontrato2.TabIndex = 14
        '
        'txtmesI2
        '
        Me.txtmesI2.Enabled = False
        Me.txtmesI2.Location = New System.Drawing.Point(79, 15)
        Me.txtmesI2.Name = "txtmesI2"
        Me.txtmesI2.Size = New System.Drawing.Size(176, 20)
        Me.txtmesI2.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(310, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Contrato:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Proveedor:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(310, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Mes Final :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 18)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(60, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Mes Inicial:"
        '
        'dgvDmonto
        '
        Me.dgvDmonto.AllowUserToAddRows = False
        Me.dgvDmonto.AllowUserToDeleteRows = False
        Me.dgvDmonto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDmonto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDmonto.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.contrato1, Me.proveedor1, Me.sku1, Me.Descripcion1, Me.notiendas, Me.mes2, Me.año1, Me.total2, Me.totald1})
        Me.dgvDmonto.Location = New System.Drawing.Point(17, 109)
        Me.dgvDmonto.Name = "dgvDmonto"
        Me.dgvDmonto.ReadOnly = True
        Me.dgvDmonto.Size = New System.Drawing.Size(884, 517)
        Me.dgvDmonto.TabIndex = 1
        '
        'contrato1
        '
        Me.contrato1.DataPropertyName = "contrato"
        Me.contrato1.HeaderText = "Contrato"
        Me.contrato1.Name = "contrato1"
        Me.contrato1.ReadOnly = True
        '
        'proveedor1
        '
        Me.proveedor1.DataPropertyName = "proveedor"
        Me.proveedor1.HeaderText = "Proveedor"
        Me.proveedor1.Name = "proveedor1"
        Me.proveedor1.ReadOnly = True
        '
        'sku1
        '
        Me.sku1.DataPropertyName = "sku"
        Me.sku1.HeaderText = "Plu"
        Me.sku1.Name = "sku1"
        Me.sku1.ReadOnly = True
        '
        'Descripcion1
        '
        Me.Descripcion1.DataPropertyName = "descripcion"
        Me.Descripcion1.HeaderText = "Descripción"
        Me.Descripcion1.Name = "Descripcion1"
        Me.Descripcion1.ReadOnly = True
        '
        'notiendas
        '
        Me.notiendas.DataPropertyName = "tienda"
        Me.notiendas.HeaderText = "Tienda"
        Me.notiendas.Name = "notiendas"
        Me.notiendas.ReadOnly = True
        '
        'mes2
        '
        Me.mes2.DataPropertyName = "mes"
        Me.mes2.HeaderText = "Mes"
        Me.mes2.Name = "mes2"
        Me.mes2.ReadOnly = True
        '
        'año1
        '
        Me.año1.DataPropertyName = "id"
        Me.año1.HeaderText = "Año"
        Me.año1.Name = "año1"
        Me.año1.ReadOnly = True
        '
        'total2
        '
        Me.total2.DataPropertyName = "totalquetzales"
        Me.total2.HeaderText = "Total Quetzales"
        Me.total2.Name = "total2"
        Me.total2.ReadOnly = True
        '
        'totald1
        '
        Me.totald1.DataPropertyName = "totaldolares"
        Me.totald1.HeaderText = "Total Dolares"
        Me.totald1.Name = "totald1"
        Me.totald1.ReadOnly = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackgroundImage = Global.BackMarginLTX.My.Resources.Resources.logo_ltx2
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(965, 35)
        Me.Panel1.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label13.Location = New System.Drawing.Point(4, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(158, 18)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Detalle del Contrato"
        '
        'Frmdetalleespacio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 733)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frmdetalleespacio"
        Me.Text = "Back Margin Express"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvDespacio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgvDingresos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.dgvDmonto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtmesfinal As System.Windows.Forms.TextBox
    Friend WithEvents txtproveedor As System.Windows.Forms.TextBox
    Friend WithEvents txtcontrato As System.Windows.Forms.TextBox
    Friend WithEvents txtmesinicial As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvDespacio As System.Windows.Forms.DataGridView
    Friend WithEvents txtfinal1 As System.Windows.Forms.TextBox
    Friend WithEvents txtproveedor1 As System.Windows.Forms.TextBox
    Friend WithEvents txtcontrato1 As System.Windows.Forms.TextBox
    Friend WithEvents txtmesI1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dgvDingresos As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtmesf2 As System.Windows.Forms.TextBox
    Friend WithEvents txtproveedor2 As System.Windows.Forms.TextBox
    Friend WithEvents txtcontrato2 As System.Windows.Forms.TextBox
    Friend WithEvents txtmesI2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dgvDmonto As System.Windows.Forms.DataGridView
    Friend WithEvents contrato As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Proveedor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tienda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents año As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tipoespacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents noespacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tarifaquetzales As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tarifadolares As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Totalq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents totald As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents contrato1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents proveedor1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sku1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents notiendas As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mes2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents año1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents totald1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents contrato2 As DataGridViewTextBoxColumn
    Friend WithEvents sku As DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As DataGridViewTextBoxColumn
    Friend WithEvents año2 As DataGridViewTextBoxColumn
    Friend WithEvents mes1 As DataGridViewTextBoxColumn
    Friend WithEvents tienda2 As DataGridViewTextBoxColumn
    Friend WithEvents Ucompradas As DataGridViewTextBoxColumn
    Friend WithEvents colPorcentaje As DataGridViewTextBoxColumn
    Friend WithEvents compras As DataGridViewTextBoxColumn
    Friend WithEvents Uvendidas As DataGridViewTextBoxColumn
    Friend WithEvents ventas As DataGridViewTextBoxColumn
End Class
