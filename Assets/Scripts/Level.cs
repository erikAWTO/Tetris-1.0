using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text txtFallTime;
    public Slider slider;

    public static float fallTime = 0.8f;

    public void Update()
    {
        slider.value = fallTime;
    }

    public void UpdateScore(float value)
    {
        fallTime = value;
        slider.value = fallTime;
        txtFallTime.text = fallTime.ToString();
    }
}