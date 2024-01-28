using Godot;
using System;

public partial class loon : CharacterBody2D
{
	[Export]
	private Godot.Collections.Array<CollisionShape2D> _collision;
	
	[Export]
	private Node2D chasing;
	
	[Export]
	private float speed = 2400;
	
	private Vector2 _startPosition;
	
	private bool Alive = true;
	
	public override void _Ready()
	{
		base._Ready();
		AddToGroup(Level.ResetableGroup);

		_startPosition = this.Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!this.Alive) {
			return;
		}
		Vector2 velocity = this.Velocity;
		Vector2 addingVelocity = Vector2.Zero;
		
		float distance = this.Position.DistanceTo(this.chasing.Position);
		if (distance < 600) {
			float Dy = (this.chasing.Position.Y - this.Position.Y) / 2;
			if (Math.Abs(Dy) > 300) {
				addingVelocity.Y = (Dy > 0) ? 1 : -1;
			} else {
				float Dx = this.chasing.Position.X - (int) this.Position.X;
				if (Dx == 0) {
					addingVelocity.X = 0;
					addingVelocity.Y = this.speed;
				} else {
				
					double l = Math.Sqrt(Math.Pow(distance, 2) + Math.Pow(Dy, 2));
					float r = (float) (Math.Pow(Dy, 2) / 2 / l + l / 2);

					addingVelocity.Y = r - Math.Abs(Dx);
					addingVelocity.X = Dy;
					if (Dx < 0) {
						addingVelocity.X *= -1;
					}

					addingVelocity /= addingVelocity.Length();
				}
			}
			addingVelocity *= this.speed * (float) delta;
		}
		velocity *= 0.99f;
		velocity += addingVelocity;
		this.Velocity = velocity;
		//GD.Print(distance, velocity, addingVelocity);
		this.MoveAndSlide();
	}
	
	public void Die(Node2D body)
	{
		if (body is Player && !((Player)body).IsDead){
			this.Alive = false;
			this.SetCollisionEnabled(false);
		}
	}
	
	public void Reset(int newDeathCount)
	{
		this.Position = _startPosition;
		this.Velocity = Vector2.Zero;

		this.Alive = true;

		this.SetCollisionEnabled(true);
		this.Show();
	}

	private void SetCollisionEnabled(bool value)
	{
		foreach (CollisionShape2D collision in _collision)
			collision.SetDeferred("disabled", !value);
	}
}
