using Godot;
using static System.Net.Mime.MediaTypeNames;

public partial class Level : Node2D
{
	public const string ResetableGroup = "resetable";

	[Export]
	public Player player;

	[Export]
	private double _deathTimerDuration = 1.0;
	private double _deathTimer = -1.0;

	[Export]
	public string[] deathDialogues;
	private int _currentDeathDialogue;

	public override void _Ready()
	{
		base._Ready();
		player.Death += StartDeathTimer;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (_deathTimer > 0.0)
		{
			_deathTimer -= delta;
			if (_deathTimer <= 0.0)
				Reset();
		}
	}

	public void StartDeathTimer()
	{
		if (_deathTimer > 0.0)
			return;
		_deathTimer = _deathTimerDuration;

		if (deathDialogues[_currentDeathDialogue] != "")
			NarratorTextLabel.Self.SetText(deathDialogues[_currentDeathDialogue]);
		_currentDeathDialogue++;
	}

	/// <summary>
	/// Resets the level and any resetables.
	/// </summary>
	public void Reset()
	{
		GetTree().CallGroup(ResetableGroup, "Reset", player.DeathCount);
	}
}
