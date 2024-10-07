using Godot;
using Godot.Collections;

[GlobalClass]
public partial class HighScoreResource : Resource
{
    [Export] public Array<ScoreObject> highScores;
    [Export] public bool resetScores = false;

    public void ResetScores()
    {
        foreach (var score in highScores)
        {
            score.name = "[right]" + "ABC";
            score.score = 0; 
        }
        resetScores = false;
        ResourceSaver.Save(this, this.ResourcePath);
    }

    public void UpdateScore(int index, int newScore, string newName)
    {
        ScoreHolder scoreObject = new ScoreHolder();
        if (index < 9)
        {
            scoreObject.name = "[right]" + newName;
            scoreObject.score = newScore;

           // highScores[index].name = "[right]" + newName;
           // highScores[index].score = newScore;
            
            
            for (int i = index; i < highScores.Count - 1; i++)
            {
                ScoreHolder scoreObject2 = new ScoreHolder();
                
                scoreObject2.name = highScores[i].name;
                scoreObject2.score = highScores[i].score;

                highScores[i].name = scoreObject.name;
                highScores[i].score = scoreObject.score;

                scoreObject.name = scoreObject2.name;
                scoreObject.score = scoreObject2.score;

            }
            ResourceSaver.Save(this, this.ResourcePath);
        }
        else
        {
            highScores[index].name = "[right]" + newName;
            highScores[index].score = newScore;
            ResourceSaver.Save(this, this.ResourcePath);
        }

        
    }
}


