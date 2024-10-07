using Godot;
using System;

public partial class NewHighScorePanel : Panel
{
    private HighScoreResource scores;
    private LineEdit line;
    private Button button;

    public override void _Ready()
    {
        scores = (HighScoreResource)GD.Load("res://Resources/HighScoresResource.tres");
        line = GetNode<LineEdit>("LineEdit");
        button = GetNode<Button>("Button");
    }
    
    private void ConfirmName()
    {
        GM gmRef = (GM)GetNode("/root/Gm");
        gmRef.UpdateHighScore(line.Text);
        Visible = false;
    }

    public override void _Process(double delta)
    {
        if (line.Text == "")
        {
            button.Disabled = true;
        }
        else
        {
            button.Disabled = false;
        }
    }
}
