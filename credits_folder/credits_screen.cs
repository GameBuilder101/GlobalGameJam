using Godot;
using System;

public partial class credits_screen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_button_pressed()
{
	GetNode<Label>("Contributors").Hide();
	GetNode<Label>("Audio").Hide();
	GetNode<Label>("Thanks").Hide();
	GetNode<Label>("Software").Hide();
	GetNode<Button>("BackButton").Hide();
	
	
	
	// Replace with function body.
}
}



