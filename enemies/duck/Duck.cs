using Godot;
using System;

// BEHOLD, FOR YOU GAZE UPON A GOD
public partial class Duck : Node2D
{
    public bool IsAwake { get; private set; }
    [Export]
    private Node2D _sleepingSprite;
    [Export]
    private Node2D _awakeSprite;

    private Vector2 _startPosition;

    [Export]
    private float _speed = 100.0f;
    [Export]
    private float _rotationSpeed = 3.0f;
    [Export]
    private Node2D _chasing;
    [Export]
    private CollisionShape2D _killTrigger;

    public override void _Ready()
	{
        AddToGroup(Level.ResetableGroup);

        _startPosition = Position;
    }

	public override void _Process(double delta)
	{
        if (IsAwake)
        {
            Position = Position.MoveToward(_chasing.Position, _speed * (float)delta); // Impending doom
            _awakeSprite.Scale = new Vector2(_chasing.Position.X < Position.X ? -1.0f : 1.0f, 1.0f);
            _awakeSprite.Rotate(_rotationSpeed * (float)delta);
        }
    }

    private void _on_awake_trigger_body_entered(Node2D body)
    {
        if (body is Player)
            Awake();
    }

    public void Awake()
    {
        if (IsAwake)
            return;
        IsAwake = true;
        _sleepingSprite.Hide();
        _awakeSprite.Show();
        _killTrigger.SetDeferred("disabled", false);
    }

    public void Reset(int newDeathCount)
    {
        Position = _startPosition;

        IsAwake = false;
        _sleepingSprite.Show();
        _awakeSprite.Hide();
        _killTrigger.SetDeferred("disabled", true);
    }
}
