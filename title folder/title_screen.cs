using Godot;
using System;

public partial class title_screen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void GoToLevelScene(){
	
	
	}
	
	private void _on_button_pressed()
{
	GetNode<Label>("Label").Hide();
	GetNode<Button>("Button").Hide();
	GetNode<Button>("Button2").Hide();
	
	var scene = GD.Load<PackedScene>("res://level/level.tscn");
	var instance = scene.Instantiate();
	AddChild(instance);
	// Replace with function body.
}
private void _on_button_2_pressed()
{
	GetTree().Quit();
	// Replace with function body.
}
}









