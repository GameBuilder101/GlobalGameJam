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
	public bool IsWriting { get { return VisibleCharacters != Text.Length; } }

	[Export]
	private double _clearDelay = 4.0;
	private double _currentClearTimer = -1.0;

	[Export]
	private AudioStreamPlayer _speechBlipPlayer;
	
	public NarratorScript OnScript = null;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Text = "";
		Self = this;
	}
	
	private bool ProgressText() {
		if (this.VisibleCharacters >= this.Text.Length) {
			this.VisibleCharacters = this.Text.Length;
			return false;
		}
		if (this.VisibleCharacters == 0) {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y + 60.0f);
		}
		//if (this.Text[this.VisibleCharacters] == '[') {
			//this.VisibleCharacters = this.Text.IndexOf(']', this.VisibleCharacters) + 1;
			//return true;
		//} 
		this.Waiting = this.Delay;
		this.VisibleCharacters++;
		return false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (VisibleCharacters >= Text.Length) {

			if (_currentClearTimer >= -1.0)
				_currentClearTimer += delta;

			if (_currentClearTimer >= _clearDelay)
			{
				_currentClearTimer = -1.0;
				Text = "";
				OnScript.Next();
				OnResized();

            }

            return;
		}
		this.Waiting -= delta;
		if (this.Waiting <= 0) {
			while (this.ProgressText()) { }
			_speechBlipPlayer.PitchScale = (float)GD.RandRange(0.8, 1.1);
			_speechBlipPlayer.Play();
		}
	}
	
	public void SetText(String text, AudioStream blip) {
        _currentClearTimer = 0.0f;
        this.Text = text;
		if (text == "") {
			this.Background.Size = new Vector2(0, 0);
		} else {
			this.Background.Size = new Vector2(this.Size.X, this.Size.Y + 60.0f);
		}
		this.VisibleCharacters = 0;
		_speechBlipPlayer.Stream = blip;
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
