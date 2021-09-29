using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text txtScore;
    //public Text txtFinalScore;
    public Text txtLinesCleared;

    //public static int finalScore = 0;

    public static int currentScore = 0;
    public static int linesCleared = 0;

    private void Update()
    {
        txtScore.text = currentScore.ToString();
        txtLinesCleared.text = linesCleared.ToString();
        //txtFinalScore.text = finalScore.ToString();
    }
}