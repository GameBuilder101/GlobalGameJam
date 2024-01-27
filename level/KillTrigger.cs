using Godot;

public partial class KillTrigger : Area2D
{
    [Export]
    public NarratorScript deathDialogue;

    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (!(body is Player))
            return;
        if (deathDialogue != null)
            deathDialogue.Start();
        ((Player)body).Die();
    }
}
