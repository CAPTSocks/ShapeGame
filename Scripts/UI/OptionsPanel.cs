using Godot;
using System;
using System.Runtime.Serialization;
using System.Xml.Schema;

public partial class OptionsPanel : Panel
{

    private Slider masterSlider, sfxSlider, musicSlider;
    private RichTextLabel masterText, sfxText, musicText;
    [Export] private SettingsResource settingsResource;

    public override void _Ready()
    {
        masterSlider = GetNode<Slider>("VBoxContainer/MasterVolumeSlider");
        sfxSlider = GetNode<Slider>("VBoxContainer/SFXVolumeSlider");
        musicSlider = GetNode<Slider>("VBoxContainer/MusicSlider");
        masterText = GetNode<RichTextLabel>("MasterValue");
        sfxText = GetNode<RichTextLabel>("SFXValue");
        musicText = GetNode<RichTextLabel>("MusicValue");        

        if (settingsResource != null)
        {
            masterSlider.Value = settingsResource.MasterVolume;
            masterText.Text = "[center]" + Mathf.Round(100 * settingsResource.MasterVolume).ToString();
            sfxSlider.Value = settingsResource.SfxVolume;
            sfxText.Text = "[center]" + Mathf.Round(100 * settingsResource.SfxVolume).ToString();
            musicSlider.Value = settingsResource.MusicVolume;
            musicText.Text = "[center]" + Mathf.Round(100 * settingsResource.MusicVolume).ToString();
        }
    }   

    private void ChangeMasterValue(float value)
    {
        masterText.Text = "[center]" + Mathf.Round(value * 100).ToString(); 
        settingsResource.SaveMasterVolume(value);
    }

    private void ChangesfxValue(float value)
    {
        sfxText.Text = "[center]" + Mathf.Round(value * 100).ToString();
        settingsResource.SaveSfxVolume(value);
    }

    private void ChangeMusicValue(float value)
    {
        musicText.Text = "[center]" + Mathf.Round(value * 100).ToString();
        settingsResource.SaveMusicVolume(value);
    }

    private void CloseButtonPressed()
    {
        Visible = false;
    }
}
