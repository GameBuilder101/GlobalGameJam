using Godot;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	[Export]
	public float speed = 300.0f;
	[Export]
	public float jumpVelocity = -400.0f;
	[Export]
	public double coyoteTime = 0.25f;
	private double _coyoteTimer;
	[Export]
	public float gravity = 200.0f;
	private bool _doubleJumped;

	[Export]
	public Node2D respawnPoint;

	public bool IsDead { get; private set; }
	/// <summary>
	/// The current death count.
	/// </summary>
	public int DeathCount { get; private set; }
	private double _resetDelay;

	[Signal]
	public delegate void DeathEventHandler();

	public override void _Ready()
	{
		base._Ready();
		AddToGroup(Level.ResetableGroup);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (IsOnFloor()) // Reset double jump
		{
			_coyoteTimer = coyoteTime;
			_doubleJumped = false;
		}
		else // Add the gravity
		{
			velocity.Y += gravity * (float)delta;
			_coyoteTimer -= delta;
		}

		// Handle jumping
		if (Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("ui_up"))
		{
			if (IsOnFloor() || _coyoteTimer > 0.0 || !_doubleJumped)
				velocity.Y = jumpVelocity;
			if (!(IsOnFloor() || _coyoteTimer > 0.0))
				_doubleJumped = true;
		}

		// Get the input direction and handle the movement/deceleration
		float direction = Input.GetAxis("ui_left", "ui_right");
		if (direction != 0.0f)
			velocity.X = direction * speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);

		Velocity = velocity;
		if (IsDead)
			Velocity = Vector2.Zero;
		MoveAndSlide();

		if (_resetDelay > 0.0)
			_resetDelay -= delta;
	}

	/// <summary>
	/// Eat sht lol.
	/// </summary>
	public void Die()
	{
		if (IsDead || _resetDelay > 0.0)
			return;
		IsDead = true;
		Velocity = Vector2.Zero;
		Hide();
		EmitSignal(SignalName.Death);
	}

	public void Reset(int newDeathCount)
	{
		Position = respawnPoint.Position;
		Show();
		IsDead = false;
		_resetDelay = 1.0;
	}
}
