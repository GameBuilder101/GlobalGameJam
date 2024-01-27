using Godot;

public partial class Enemy : CharacterBody2D
{
	private Vector2 _startPosition;

	/// <summary>
	/// The movement speed of the enemy.
	/// </summary>
	[Export]
	public float speed = 300.0f;

	[Export]
	private Node2D _minWanderPoint;
	[Export]
	private Node2D _maxWanderPoint;
	private bool _movingToMax = true;

	public bool IsDead { get; private set; }
	[Export]
	private Node2D _aliveSprite;
	[Export]
	private Node2D _deadSprite;
	[Export]
	private Godot.Collections.Array<CollisionShape2D> _collision;

	[Export]
	public double deathDelay = 1.0;
	private double _deathTime;
	private double _resetDelay;

	public override void _Ready()
	{
		base._Ready();
		AddToGroup(Level.ResetableGroup);

		_startPosition = Position;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (IsDead && _deathTime > 0.0f)
		{
			_deathTime -= delta;
			if (_deathTime <= 0.0)
				Hide();
		}

		if (_resetDelay > 0.0)
		{
			_resetDelay -= delta;
			if (_resetDelay <= 0.0f)
				SetCollisionEnabled(true);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_movingToMax && Position.X >= _maxWanderPoint.Position.X)
			_movingToMax = false;
		else if (!_movingToMax && Position.X <= _minWanderPoint.Position.X)
			_movingToMax = true;

		Vector2 velocity = Velocity;
		if (_movingToMax)
			velocity.X = speed;
		else
			velocity.X = -speed;

		Velocity = velocity;
		if (IsDead)
			Velocity = Vector2.Zero;
		MoveAndSlide();
	}

	public void _on_die_trigger_body_entered(Node2D body)
	{
		if (body is Player && !((Player)body).IsDead)
			Die();
	}

	public void _on_kill_trigger_body_entered(Node2D body)
	{
		if (body is Player)
			SetCollisionEnabled(false);
	}

	public void Die()
	{
		if (IsDead || _resetDelay > 0.0)
			return;
		IsDead = true;

		_aliveSprite.Hide();
		_deadSprite.Show();
		_deathTime = deathDelay;

		SetCollisionEnabled(false);
	}

	public void Reset(int newDeathCount)
	{
		Position = _startPosition;
		Velocity = Vector2.Zero;

		IsDead = false;
		_aliveSprite.Show();
		_deadSprite.Hide();

		SetCollisionEnabled(true);

		Show();

		_resetDelay = 1.0;
	}

	private void SetCollisionEnabled(bool value)
	{
		// Stupid stinky ugly meanie code deferred disable is cring
		foreach (CollisionShape2D collision in _collision)
			collision.SetDeferred("disabled", !value);
	}
}