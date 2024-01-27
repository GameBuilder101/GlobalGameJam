using Godot;
using System;

public partial class NarratorAudio : AudioStreamPlayer
{
	[Export]
	public static NarratorAudio Self;
	
	public NarratorScript OnScript = null;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NarratorAudio.Self = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnFinished()
	{
		this.OnScript.Next();
	}
}
