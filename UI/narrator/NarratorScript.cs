using Godot;
using System;

[GlobalClass]
public partial class NarratorScript : Resource
{
	[Export]
	private String[] Lines;
	
	[Export]
	private AudioStream[] Voices;
	
	private int On = 0;
	
	public void Start() {
		this.On = 0;
		NarratorAudio.Self.OnScript = this;
		NarratorAudio.Self.Stop();
		this.Next();
	}
	
	public void StartRespectful() {
		if (NarratorAudio.Self.Playing) {
			return;
		}
		this.Start();
	}
	
	public void Next() {
		if (!Object.ReferenceEquals(NarratorAudio.Self.OnScript, this)) {
			return;
		}
		if (this.On < Lines.Length) {
			NarratorTextLabel.Self.SetText(Lines[this.On]);
		}
		if (this.On < Voices.Length) {
			NarratorAudio.Self.Stream = this.Voices[this.On];
			NarratorAudio.Self.Play(0);
		}
		this.On += 1;
	}
	
	public NarratorScript() {
		this.Lines = new String[0];
		this.Voices = new AudioStream[0];
	}
	
	public NarratorScript(String[] lines, AudioStream[] voices) {
		this.Lines = lines;
		this.Voices = voices;
	}
}
