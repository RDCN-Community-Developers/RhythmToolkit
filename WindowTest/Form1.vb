Imports System.Security.Policy
Imports Sprite.RhythmSprite
Imports Sprite
Public Class Form1
	Public times As Integer = 0
	Public A As Sprite.RhythmSprite.Sprite
	Public Sub New()
		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

	End Sub

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim Sets As New SpriteOutputSettings With {
			.OverWrite = True
			}
		A = Sprite.RhythmSprite.Sprite.ReadJson("C:\Users\30698\OneDrive\文档\rdlevels\OFN\CB&HB_With_Syncopation.json")
		Me.BackColor = Color.Black
	End Sub
	Private Sub Form1Click() Handles MyBase.Click
		Me.CreateGraphics.Clear(Me.BackColor)
		Dim img = Sprite.RhythmSprite.Sprite.OutGlow(A.Images(times Mod A.Images.Count))
		Me.CreateGraphics.DrawImage(img, 0, 0, New Rectangle(New Point, New Size(240, 320)), GraphicsUnit.Document)
		times += 1
	End Sub
End Class