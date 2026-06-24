using System.Collections;
using UnityEngine;

public class LocationPopupUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;
    public float showDuration = 2f;

    private void OnEnable()
    {
        StartCoroutine(PopupRoutine());
    }

    IEnumerator PopupRoutine()
    {
        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(showDuration);

        yield return StartCoroutine(Fade(1f, 0f));

        gameObject.SetActive(false);
    }

    IEnumerator Fade(float from, float to)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}