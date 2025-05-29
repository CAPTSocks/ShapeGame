using Godot;
using System;
using System.Reflection.Emit;

public partial class GM : Node2D
{
	private int score = 0;
	private int finalScore = 0;
	private RichTextLabel scoreLabel;
	private DumbAss dmitri = new DumbAss(); 
	private HighScoreResource scores;
	private int newScoreIndex;
	private Panel gameoverPanel;
	private Vector2 gameoverPanelStartPos; 

	//Events
	[Signal] public delegate void ScoreUpdateEventHandler (int points);
	[Signal] public delegate void GameOverEventHandler();
	[Signal] public delegate void StartGameEventHandler();

    public override void _Ready()
    {
        scores = (HighScoreResource)GD.Load("res://Resources/HighScoresResource.tres");
		if (scores.resetScores == true)
		{
			scores.ResetScores();
		}
		ScoreUpdate += UpdateScore;

    }

	public void StartTheGame()
	{
		EmitSignal(SignalName.StartGame);
		GD.Print("Start Game ");
		score = 0;
	}

	private void UpdateScore(int points)
	{
		score += points;
	}

	public void gameOver()
	{
		EmitSignal(SignalName.GameOver);
		finalScore = score; 
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
		scores.UpdateScore(newScoreIndex, finalScore, name);
		HighScorePanel highscorePanel = GetParent().GetNode<HighScorePanel>("Main/HUD/HighScorePanel");
		highscorePanel.SetupPanel(); 
		//GetParent().GetNode<Panel>("Main/HUD/HighScorePanel").Visible = true;
	}

	private void TweenGameoverPanel() 
	{
		GD.Print("Gameover tween called");
		gameoverPanel.Position -= new Vector2(gameoverPanel.Position.X , GetViewportRect().Size.Y);
        gameoverPanelStartPos = gameoverPanel.Position;
		RichTextLabel scoreLabel = gameoverPanel.GetNode<RichTextLabel>("ScoreNumberLabel");
		scoreLabel.Text = "[center]" + finalScore.ToString();
		gameoverPanel.Visible = true; 
		Tween tween = GetTree().CreateTween();
        tween.TweenProperty(gameoverPanel, "position", Vector2.Zero, 0.5f);
	}

	private bool HandleHighScores()
	{
		if (finalScore <= 0)
			return false;

		for (int i = 0; i < scores.highScores.Count; i++)
		{
			if (finalScore > scores.highScores[i].score)
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
