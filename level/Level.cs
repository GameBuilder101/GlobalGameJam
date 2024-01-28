using Godot;

public partial class Level : Node2D
{
	public const string ResetableGroup = "resetable";

	[Export]
	public Player player;

	[Export]
	private double _deathTimerDuration = 1.0;
	private double _deathTimer = -1.0;

	[Export]
	public Godot.Collections.Array<NarratorScript> deathDialogues;
	private int _currentDeathDialogue;

	private bool _playedSlide;

	public override void _Ready()
	{
		base._Ready();
		GetNode<AudioStreamPlayer>("GoofballMarch").Play();
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

		if (_currentDeathDialogue < deathDialogues.Count && !NarratorTextLabel.Self.IsWriting)
		{
			if (deathDialogues[_currentDeathDialogue] != null)
				deathDialogues[_currentDeathDialogue].Start();
			_currentDeathDialogue++;
		}
	}
	
	///restarts music
	private void _on_goofball_march_finished()
	{
		GetNode<AudioStreamPlayer>("GoofballMarch").Play();
	}

	private void _on_duck_theme_1_finished()
	{
		GetNode<AudioStreamPlayer>("DuckTheme2").Play();
	}

	private void _on_duck_theme_2_finished()
	{
		GetNode<AudioStreamPlayer>("DuckTheme2").Play();
	}

	/// <summary>
	/// Resets the level and any resetables.
	/// </summary>
	public void Reset()
	{
		GetTree().CallGroup(ResetableGroup, "Reset", player.DeathCount);
	}

	public void _on_checkpoint_slide_trigger_body_entered(Node2D body)
	{
		if (_playedSlide)
			return;
		_playedSlide = true;
		GetNode<AudioStreamPlayer>("CheckpointSlide").Play();
	}

	public void _on_duck_reveal_trigger_body_entered(Node2D body)
	{
		GetNode<AudioStreamPlayer>("DuckReveal").Play();
	}

	private void _on_checkpoint_trigger_body_entered(Node2D body)
	{
		FadeAudioStreamPlayer(GetNode<AudioStreamPlayer>("GoofballMarch"));
	}

	public void _on_duck_theme_1_trigger_body_entered(Node2D body)
	{
		GetNode<AudioStreamPlayer>("DuckTheme1").Play();
	}

	public void _on_duck_theme_2_trigger_body_entered(Node2D body)
	{
		FadeAudioStreamPlayer(GetNode<AudioStreamPlayer>("DuckTheme2"));
	}

	private void FadeAudioStreamPlayer(AudioStreamPlayer player)
	{
		Tween tween = CreateTween();
		tween.TweenProperty(player, "volume_db", -80.0f, 3.0f);
	}
}


