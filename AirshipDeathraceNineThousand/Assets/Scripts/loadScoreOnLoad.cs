using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadScoreOnLoad : MonoBehaviour
{
    public float score = 0.0f;
    public UnityEngine.UI.Text scoreForm;

    // Use this for initialization
    void Start ()
    {
        score = PlayerPrefs.GetFloat("score");
        Debug.Log(score);
        scoreForm.text = string.Format("You Scored: {0:0}", (float)score);
    }
}
