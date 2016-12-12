using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadScoreOnLoad : MonoBehaviour
{
    public float score = 0.0f;
    public UnityEngine.UI.Text scoreForm;
    public UnityEngine.UI.Text scoreRank;

    // Use this for initialization
    void Start ()
    {
        score = PlayerPrefs.GetFloat("score");
        Debug.Log(score);
        scoreForm.text = string.Format("You Scored: {0:0}", (float)score);
        
        if (score < 20000.0f)
        {
            scoreRank.text = string.Format("Your Rank: Pile of Wood", (float)score);
        }
        else if (score < 30000.0f)
        {
            scoreRank.text = string.Format("Your Rank: Floundering Airship", (float)score);
        }
        else if (score < 50000.0f)
        {
            scoreRank.text = string.Format("Your Rank: Gassed up and ready to go", (float)score);
        }
        else if (score < 100000.0f)
        {
            scoreRank.text = string.Format("Your Rank: High Flyer", (float)score);
        }
        else if (score >= 100000.0f)
        {
            scoreRank.text = string.Format("Your Rank: Proud Captain", (float)score);
        }

    }
}
