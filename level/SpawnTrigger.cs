using Godot;

/// <summary>
/// Spawns an object
/// </summary>
public partial class SpawnTrigger : Area2D
{
	[Export]
	public Godot.Collections.Array<Node2D> spawnObjects;
	private bool _triggered;
	/// <summary>
	/// When true, the trigger will reset on death.
	/// </summary>
	[Export]
	public bool reset = true;

	public override void _Ready()
	{
        AddToGroup(Level.ResetableGroup);
        BodyEntered += OnTrigger;
        DespawnObjects();
    }

	private void OnTrigger(Node2D body)
	{
		if (_triggered || !(body is Player))
			return;
		_triggered = true;
		foreach (Node2D node in spawnObjects)
		{
            node.ProcessMode = ProcessModeEnum.Inherit;
			node.Show();
        }
    }

    public void Reset(int newDeathCount)
    {
		if (!reset)
			return;
		_triggered = false;
		DespawnObjects();
    }

	private void DespawnObjects()
	{
        foreach (Node2D node in spawnObjects)
        {
            node.ProcessMode = ProcessModeEnum.Disabled;
            node.Hide();
        }
    }
}
