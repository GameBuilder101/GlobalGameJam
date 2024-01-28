using Godot;
using System;

public partial class title_screen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("Start_To_Suffer").Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void GoToLevelScene(){
	
	
	}
	
	private void _on_start_button_pressed()
	{
		GetNode<AudioStreamPlayer>("Start_To_Suffer").Stop();
		
		GetTree().ChangeSceneToFile("res://level/level.tscn");
	}
	private void _on_quit_button_pressed()
	{
		GetTree().Quit();
		// Replace with function body.
	}
	private void _on_credits_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://credits_folder/CreditsScreen.tscn");
	}
	private void _on_start_to_suffer_finished()
	{
			GetNode<AudioStreamPlayer>("Start_To_Suffer").Play();
	}
}



