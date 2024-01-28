using Godot;
using System;

public partial class GooseTrigger : Area2D
{
    private bool _triggered;

    [Export]
	private Player _player;
	[Export]
	private Duck _duck;

	[Export]
	private NarratorScript _endDialogue;

	private double _dialogueStartDelay;
	private double _endDelay;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        BodyEntered += OnTrigger;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnTrigger(Node2D body)
    {
        if (_triggered || !(body is Player))
            return;
        _triggered = true;
		_player.Goose();
		_duck.Reset(0);
    }
}
