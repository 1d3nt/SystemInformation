<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        Dim Brandlabel As Label
        Dim ProductNameLabel As Label
        Dim Label1 As Label
        Dim SnidLabel As Label
        Dim LineBreakerLabel As Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        BrandTextBox = New TextBox()
        ProductNameTextBox = New TextBox()
        SerialNumberTextBox = New TextBox()
        TextBox1 = New TextBox()
        CopyAllButton = New Button()
        Brandlabel = New Label()
        ProductNameLabel = New Label()
        Label1 = New Label()
        SnidLabel = New Label()
        LineBreakerLabel = New Label()
        SuspendLayout()
        ' 
        ' Brandlabel
        ' 
        Brandlabel.AutoSize = True
        Brandlabel.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Brandlabel.Location = New Point(155, 63)
        Brandlabel.Name = "Brandlabel"
        Brandlabel.Size = New Size(76, 28)
        Brandlabel.TabIndex = 0
        Brandlabel.Text = "Brand :"
        ' 
        ' ProductNameLabel
        ' 
        ProductNameLabel.AutoSize = True
        ProductNameLabel.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ProductNameLabel.Location = New Point(77, 104)
        ProductNameLabel.Name = "ProductNameLabel"
        ProductNameLabel.Size = New Size(154, 28)
        ProductNameLabel.TabIndex = 2
        ProductNameLabel.Text = "Product Name :"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(78, 139)
        Label1.Name = "Label1"
        Label1.Size = New Size(153, 28)
        Label1.TabIndex = 4
        Label1.Text = "Serial Number :"
        ' 
        ' SnidLabel
        ' 
        SnidLabel.AutoSize = True
        SnidLabel.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        SnidLabel.Location = New Point(162, 177)
        SnidLabel.Name = "SnidLabel"
        SnidLabel.Size = New Size(69, 28)
        SnidLabel.TabIndex = 6
        SnidLabel.Text = "SNID :"
        ' 
        ' LineBreakerLabel
        ' 
        LineBreakerLabel.BorderStyle = BorderStyle.Fixed3D
        LineBreakerLabel.Location = New Point(12, 226)
        LineBreakerLabel.Name = "LineBreakerLabel"
        LineBreakerLabel.Size = New Size(728, 2)
        LineBreakerLabel.TabIndex = 8
        ' 
        ' BrandTextBox
        ' 
        BrandTextBox.Location = New Point(237, 67)
        BrandTextBox.Name = "BrandTextBox"
        BrandTextBox.ReadOnly = True
        BrandTextBox.Size = New Size(482, 27)
        BrandTextBox.TabIndex = 1
        ' 
        ' ProductNameTextBox
        ' 
        ProductNameTextBox.Location = New Point(237, 105)
        ProductNameTextBox.Name = "ProductNameTextBox"
        ProductNameTextBox.ReadOnly = True
        ProductNameTextBox.Size = New Size(482, 27)
        ProductNameTextBox.TabIndex = 3
        ' 
        ' SerialNumberTextBox
        ' 
        SerialNumberTextBox.Location = New Point(237, 143)
        SerialNumberTextBox.Name = "SerialNumberTextBox"
        SerialNumberTextBox.ReadOnly = True
        SerialNumberTextBox.Size = New Size(482, 27)
        SerialNumberTextBox.TabIndex = 5
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(237, 181)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(482, 27)
        TextBox1.TabIndex = 7
        ' 
        ' CopyAllButton
        ' 
        CopyAllButton.Image = CType(resources.GetObject("CopyAllButton.Image"), Image)
        CopyAllButton.Location = New Point(15, 245)
        CopyAllButton.Name = "CopyAllButton"
        CopyAllButton.Size = New Size(722, 29)
        CopyAllButton.TabIndex = 9
        CopyAllButton.Text = "Copy All Info"
        CopyAllButton.TextImageRelation = TextImageRelation.ImageBeforeText
        CopyAllButton.UseCompatibleTextRendering = True
        CopyAllButton.UseVisualStyleBackColor = True
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(754, 337)
        Controls.Add(CopyAllButton)
        Controls.Add(LineBreakerLabel)
        Controls.Add(TextBox1)
        Controls.Add(SnidLabel)
        Controls.Add(SerialNumberTextBox)
        Controls.Add(Label1)
        Controls.Add(ProductNameTextBox)
        Controls.Add(ProductNameLabel)
        Controls.Add(BrandTextBox)
        Controls.Add(Brandlabel)
        MaximizeBox = False
        Name = "MainForm"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "System Information Utility"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Private WithEvents Brandlabel As Label
    Private WithEvents BrandTextBox As TextBox
    Private WithEvents ProductNameTextBox As TextBox
    Private WithEvents SerialNumberTextBox As TextBox
    Private WithEvents TextBox1 As TextBox
    Private WithEvents LineBreakerLabel As Label
    Private WithEvents CopyAllButton As Button

End Class
