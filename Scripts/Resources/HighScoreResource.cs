using Godot;
using Godot.Collections;

[GlobalClass]
public partial class HighScoreResource : Resource
{
    [Export] public Array<ScoreObject> highScores;

    public void UpdateScore(int index, int newScore, string newName)
    {
        highScores[index].name = "[right]" + newName;
        highScores[index].score = newScore;
        ResourceSaver.Save(this, this.ResourcePath);
    }
}
