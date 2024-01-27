using Godot;
using System;

public partial class NarratorScripts : Resource
{
	[Export]
	private String[] Lines;
	
	[Export]
	private AudioStream[] Voices;
	
	private int On = 0;
	
	public void StartOver() {
		this.On = 0;
		NarratorAudio.Self.OnScript = this;
		this.Next();
	}
	
	public void Next() {
		if (!Object.ReferenceEquals(NarratorAudio.Self.OnScript, this)) {
			return;
		}
		if (this.On < Lines.Length && this.On < Voices.Length) {
			NarratorTextLabel.Self.SetText(Lines[this.On]);
			NarratorAudio.Self.OnScript = this;
		}
		this.On += 1;
	}
}
