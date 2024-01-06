Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmMain

    Dim filesToPrcess As List(Of String)

    ''' <summary>
    ''' Execute a process, wait for it to finish and redirect output if needed.
    ''' </summary>
    ''' <param name="processName"></param>
    ''' <param name="processArgs"></param>
    ''' <param name="outStr"></param>
    ''' <param name="redirOut"></param>
    ''' <param name="redirErr"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Processes the files one by one and calls the required function based by the file extension.
    ''' </summary>
    Sub ProcessFiles()
        Dim ret As Integer = 0

        For Each file In filesToPrcess
            txtLog.Text &= "Processing " & file & vbCrLf & vbCrLf
            If file.EndsWith(".bas", StringComparison.OrdinalIgnoreCase) Then
                Dim sharpFileName As String
                If optUseFilename.Checked = True Then
                    'Use the specified file name
                    sharpFileName = txtFileName.Text.ToUpper()
                Else
                    If Path.GetFileNameWithoutExtension(file).Length > txtFileName.MaxLength Then
                        'Truncate the file name if it is longer than 7 characters (or 16 for Sharp 1500)
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

        'Always scroll the Log to the end.
        txtLog.SelectionStart = txtLog.Text.Length
        txtLog.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' Converts a BAS file to audio file
    ''' </summary>
    ''' <param name="fileName">The file name and path to the BAS file</param>
    ''' <param name="sharpFileName">The final name of the file on tape</param>
    ''' <returns></returns>
    Function ProcessBAS(fileName As String, sharpFileName As String)
        Dim out As String = ""
        Dim ret As Integer
        Dim imgFile As String
        Dim wavFile As String
        Dim mp3File As String

        My.Application.DoEvents()

        'Convert to binary IMG file first.
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

        'Convert binary IMG to WAV file.
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

        'Convert to MP3 if requested.
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
            '-c number of audio tracks (1=mono), -C bitrate for MP3 (=128kbps), rate = sample rate
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

    ''' <summary>
    ''' Converts an audio file to BAS file
    ''' </summary>
    ''' <param name="fileName">The file name and path to the audio file</param>
    ''' <param name="isMp3">If it is MP3 instead of WAV</param>
    ''' <returns></returns>
    Function ProcessWAV(fileName As String, isMp3 As Boolean)
        Dim out As String = ""
        Dim ret As Integer
        Dim wavFile As String = fileName
        Dim basFile As String

        'Convert MP3 to WAV first if required.
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
            '-c number of audio tracks (1=mono), -b bit depth, rate = sample rate
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

        'Convert WAV to BAS file
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

        'Add only the supported files to the process queue
        For Each file In files
            If optRenumber.Checked Then
                If file.EndsWith(".bas", StringComparison.OrdinalIgnoreCase) Then
                    txtLog.Text &= "Renumbering " & file & vbCrLf
                    ChangeLineNumbers(file, numStart.Value, numStep.Value, file)
                Else
                    txtLog.Text &= "Unsupported " & file & vbCrLf
                End If
            Else
                If file.EndsWith(".bas", StringComparison.OrdinalIgnoreCase) OrElse
                file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) OrElse
                file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) Then
                    filesToPrcess.Add(file)
                    txtLog.Text &= "Will process " & file & vbCrLf
                Else
                    txtLog.Text &= "Unsupported " & file & vbCrLf
                End If
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

        'Load settings
        cmbPcModel.Text = My.Settings.pcModel
        txtFileName.Text = My.Settings.sharpFileName
        numStart.Value = My.Settings.startLine
        numStep.Value = My.Settings.stepLine

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
        If My.Settings.operation = 1 Then
            optRenumber.Checked = True
        Else
            optConvert.Checked = True
        End If
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
            'The only Sharp PC model that supports 16 characters file names.
            txtFileName.MaxLength = 16
        Else
            'All others support a maximum of 7 characters.
            txtFileName.MaxLength = 7
        End If
    End Sub

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        'Save settings
        My.Settings.pcModel = cmbPcModel.Text
        My.Settings.sharpFileName = txtFileName.Text
        My.Settings.startLine = numStart.Value
        My.Settings.stepLine = numStep.Value

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
        If optRenumber.Checked Then
            My.Settings.operation = 1
        Else
            My.Settings.operation = 0
        End If

    End Sub

    Private Sub linkWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkWeb.LinkClicked
        Process.Start("explorer.exe", "https://github.com/SilverGreen93/SharpPocketToolsGUI")
    End Sub

    ''' <summary>
    ''' Renumber BASIC lines in source files
    ''' </summary>
    ''' <param name="fileName">Input file name</param>
    ''' <param name="start">Start line number</param>
    ''' <param name="increment">Increment step</param>
    ''' <param name="finalFile">Output file name</param>
    Sub ChangeLineNumbers(fileName As String, start As Integer, increment As Integer, finalFile As String)
        Dim stream As StreamReader
        Dim newLines As New List(Of String)
        Dim changedLines As String = ""
        Dim matchMap As New Hashtable()
        Dim match As Match
        Dim lineIndex As Integer = start

        'Read the file line by line, parse each line, create a map between the old line number and the new one.
        'Write each line into a list of lines.
        Try
            stream = New StreamReader(File.Open(fileName, FileMode.Open, FileAccess.Read))
            While Not stream.EndOfStream
                Dim line As String = stream.ReadLine()
                If lineIndex > 999 Then
                    txtLog.Text &= vbCrLf & "Error: Line number is greater than 999! Please adjust and retry operation!" & vbCrLf & vbCrLf
                    Return
                End If
                match = Regex.Match(line, "^ *[0-9]+") 'match the number at the start of the line
                If match.Success Then
                    matchMap.Add(match.Value.Trim(), lineIndex)
                    Dim newLine = Regex.Replace(line, "^ *[0-9]+", lineIndex.ToString().PadLeft(5))
                    newLines.Add(newLine)
                    lineIndex += increment
                Else
                    'If line is a comment or empty line, copy as it is.
                    newLines.Add(line)
                End If
            End While
            stream.Close()
        Catch ex As Exception
            txtLog.Text &= "Error opening file: " & fileName & vbCrLf & ex.Message & vbCrLf
            Return
        End Try

        'Parse lines again to find GOTO or GOSUB statements to replace the line numbers
        For Each line In newLines
            Dim goSubEncountered As Boolean = False

            match = Regex.Match(line, "^ *[']") 'skip lines starting with comments
            If match.Success Then
                changedLines &= line & vbCrLf
                Continue For
            End If
            'The line is not a comment, go ahead

            'Find lines that contain GOTO, GOSUB or THEN
            match = Regex.Match(line, "(GOTO|GOSUB|THEN)", RegexOptions.IgnoreCase)
            If Not match.Success Then
                'If the line does not contain GOTO, GOSUB or THEN, skip it.
                changedLines &= line & vbCrLf
                Continue For
            End If
            'The line contains GOTO, GOSUB or THEN, go ahead

            'On a line there can be multiple GOSUB statements. Replace each of them.
            Dim matches As MatchCollection = Regex.Matches(line, "GOSUB *[0-9]+ *(:|$)", RegexOptions.IgnoreCase) 'find all occurencies
            If matches.Count > 0 Then
                Dim editLine As String = "" 'construct a temporary copy of the line with new GOSUB lines replaced
                Dim curIndex As Integer = 0 'keep the index of current line position
                For Each m In matches
                    Dim line_no As Match = Regex.Match(m.Value, "[0-9]+") 'get the original line number reference
                    editLine &= line.Substring(curIndex, m.Index + line_no.Index - curIndex) 'copy the line until the match including GOSUB
                    curIndex = m.Index + line_no.Index + line_no.Length 'position the index after the old line number
                    If Not matchMap.Contains(line_no.Value.Trim()) Then
                        Dim lineMatch = Regex.Match(line, "^ *[0-9]+")
                        txtLog.Text &= vbCrLf & "Warning: Invalid line reference at line " & lineMatch.Value.Trim() & ": Line " & line_no.Value.Trim() & " does not exist!" & vbCrLf & vbCrLf
                    End If
                    editLine &= matchMap(line_no.Value.Trim()) 'add the new line number
                Next
                editLine &= line.Substring(curIndex, line.Length - curIndex) 'copy the rest of the line untill the end
                line = editLine
                'Do not Continue For here, as there might be other GOTO or THEN statements in the same line
                goSubEncountered = True
            End If

            match = Regex.Match(line, "(GOTO|THEN) *[0-9]+$", RegexOptions.IgnoreCase)
            If match.Success Then
                'The line contains GOTO, GOSUB or THEN followed by constant line number at the end of line.
                Dim line_no As Match = Regex.Match(match.Value, "[0-9]+$") 'get the original line number reference
                If Not matchMap.Contains(line_no.Value.Trim()) Then
                    Dim lineMatch = Regex.Match(line, "^ *[0-9]+")
                    txtLog.Text &= vbCrLf & "Warning: Invalid line reference at line " & lineMatch.Value.Trim() & ": Line " & line_no.Value.Trim() & " does not exist!" & vbCrLf
                End If
                line = Regex.Replace(line, "GOTO *[0-9]+$", "GOTO " & matchMap(line_no.Value.Trim()), RegexOptions.IgnoreCase)
                line = Regex.Replace(line, "THEN *[0-9]+$", "THEN " & matchMap(line_no.Value.Trim()), RegexOptions.IgnoreCase)
                changedLines &= line & vbCrLf
                Continue For
            End If
            'The line contains GOTO, GOSUB or THEN, but with variable, label, or plain string insted of constant line number

            match = Regex.Match(line, "(GOTO|GOSUB|THEN) *"".*""", RegexOptions.IgnoreCase)
            If match.Success Then
                'The line contains GOTO, GOSUB or THEN with a label, it is safe to copy unchanged.
                changedLines &= line & vbCrLf
                Continue For
            End If

            match = Regex.Match(line, "(GOTO|GOSUB|THEN) *.*\$", RegexOptions.IgnoreCase)
            If match.Success Then
                'The line contains GOTO, GOSUB or THEN with a string variable ($), it is safe to copy unchanged.
                changedLines &= line & vbCrLf
                Continue For
            End If

            If Not goSubEncountered Then
                'If there was a GOSUB statement and it was not changed, it means that it was GOSUB with integer variable
                match = Regex.Match(line, "^ *[0-9]+")
                txtLog.Text &= vbCrLf & "Warning: At line " & match.Value.Trim() & ": GOTO/GOSUB/THEN with non-constant line number found! Please adjust logic manually!" & vbCrLf
            End If
            changedLines &= line & vbCrLf
        Next

        'Replace the original file if the Overwrite option is checked.
        If optOverwrite.Checked Then
            My.Computer.FileSystem.WriteAllText(finalFile, changedLines, False)
        Else
            If My.Computer.FileSystem.FileExists(finalFile) Then
                Dim index As Integer = 1
                Do While My.Computer.FileSystem.FileExists(String.Concat(finalFile.AsSpan(0, finalFile.Length - 4), " (" & index & ").BAS"))
                    index += 1
                Loop
                My.Computer.FileSystem.WriteAllText(String.Concat(finalFile.AsSpan(0, finalFile.Length - 4), " (" & index & ").BAS"), changedLines, False)
            End If
        End If
    End Sub

    Private Sub optRenumber_CheckedChanged(sender As Object, e As EventArgs) Handles optRenumber.CheckedChanged
        If optRenumber.Checked Then
            numStart.Enabled = True
            numStep.Enabled = True
            grpOutput.Enabled = False
            grpFilename.Enabled = False
        Else
            numStart.Enabled = False
            numStep.Enabled = False
            grpOutput.Enabled = True
            grpFilename.Enabled = True
        End If
    End Sub
End Class
