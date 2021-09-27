using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text textScore;

    public static int currentScore = 0;
    

    // Update is called once per frame
    void Update()
    {
        textScore.text = currentScore.ToString();
    }
}
