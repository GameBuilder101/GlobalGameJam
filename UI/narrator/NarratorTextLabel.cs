using Godot;
using System;

public partial class NarratorTextLabel : RichTextLabel
{
	[Export]
	public static NarratorTextLabel Self;
	
	[Export]
	private Panel Background;
	
	[Export]
	private double Delay;
	private double Waiting = 0;
	
	private String TextTotal = "";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.SetText("");
	}
	
	private bool ProgressText() {
		if (this.Text.Length >= this.TextTotal.Length - 1) {
			this.Text = this.TextTotal;
			return false;
		}
		//if (this.Text == "") {
			//this.Background.Size = new Vector2(this.Size.X, this.Size.Y);
		//}
		if (this.TextTotal[this.Text.Length] == '[') {
			this.Text = this.TextTotal.Substring(0, this.TextTotal.IndexOf(']', this.Text.Length) + 1);
			return true;
		} 
		this.Waiting = this.Delay;
		this.Text = this.TextTotal.Substring(0, this.Text.Length + 1);
		return false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Object.ReferenceEquals(this.Text, this.TextTotal)) {
			return;
		}
		this.Waiting -= delta;
		if (this.Waiting <= 0) {
			while (this.ProgressText()) {}
		}
	}
	
	public void SetText(String text) {
		this.Text = "";
		if (text == "") {
			this.Background.Size = new Vector2(0, 0);
		} else {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y);
		}
		this.TextTotal = text;
	}
	
	private void OnResized()
	{
		if (this.Text == "") {
			this.Background.Size = new Vector2(0, 0);
		} else {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y);
		}
	}
}
