<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        txtTelefone = New TextBox()
        btnConsultar = New Button()
        txtResposta = New TextBox()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(68, 40)
        Label1.Name = "Label1"
        Label1.Size = New Size(105, 31)
        Label1.TabIndex = 0
        Label1.Text = "Telefone"
        ' 
        ' txtTelefone
        ' 
        txtTelefone.Font = New Font("Segoe UI", 12F)
        txtTelefone.Location = New Point(68, 74)
        txtTelefone.Name = "txtTelefone"
        txtTelefone.Size = New Size(202, 34)
        txtTelefone.TabIndex = 1
        ' 
        ' btnConsultar
        ' 
        btnConsultar.BackColor = SystemColors.Control
        btnConsultar.Cursor = Cursors.Hand
        btnConsultar.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnConsultar.Location = New Point(68, 114)
        btnConsultar.Name = "btnConsultar"
        btnConsultar.Size = New Size(202, 39)
        btnConsultar.TabIndex = 2
        btnConsultar.Text = "Consultar"
        btnConsultar.UseVisualStyleBackColor = False
        ' 
        ' txtResposta
        ' 
        txtResposta.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtResposta.Location = New Point(68, 164)
        txtResposta.Multiline = True
        txtResposta.Name = "txtResposta"
        txtResposta.ReadOnly = True
        txtResposta.ScrollBars = ScrollBars.Both
        txtResposta.Size = New Size(778, 367)
        txtResposta.TabIndex = 3
        txtResposta.WordWrap = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(902, 570)
        Controls.Add(txtResposta)
        Controls.Add(btnConsultar)
        Controls.Add(txtTelefone)
        Controls.Add(Label1)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtTelefone As TextBox
    Friend WithEvents btnConsultar As Button
    Friend WithEvents txtResposta As TextBox

End Class
