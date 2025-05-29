using Godot;
using System;

public partial class MainMenu : Control
{
	private Control quitPanel;
	private Control screenCover;
	private Control optionsPanel;
	private Control highScorePanel;


	public override void _Ready()
	{
		screenCover = GetNode<Control>("ScreenCoverPanel");
		quitPanel = GetParent().GetNode<Control>("QuitPanel");
		optionsPanel = GetNode<Control>("OptionsPanel");
		highScorePanel = GetParent().GetNode<Control>("HighScorePanel");
		screenCover.Visible = false;
		quitPanel.Visible = false;
		optionsPanel.Visible = false;
		highScorePanel.Visible = false;
	}

	private void PlayButtonPressed()
	{
		//var sceneManager = (SceneManager)GetNode("/root/SceneManager");
		//sceneManager.HandleLevelChange("main");
		var gmRef = (GM)GetNode("/root/Gm");
		gmRef.StartTheGame();
		Visible = false;
	}

	private void HighScoreButtonPressed()
	{
		highScorePanel.Visible = true; 
	}

	private void OptionsButtonPressed()
	{
	   optionsPanel.Visible = true; 
	}

	private void QuitButtonPressed()
	{
		screenCover.Visible = true; 
		quitPanel.Visible = true;
	}

	private void NoButtonPressed()
	{
		screenCover.Visible = false; 
		quitPanel.Visible = false;
	}
	
	private void YesButtonPressed()
	{
		GetTree().Quit();
	}
}
