using Godot;

public partial class FallingTile : Area2D
{
	[Export]
    public float fallGravity = 100.0f;
	[Export]
	public float maxFallVelocity = 1000.0f;
    private float _currentFallVelocity;

    private Vector2 _startPosition;

    private bool _fall;

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
            Position = new Vector2(Position.X, Position.Y + _currentFallVelocity);
        }
	}

	public void Fall(Node2D body)
	{
        if (body is Player)
            _fall = true;
	}

    public void Reset(int newDeathCount)
    {
        Position = _startPosition;
        _fall = false;
        _currentFallVelocity = 0.0f;
    }
}
