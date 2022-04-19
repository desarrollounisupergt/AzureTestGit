<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMenuContratos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMenuContratos))
        Me.btnModificarcontrato = New System.Windows.Forms.Button()
        Me.btneliminarcontrato = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btncrearcontrato = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnModificarcontrato
        '
        Me.btnModificarcontrato.Enabled = False
        Me.btnModificarcontrato.Image = Global.BackMarginLTX.My.Resources.Resources.edit_property_24
        Me.btnModificarcontrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificarcontrato.Location = New System.Drawing.Point(64, 133)
        Me.btnModificarcontrato.Name = "btnModificarcontrato"
        Me.btnModificarcontrato.Size = New System.Drawing.Size(129, 48)
        Me.btnModificarcontrato.TabIndex = 1
        Me.btnModificarcontrato.Text = "Modificar Contrato"
        Me.btnModificarcontrato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificarcontrato.UseVisualStyleBackColor = True
        '
        'btneliminarcontrato
        '
        Me.btneliminarcontrato.Enabled = False
        Me.btneliminarcontrato.Image = Global.BackMarginLTX.My.Resources.Resources.delete_property_24
        Me.btneliminarcontrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btneliminarcontrato.Location = New System.Drawing.Point(64, 198)
        Me.btneliminarcontrato.Name = "btneliminarcontrato"
        Me.btneliminarcontrato.Size = New System.Drawing.Size(129, 48)
        Me.btneliminarcontrato.TabIndex = 2
        Me.btneliminarcontrato.Text = "Eliminar Contrato"
        Me.btneliminarcontrato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btneliminarcontrato.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackgroundImage = Global.BackMarginLTX.My.Resources.Resources.logo_ltx2
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(275, 50)
        Me.Panel1.TabIndex = 3
        '
        'btncrearcontrato
        '
        Me.btncrearcontrato.Enabled = False
        Me.btncrearcontrato.Image = Global.BackMarginLTX.My.Resources.Resources.add_property_24
        Me.btncrearcontrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btncrearcontrato.Location = New System.Drawing.Point(64, 65)
        Me.btncrearcontrato.Name = "btncrearcontrato"
        Me.btncrearcontrato.Size = New System.Drawing.Size(129, 48)
        Me.btncrearcontrato.TabIndex = 0
        Me.btncrearcontrato.Text = "Crear Contrato"
        Me.btncrearcontrato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncrearcontrato.UseVisualStyleBackColor = True
        '
        'FrmMenuContratos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 290)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btneliminarcontrato)
        Me.Controls.Add(Me.btnModificarcontrato)
        Me.Controls.Add(Me.btncrearcontrato)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmMenuContratos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Back Margin Express"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btncrearcontrato As System.Windows.Forms.Button
    Friend WithEvents btnModificarcontrato As System.Windows.Forms.Button
    Friend WithEvents btneliminarcontrato As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
