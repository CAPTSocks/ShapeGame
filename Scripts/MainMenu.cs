using Godot;
using System;

public partial class MainMenu : Control
{
    private void PlayButtonPressed()
    {
        var sceneManager = (SceneManager)GetNode("/root/SceneManager");
        sceneManager.HandleLevelChange("main");
        
        // var newlevel = GD.Load<PackedScene>("res://Scenes/LevelScenes/main.tscn").Instantiate();
        // GetParent().AddChild(newlevel);
        // GetTree().CurrentScene = newlevel;
        // QueueFree();
    }

    private void OptionsButtonPressed()
    {
        GD.Print("Open options");
    }

    private void QuitButtonPressed()
    {
        GetTree().Quit(); 
    }

    private void NoButtonPressed()
    {
        GD.Print("Close quit pop up");
    }
    
    private void YesButtonPressed()
    {
        GD.Print("Close game");
    }
}
