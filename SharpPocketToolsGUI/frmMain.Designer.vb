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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        txtLog = New TextBox()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        GroupBox2 = New GroupBox()
        optWav = New RadioButton()
        optMp3 = New RadioButton()
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        optRename = New RadioButton()
        optOverwrite = New RadioButton()
        cmbPcModel = New ComboBox()
        Label3 = New Label()
        GroupBox3 = New GroupBox()
        txtFileName = New TextBox()
        optFromFilename = New RadioButton()
        optUseFilename = New RadioButton()
        linkWeb = New LinkLabel()
        Label2 = New Label()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox3.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtLog
        ' 
        txtLog.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        txtLog.Font = New Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        txtLog.ForeColor = Color.White
        txtLog.Location = New Point(12, 136)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(807, 391)
        txtLog.TabIndex = 5
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(optWav)
        GroupBox2.Controls.Add(optMp3)
        GroupBox2.Location = New Point(12, 44)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(166, 60)
        GroupBox2.TabIndex = 9
        GroupBox2.TabStop = False
        GroupBox2.Text = "Audio output format"
        ' 
        ' optWav
        ' 
        optWav.AutoSize = True
        optWav.Checked = True
        optWav.Location = New Point(17, 26)
        optWav.Name = "optWav"
        optWav.Size = New Size(61, 24)
        optWav.TabIndex = 1
        optWav.TabStop = True
        optWav.Text = "WAV"
        optWav.UseVisualStyleBackColor = True
        ' 
        ' optMp3
        ' 
        optMp3.AutoSize = True
        optMp3.Location = New Point(84, 26)
        optMp3.Name = "optMp3"
        optMp3.Size = New Size(59, 24)
        optMp3.TabIndex = 0
        optMp3.TabStop = True
        optMp3.Text = "MP3"
        optMp3.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 113)
        Label1.Name = "Label1"
        Label1.Size = New Size(85, 20)
        Label1.TabIndex = 10
        Label1.Text = "Log output:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(optRename)
        GroupBox1.Controls.Add(optOverwrite)
        GroupBox1.Location = New Point(195, 44)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(218, 60)
        GroupBox1.TabIndex = 12
        GroupBox1.TabStop = False
        GroupBox1.Text = "File overwrite policy"
        ' 
        ' optRename
        ' 
        optRename.AutoSize = True
        optRename.Checked = True
        optRename.Location = New Point(19, 26)
        optRename.Name = "optRename"
        optRename.Size = New Size(84, 24)
        optRename.TabIndex = 1
        optRename.TabStop = True
        optRename.Text = "Rename"
        optRename.UseVisualStyleBackColor = True
        ' 
        ' optOverwrite
        ' 
        optOverwrite.AutoSize = True
        optOverwrite.Location = New Point(109, 26)
        optOverwrite.Name = "optOverwrite"
        optOverwrite.Size = New Size(94, 24)
        optOverwrite.TabIndex = 0
        optOverwrite.Text = "Overwrite"
        optOverwrite.UseVisualStyleBackColor = True
        ' 
        ' cmbPcModel
        ' 
        cmbPcModel.FormattingEnabled = True
        cmbPcModel.Items.AddRange(New Object() {"1150", "1211", "1245", "1248", "1251", "1261", "1280", "1350", "1360", "1401", "1402", "1403", "1421", "1425", "1430", "1445", "1450", "1460", "1475", "1500", "1600", "E220", "G850"})
        cmbPcModel.Location = New Point(136, 6)
        cmbPcModel.Name = "cmbPcModel"
        cmbPcModel.Size = New Size(76, 28)
        cmbPcModel.TabIndex = 13
        cmbPcModel.Text = "1211"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 9)
        Label3.Name = "Label3"
        Label3.Size = New Size(118, 20)
        Label3.TabIndex = 14
        Label3.Text = "Sharp PC model:"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(txtFileName)
        GroupBox3.Controls.Add(optFromFilename)
        GroupBox3.Controls.Add(optUseFilename)
        GroupBox3.Location = New Point(430, 44)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(389, 60)
        GroupBox3.TabIndex = 13
        GroupBox3.TabStop = False
        GroupBox3.Text = "Sharp audio recording file name"
        ' 
        ' txtFileName
        ' 
        txtFileName.Location = New Point(214, 23)
        txtFileName.MaxLength = 7
        txtFileName.Name = "txtFileName"
        txtFileName.Size = New Size(169, 27)
        txtFileName.TabIndex = 2
        ' 
        ' optFromFilename
        ' 
        optFromFilename.AutoSize = True
        optFromFilename.Checked = True
        optFromFilename.Location = New Point(19, 26)
        optFromFilename.Name = "optFromFilename"
        optFromFilename.Size = New Size(126, 24)
        optFromFilename.TabIndex = 1
        optFromFilename.TabStop = True
        optFromFilename.Text = "From filename"
        optFromFilename.UseVisualStyleBackColor = True
        ' 
        ' optUseFilename
        ' 
        optUseFilename.AutoSize = True
        optUseFilename.Location = New Point(151, 26)
        optUseFilename.Name = "optUseFilename"
        optUseFilename.Size = New Size(57, 24)
        optUseFilename.TabIndex = 0
        optUseFilename.Text = "Use:"
        optUseFilename.UseVisualStyleBackColor = True
        ' 
        ' linkWeb
        ' 
        linkWeb.AutoSize = True
        linkWeb.Location = New Point(621, 9)
        linkWeb.Name = "linkWeb"
        linkWeb.Size = New Size(198, 20)
        linkWeb.TabIndex = 15
        linkWeb.TabStop = True
        linkWeb.Text = "Visit project page on GitHub"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(240, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(313, 20)
        Label2.TabIndex = 16
        Label2.Text = "Drag && Drop bas, wav or mp3 files to convert."
        ' 
        ' frmMain
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(831, 539)
        Controls.Add(Label2)
        Controls.Add(linkWeb)
        Controls.Add(GroupBox3)
        Controls.Add(Label3)
        Controls.Add(cmbPcModel)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Controls.Add(GroupBox2)
        Controls.Add(txtLog)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "frmMain"
        Text = "PT GUI"
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents txtLog As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents optWav As RadioButton
    Friend WithEvents optMp3 As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents optRename As RadioButton
    Friend WithEvents optOverwrite As RadioButton
    Friend WithEvents cmbPcModel As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents optFromFilename As RadioButton
    Friend WithEvents optUseFilename As RadioButton
    Friend WithEvents linkWeb As LinkLabel
    Friend WithEvents Label2 As Label

End Class
