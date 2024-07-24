Imports RhythmBase.Components
Imports RhythmBase
Imports RhythmBase.Events
Imports RhythmBase.Settings
Imports RhythmBase.Utils
Imports RhythmBase.Tools
Imports System.Reflection
Imports RhythmBase.Components
Public Class Form1
	Private able As Boolean = False
	Private LevelHandler As LevelHandler
	Private Calculator As BeatCalculator
	Private processingLevel As RDLevel
	Private viewIndex As Integer = -1
	Private Sub CreateLevelButton_Click(sender As Object, e As EventArgs) Handles CreateLevelButton.Click
		processingLevel = New RDLevel
		LevelHandler = New LevelHandler(processingLevel)
		Calculator = processingLevel.Calculator
		able = True
		viewIndex = -1
	End Sub
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ImportButton.Click
		OpenFileDialog1.Filter = "节奏医生游戏关卡文件|*.rdlevel|节奏医生游戏关卡压缩包文件|*.rdzip"
		If OpenFileDialog1.ShowDialog <> DialogResult.OK Then
			Return
		End If
		processingLevel = RDLevel.LoadFile(OpenFileDialog1.FileName, New LevelReadOrWriteSettings)
		Dim file = New IO.FileInfo(OpenFileDialog1.FileName)
		Text = file.Directory.Name + "\" + file.Name
		LevelHandler = New LevelHandler(processingLevel)
		Calculator = processingLevel.Calculator
		able = True
		viewIndex = -1
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles SaveFileButton.Click
		If Not able Then
			Return
		End If
		SaveFileDialog1.Filter = "节奏医生游戏关卡文件|*.rdlevel"
		If SaveFileDialog1.ShowDialog = DialogResult.OK Then
			processingLevel.SaveFile(SaveFileDialog1.FileName)
		End If
		MsgBox("完成")
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles PageUpButton.Click
		viewIndex += 1
		If Not able OrElse viewIndex > processingLevel.Count Then
			viewIndex = processingLevel.Count - 1
			Return
		End If
		ShowEvent(viewIndex)
	End Sub

	Private Sub Button9_Click(sender As Object, e As EventArgs) Handles PageDownButton.Click
		viewIndex -= 1
		If Not able OrElse viewIndex < 0 Then
			viewIndex = 0
			Return
		End If
		ShowEvent(viewIndex)
	End Sub
	Public Shared Manager As New RhythmBase.Utils.TranaslationManager(New IO.FileInfo(Application.StartupPath + "\zh-cn.json"))
	Private Sub ShowEvent(index As Integer)
		TableLayoutPanel1.Controls.Clear()
		If Not (0 <= viewIndex And viewIndex < processingLevel.Count) Then
			Exit Sub
		End If
		Dim processingEvent = processingLevel(viewIndex)
		Dim T As Type = processingEvent.GetType

		Dim enump = GetType(EventType).GetMember(processingEvent.Type.ToString).FirstOrDefault

		Dim nameLabel As New Label With {
			.Text = Manager.GetValue(enump),
			.AccessibleName = Manager.GetValue(enump),
			.AccessibleDescription = "事件名称"
		}
		Dim propertyLabel As New Label With {
			.Text = Calculator.BeatOnlyToBarBeat(processingEvent.Beat.BeatOnly).ToString,
			.AccessibleName = "节拍",
			.AccessibleDescription = "事件的节拍位置"
		}
		TableLayoutPanel1.Controls.Add(nameLabel)
		TableLayoutPanel1.Controls.Add(propertyLabel)
		For Each p In T.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
			If p.CanRead AndAlso p.CanWrite Then

				Dim pLabel As New Label With {
					.Text = Manager.GetValue(p)
					}

				Dim editorControl As Control
				Dim editorType = p.PropertyType

				If editorType = GetType(String) Then
					Dim pTextBox = New TextBox
					pTextBox.DataBindings.Add("Text", processingEvent, p.Name)
					editorControl = pTextBox
				ElseIf editorType.IsEnum Then
					Dim pairType = GetType(EnumNamePair(Of )).MakeGenericType(editorType)
					Dim pComboBox = New ComboBox With {
						.DataSource = pairType.GetMethod("GetEnumNamePair").Invoke(Nothing, Nothing),
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
					'ElseIf p.Name = NameOf(BaseBeat.Beat) Then
					'	Dim pNumericUpDown = New NumericUpDown With {
					'		.DecimalPlaces = 2,
					'		.Minimum = 1,
					'		.Maximum = 32767
					'	}
					'	pNumericUpDown.DataBindings.Add("Value", processingEvent, p.Name)
					'	editorControl = pNumericUpDown
				ElseIf editorType = GetType(Single) Then
					Dim pNumericUpDown = New NumericUpDown With {
						.DecimalPlaces = 2,
						.Minimum = -32768,
						.Maximum = 32767
					}
					pNumericUpDown.DataBindings.Add("Value", processingEvent, p.Name)
					editorControl = pNumericUpDown
				ElseIf editorType = GetType(Expression) Then
					Dim pTextBox = New TextBox
					pTextBox.DataBindings.Add("Text", processingEvent, p.Name)
					editorControl = pTextBox
					'ElseIf editorType = GetType(NumberOrExpressionPair) Then
					'	Dim pPanel = New TableLayoutPanel
					'	Dim pTextBoxX = New TextBox
					'	'pTextBoxX.DataBindings.Add("Text", p.GetValue(processingEvent), NameOf(NumberOrExpressionPair.X))
					'	Dim pTextBoxY = New TextBox
					'	'pTextBoxY.DataBindings.Add("Text", p.GetValue(processingEvent), NameOf(NumberOrExpressionPair.Y))
					'	pPanel.Controls.Add(pTextBoxX)
					'	pPanel.Controls.Add(pTextBoxY)
					'	editorControl = pPanel
				Else
					Dim pValue = New Label
					pValue.Text = "搁这画个饼先"
					editorControl = pValue
				End If
				editorControl.AccessibleRole = AccessibleRole.None
				editorControl.AccessibleName = Manager.GetValue(p)
				TableLayoutPanel1.Controls.Add(pLabel)
				TableLayoutPanel1.Controls.Add(editorControl)
			End If
		Next

	End Sub

	'Private Class EnumNamePair
	'	Public ReadOnly Name As String
	'	Public ReadOnly Value As Structure
	'	Public ReadOnly T As Type
	'	Public Sub New(T As Type, value As Object)
	'		Me.Value = value
	'		Me.T = T
	'		Me.Name = Manager.GetValue(T.GetMember(value.ToString).FirstOrDefault)
	'	End Sub
	'	Public Shared Function GetEnumNamePair(T As Type) As List(Of EnumNamePair)
	'		Dim L As New List(Of EnumNamePair)
	'		For Each item In [Enum].GetValues(T)
	'			L.Add(New EnumNamePair(T, item))
	'		Next
	'		Return L
	'	End Function
	'	Public Overrides Function ToString() As String
	'		Return Name
	'	End Function
	'	Public Shared Widening Operator CType(value As EnumNamePair) As Object
	'		Return value.Value
	'	End Operator
	'End Class
	Private Class EnumNamePair(Of T)
		Public ReadOnly Name As String
		Public ReadOnly Value As T
		Public Sub New(value As T)
			Me.Value = value
			Me.Name = Manager.GetValue(GetType(T).GetMember(value.ToString).FirstOrDefault)
		End Sub
		Public Shared Function GetEnumNamePair() As List(Of EnumNamePair(Of T))
			Dim L As New List(Of EnumNamePair(Of T))
			For Each item As T In [Enum].GetValues(GetType(T))
				L.Add(New EnumNamePair(Of T)(item))
			Next
			Return L
		End Function
		Public Overrides Function ToString() As String
			Return Name
		End Function
		Public Shared Widening Operator CType(value As EnumNamePair(Of T)) As T
			Return value.Value
		End Operator
	End Class
End Class
