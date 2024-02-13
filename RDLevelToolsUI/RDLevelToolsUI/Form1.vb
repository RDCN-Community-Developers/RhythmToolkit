Imports RhythmBase.Objects
Imports RhythmBase.Util
Imports RhythmTools.Tools
Imports System.Reflection
Public Class Form1
	Private able As Boolean = False
	Private LevelHandler As RDLevelHandler
	Private Calculator As BeatCalculator
	Private processingLevel As RDLevel
	Private viewIndex As Integer = -1
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ImportButton.Click
		OpenFileDialog1.Filter = "节奏医生游戏关卡文件|*.rdlevel"
		If OpenFileDialog1.ShowDialog <> DialogResult.OK Then
			Return
		End If
		processingLevel = RDLevel.LoadFile(OpenFileDialog1.FileName)
		Dim file = New IO.FileInfo(OpenFileDialog1.FileName)
		Text = file.Directory.Name + "\" + file.Name
		LevelHandler = New RDLevelHandler(processingLevel)
		Calculator = New BeatCalculator(processingLevel)
		able = True
		viewIndex = -1
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Not able Then
			Return
		End If
		LevelHandler.SplitRDGSG()
		MsgBox("完成")
	End Sub

	Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
		If Not able Then
			Return
		End If
		LevelHandler.SplitClassicBeat()
		MsgBox("完成")
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		If Not able Then
			Return
		End If
		LevelHandler.SplitOneShotBeat()
		MsgBox("完成")
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		If Not able Then
			Return
		End If
		LevelHandler.RemoveUnactive()
		MsgBox("完成")
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		If Not able Then
			Return
		End If
		LevelHandler.DisposeTags()
		MsgBox("完成")
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		If Not able Then
			Return
		End If
		LevelHandler.CombineToTag(TextBox2.Text, FilterCodeCSharp(TextBox1.Text), True)
		MsgBox("完成")
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		If Not able Then
			Return
		End If
		SaveFileDialog1.Filter = "节奏医生游戏关卡文件|*.rdlevel"
		If SaveFileDialog1.ShowDialog = DialogResult.OK Then
			processingLevel.SaveFile(SaveFileDialog1.FileName)
		End If
		MsgBox("完成")
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		viewIndex += 1
		If Not able OrElse viewIndex > processingLevel.Events.Count Then
			viewIndex = processingLevel.Events.Count - 1
			Return
		End If
		ShowEvent(viewIndex)
	End Sub

	Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
		viewIndex -= 1
		If Not able OrElse viewIndex < 0 Then
			viewIndex = 0
			Return
		End If
		ShowEvent(viewIndex)
	End Sub
	Private Sub ShowEvent(index As Integer)
		TableLayoutPanel1.Controls.Clear()
		Dim processingEvent = processingLevel.Events(viewIndex)
		Dim T As Type = processingEvent.GetType
		Dim nameLabel As New Label With {
			.Text = processingEvent.Type.ToString
		}
		Dim propertyLabel As New Label With {
			.Text = Calculator.BeatOnly_BarBeat(processingEvent.BeatOnly).ToString
		}
		TableLayoutPanel1.Controls.Add(nameLabel)
		TableLayoutPanel1.Controls.Add(propertyLabel)
		For Each p In T.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
			If p.CanRead AndAlso p.CanWrite Then
				Dim pLabel = New Label With {
					.Text = p.Name
				}

				Dim editorControl As Control
				Dim editorType = p.PropertyType

				If editorType = GetType(String) Then
					Dim pTextBox = New TextBox
					pTextBox.DataBindings.Add("Text", processingEvent, p.Name)
					editorControl = pTextBox
				ElseIf editorType.IsEnum Then
					Dim pComboBox = New ComboBox With {
						.DataSource = [Enum].GetValues(editorType),
						.DropDownStyle = ComboBoxStyle.DropDownList
					}
					pComboBox.DataBindings.Add("SelectedItem", processingEvent, p.Name)
					editorControl = pComboBox
				ElseIf editorType = GetType(Boolean) Then
					Dim pCheckBox = New CheckBox
					pCheckBox.DataBindings.Add("Checked", processingEvent, p.Name)
					editorControl = pCheckBox
				ElseIf editorType = GetType(Integer) Then
					Dim pNumericUpDown = New NumericUpDown With {
						.DecimalPlaces = 0,
						.Minimum = -32768,
						.Maximum = 32767
					}
					pNumericUpDown.DataBindings.Add("Value", processingEvent, p.Name)
					editorControl = pNumericUpDown
				ElseIf editorType = GetType(UInteger) Then
					Dim pNumericUpDown = New NumericUpDown With {
						.DecimalPlaces = 0,
						.Minimum = 0,
						.Maximum = 65535
					}
					pNumericUpDown.DataBindings.Add("Value", processingEvent, p.Name)
					editorControl = pNumericUpDown
				ElseIf editorType = GetType(Single) Then
					Dim pNumericUpDown = New NumericUpDown With {
						.DecimalPlaces = 4,
						.Minimum = -32768,
						.Maximum = 32767
					}
					pNumericUpDown.DataBindings.Add("Value", processingEvent, p.Name)
					editorControl = pNumericUpDown
				ElseIf editorType = GetType(INumberOrExpression) Then
					Dim pTextBox = New TextBox
					pTextBox.DataBindings.Add("Text", processingEvent, p.Name)
					editorControl = pTextBox
				Else
					Continue For
				End If
				TableLayoutPanel1.Controls.Add(pLabel)
				TableLayoutPanel1.Controls.Add(editorControl)
			End If
		Next

	End Sub
End Class
