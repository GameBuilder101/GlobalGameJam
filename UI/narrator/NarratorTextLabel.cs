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

	[Export]
	private double _clearDelay = 4.0;
	private double _currentClearTimer = -1.0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Text = "";
		Self = this;
	}
	
	private bool ProgressText() {
		if (this.Text.Length >= this.TextTotal.Length - 1) {
			this.Text = this.TextTotal;
			return false;
		}
		if (this.Text == "") {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y + 60.0f);
		}
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
		if (Text == TextTotal) {

			if (_currentClearTimer >= -1.0)
				_currentClearTimer += delta;

			if (_currentClearTimer >= _clearDelay)
			{
				_currentClearTimer = -1.0;
				Text = "";
				TextTotal = "";
				OnResized();

            }

            return;
		}
		this.Waiting -= delta;
		if (this.Waiting <= 0) {
			while (this.ProgressText()) { }
		}
	}
	
	public void SetText(String text) {
        _currentClearTimer = 0.0f;
        this.Text = "";
		if (text == "") {
			this.Background.Size = new Vector2(0, 0);
		} else {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y + 60.0f);
		}
		this.TextTotal = text;
	}
	
	private void OnResized()
	{
		if (this.Text == "") {
			this.Background.Size = new Vector2(0, 0);
		} else {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y + 60.0f);
		}
	}
}
