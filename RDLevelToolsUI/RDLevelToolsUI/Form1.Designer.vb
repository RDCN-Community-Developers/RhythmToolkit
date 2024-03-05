<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
		ImportButton = New Button()
		OpenFileDialog1 = New OpenFileDialog()
		SaveFileDialog1 = New SaveFileDialog()
		SaveFileButton = New Button()
		PageUpButton = New Button()
		TableLayoutPanel1 = New TableLayoutPanel()
		PageDownButton = New Button()
		CreateLevelButton = New Button()
		SuspendLayout()
		' 
		' ImportButton
		' 
		ImportButton.AccessibleDescription = "导入关卡"
		ImportButton.AccessibleName = "导入"
		ImportButton.AccessibleRole = AccessibleRole.PushButton
		ImportButton.Location = New Point(93, 12)
		ImportButton.Name = "ImportButton"
		ImportButton.Size = New Size(75, 23)
		ImportButton.TabIndex = 1
		ImportButton.Text = "导入"
		ImportButton.UseVisualStyleBackColor = True
		' 
		' OpenFileDialog1
		' 
		OpenFileDialog1.FileName = "OpenFileDialog1"
		' 
		' SaveFileButton
		' 
		SaveFileButton.AccessibleDescription = "导出关卡"
		SaveFileButton.AccessibleName = "导出"
		SaveFileButton.AccessibleRole = AccessibleRole.PushButton
		SaveFileButton.Location = New Point(174, 12)
		SaveFileButton.Name = "SaveFileButton"
		SaveFileButton.Size = New Size(75, 23)
		SaveFileButton.TabIndex = 2
		SaveFileButton.Text = "导出"
		SaveFileButton.UseVisualStyleBackColor = True
		' 
		' PageUpButton
		' 
		PageUpButton.AccessibleDescription = "查看下一个事件"
		PageUpButton.AccessibleName = "下一个"
		PageUpButton.AccessibleRole = AccessibleRole.PushButton
		PageUpButton.Location = New Point(12, 70)
		PageUpButton.Name = "PageUpButton"
		PageUpButton.Size = New Size(75, 23)
		PageUpButton.TabIndex = 4
		PageUpButton.Text = "下一个"
		PageUpButton.UseVisualStyleBackColor = True
		' 
		' TableLayoutPanel1
		' 
		TableLayoutPanel1.AccessibleDescription = "呈现事件的所有可编辑属性"
		TableLayoutPanel1.AccessibleName = "事件属性框"
		TableLayoutPanel1.AutoSize = True
		TableLayoutPanel1.ColumnCount = 2
		TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 35.44304F))
		TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 64.55696F))
		TableLayoutPanel1.Location = New Point(93, 41)
		TableLayoutPanel1.Name = "TableLayoutPanel1"
		TableLayoutPanel1.RowCount = 2
		TableLayoutPanel1.RowStyles.Add(New RowStyle())
		TableLayoutPanel1.RowStyles.Add(New RowStyle())
		TableLayoutPanel1.Size = New Size(316, 26)
		TableLayoutPanel1.TabIndex = 5
		' 
		' PageDownButton
		' 
		PageDownButton.AccessibleDescription = "查看上一个事件"
		PageDownButton.AccessibleName = "上一个"
		PageDownButton.AccessibleRole = AccessibleRole.PushButton
		PageDownButton.Location = New Point(12, 41)
		PageDownButton.Name = "PageDownButton"
		PageDownButton.Size = New Size(75, 23)
		PageDownButton.TabIndex = 3
		PageDownButton.Text = "上一个"
		PageDownButton.UseVisualStyleBackColor = True
		' 
		' CreateLevelButton
		' 
		CreateLevelButton.AccessibleDescription = "新建关卡"
		CreateLevelButton.AccessibleName = "新建"
		CreateLevelButton.AccessibleRole = AccessibleRole.PushButton
		CreateLevelButton.Enabled = False
		CreateLevelButton.Location = New Point(12, 12)
		CreateLevelButton.Name = "CreateLevelButton"
		CreateLevelButton.Size = New Size(75, 23)
		CreateLevelButton.TabIndex = 0
		CreateLevelButton.Text = "新建"
		CreateLevelButton.UseVisualStyleBackColor = True
		' 
		' Form1
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(422, 658)
		Controls.Add(CreateLevelButton)
		Controls.Add(PageDownButton)
		Controls.Add(PageUpButton)
		Controls.Add(TableLayoutPanel1)
		Controls.Add(SaveFileButton)
		Controls.Add(ImportButton)
		DoubleBuffered = True
		Name = "Form1"
		Text = "Form1"
		ResumeLayout(False)
		PerformLayout()
	End Sub
	Friend WithEvents ImportButton As Button
	Friend WithEvents OpenFileDialog1 As OpenFileDialog
	Friend WithEvents SaveFileDialog1 As SaveFileDialog
	Friend WithEvents SaveFileButton As Button
	Friend WithEvents PageUpButton As Button
	Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
	Friend WithEvents PageDownButton As Button
	Friend WithEvents CreateLevelButton As Button

End Class
