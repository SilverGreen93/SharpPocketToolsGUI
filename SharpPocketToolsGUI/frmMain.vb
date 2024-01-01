Imports System.ComponentModel
Imports System.Formats.Tar
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class frmMain

    Dim filesToPrcess As List(Of String)

    Function ExecuteProcess(processName As String, processArgs As String, ByRef outStr As String, redirOut As Boolean, redirErr As Boolean) As Integer
        Try
            Dim myProcess As New Process()
            myProcess.StartInfo.FileName = processName
            myProcess.StartInfo.Arguments = processArgs

            myProcess.StartInfo.UseShellExecute = False
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            myProcess.StartInfo.CreateNoWindow = True

            myProcess.StartInfo.RedirectStandardOutput = redirOut
            myProcess.StartInfo.RedirectStandardError = redirErr

            myProcess.Start()
            outStr = "Launching external tool: " & processName & " " & processArgs & vbCrLf & vbCrLf

            If redirOut Then
                outStr &= myProcess.StandardOutput.ReadToEnd
            End If
            If redirErr Then
                outStr &= myProcess.StandardError.ReadToEnd
            End If

            myProcess.WaitForExit()

            Return myProcess.ExitCode

        Catch ex As Exception
            outStr = "Fatal error when launching external tool: " & processName & " " & processArgs & vbCrLf & ex.ToString
            Return -1
        End Try

    End Function

    Sub ProcessFiles()
        Dim ret As Integer = 0

        For Each file In filesToPrcess
            txtLog.Text &= "Processing " & file & vbCrLf & vbCrLf
            If file.EndsWith(".bas", StringComparison.OrdinalIgnoreCase) Then
                Dim sharpFileName As String
                If optUseFilename.Checked = True Then
                    sharpFileName = txtFileName.Text.ToUpper()
                Else
                    If Path.GetFileNameWithoutExtension(file).Length > txtFileName.MaxLength Then
                        sharpFileName = Path.GetFileNameWithoutExtension(file).Substring(0, txtFileName.MaxLength).ToUpper()
                        txtLog.Text &= "Warning! Filename is longer than " & txtFileName.MaxLength & " characters! It will be truncated to " & sharpFileName & vbCrLf & vbCrLf
                    Else
                        sharpFileName = Path.GetFileNameWithoutExtension(file).ToUpper()
                    End If
                End If
                ret = ProcessBAS(file, sharpFileName)
            ElseIf file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) Then
                ret = ProcessWAV(file, False)
            ElseIf file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) Then
                ret = ProcessWAV(file, True)
            End If
            If ret = -1 Then
                Exit For
            End If
            txtLog.Text &= "---------------------------------------------------------------------" & vbCrLf
        Next

        If ret = -1 Then
            txtLog.Text &= vbCrLf & "Please check output above!" & vbCrLf
        End If
        txtLog.Text &= "Done." & vbCrLf

        txtLog.SelectionStart = txtLog.Text.Length
        txtLog.ScrollToCaret()
    End Sub

    Function ProcessBAS(fileName As String, sharpFileName As String)
        Dim out As String = ""
        Dim ret As Integer
        Dim imgFile As String
        Dim wavFile As String
        Dim mp3File As String

        My.Application.DoEvents()

        imgFile = String.Concat(fileName.AsSpan(0, fileName.Length - 3), "IMG")

        ret = ExecuteProcess("ptools\bas2img.exe", "--pc=" & cmbPcModel.Text & " """ & fileName & """ """ & imgFile & """", out, True, False)
        txtLog.Text &= out & vbCrLf
        If ret <> 0 Then
            txtLog.Text &= vbCrLf & "bas2img returned error " & ret & "!" & vbCrLf
            Return -1
        End If

        If Not My.Computer.FileSystem.FileExists(imgFile) Then
            txtLog.Text &= vbCrLf & "bas2img failed to create image file: " & imgFile & vbCrLf
            Return -1
        End If

        My.Application.DoEvents()

        wavFile = Path.GetDirectoryName(fileName) & "\" & sharpFileName & ".WAV"
        If optRename.Checked Then
            If My.Computer.FileSystem.FileExists(wavFile) Then
                Dim index As Integer = 1
                Do While My.Computer.FileSystem.FileExists(Path.GetDirectoryName(fileName) & "\" & sharpFileName & " (" & index & ").WAV")
                    index += 1
                Loop
                wavFile = Path.GetDirectoryName(fileName) & "\" & sharpFileName & " (" & index & ").WAV"
            End If
        End If

        ret = ExecuteProcess("ptools\bin2wav.exe", "--type=img --pc=" & cmbPcModel.Text & " --name=" & sharpFileName & " """ & imgFile & """ """ & wavFile & """", out, True, False)
        txtLog.Text &= out & vbCrLf
        If ret <> 0 Then
            txtLog.Text &= vbCrLf & "bin2wav returned error " & ret & "!" & vbCrLf
            Return -1
        End If

        If Not My.Computer.FileSystem.FileExists(wavFile) Then
            txtLog.Text &= vbCrLf & "bin2wav failed to create wave file: " & wavFile & vbCrLf
            Return -1
        End If

        txtLog.Text &= "Deleting temporary img file: " & imgFile & vbCrLf & vbCrLf
        My.Computer.FileSystem.DeleteFile(imgFile)

        If optMp3.Checked Then
            My.Application.DoEvents()
            mp3File = String.Concat(wavFile.AsSpan(0, wavFile.Length - 3), "MP3")
            If optRename.Checked Then
                If My.Computer.FileSystem.FileExists(mp3File) Then
                    Dim index As Integer = 1
                    Do While My.Computer.FileSystem.FileExists(String.Concat(wavFile.AsSpan(0, wavFile.Length - 4), " (" & index & ").MP3"))
                        index += 1
                    Loop
                    mp3File = String.Concat(wavFile.AsSpan(0, wavFile.Length - 4), " (" & index & ").MP3")
                End If
            End If
            ret = ExecuteProcess("sox\sox.exe", """" & wavFile & """ -c 1 -C 128 """ & mp3File & """ rate 16000", out, False, True)
            txtLog.Text &= out & vbCrLf
            If ret <> 0 Then
                txtLog.Text &= vbCrLf & "sox encoder returned error " & ret & "!" & vbCrLf
                Return -1
            End If
            If Not My.Computer.FileSystem.FileExists(mp3File) Then
                txtLog.Text &= vbCrLf & "sox encoder failed to create mp3 file: " & mp3File & vbCrLf
                Return -1
            End If
            txtLog.Text &= "Deleting temporary wav file: " & wavFile & vbCrLf & vbCrLf
            My.Computer.FileSystem.DeleteFile(wavFile)
        End If

        Return 0
    End Function

    Function ProcessWAV(fileName As String, isMp3 As Boolean)
        Dim out As String = ""
        Dim ret As Integer
        Dim wavFile As String = fileName
        Dim basFile As String

        If isMp3 Then
            My.Application.DoEvents()
            wavFile = String.Concat(fileName.AsSpan(0, fileName.Length - 3), "WAV")
            If optRename.Checked Then
                If My.Computer.FileSystem.FileExists(wavFile) Then
                    Dim index As Integer = 1
                    Do While My.Computer.FileSystem.FileExists(String.Concat(fileName.AsSpan(0, fileName.Length - 4), " (" & index & ").WAV"))
                        index += 1
                    Loop
                    wavFile = String.Concat(fileName.AsSpan(0, fileName.Length - 4), " (" & index & ").WAV")
                End If
            End If
            ret = ExecuteProcess("sox\sox.exe", """" & fileName & """ -c 1 -b 8 """ & wavFile & """ rate 16000", out, False, True)
            txtLog.Text &= out & vbCrLf
            If ret <> 0 Then
                txtLog.Text &= vbCrLf & "sox encoder returned error " & ret & "!" & vbCrLf
                Return -1
            End If
            If Not My.Computer.FileSystem.FileExists(wavFile) Then
                txtLog.Text &= vbCrLf & "sox encoder failed to create wav file: " & wavFile & vbCrLf
                Return -1
            End If
        End If

        basFile = String.Concat(wavFile.AsSpan(0, wavFile.Length - 3), "BAS")
        If optRename.Checked Then
            If My.Computer.FileSystem.FileExists(basFile) Then
                Dim index As Integer = 1
                Do While My.Computer.FileSystem.FileExists(String.Concat(wavFile.AsSpan(0, wavFile.Length - 4), " (" & index & ").BAS"))
                    index += 1
                Loop
                basFile = String.Concat(wavFile.AsSpan(0, wavFile.Length - 4), " (" & index & ").BAS")
            End If
        End If

        ret = ExecuteProcess("ptools\wav2bin.exe", "--pc=" & cmbPcModel.Text & " """ & wavFile & """ """ & basFile & """", out, True, False)
        txtLog.Text &= out & vbCrLf
        If ret <> 0 Then
            txtLog.Text &= vbCrLf & "wav2bin returned error " & ret & "!" & vbCrLf
            Return -1
        End If

        If Not My.Computer.FileSystem.FileExists(basFile) Then
            txtLog.Text &= vbCrLf & "wav2bin failed to create bas file: " & basFile & vbCrLf
            Return -1
        End If

        If isMp3 Then
            txtLog.Text &= "Deleting temporary wav file: " & wavFile & vbCrLf & vbCrLf
            My.Computer.FileSystem.DeleteFile(wavFile)
        End If

        Return 0
    End Function


    Private Sub frmMain_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim files As String() = e.Data.GetData(DataFormats.FileDrop)
        filesToPrcess.Clear()
        txtLog.Clear()

        For Each file In files
            If file.EndsWith(".bas", StringComparison.OrdinalIgnoreCase) OrElse
                file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) OrElse
                file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) Then
                filesToPrcess.Add(file)
                txtLog.Text &= "Will process " & file & vbCrLf
            Else
                txtLog.Text &= "Unsupported " & file & vbCrLf
            End If
        Next

        txtLog.Text &= "====================================================================" & vbCrLf & vbCrLf

        ProcessFiles()
    End Sub

    Private Sub frmMain_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = My.Application.Info.Title & " v" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor
        filesToPrcess = New List(Of String)

        cmbPcModel.Text = My.Settings.pcModel

        If My.Settings.outputFormat = 1 Then
            optMp3.Checked = True
        Else
            optWav.Checked = True
        End If
        If My.Settings.filePolicy = 1 Then
            optOverwrite.Checked = True
        Else
            optRename.Checked = True
        End If
        If My.Settings.fileNamePolicy = 1 Then
            optUseFilename.Checked = True
        Else
            optFromFilename.Checked = True
        End If

        txtFileName.Text = My.Settings.sharpFileName
    End Sub

    Private Sub optUseFilename_CheckedChanged(sender As Object, e As EventArgs) Handles optUseFilename.CheckedChanged
        If optUseFilename.Checked = True Then
            txtFileName.Enabled = True
        End If
    End Sub

    Private Sub optFromFilename_CheckedChanged(sender As Object, e As EventArgs) Handles optFromFilename.CheckedChanged
        If optFromFilename.Checked = True Then
            txtFileName.Enabled = False
        End If
    End Sub

    Private Sub cmbPcModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPcModel.SelectedIndexChanged
        If cmbPcModel.Text = "1500" Then
            txtFileName.MaxLength = 16
        Else
            txtFileName.MaxLength = 7
        End If
    End Sub

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        My.Settings.pcModel = cmbPcModel.Text

        If optMp3.Checked Then
            My.Settings.outputFormat = 1
        Else
            My.Settings.outputFormat = 0
        End If
        If optOverwrite.Checked Then
            My.Settings.filePolicy = 1
        Else
            My.Settings.filePolicy = 0
        End If
        If optUseFilename.Checked Then
            My.Settings.fileNamePolicy = 1
        Else
            My.Settings.fileNamePolicy = 0
        End If

        My.Settings.sharpFileName = txtFileName.Text
    End Sub

    Private Sub linkWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkWeb.LinkClicked
        Process.Start("https://github.com/SilverGreen93")
    End Sub
End Class
