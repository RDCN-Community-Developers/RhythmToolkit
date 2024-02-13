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
		FlowLayoutPanel1 = New FlowLayoutPanel()
		Button1 = New Button()
		Button2 = New Button()
		Button3 = New Button()
		Button4 = New Button()
		Button5 = New Button()
		FlowLayoutPanel2 = New FlowLayoutPanel()
		Button6 = New Button()
		TextBox2 = New TextBox()
		TextBox1 = New TextBox()
		Button7 = New Button()
		Button8 = New Button()
		TableLayoutPanel1 = New TableLayoutPanel()
		Button9 = New Button()
		FlowLayoutPanel1.SuspendLayout()
		FlowLayoutPanel2.SuspendLayout()
		SuspendLayout()
		' 
		' ImportButton
		' 
		ImportButton.Location = New Point(12, 12)
		ImportButton.Name = "ImportButton"
		ImportButton.Size = New Size(75, 23)
		ImportButton.TabIndex = 1
		ImportButton.Text = "导入文件"
		ImportButton.UseVisualStyleBackColor = True
		' 
		' OpenFileDialog1
		' 
		OpenFileDialog1.FileName = "OpenFileDialog1"
		' 
		' FlowLayoutPanel1
		' 
		FlowLayoutPanel1.Controls.Add(Button1)
		FlowLayoutPanel1.Controls.Add(Button2)
		FlowLayoutPanel1.Controls.Add(Button3)
		FlowLayoutPanel1.Controls.Add(Button4)
		FlowLayoutPanel1.Controls.Add(Button5)
		FlowLayoutPanel1.Controls.Add(FlowLayoutPanel2)
		FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown
		FlowLayoutPanel1.Location = New Point(12, 41)
		FlowLayoutPanel1.Name = "FlowLayoutPanel1"
		FlowLayoutPanel1.Size = New Size(472, 397)
		FlowLayoutPanel1.TabIndex = 2
		' 
		' Button1
		' 
		Button1.Location = New Point(3, 3)
		Button1.Name = "Button1"
		Button1.Size = New Size(122, 23)
		Button1.TabIndex = 0
		Button1.Text = "拆分护士提示"
		Button1.UseVisualStyleBackColor = True
		' 
		' Button2
		' 
		Button2.Location = New Point(3, 32)
		Button2.Name = "Button2"
		Button2.Size = New Size(122, 23)
		Button2.TabIndex = 1
		Button2.Text = "拆分七拍子"
		Button2.UseVisualStyleBackColor = True
		' 
		' Button3
		' 
		Button3.Location = New Point(3, 61)
		Button3.Name = "Button3"
		Button3.Size = New Size(122, 23)
		Button3.TabIndex = 2
		Button3.Text = "拆分二拍子"
		Button3.UseVisualStyleBackColor = True
		' 
		' Button4
		' 
		Button4.Location = New Point(3, 90)
		Button4.Name = "Button4"
		Button4.Size = New Size(122, 23)
		Button4.TabIndex = 3
		Button4.Text = "删除未激活事件"
		Button4.UseVisualStyleBackColor = True
		' 
		' Button5
		' 
		Button5.Location = New Point(3, 119)
		Button5.Name = "Button5"
		Button5.Size = New Size(122, 23)
		Button5.TabIndex = 4
		Button5.Text = " 释放标签"
		Button5.UseVisualStyleBackColor = True
		' 
		' FlowLayoutPanel2
		' 
		FlowLayoutPanel2.Controls.Add(Button6)
		FlowLayoutPanel2.Controls.Add(TextBox2)
		FlowLayoutPanel2.Controls.Add(TextBox1)
		FlowLayoutPanel2.Location = New Point(3, 148)
		FlowLayoutPanel2.Name = "FlowLayoutPanel2"
		FlowLayoutPanel2.Size = New Size(455, 227)
		FlowLayoutPanel2.TabIndex = 5
		' 
		' Button6
		' 
		Button6.Location = New Point(3, 3)
		Button6.Name = "Button6"
		Button6.Size = New Size(119, 23)
		Button6.TabIndex = 0
		Button6.Text = "批量添加标签"
		Button6.UseVisualStyleBackColor = True
		' 
		' TextBox2
		' 
		TextBox2.Location = New Point(128, 3)
		TextBox2.Name = "TextBox2"
		TextBox2.PlaceholderText = "标签名"
		TextBox2.Size = New Size(100, 23)
		TextBox2.TabIndex = 2
		' 
		' TextBox1
		' 
		TextBox1.Location = New Point(3, 32)
		TextBox1.Multiline = True
		TextBox1.Name = "TextBox1"
		TextBox1.PlaceholderText = "条件"
		TextBox1.Size = New Size(440, 174)
		TextBox1.TabIndex = 1
		TextBox1.Text = " return true;"
		' 
		' Button7
		' 
		Button7.Location = New Point(93, 12)
		Button7.Name = "Button7"
		Button7.Size = New Size(75, 23)
		Button7.TabIndex = 3
		Button7.Text = "导出文件"
		Button7.UseVisualStyleBackColor = True
		' 
		' Button8
		' 
		Button8.Location = New Point(730, 372)
		Button8.Name = "Button8"
		Button8.Size = New Size(75, 23)
		Button8.TabIndex = 0
		Button8.Text = "Button8"
		Button8.UseVisualStyleBackColor = True
		' 
		' TableLayoutPanel1
		' 
		TableLayoutPanel1.AutoSize = True
		TableLayoutPanel1.ColumnCount = 2
		TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 31.2080536F))
		TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 68.79195F))
		TableLayoutPanel1.Location = New Point(490, 41)
		TableLayoutPanel1.Name = "TableLayoutPanel1"
		TableLayoutPanel1.RowCount = 2
		TableLayoutPanel1.RowStyles.Add(New RowStyle())
		TableLayoutPanel1.RowStyles.Add(New RowStyle())
		TableLayoutPanel1.Size = New Size(298, 26)
		TableLayoutPanel1.TabIndex = 5
		' 
		' Button9
		' 
		Button9.Location = New Point(490, 372)
		Button9.Name = "Button9"
		Button9.Size = New Size(75, 23)
		Button9.TabIndex = 6
		Button9.Text = "Button9"
		Button9.UseVisualStyleBackColor = True
		' 
		' Form1
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(800, 450)
		Controls.Add(Button9)
		Controls.Add(Button8)
		Controls.Add(TableLayoutPanel1)
		Controls.Add(Button7)
		Controls.Add(FlowLayoutPanel1)
		Controls.Add(ImportButton)
		DoubleBuffered = True
		Name = "Form1"
		Text = "Form1"
		FlowLayoutPanel1.ResumeLayout(False)
		FlowLayoutPanel2.ResumeLayout(False)
		FlowLayoutPanel2.PerformLayout()
		ResumeLayout(False)
		PerformLayout()
	End Sub
	Friend WithEvents ImportButton As Button
	Friend WithEvents OpenFileDialog1 As OpenFileDialog
	Friend WithEvents SaveFileDialog1 As SaveFileDialog
	Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
	Friend WithEvents Button1 As Button
	Friend WithEvents Button2 As Button
	Friend WithEvents Button3 As Button
	Friend WithEvents Button4 As Button
	Friend WithEvents Button5 As Button
	Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
	Friend WithEvents Button6 As Button
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Button7 As Button
	Friend WithEvents Button8 As Button
	Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
	Friend WithEvents Button9 As Button

End Class
