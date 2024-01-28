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
	[Export]
	private AudioStreamPlayer _endAudio;

	private double _currentDelay = -1.0;
	private double _dialogueStartDelay = 3.0;
	private bool _displayedDialogue;
	private double _endDelay = 20.0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        BodyEntered += OnTrigger;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_currentDelay < 0.0)
			return;
		_currentDelay += delta;
		if (_currentDelay >= _dialogueStartDelay && !_displayedDialogue)
		{
			_endDialogue.Start();
			_endAudio.Play();
			_displayedDialogue = true;
		}
		else if (_currentDelay >= _endDelay)
		{
            GetTree().ChangeSceneToFile("res://title_folder/TitleNode.tscn");
        }
	}

    private void OnTrigger(Node2D body)
    {
        if (_triggered || !(body is Player))
            return;
        _triggered = true;
		_player.Goose();
		_duck.Reset(0);
		_currentDelay = 0.0;
    }
}
