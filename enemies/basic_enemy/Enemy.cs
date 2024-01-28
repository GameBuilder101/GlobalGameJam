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
	private Node2D _movingSprite1;
    [Export]
    private Node2D _movingSprite2;
	private double _movingSpriteTimer;
    [Export]
	private Godot.Collections.Array<CollisionShape2D> _collision;

	[Export]
	public double deathDelay = 1.0;
	private double _deathTime;
	private double _resetDelay;

	[Export]
	private AudioStreamPlayer _deathAudio;

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

		_movingSpriteTimer += delta;
		if (_movingSpriteTimer >= 0.5)
		{
			if (_movingSprite1 != null)
				_movingSprite1.Visible = !_movingSprite1.Visible;
            if (_movingSprite2 != null)
                _movingSprite2.Visible = !_movingSprite2.Visible;
			_movingSpriteTimer = 0.0;
        }
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_movingToMax && Position.X >= _maxWanderPoint.Position.X)
		{
            _movingToMax = false;
            _aliveSprite.Scale = new Vector2(1.0f, _aliveSprite.Scale.Y);
			if (_deadSprite != null)
				_deadSprite.Scale = new Vector2(1.0f, _deadSprite.Scale.Y);
        }
		else if (!_movingToMax && Position.X <= _minWanderPoint.Position.X)
		{
            _movingToMax = true;
            _aliveSprite.Scale = new Vector2(-1.0f, _aliveSprite.Scale.Y);
            if (_deadSprite != null)
                _deadSprite.Scale = new Vector2(-1.0f, _deadSprite.Scale.Y);
        }

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
		{
			Die();
            Player player = (Player)body;
            Vector2 velocity = player.Velocity;
            velocity.Y = 0;
            player.Velocity = velocity;
        }
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

		_deathAudio.Play();
	}

	public void Reset(int newDeathCount)
	{
		Position = _startPosition;
		Velocity = Vector2.Zero;

		IsDead = false;
		_aliveSprite.Show();
		if (_deadSprite != null)
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
