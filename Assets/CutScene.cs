using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Camera cam;
    [SerializeField] GameObject player;

    private float fadeTime = 2f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        Color color = text.color;
        color.a = 0;

        float elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeTime);

            text.color = color;
            yield return null;
        }

        color.a = 1;
        text.color = color;

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeOut());

    }

    IEnumerator FadeOut()
    {
        Color color = text.color;
        color.a = 1;

        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeTime);

            text.color = color;
            yield return null;
        }

        color.a = 0;
        text.color = color;
        yield return new WaitForSeconds(1.0f);

        cam.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
}
