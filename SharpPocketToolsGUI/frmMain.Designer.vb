<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        txtLog = New TextBox()
        grpOutput = New GroupBox()
        optWav = New RadioButton()
        optMp3 = New RadioButton()
        lblLog = New Label()
        grpOverwrite = New GroupBox()
        optRename = New RadioButton()
        optOverwrite = New RadioButton()
        cmbPcModel = New ComboBox()
        lblModel = New Label()
        grpFilename = New GroupBox()
        txtFileName = New TextBox()
        optFromFilename = New RadioButton()
        optUseFilename = New RadioButton()
        linkWeb = New LinkLabel()
        lblInfo = New Label()
        numStart = New NumericUpDown()
        numStep = New NumericUpDown()
        Label4 = New Label()
        grpOperation = New GroupBox()
        optRenumber = New RadioButton()
        optConvert = New RadioButton()
        ttText = New ToolTip(components)
        grpOutput.SuspendLayout()
        grpOverwrite.SuspendLayout()
        grpFilename.SuspendLayout()
        CType(numStart, ComponentModel.ISupportInitialize).BeginInit()
        CType(numStep, ComponentModel.ISupportInitialize).BeginInit()
        grpOperation.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtLog
        ' 
        txtLog.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        txtLog.Font = New Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        txtLog.ForeColor = Color.White
        txtLog.Location = New Point(10, 149)
        txtLog.Margin = New Padding(3, 2, 3, 2)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ReadOnly = True
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(609, 267)
        txtLog.TabIndex = 5
        ' 
        ' grpOutput
        ' 
        grpOutput.Controls.Add(optWav)
        grpOutput.Controls.Add(optMp3)
        grpOutput.Location = New Point(457, 85)
        grpOutput.Margin = New Padding(3, 2, 3, 2)
        grpOutput.Name = "grpOutput"
        grpOutput.Padding = New Padding(3, 2, 3, 2)
        grpOutput.Size = New Size(159, 45)
        grpOutput.TabIndex = 9
        grpOutput.TabStop = False
        grpOutput.Text = "Audio output format"
        ' 
        ' optWav
        ' 
        optWav.AutoSize = True
        optWav.Checked = True
        optWav.Location = New Point(6, 20)
        optWav.Margin = New Padding(3, 2, 3, 2)
        optWav.Name = "optWav"
        optWav.Size = New Size(50, 19)
        optWav.TabIndex = 1
        optWav.TabStop = True
        optWav.Text = "WAV"
        ttText.SetToolTip(optWav, "Convert BAS to WAV file.")
        optWav.UseVisualStyleBackColor = True
        ' 
        ' optMp3
        ' 
        optMp3.AutoSize = True
        optMp3.Location = New Point(80, 20)
        optMp3.Margin = New Padding(3, 2, 3, 2)
        optMp3.Name = "optMp3"
        optMp3.Size = New Size(49, 19)
        optMp3.TabIndex = 0
        optMp3.TabStop = True
        optMp3.Text = "MP3"
        ttText.SetToolTip(optMp3, "Convert BAS to MP3 file.")
        optMp3.UseVisualStyleBackColor = True
        ' 
        ' lblLog
        ' 
        lblLog.AutoSize = True
        lblLog.Location = New Point(10, 132)
        lblLog.Name = "lblLog"
        lblLog.Size = New Size(69, 15)
        lblLog.TabIndex = 10
        lblLog.Text = "Log output:"
        ' 
        ' grpOverwrite
        ' 
        grpOverwrite.Controls.Add(optRename)
        grpOverwrite.Controls.Add(optOverwrite)
        grpOverwrite.Location = New Point(457, 33)
        grpOverwrite.Margin = New Padding(3, 2, 3, 2)
        grpOverwrite.Name = "grpOverwrite"
        grpOverwrite.Padding = New Padding(3, 2, 3, 2)
        grpOverwrite.Size = New Size(159, 47)
        grpOverwrite.TabIndex = 12
        grpOverwrite.TabStop = False
        grpOverwrite.Text = "File overwrite policy"
        ' 
        ' optRename
        ' 
        optRename.AutoSize = True
        optRename.Checked = True
        optRename.Location = New Point(6, 19)
        optRename.Margin = New Padding(3, 2, 3, 2)
        optRename.Name = "optRename"
        optRename.Size = New Size(68, 19)
        optRename.TabIndex = 1
        optRename.TabStop = True
        optRename.Text = "Rename"
        ttText.SetToolTip(optRename, "If the destination file exists, create a new file and append an index.")
        optRename.UseVisualStyleBackColor = True
        ' 
        ' optOverwrite
        ' 
        optOverwrite.AutoSize = True
        optOverwrite.Location = New Point(80, 19)
        optOverwrite.Margin = New Padding(3, 2, 3, 2)
        optOverwrite.Name = "optOverwrite"
        optOverwrite.Size = New Size(76, 19)
        optOverwrite.TabIndex = 0
        optOverwrite.Text = "Overwrite"
        ttText.SetToolTip(optOverwrite, "If the destination file exists, overwrite it without asking.")
        optOverwrite.UseVisualStyleBackColor = True
        ' 
        ' cmbPcModel
        ' 
        cmbPcModel.FormattingEnabled = True
        cmbPcModel.Items.AddRange(New Object() {"1150", "1211", "1245", "1248", "1251", "1261", "1280", "1350", "1360", "1401", "1402", "1403", "1421", "1425", "1430", "1445", "1450", "1460", "1475", "1500", "1600", "E220", "G850"})
        cmbPcModel.Location = New Point(111, 6)
        cmbPcModel.Margin = New Padding(3, 2, 3, 2)
        cmbPcModel.Name = "cmbPcModel"
        cmbPcModel.Size = New Size(67, 23)
        cmbPcModel.TabIndex = 13
        cmbPcModel.Text = "1211"
        ttText.SetToolTip(cmbPcModel, "Select your Sharp Pocket PC model.")
        ' 
        ' lblModel
        ' 
        lblModel.AutoSize = True
        lblModel.Location = New Point(10, 9)
        lblModel.Name = "lblModel"
        lblModel.Size = New Size(95, 15)
        lblModel.TabIndex = 14
        lblModel.Text = "Sharp PC model:"
        ' 
        ' grpFilename
        ' 
        grpFilename.Controls.Add(txtFileName)
        grpFilename.Controls.Add(optFromFilename)
        grpFilename.Controls.Add(optUseFilename)
        grpFilename.Location = New Point(10, 85)
        grpFilename.Margin = New Padding(3, 2, 3, 2)
        grpFilename.Name = "grpFilename"
        grpFilename.Padding = New Padding(3, 2, 3, 2)
        grpFilename.Size = New Size(441, 45)
        grpFilename.TabIndex = 13
        grpFilename.TabStop = False
        grpFilename.Text = "Sharp audio recording file name"
        ' 
        ' txtFileName
        ' 
        txtFileName.Location = New Point(244, 16)
        txtFileName.Margin = New Padding(3, 2, 3, 2)
        txtFileName.MaxLength = 7
        txtFileName.Name = "txtFileName"
        txtFileName.Size = New Size(185, 23)
        txtFileName.TabIndex = 2
        ttText.SetToolTip(txtFileName, "Enter the name of the Sharp program.")
        ' 
        ' optFromFilename
        ' 
        optFromFilename.AutoSize = True
        optFromFilename.Checked = True
        optFromFilename.Location = New Point(7, 20)
        optFromFilename.Margin = New Padding(3, 2, 3, 2)
        optFromFilename.Name = "optFromFilename"
        optFromFilename.Size = New Size(102, 19)
        optFromFilename.TabIndex = 1
        optFromFilename.TabStop = True
        optFromFilename.Text = "From filename"
        ttText.SetToolTip(optFromFilename, "Sharp program name will be the same as the BAS file name, but truncated if it is over the supported limit.")
        optFromFilename.UseVisualStyleBackColor = True
        ' 
        ' optUseFilename
        ' 
        optUseFilename.AutoSize = True
        optUseFilename.Location = New Point(115, 20)
        optUseFilename.Margin = New Padding(3, 2, 3, 2)
        optUseFilename.Name = "optUseFilename"
        optUseFilename.Size = New Size(123, 19)
        optUseFilename.TabIndex = 0
        optUseFilename.Text = "Use custom name:"
        ttText.SetToolTip(optUseFilename, "Specify a custom file name for the Sharp program.")
        optUseFilename.UseVisualStyleBackColor = True
        ' 
        ' linkWeb
        ' 
        linkWeb.AutoSize = True
        linkWeb.Location = New Point(463, 9)
        linkWeb.Name = "linkWeb"
        linkWeb.Size = New Size(156, 15)
        linkWeb.TabIndex = 15
        linkWeb.TabStop = True
        linkWeb.Text = "Visit project page on GitHub"
        ttText.SetToolTip(linkWeb, "See source code or check for updates.")
        ' 
        ' lblInfo
        ' 
        lblInfo.AutoSize = True
        lblInfo.Location = New Point(192, 9)
        lblInfo.Name = "lblInfo"
        lblInfo.Size = New Size(254, 15)
        lblInfo.TabIndex = 16
        lblInfo.Text = "Drag && Drop BAS, WAV or MP3 files to convert."
        ' 
        ' numStart
        ' 
        numStart.Enabled = False
        numStart.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        numStart.Location = New Point(290, 17)
        numStart.Margin = New Padding(3, 2, 3, 2)
        numStart.Maximum = New Decimal(New Integer() {990, 0, 0, 0})
        numStart.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numStart.Name = "numStart"
        numStart.Size = New Size(49, 23)
        numStart.TabIndex = 19
        ttText.SetToolTip(numStart, "Specify the new start line number.")
        numStart.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' numStep
        ' 
        numStep.Enabled = False
        numStep.Location = New Point(380, 16)
        numStep.Margin = New Padding(3, 2, 3, 2)
        numStep.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numStep.Name = "numStep"
        numStep.Size = New Size(49, 23)
        numStep.TabIndex = 20
        ttText.SetToolTip(numStep, "Specify the line increment.")
        numStep.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(345, 19)
        Label4.Name = "Label4"
        Label4.Size = New Size(29, 15)
        Label4.TabIndex = 21
        Label4.Text = "step"
        ' 
        ' grpOperation
        ' 
        grpOperation.Controls.Add(optRenumber)
        grpOperation.Controls.Add(Label4)
        grpOperation.Controls.Add(optConvert)
        grpOperation.Controls.Add(numStep)
        grpOperation.Controls.Add(numStart)
        grpOperation.Location = New Point(10, 33)
        grpOperation.Margin = New Padding(3, 2, 3, 2)
        grpOperation.Name = "grpOperation"
        grpOperation.Padding = New Padding(3, 2, 3, 2)
        grpOperation.Size = New Size(441, 47)
        grpOperation.TabIndex = 10
        grpOperation.TabStop = False
        grpOperation.Text = "Operation"
        ' 
        ' optRenumber
        ' 
        optRenumber.AutoSize = True
        optRenumber.Location = New Point(115, 20)
        optRenumber.Name = "optRenumber"
        optRenumber.Size = New Size(174, 19)
        optRenumber.TabIndex = 2
        optRenumber.TabStop = True
        optRenumber.Text = "Renumber BASIC lines from:"
        ttText.SetToolTip(optRenumber, "Use this option to automatically renumber lines in a BAS file." & vbCrLf)
        optRenumber.UseVisualStyleBackColor = True
        ' 
        ' optConvert
        ' 
        optConvert.AutoSize = True
        optConvert.Checked = True
        optConvert.Location = New Point(7, 20)
        optConvert.Margin = New Padding(3, 2, 3, 2)
        optConvert.Name = "optConvert"
        optConvert.Size = New Size(91, 19)
        optConvert.TabIndex = 1
        optConvert.TabStop = True
        optConvert.Text = "Convert files"
        ttText.SetToolTip(optConvert, "Use this option to perform conversions from BAS to WAV or MP3 or viceversa.")
        optConvert.UseVisualStyleBackColor = True
        ' 
        ' frmMain
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(626, 423)
        Controls.Add(grpOperation)
        Controls.Add(lblInfo)
        Controls.Add(linkWeb)
        Controls.Add(grpFilename)
        Controls.Add(lblModel)
        Controls.Add(cmbPcModel)
        Controls.Add(grpOverwrite)
        Controls.Add(lblLog)
        Controls.Add(grpOutput)
        Controls.Add(txtLog)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 2, 3, 2)
        MaximizeBox = False
        Name = "frmMain"
        Text = "PT GUI"
        grpOutput.ResumeLayout(False)
        grpOutput.PerformLayout()
        grpOverwrite.ResumeLayout(False)
        grpOverwrite.PerformLayout()
        grpFilename.ResumeLayout(False)
        grpFilename.PerformLayout()
        CType(numStart, ComponentModel.ISupportInitialize).EndInit()
        CType(numStep, ComponentModel.ISupportInitialize).EndInit()
        grpOperation.ResumeLayout(False)
        grpOperation.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents txtLog As TextBox
    Friend WithEvents grpOutput As GroupBox
    Friend WithEvents optWav As RadioButton
    Friend WithEvents optMp3 As RadioButton
    Friend WithEvents lblLog As Label
    Friend WithEvents grpOverwrite As GroupBox
    Friend WithEvents optRename As RadioButton
    Friend WithEvents optOverwrite As RadioButton
    Friend WithEvents cmbPcModel As ComboBox
    Friend WithEvents lblModel As Label
    Friend WithEvents grpFilename As GroupBox
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents optFromFilename As RadioButton
    Friend WithEvents optUseFilename As RadioButton
    Friend WithEvents linkWeb As LinkLabel
    Friend WithEvents lblInfo As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents numStart As NumericUpDown
    Friend WithEvents numStep As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents grpOperation As GroupBox
    Friend WithEvents optRenumber As RadioButton
    Friend WithEvents optConvert As RadioButton
    Friend WithEvents ttText As ToolTip

End Class
