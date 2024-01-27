using Godot;

public partial class Camera : Camera2D
{
	/// <summary>
	/// The player to follow.
	/// </summary>
	[Export]
	private Player _player;

    [Export]
	private float _xOffset;
	private bool _removeOffset;

	[Export]
	private float _smoothingSpeed = 4.0f;

    [Export]
    private float _yBaseline;
	[Export]
	private float _yOffset;
    [Export]
	private float _yScale;
    [Export]
    private float _yExtremeMin;
    [Export]
    private float _yExtremeMax;

    public override void _PhysicsProcess(double delta)
	{
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction.X != 0.0f)
            _removeOffset = direction.X < 0;

        float xOffset = _xOffset;
		if (_removeOffset)
			xOffset = 0.0f;

		float y = (_player.Position.Y - _yBaseline) * _yScale;
		if (y < _yExtremeMin)
			y = _yExtremeMin;
		else if (y > _yExtremeMax)
			y = _yExtremeMax;

		Vector2 targetPosition = new Vector2(_player.Position.X + xOffset, y + _yOffset);

		Position = Position.MoveToward(targetPosition, _smoothingSpeed * (float)delta * Position.DistanceTo(targetPosition));
    }
}
