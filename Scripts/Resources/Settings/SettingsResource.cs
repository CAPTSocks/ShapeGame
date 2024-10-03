using Godot;
using System;


[GlobalClass]
public partial class SettingsResource : Resource
{
    [Export] private float masterVolume = 1;
    [Export] private float sfxVolume = 1;
    [Export] private float musicVolume = 1;

    public float MasterVolume { get{return masterVolume;}  }
    public float SfxVolume { get{return sfxVolume;}  }
    public float MusicVolume { get{return musicVolume;} }
   
    public void SaveMasterVolume(float value)
    {
        masterVolume = value;
        ResourceSaver.Save(this, this.ResourcePath);
    }
    public void SaveSfxVolume(float value)
    {
        sfxVolume = value;
        ResourceSaver.Save(this, this.ResourcePath);
    }

    public void SaveMusicVolume(float value)
    {
        musicVolume = value;
        ResourceSaver.Save(this, this.ResourcePath);
    }

}
