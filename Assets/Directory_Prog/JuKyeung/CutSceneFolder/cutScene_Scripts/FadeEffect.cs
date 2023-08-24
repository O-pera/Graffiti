using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image image;
    public UnityEvent fadeEndEvent;

    private void OnEnable()
    {
        StartCoroutine(FadeCorutine());

    }
    IEnumerator FadeCorutine()
    {
        Color imageColor = image.color;
        imageColor.a = 1.0f;
        image.color = imageColor;

        float fadeDuration = 1.0f; // ���̵� �ο� �ɸ��� �ð�
        float fadeStartTime = Time.time;

        while (imageColor.a > 0)
        {
            float elapsed = Time.time - fadeStartTime;
            float normalizedTime = Mathf.Clamp01(elapsed / fadeDuration);
            imageColor.a = 1.0f - normalizedTime;
            image.color = imageColor;

            yield return null;
        }

        // ���̵� ���� ���� �� ��� �̺�Ʈ ȣ��
        fadeEndEvent.Invoke();
    }
}

