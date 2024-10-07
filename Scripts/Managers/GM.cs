using Godot;
using System;
using System.Reflection.Emit;

public partial class GM : Node2D
{
	private int score = 0;
	private RichTextLabel scoreLabel;
	private DumbAss dmitri = new DumbAss(); 
	[Signal] public delegate void ScoreUpdateEventHandler (int points);
	[Signal] public delegate void GameOverEventHandler ();
	private HighScoreResource scores;
	private int newScoreIndex;
	private Panel gameoverPanel;
	private Vector2 gameoverPanelStartPos; 

    public override void _Ready()
    {
        scores = (HighScoreResource)GD.Load("res://Resources/HighScoresResource.tres");
		if (scores.resetScores == true)
		{
			scores.ResetScores();
		}
		ScoreUpdate += UpdateScore;

    }

	private void UpdateScore(int points)
	{
		score += points;
	}

	public void gameOver()
	{
		EmitSignal(SignalName.GameOver);
		gameoverPanel = GetParent().GetNode<Panel>("Main/HUD/GameoverPanel");
		TweenGameoverPanel();
		
		if (HandleHighScores())
		{
			GetParent().GetNode<Panel>("Main/HUD/NewHighScorePanel").Visible = true;
		}
	}

	public void UpdateHighScore(string name)
	{
		//scores.highScores[newScoreIndex].score = score;
		//scores.highScores[newScoreIndex].name = name; 
		scores.UpdateScore(newScoreIndex, score, name);
		HighScorePanel highscorePanel = GetParent().GetNode<HighScorePanel>("Main/HUD/GameoverPanel/HighScorePanel");
		highscorePanel.SetupPanel(); 
		//GetParent().GetNode<Panel>("Main/HUD/HighScorePanel").Visible = true;
	}

	private void TweenGameoverPanel() 
	{
		GD.Print("Gameover tween called");
		gameoverPanel.Position -= new Vector2(gameoverPanel.Position.X , GetViewportRect().Size.Y);
        gameoverPanelStartPos = gameoverPanel.Position;
		RichTextLabel scoreLabel = gameoverPanel.GetNode<RichTextLabel>("ScoreNumberLabel");
		scoreLabel.Text = "[center]" + score.ToString();
		gameoverPanel.Visible = true; 
		Tween tween = GetTree().CreateTween();
        tween.TweenProperty(gameoverPanel, "position", Vector2.Zero, 0.5f);
	}

	private bool HandleHighScores()
	{
		if (score <= 0)
			return false;

		for (int i = 0; i < scores.highScores.Count; i++)
		{
			if (score > scores.highScores[i].score)
			{
				newScoreIndex = i;
				return true;
			}
		}
		return false;
	}

    public override void _ExitTree()
    {
        ScoreUpdate -= UpdateScore;
    }
}
