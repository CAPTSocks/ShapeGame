using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class HighScorePanel : Panel
{
    [Export] private HighScoreResource highScoreResource;
    private List<ScoreUIObjects> scores = new List<ScoreUIObjects>();

    public override void _Ready()
    {
        SetupPanel();
    }

    private void SetupPanel()
    {
        for (int i = 0; i < highScoreResource.highScores.Count; i++)
        {
            ScoreUIObjects newScore = new ScoreUIObjects();
            int scoreIndex = i + 1;
            newScore.name = GetNode<RichTextLabel>("VBoxContainer/Score" + scoreIndex.ToString() + "/Name");
            newScore.score = GetNode<RichTextLabel>("VBoxContainer/Score" + scoreIndex.ToString() + "/Score");
            newScore.name.Text = highScoreResource.highScores[i].name;
            newScore.score.Text = "[right]" + highScoreResource.highScores[i].score;
            scores.Add(newScore);
        }
    }

      private void CloseButtonPressed()
    {
        Visible = false;
    }
}

public partial class ScoreUIObjects : Node
{
    public RichTextLabel name;
    public RichTextLabel score;
}


