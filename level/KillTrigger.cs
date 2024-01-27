using Godot;

public partial class KillTrigger : Area2D
{
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (!(body is Player))
            return;
        ((Player)body).Die();
    }
}
