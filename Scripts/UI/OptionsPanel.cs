using Godot;
using System;
using System.Xml.Schema;

public partial class OptionsPanel : Panel
{

    private Slider masterSlider, sfxSlider, musicSlider;
    private RichTextLabel masterText, sfxText, musicText;

    public override void _Ready()
    {
        masterSlider = GetNode<Slider>("VBoxContainer/MasterVolumeSlider");
        sfxSlider = GetNode<Slider>("VBoxContainer/SFXVolumeSlider");
        musicSlider = GetNode<Slider>("VBoxContainer/MusicSlider");
        masterText = GetNode<RichTextLabel>("MasterValue");
        sfxText = GetNode<RichTextLabel>("SFXValue");
        musicText = GetNode<RichTextLabel>("MusicValue");        
    }   

    private void ChangeMasterValue(float value)
    {
        masterText.Text = "[center]" + Mathf.Round(value * 100).ToString(); 
    }

    private void ChangesfxValue(float value)
    {
        sfxText.Text = "[center]" + Mathf.Round(value * 100).ToString();
    }

    private void ChangeMusicValue(float value)
    {
        musicText.Text = "[center]" + Mathf.Round(value * 100).ToString();
    }

    private void CloseButtonPressed()
    {
        Visible = false;
    }
}
