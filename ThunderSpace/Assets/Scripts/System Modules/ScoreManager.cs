using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : PersistentSingleton<ScoreManager>
{
    int score;
    int curScore;

    public void ResetScore(){
        score = 0;
        curScore = 0;
        ScoreDisplay.UpdateText(score);
    }

    public void AddScore(int scorePoint){
        //active
        curScore += scorePoint;
        StartCoroutine(nameof(AddScoreCoroutine));

        //static
        //score += scorePoint;
        //ScoreDisplay.UpdateText(score);
    }

    //active increase
    IEnumerator AddScoreCoroutine(){
        while(score < curScore){
            score += 1;
            ScoreDisplay.UpdateText(score);

            yield return null;
        }
    }
}
