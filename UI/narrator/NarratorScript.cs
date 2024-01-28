using Godot;
using System;
using System.Diagnostics;

[GlobalClass]
public partial class NarratorScript : Resource
{
	[Export]
	private String[] Lines;

	[Export]
	private AudioStream _speechBlipAudio;
	public AudioStream SpeechBlipAudio { get; private set; }
	
	private int On = 0;
	
	public void Start() {
		this.On = 0;
		NarratorTextLabel.Self.OnScript = this;
		this.Next();
	}
	
	public bool StartRespectful() {
		if (this.Available()) {
			return false;
		}
		this.Start();
		return true;
	}

	public bool Available() {
		return NarratorTextLabel.Self.IsWriting;
	}
	
	
	public void Next() {
		if (!Object.ReferenceEquals(NarratorTextLabel.Self.OnScript, this)) {
			return;
		}
		if (this.On < Lines.Length) {
			NarratorTextLabel.Self.SetText(Lines[this.On], _speechBlipAudio);
		}
		this.On += 1;
	}
	
	public NarratorScript() {
		this.Lines = new String[0];
	}
	
	public NarratorScript(String[] lines) {
		this.Lines = lines;
	}
}
