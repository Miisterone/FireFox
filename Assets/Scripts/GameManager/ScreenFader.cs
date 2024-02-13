using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeDuration = 2.0f;

    void Start()
    {
        // Commencer par un écran complètement transparent
        SetAlpha(0f);
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f)); // Du complètement opaque au transparent
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f)); // Du transparent au complètement opaque
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            SetAlpha(alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(endAlpha);
    }

    void SetAlpha(float alpha)
    {
        if (fadeMaterial != null)
        {
            Color color = fadeMaterial.color;
            color.a = alpha;
            fadeMaterial.color = color;
        }
    }
}