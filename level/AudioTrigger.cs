using Godot;
using System;

public partial class AudioTrigger : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += TriggerEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void TriggerEntered(Node2D body)
	{
		SetDeferred("process_mode", 4);
	}
}
