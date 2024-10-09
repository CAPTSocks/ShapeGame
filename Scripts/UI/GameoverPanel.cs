using Godot;
using System;

public partial class GameoverPanel : Panel
{
    private Panel highScorePanel;
    private Panel QuitPanel;
    private SceneManager sceneManagerAccess;

    public override void _Ready()
    {
        highScorePanel = GetParent().GetNode<Panel>("HighScorePanel");
        QuitPanel = GetParent().GetNode<Panel>("QuitPanel");
        sceneManagerAccess = GetTree().Root.GetNode<SceneManager>("SceneManager");
    }

    private void ReplayButton()
    {
        sceneManagerAccess.RestartLevelQuick();
        
    }

    private void HighscoreButton()
    {
        highScorePanel.Visible = true;
    }

    private void MainMenuButton()
    {
        sceneManagerAccess.RestartLevel();
    }

    private void ExitButton()
    {
        QuitPanel.Visible = true; 
    }

    private void ConfirmButton()
    {

    }

    private void CloseButton()
    {
        
    }
}
