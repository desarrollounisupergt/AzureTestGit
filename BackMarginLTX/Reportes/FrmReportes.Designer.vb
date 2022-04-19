<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportes
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportes))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbcontrato = New System.Windows.Forms.ComboBox()
        Me.cmbproveedores = New System.Windows.Forms.ComboBox()
        Me.dgvreportes = New System.Windows.Forms.DataGridView()
        Me.Nocontrato = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.proveedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Espacioq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.espaciod2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.montofijoq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.montofijod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.variable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.procesado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.impresion1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.cmsespacios = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.VerDetalleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcesarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnConsultar = New System.Windows.Forms.Button()
        Me.ckbcontrato = New System.Windows.Forms.CheckBox()
        Me.chkproveedor = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbMesinicial = New System.Windows.Forms.ComboBox()
        Me.cmbMesfinal = New System.Windows.Forms.ComboBox()
        Me.cmbañofinal = New System.Windows.Forms.ComboBox()
        Me.cmbañoinicial = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkconsololidadoC = New System.Windows.Forms.CheckBox()
        Me.chkconsoproveedor = New System.Windows.Forms.CheckBox()
        Me.dgvreportenormal = New System.Windows.Forms.DataGridView()
        Me.colContrato = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.año = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mesfinal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.añofinal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantidadDolares = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadQ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProcesado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.impresion = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.CSV = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.dgvespacios = New System.Windows.Forms.DataGridView()
        Me.nombreespacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantidadespacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.costoespacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tienda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ttotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totalq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvproductosM = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvproductosV = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvproductosC = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCobro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblconteocontratos = New System.Windows.Forms.Label()
        Me.dgvTotales = New System.Windows.Forms.DataGridView()
        Me.pnlFiltros = New System.Windows.Forms.Panel()
        Me.btn_Aceptar = New System.Windows.Forms.Button()
        Me.chTodos = New System.Windows.Forms.CheckBox()
        Me.dgv_Filtros = New System.Windows.Forms.DataGridView()
        Me.colActivo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnElegir_Tienda = New System.Windows.Forms.Button()
        Me.txtLista_Tiendas = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.dgvreportes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsespacios.SuspendLayout()
        CType(Me.dgvreportenormal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvespacios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvproductosM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvproductosV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvproductosC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvTotales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFiltros.SuspendLayout()
        CType(Me.dgv_Filtros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "No. Contrato"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Proveedor"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Mes Inicial"
        '
        'cmbcontrato
        '
        Me.cmbcontrato.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbcontrato.FormattingEnabled = True
        Me.cmbcontrato.Items.AddRange(New Object() {"Todos los Contratos"})
        Me.cmbcontrato.Location = New System.Drawing.Point(109, 58)
        Me.cmbcontrato.Name = "cmbcontrato"
        Me.cmbcontrato.Size = New System.Drawing.Size(234, 21)
        Me.cmbcontrato.TabIndex = 4
        '
        'cmbproveedores
        '
        Me.cmbproveedores.FormattingEnabled = True
        Me.cmbproveedores.Items.AddRange(New Object() {"TODOS LOS PROVEEDORES"})
        Me.cmbproveedores.Location = New System.Drawing.Point(109, 94)
        Me.cmbproveedores.Name = "cmbproveedores"
        Me.cmbproveedores.Size = New System.Drawing.Size(234, 21)
        Me.cmbproveedores.TabIndex = 5
        '
        'dgvreportes
        '
        Me.dgvreportes.AllowUserToAddRows = False
        Me.dgvreportes.AllowUserToDeleteRows = False
        Me.dgvreportes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvreportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvreportes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nocontrato, Me.proveedor, Me.Espacioq, Me.espaciod2, Me.montofijoq, Me.montofijod, Me.variable, Me.procesado, Me.impresion1})
        Me.dgvreportes.ContextMenuStrip = Me.cmsespacios
        Me.dgvreportes.Location = New System.Drawing.Point(32, 210)
        Me.dgvreportes.Name = "dgvreportes"
        Me.dgvreportes.ReadOnly = True
        Me.dgvreportes.Size = New System.Drawing.Size(1124, 441)
        Me.dgvreportes.TabIndex = 8
        '
        'Nocontrato
        '
        Me.Nocontrato.DataPropertyName = "contrato"
        Me.Nocontrato.HeaderText = "No Contrato"
        Me.Nocontrato.Name = "Nocontrato"
        Me.Nocontrato.ReadOnly = True
        '
        'proveedor
        '
        Me.proveedor.DataPropertyName = "proveedor"
        Me.proveedor.HeaderText = "Nombre Proveedor"
        Me.proveedor.Name = "proveedor"
        Me.proveedor.ReadOnly = True
        '
        'Espacioq
        '
        Me.Espacioq.DataPropertyName = "espacioquetzales"
        Me.Espacioq.HeaderText = "Espacio en Quetzales"
        Me.Espacioq.Name = "Espacioq"
        Me.Espacioq.ReadOnly = True
        '
        'espaciod2
        '
        Me.espaciod2.DataPropertyName = "espaciodolares"
        Me.espaciod2.HeaderText = "Espacio en Dolares"
        Me.espaciod2.Name = "espaciod2"
        Me.espaciod2.ReadOnly = True
        '
        'montofijoq
        '
        Me.montofijoq.DataPropertyName = "MontofijoQuetzales"
        Me.montofijoq.HeaderText = "Monto Fijo Quetzales"
        Me.montofijoq.Name = "montofijoq"
        Me.montofijoq.ReadOnly = True
        '
        'montofijod
        '
        Me.montofijod.DataPropertyName = "MontoDolares"
        Me.montofijod.HeaderText = "Monto Fijo Dolares"
        Me.montofijod.Name = "montofijod"
        Me.montofijod.ReadOnly = True
        '
        'variable
        '
        Me.variable.DataPropertyName = "variable"
        Me.variable.HeaderText = "Varible(Ingreso/Ventas)"
        Me.variable.Name = "variable"
        Me.variable.ReadOnly = True
        '
        'procesado
        '
        Me.procesado.DataPropertyName = "procesado"
        Me.procesado.HeaderText = "Procesado"
        Me.procesado.Name = "procesado"
        Me.procesado.ReadOnly = True
        Me.procesado.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.procesado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'impresion1
        '
        Me.impresion1.DataPropertyName = "impresion1"
        Me.impresion1.HeaderText = "Impresión"
        Me.impresion1.Name = "impresion1"
        Me.impresion1.ReadOnly = True
        Me.impresion1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.impresion1.Text = "Impresión"
        Me.impresion1.UseColumnTextForButtonValue = True
        '
        'cmsespacios
        '
        Me.cmsespacios.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VerDetalleToolStripMenuItem, Me.ProcesarToolStripMenuItem})
        Me.cmsespacios.Name = "cmsespacios"
        Me.cmsespacios.Size = New System.Drawing.Size(130, 48)
        '
        'VerDetalleToolStripMenuItem
        '
        Me.VerDetalleToolStripMenuItem.Name = "VerDetalleToolStripMenuItem"
        Me.VerDetalleToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.VerDetalleToolStripMenuItem.Text = "Ver Detalle"
        '
        'ProcesarToolStripMenuItem
        '
        Me.ProcesarToolStripMenuItem.Name = "ProcesarToolStripMenuItem"
        Me.ProcesarToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ProcesarToolStripMenuItem.Text = "Procesar"
        '
        'btnConsultar
        '
        Me.btnConsultar.Image = Global.BackMarginLTX.My.Resources.Resources.search_13_24__1_
        Me.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConsultar.Location = New System.Drawing.Point(983, 123)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Size = New System.Drawing.Size(95, 37)
        Me.btnConsultar.TabIndex = 10
        Me.btnConsultar.Text = "    Consultar"
        Me.btnConsultar.UseVisualStyleBackColor = True
        '
        'ckbcontrato
        '
        Me.ckbcontrato.AutoSize = True
        Me.ckbcontrato.Location = New System.Drawing.Point(12, 63)
        Me.ckbcontrato.Name = "ckbcontrato"
        Me.ckbcontrato.Size = New System.Drawing.Size(15, 14)
        Me.ckbcontrato.TabIndex = 13
        Me.ckbcontrato.UseVisualStyleBackColor = True
        '
        'chkproveedor
        '
        Me.chkproveedor.AutoSize = True
        Me.chkproveedor.Location = New System.Drawing.Point(12, 98)
        Me.chkproveedor.Name = "chkproveedor"
        Me.chkproveedor.Size = New System.Drawing.Size(15, 14)
        Me.chkproveedor.TabIndex = 14
        Me.chkproveedor.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(282, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Mes Final"
        '
        'cmbMesinicial
        '
        Me.cmbMesinicial.FormattingEnabled = True
        Me.cmbMesinicial.Items.AddRange(New Object() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"})
        Me.cmbMesinicial.Location = New System.Drawing.Point(109, 125)
        Me.cmbMesinicial.Name = "cmbMesinicial"
        Me.cmbMesinicial.Size = New System.Drawing.Size(118, 21)
        Me.cmbMesinicial.TabIndex = 16
        '
        'cmbMesfinal
        '
        Me.cmbMesfinal.FormattingEnabled = True
        Me.cmbMesfinal.Items.AddRange(New Object() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"})
        Me.cmbMesfinal.Location = New System.Drawing.Point(360, 126)
        Me.cmbMesfinal.Name = "cmbMesfinal"
        Me.cmbMesfinal.Size = New System.Drawing.Size(118, 21)
        Me.cmbMesfinal.TabIndex = 17
        '
        'cmbañofinal
        '
        Me.cmbañofinal.FormattingEnabled = True
        Me.cmbañofinal.Location = New System.Drawing.Point(360, 170)
        Me.cmbañofinal.Name = "cmbañofinal"
        Me.cmbañofinal.Size = New System.Drawing.Size(98, 21)
        Me.cmbañofinal.TabIndex = 21
        '
        'cmbañoinicial
        '
        Me.cmbañoinicial.FormattingEnabled = True
        Me.cmbañoinicial.Location = New System.Drawing.Point(109, 170)
        Me.cmbañoinicial.Name = "cmbañoinicial"
        Me.cmbañoinicial.Size = New System.Drawing.Size(98, 21)
        Me.cmbañoinicial.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(282, 178)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Año Final"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Año Inicial"
        '
        'chkconsololidadoC
        '
        Me.chkconsololidadoC.AutoSize = True
        Me.chkconsololidadoC.Location = New System.Drawing.Point(397, 62)
        Me.chkconsololidadoC.Name = "chkconsololidadoC"
        Me.chkconsololidadoC.Size = New System.Drawing.Size(75, 17)
        Me.chkconsololidadoC.TabIndex = 22
        Me.chkconsololidadoC.Text = "Consolidar"
        Me.chkconsololidadoC.UseVisualStyleBackColor = True
        '
        'chkconsoproveedor
        '
        Me.chkconsoproveedor.AutoSize = True
        Me.chkconsoproveedor.Location = New System.Drawing.Point(397, 97)
        Me.chkconsoproveedor.Name = "chkconsoproveedor"
        Me.chkconsoproveedor.Size = New System.Drawing.Size(75, 17)
        Me.chkconsoproveedor.TabIndex = 23
        Me.chkconsoproveedor.Text = "Consolidar"
        Me.chkconsoproveedor.UseVisualStyleBackColor = True
        '
        'dgvreportenormal
        '
        Me.dgvreportenormal.AllowUserToAddRows = False
        Me.dgvreportenormal.AllowUserToDeleteRows = False
        Me.dgvreportenormal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvreportenormal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvreportenormal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colContrato, Me.DataGridViewTextBoxColumn2, Me.mes, Me.año, Me.mesfinal, Me.añofinal, Me.cantidadDolares, Me.CantidadQ, Me.colProcesado, Me.impresion, Me.CSV})
        Me.dgvreportenormal.ContextMenuStrip = Me.cmsespacios
        Me.dgvreportenormal.Location = New System.Drawing.Point(32, 210)
        Me.dgvreportenormal.Name = "dgvreportenormal"
        Me.dgvreportenormal.ReadOnly = True
        Me.dgvreportenormal.Size = New System.Drawing.Size(1124, 443)
        Me.dgvreportenormal.TabIndex = 24
        '
        'colContrato
        '
        Me.colContrato.DataPropertyName = "contrato"
        Me.colContrato.HeaderText = "No Contrato"
        Me.colContrato.Name = "colContrato"
        Me.colContrato.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "proveedor"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Nombre Proveedor"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'mes
        '
        Me.mes.DataPropertyName = "mesinicial"
        Me.mes.HeaderText = "Mes Inicial"
        Me.mes.Name = "mes"
        Me.mes.ReadOnly = True
        '
        'año
        '
        Me.año.DataPropertyName = "idinicial"
        Me.año.HeaderText = "Año Inicial"
        Me.año.Name = "año"
        Me.año.ReadOnly = True
        '
        'mesfinal
        '
        Me.mesfinal.DataPropertyName = "mesfinal"
        Me.mesfinal.HeaderText = "Mes Final"
        Me.mesfinal.Name = "mesfinal"
        Me.mesfinal.ReadOnly = True
        '
        'añofinal
        '
        Me.añofinal.DataPropertyName = "idfinal"
        Me.añofinal.HeaderText = "Año Final"
        Me.añofinal.Name = "añofinal"
        Me.añofinal.ReadOnly = True
        '
        'cantidadDolares
        '
        Me.cantidadDolares.DataPropertyName = "cantidaddolares"
        Me.cantidadDolares.HeaderText = "Cantidad en Dolares"
        Me.cantidadDolares.Name = "cantidadDolares"
        Me.cantidadDolares.ReadOnly = True
        '
        'CantidadQ
        '
        Me.CantidadQ.DataPropertyName = "cantidadquetzales"
        Me.CantidadQ.HeaderText = "Cantidad Quetzales"
        Me.CantidadQ.Name = "CantidadQ"
        Me.CantidadQ.ReadOnly = True
        '
        'colProcesado
        '
        Me.colProcesado.DataPropertyName = "procesado"
        Me.colProcesado.HeaderText = "Procesado"
        Me.colProcesado.Name = "colProcesado"
        Me.colProcesado.ReadOnly = True
        Me.colProcesado.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colProcesado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'impresion
        '
        Me.impresion.DataPropertyName = "imprimir"
        Me.impresion.HeaderText = "Imprimir"
        Me.impresion.Name = "impresion"
        Me.impresion.ReadOnly = True
        Me.impresion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.impresion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.impresion.Text = "Imprimir"
        Me.impresion.UseColumnTextForButtonValue = True
        '
        'CSV
        '
        Me.CSV.DataPropertyName = "CSV"
        Me.CSV.HeaderText = "CSV"
        Me.CSV.Name = "CSV"
        Me.CSV.ReadOnly = True
        Me.CSV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CSV.Text = "CSV"
        Me.CSV.UseColumnTextForButtonValue = True
        '
        'dgvespacios
        '
        Me.dgvespacios.AllowUserToAddRows = False
        Me.dgvespacios.AllowUserToDeleteRows = False
        Me.dgvespacios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvespacios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nombreespacio, Me.cantidadespacio, Me.costoespacio, Me.tienda, Me.Ttotal, Me.totalq})
        Me.dgvespacios.Location = New System.Drawing.Point(1112, 97)
        Me.dgvespacios.Name = "dgvespacios"
        Me.dgvespacios.ReadOnly = True
        Me.dgvespacios.Size = New System.Drawing.Size(44, 35)
        Me.dgvespacios.TabIndex = 25
        Me.dgvespacios.Visible = False
        '
        'nombreespacio
        '
        Me.nombreespacio.DataPropertyName = "nombreespacio"
        Me.nombreespacio.HeaderText = "Nombre Espacios"
        Me.nombreespacio.Name = "nombreespacio"
        Me.nombreespacio.ReadOnly = True
        '
        'cantidadespacio
        '
        Me.cantidadespacio.DataPropertyName = "cantidadrentada"
        Me.cantidadespacio.HeaderText = "Cantidad Espacios"
        Me.cantidadespacio.Name = "cantidadespacio"
        Me.cantidadespacio.ReadOnly = True
        '
        'costoespacio
        '
        Me.costoespacio.DataPropertyName = "costoespacio"
        Me.costoespacio.HeaderText = "Costo Espacio"
        Me.costoespacio.Name = "costoespacio"
        Me.costoespacio.ReadOnly = True
        '
        'tienda
        '
        Me.tienda.DataPropertyName = "tienda"
        Me.tienda.HeaderText = "Tienda"
        Me.tienda.Name = "tienda"
        Me.tienda.ReadOnly = True
        '
        'Ttotal
        '
        Me.Ttotal.DataPropertyName = "totaldolares"
        Me.Ttotal.HeaderText = "Total Dolares"
        Me.Ttotal.Name = "Ttotal"
        Me.Ttotal.ReadOnly = True
        '
        'totalq
        '
        Me.totalq.DataPropertyName = "totalquetzales"
        Me.totalq.HeaderText = "Total Quetzales"
        Me.totalq.Name = "totalq"
        Me.totalq.ReadOnly = True
        '
        'dgvproductosM
        '
        Me.dgvproductosM.AllowUserToAddRows = False
        Me.dgvproductosM.AllowUserToDeleteRows = False
        Me.dgvproductosM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvproductosM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8})
        Me.dgvproductosM.Location = New System.Drawing.Point(1112, 97)
        Me.dgvproductosM.Name = "dgvproductosM"
        Me.dgvproductosM.ReadOnly = True
        Me.dgvproductosM.Size = New System.Drawing.Size(44, 35)
        Me.dgvproductosM.TabIndex = 26
        Me.dgvproductosM.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "sku"
        Me.DataGridViewTextBoxColumn3.HeaderText = "SKU"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "mes"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Mes"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "tienda"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Tienda"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "totaldolares"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Total Dolares"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "totalquetzales"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Total Quetzales"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'dgvproductosV
        '
        Me.dgvproductosV.AllowUserToAddRows = False
        Me.dgvproductosV.AllowUserToDeleteRows = False
        Me.dgvproductosV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvproductosV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12})
        Me.dgvproductosV.Location = New System.Drawing.Point(1112, 97)
        Me.dgvproductosV.Name = "dgvproductosV"
        Me.dgvproductosV.ReadOnly = True
        Me.dgvproductosV.Size = New System.Drawing.Size(44, 35)
        Me.dgvproductosV.TabIndex = 27
        Me.dgvproductosV.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "sku"
        Me.DataGridViewTextBoxColumn6.HeaderText = "SKU"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "tienda"
        Me.DataGridViewTextBoxColumn10.HeaderText = "Tienda"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "mes"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Mes"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "descripcion"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Descripcion"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "total"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Total"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'dgvproductosC
        '
        Me.dgvproductosC.AllowUserToAddRows = False
        Me.dgvproductosC.AllowUserToDeleteRows = False
        Me.dgvproductosC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvproductosC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn17, Me.colCobro})
        Me.dgvproductosC.Location = New System.Drawing.Point(1112, 94)
        Me.dgvproductosC.Name = "dgvproductosC"
        Me.dgvproductosC.ReadOnly = True
        Me.dgvproductosC.Size = New System.Drawing.Size(44, 35)
        Me.dgvproductosC.TabIndex = 28
        Me.dgvproductosC.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "sku"
        Me.DataGridViewTextBoxColumn13.HeaderText = "SKU"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "tienda"
        Me.DataGridViewTextBoxColumn14.HeaderText = "Tienda"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "mes"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Mes"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "descripcion"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Descripcion"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "total"
        Me.DataGridViewTextBoxColumn17.HeaderText = "Total"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'colCobro
        '
        Me.colCobro.DataPropertyName = "cobro"
        Me.colCobro.HeaderText = "% Cobro"
        Me.colCobro.Name = "colCobro"
        Me.colCobro.ReadOnly = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackgroundImage = Global.BackMarginLTX.My.Resources.Resources.logo_ltx2
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1191, 41)
        Me.Panel1.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label7.Location = New System.Drawing.Point(13, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 20)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Reportes"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(998, 181)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 18)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Total Contratos: "
        '
        'lblconteocontratos
        '
        Me.lblconteocontratos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblconteocontratos.AutoSize = True
        Me.lblconteocontratos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblconteocontratos.Location = New System.Drawing.Point(1137, 181)
        Me.lblconteocontratos.Name = "lblconteocontratos"
        Me.lblconteocontratos.Size = New System.Drawing.Size(17, 18)
        Me.lblconteocontratos.TabIndex = 31
        Me.lblconteocontratos.Text = "0"
        '
        'dgvTotales
        '
        Me.dgvTotales.AllowUserToAddRows = False
        Me.dgvTotales.AllowUserToDeleteRows = False
        Me.dgvTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTotales.Location = New System.Drawing.Point(1112, 97)
        Me.dgvTotales.Name = "dgvTotales"
        Me.dgvTotales.ReadOnly = True
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = "0.00"
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTotales.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTotales.Size = New System.Drawing.Size(44, 35)
        Me.dgvTotales.TabIndex = 32
        Me.dgvTotales.Visible = False
        '
        'pnlFiltros
        '
        Me.pnlFiltros.Controls.Add(Me.btn_Aceptar)
        Me.pnlFiltros.Controls.Add(Me.chTodos)
        Me.pnlFiltros.Controls.Add(Me.dgv_Filtros)
        Me.pnlFiltros.Location = New System.Drawing.Point(538, 169)
        Me.pnlFiltros.Name = "pnlFiltros"
        Me.pnlFiltros.Size = New System.Drawing.Size(437, 265)
        Me.pnlFiltros.TabIndex = 79
        '
        'btn_Aceptar
        '
        Me.btn_Aceptar.Image = Global.BackMarginLTX.My.Resources.Resources.ok_24
        Me.btn_Aceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Aceptar.Location = New System.Drawing.Point(167, 222)
        Me.btn_Aceptar.Name = "btn_Aceptar"
        Me.btn_Aceptar.Size = New System.Drawing.Size(95, 37)
        Me.btn_Aceptar.TabIndex = 11
        Me.btn_Aceptar.Text = "    Aceptar"
        Me.btn_Aceptar.UseVisualStyleBackColor = True
        '
        'chTodos
        '
        Me.chTodos.AutoSize = True
        Me.chTodos.Location = New System.Drawing.Point(35, 22)
        Me.chTodos.Name = "chTodos"
        Me.chTodos.Size = New System.Drawing.Size(15, 14)
        Me.chTodos.TabIndex = 5
        Me.chTodos.UseVisualStyleBackColor = True
        '
        'dgv_Filtros
        '
        Me.dgv_Filtros.AllowUserToAddRows = False
        Me.dgv_Filtros.AllowUserToDeleteRows = False
        Me.dgv_Filtros.BackgroundColor = System.Drawing.Color.White
        Me.dgv_Filtros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgv_Filtros.ColumnHeadersHeight = 30
        Me.dgv_Filtros.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colActivo})
        Me.dgv_Filtros.Location = New System.Drawing.Point(10, 12)
        Me.dgv_Filtros.Name = "dgv_Filtros"
        Me.dgv_Filtros.RowHeadersVisible = False
        Me.dgv_Filtros.Size = New System.Drawing.Size(417, 198)
        Me.dgv_Filtros.TabIndex = 2
        '
        'colActivo
        '
        Me.colActivo.DataPropertyName = "activo"
        Me.colActivo.FalseValue = "0"
        Me.colActivo.HeaderText = ""
        Me.colActivo.Name = "colActivo"
        Me.colActivo.TrueValue = "1"
        Me.colActivo.Width = 60
        '
        'btnElegir_Tienda
        '
        Me.btnElegir_Tienda.Location = New System.Drawing.Point(904, 130)
        Me.btnElegir_Tienda.Name = "btnElegir_Tienda"
        Me.btnElegir_Tienda.Size = New System.Drawing.Size(47, 23)
        Me.btnElegir_Tienda.TabIndex = 83
        Me.btnElegir_Tienda.Text = "Elegir"
        Me.btnElegir_Tienda.UseVisualStyleBackColor = True
        '
        'txtLista_Tiendas
        '
        Me.txtLista_Tiendas.Enabled = False
        Me.txtLista_Tiendas.Location = New System.Drawing.Point(538, 123)
        Me.txtLista_Tiendas.Multiline = True
        Me.txtLista_Tiendas.Name = "txtLista_Tiendas"
        Me.txtLista_Tiendas.Size = New System.Drawing.Size(360, 40)
        Me.txtLista_Tiendas.TabIndex = 82
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(490, 125)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 13)
        Me.Label9.TabIndex = 80
        Me.Label9.Text = "Tienda"
        '
        'FrmReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1190, 696)
        Me.Controls.Add(Me.btnElegir_Tienda)
        Me.Controls.Add(Me.txtLista_Tiendas)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.pnlFiltros)
        Me.Controls.Add(Me.dgvTotales)
        Me.Controls.Add(Me.lblconteocontratos)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dgvproductosC)
        Me.Controls.Add(Me.dgvproductosV)
        Me.Controls.Add(Me.dgvproductosM)
        Me.Controls.Add(Me.dgvespacios)
        Me.Controls.Add(Me.dgvreportenormal)
        Me.Controls.Add(Me.chkconsoproveedor)
        Me.Controls.Add(Me.chkconsololidadoC)
        Me.Controls.Add(Me.cmbañofinal)
        Me.Controls.Add(Me.cmbañoinicial)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbMesfinal)
        Me.Controls.Add(Me.cmbMesinicial)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkproveedor)
        Me.Controls.Add(Me.ckbcontrato)
        Me.Controls.Add(Me.btnConsultar)
        Me.Controls.Add(Me.dgvreportes)
        Me.Controls.Add(Me.cmbproveedores)
        Me.Controls.Add(Me.cmbcontrato)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmReportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Back Margin Express"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvreportes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsespacios.ResumeLayout(False)
        CType(Me.dgvreportenormal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvespacios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvproductosM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvproductosV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvproductosC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvTotales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFiltros.ResumeLayout(False)
        Me.pnlFiltros.PerformLayout()
        CType(Me.dgv_Filtros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbcontrato As System.Windows.Forms.ComboBox
    Friend WithEvents cmbproveedores As System.Windows.Forms.ComboBox
    Friend WithEvents dgvreportes As System.Windows.Forms.DataGridView
    Friend WithEvents btnConsultar As System.Windows.Forms.Button
    Friend WithEvents ckbcontrato As System.Windows.Forms.CheckBox
    Friend WithEvents chkproveedor As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmsespacios As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents VerDetalleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbMesinicial As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMesfinal As System.Windows.Forms.ComboBox
    Friend WithEvents cmbañofinal As System.Windows.Forms.ComboBox
    Friend WithEvents cmbañoinicial As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ProcesarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkconsololidadoC As System.Windows.Forms.CheckBox
    Friend WithEvents chkconsoproveedor As System.Windows.Forms.CheckBox
    Friend WithEvents dgvreportenormal As System.Windows.Forms.DataGridView
    Friend WithEvents dgvespacios As System.Windows.Forms.DataGridView
    Friend WithEvents dgvproductosM As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvproductosV As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvproductosC As System.Windows.Forms.DataGridView
    Friend WithEvents nombreespacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantidadespacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents costoespacio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tienda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ttotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents totalq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblconteocontratos As System.Windows.Forms.Label
    Friend WithEvents dgvTotales As System.Windows.Forms.DataGridView
    Friend WithEvents colContrato As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents mes As DataGridViewTextBoxColumn
    Friend WithEvents año As DataGridViewTextBoxColumn
    Friend WithEvents mesfinal As DataGridViewTextBoxColumn
    Friend WithEvents añofinal As DataGridViewTextBoxColumn
    Friend WithEvents cantidadDolares As DataGridViewTextBoxColumn
    Friend WithEvents CantidadQ As DataGridViewTextBoxColumn
    Friend WithEvents colProcesado As DataGridViewCheckBoxColumn
    Friend WithEvents impresion As DataGridViewButtonColumn
    Friend WithEvents CSV As DataGridViewButtonColumn
    Friend WithEvents pnlFiltros As Panel
    Friend WithEvents chTodos As CheckBox
    Friend WithEvents dgv_Filtros As DataGridView
    Friend WithEvents colActivo As DataGridViewCheckBoxColumn
    Friend WithEvents btnElegir_Tienda As Button
    Friend WithEvents txtLista_Tiendas As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btn_Aceptar As Button
    Friend WithEvents Nocontrato As DataGridViewTextBoxColumn
    Friend WithEvents proveedor As DataGridViewTextBoxColumn
    Friend WithEvents Espacioq As DataGridViewTextBoxColumn
    Friend WithEvents espaciod2 As DataGridViewTextBoxColumn
    Friend WithEvents montofijoq As DataGridViewTextBoxColumn
    Friend WithEvents montofijod As DataGridViewTextBoxColumn
    Friend WithEvents variable As DataGridViewTextBoxColumn
    Friend WithEvents procesado As DataGridViewCheckBoxColumn
    Friend WithEvents impresion1 As DataGridViewButtonColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
    Friend WithEvents colCobro As DataGridViewTextBoxColumn
End Class
