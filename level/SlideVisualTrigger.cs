using Godot;
using System;

public partial class SlideVisualTrigger : Area2D
{
	[Export]
	public Node2D slideTarget;
	[Export]
	public Node2D targetPosition;
	[Export]
	public double duration;
    private double _currentSlideTime = -1.0;
    /// <summary>
    /// When true, the trigger will reset on death.
    /// </summary>
    [Export]
    public bool reset = true;

    private Vector2 _startPosition;

    public override void _Ready()
	{
        AddToGroup(Level.ResetableGroup);
        BodyEntered += OnTrigger;
        _startPosition = slideTarget.GlobalPosition;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_currentSlideTime >= 0.0)
        {
            _currentSlideTime += delta;
            if (_currentSlideTime > duration)
                _currentSlideTime = duration;
            slideTarget.GlobalPosition = _startPosition.Lerp(targetPosition.GlobalPosition, (float)(_currentSlideTime / duration));
        }
    }

    private void OnTrigger(Node2D body)
    {
        if (_currentSlideTime >= 0.0 || !(body is Player))
            return;
        _currentSlideTime = 0.0;
    }

    public void Reset(int newDeathCount)
    {
        if (!reset)
            return;
        _currentSlideTime = -1.0;
        slideTarget.GlobalPosition = _startPosition;
    }
}
