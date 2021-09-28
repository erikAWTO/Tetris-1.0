using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text textScore;

    public static int currentScore = 0;

    private void Update()
    {
        textScore.text = currentScore.ToString();
    }
}