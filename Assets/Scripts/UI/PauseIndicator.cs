using UnityEngine;
using UnityEngine.UI;

public class PauseIndicator : MonoBehaviour
{
    public float delayTime = 5f;
    public float time = 2f;

    private RawImage fadeImage;
    private float alphaValue;
    private float fadeRate;

    private void Start()
    {
        fadeImage = GetComponent<RawImage>();

        if (time <= 0) time = 0.1f;

        fadeRate = 1f / time;

        if (fadeImage != null)
        {
            alphaValue = fadeImage.color.a;
        }
    }

    private void Update()
    {
        if (fadeImage == null) return;

        if (delayTime > 0)
        {
            delayTime -= Time.unscaledDeltaTime;
        }
        else
        {
            if (time > 0)
            {
                alphaValue -= fadeRate * Time.unscaledDeltaTime;
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alphaValue);
                time -= Time.unscaledDeltaTime;
            }
            else if (time <= 0 && fadeImage.color.a > 0)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
            }
        }
    }
}