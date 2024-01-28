using Godot;
using System;

public partial class DeadParticle : Sprite2D
{
	[Export]
	private CharacterBody2D player;
	private Vector2 startPosition;
	private Vector2 Velocity;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.startPosition = this.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!this.Visible) {
			return;
		}
		Vector2 velocity = this.Velocity;
		velocity.Y += 700 * (float) delta;
		this.Position += velocity * (float) delta;
		this.Velocity = velocity;
	}
	
	public void Shoot(float angle) {
		this.Position = this.startPosition;
		this.Velocity = new Vector2(0, 1).Rotated(angle) * 400;
	}
}
