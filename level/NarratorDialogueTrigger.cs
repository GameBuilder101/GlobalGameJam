using Godot;

public partial class NarratorDialogueTrigger : Area2D
{
    private bool _triggered;

    [Export]
    public NarratorScript dialogue;

    public override void _Ready()
	{
        BodyEntered += OnTrigger;
    }

    private void OnTrigger(Node2D body)
    {
        if (_triggered || !(body is Player))
            return;
        _triggered = true;
        dialogue.Start();
    }
}
