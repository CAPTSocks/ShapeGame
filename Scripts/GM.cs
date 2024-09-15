using Godot;
using System;

public partial class GM : Node
{
	private int score = 0;
	private RichTextLabel scoreLabel;
	private DumbAss dmitri = new DumbAss(); 
	[Signal] public delegate void ScoreUpdateEventHandler (int points);
	

	public void RestartLevel()
	{
		GetTree().ReloadCurrentScene();
	}
}
