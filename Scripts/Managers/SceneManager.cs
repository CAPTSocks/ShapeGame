using Godot;
using System;

public partial class SceneManager : Node2D
{
    private Node levelInstance;
    private string storedLevelName;
    private Sprite2D screenCover;
    private Vector2 screenCoverStartPos; 
    [Export] private float screenCoverSpeed = 0.5f;

    public override void _Ready()
    {
        levelInstance = GetTree().CurrentScene;
        screenCover = GetNode<Sprite2D>("ScreenCover");
        screenCover.Position -= new Vector2(screenCover.Position.X , GetViewportRect().Size.Y);
        screenCoverStartPos = screenCover.Position;
    }

    public void HandleLevelChange(String levelName)
    {
        storedLevelName = null;
        storedLevelName = levelName;
        PlayDropDown();
    }

    private void LoadLevel()
    {
        UnloadLevel();
        string levelPath = "res://Scenes/LevelScenes/" + storedLevelName + ".tscn";
        PackedScene packedLevel = GD.Load<PackedScene>(levelPath);
        if (levelPath != null)
        {
            levelInstance = packedLevel.Instantiate();
            GetTree().Root.AddChild(levelInstance);
            GetTree().CurrentScene = levelInstance;
        }
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(screenCover, "position", screenCoverStartPos, screenCoverSpeed);
    }

    private void UnloadLevel()
    {
        if (IsInstanceValid(levelInstance))
        {
            levelInstance.QueueFree();
        }
        levelInstance = null;
    }

    private void PlayDropDown()
    {
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(screenCover, "position", Vector2.Zero, screenCoverSpeed);
        tween.Finished += LoadLevel;
    }

    public void RestartLevel()
    {
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(screenCover, "position", Vector2.Zero, screenCoverSpeed);
        tween.Finished += LiftScreenCover;
    }

    private void LiftScreenCover()
    {
        GetTree().ReloadCurrentScene();
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(screenCover, "position", screenCoverStartPos, screenCoverSpeed);
    }

}
