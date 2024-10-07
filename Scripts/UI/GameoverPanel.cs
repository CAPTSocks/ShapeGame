using Godot;
using System;

public partial class GameoverPanel : Panel
{
    private Panel highScorePanel;

    public override void _Ready()
    {
        highScorePanel = GetNode<Panel>("HighScorePanel");
    }

    private void ReplayButton()
    {

    }

    private void HighscoreButton()
    {
        highScorePanel.Visible = true;
    }

    private void MainMenuButton()
    {

    }

    private void ExitButton()
    {

    }

    private void ConfirmButton()
    {

    }

    private void CloseButton()
    {
        
    }
}
