using Godot;
using System;

public partial class CheckpointTrigger : Area2D
{
	[Export]
	public Player player;
	[Export]
	public Node2D respawnPoint;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        BodyEntered += OnTrigger;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnTrigger(Node2D body)
    {
		if (body is Player)
		{
            player.respawnPoint = respawnPoint;
            SetDeferred("process_mode", 4);
        }
    }
}
