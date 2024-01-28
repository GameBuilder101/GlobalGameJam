using Godot;

public partial class FallingTile : Area2D
{
	[Export]
    public float fallGravity = 100.0f;
	[Export]
	public float maxFallVelocity = 1000.0f;
    private float _currentFallVelocity;
    [Export]
    private Node2D _targetDistance;

    private Vector2 _startPosition;

    private bool _fall;

    private double _delay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        AddToGroup(Level.ResetableGroup);
        _startPosition = Position;
        BodyEntered += Fall;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (_fall)
        {
            _currentFallVelocity += fallGravity * (float)delta;
            if (_currentFallVelocity > fallGravity)
                _currentFallVelocity = fallGravity;
            Vector2 newPos = new Vector2(Position.X, Position.Y + _currentFallVelocity);
            if (_targetDistance != null && newPos.Y > _targetDistance.Position.Y)
                newPos.Y = _targetDistance.Position.Y;
            Position = newPos;
        }

        if (_delay > 0.0)
            _delay -= delta;
	}

	public void Fall(Node2D body)
	{
        if (body is Player && _delay <= 0.0)
            _fall = true;
	}

    public void Reset(int newDeathCount)
    {
        Position = _startPosition;
        _fall = false;
        _currentFallVelocity = 0.0f;
        _delay = 1.0;
    }
}
