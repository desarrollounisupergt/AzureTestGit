<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenuPrincipal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMenuPrincipal))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnAdministracion = New System.Windows.Forms.Button()
        Me.btnMantemiento = New System.Windows.Forms.Button()
        Me.btnReportes = New System.Windows.Forms.Button()
        Me.btnContratos = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.BackMarginLTX.My.Resources.Resources.logo_ltx2
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel1.Size = New System.Drawing.Size(355, 58)
        Me.Panel1.TabIndex = 4
        '
        'btnAdministracion
        '
        Me.btnAdministracion.Enabled = False
        Me.btnAdministracion.Image = Global.BackMarginLTX.My.Resources.Resources.conference_24
        Me.btnAdministracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdministracion.Location = New System.Drawing.Point(111, 228)
        Me.btnAdministracion.Name = "btnAdministracion"
        Me.btnAdministracion.Size = New System.Drawing.Size(114, 48)
        Me.btnAdministracion.TabIndex = 3
        Me.btnAdministracion.Text = "Usuarios"
        Me.btnAdministracion.UseVisualStyleBackColor = True
        Me.btnAdministracion.Visible = False
        '
        'btnMantemiento
        '
        Me.btnMantemiento.Enabled = False
        Me.btnMantemiento.Image = Global.BackMarginLTX.My.Resources.Resources.folder_2_24
        Me.btnMantemiento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMantemiento.Location = New System.Drawing.Point(111, 174)
        Me.btnMantemiento.Name = "btnMantemiento"
        Me.btnMantemiento.Size = New System.Drawing.Size(114, 48)
        Me.btnMantemiento.TabIndex = 2
        Me.btnMantemiento.Text = "        Mantenimiento"
        Me.btnMantemiento.UseVisualStyleBackColor = True
        '
        'btnReportes
        '
        Me.btnReportes.Enabled = False
        Me.btnReportes.Image = Global.BackMarginLTX.My.Resources.Resources.report_2_24
        Me.btnReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReportes.Location = New System.Drawing.Point(111, 120)
        Me.btnReportes.Name = "btnReportes"
        Me.btnReportes.Size = New System.Drawing.Size(114, 48)
        Me.btnReportes.TabIndex = 1
        Me.btnReportes.Text = "     Reportes"
        Me.btnReportes.UseVisualStyleBackColor = True
        '
        'btnContratos
        '
        Me.btnContratos.Enabled = False
        Me.btnContratos.Image = Global.BackMarginLTX.My.Resources.Resources.add_property_24
        Me.btnContratos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContratos.Location = New System.Drawing.Point(111, 64)
        Me.btnContratos.Name = "btnContratos"
        Me.btnContratos.Size = New System.Drawing.Size(114, 48)
        Me.btnContratos.TabIndex = 0
        Me.btnContratos.Text = "    Contratos"
        Me.btnContratos.UseVisualStyleBackColor = True
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(11, 203)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(62, 13)
        Me.lblVersion.TabIndex = 8
        Me.lblVersion.Text = "V31082017"
        '
        'FrmMenuPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(354, 233)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnAdministracion)
        Me.Controls.Add(Me.btnMantemiento)
        Me.Controls.Add(Me.btnReportes)
        Me.Controls.Add(Me.btnContratos)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmMenuPrincipal"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu Principal"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnContratos As System.Windows.Forms.Button
    Friend WithEvents btnReportes As System.Windows.Forms.Button
    Friend WithEvents btnMantemiento As System.Windows.Forms.Button
    Friend WithEvents btnAdministracion As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblVersion As Label
End Class
