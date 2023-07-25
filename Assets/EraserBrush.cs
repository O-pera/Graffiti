using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EraserBrush : MonoBehaviour, IPointerClickHandler
{
    public RawImage rawImage; // RawImage�� ����մϴ�.
    public float eraserBrushSize = 40f;
    private bool isImageErased = false;

    private Texture2D originalTexture; // ���� �ؽ�ó�� �����ϴ� ����

    private void Awake()
    {
        if (rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
        }
    }

    // ���� �ؽ�ó�� �����Ͽ� �۾� �ؽ�ó�� �ʱ�ȭ�մϴ�.
    private void InitTexture()
    {
        originalTexture = (Texture2D)rawImage.texture;
        Texture2D workingTexture = new Texture2D(originalTexture.width, originalTexture.height);
        workingTexture.SetPixels(originalTexture.GetPixels());
        workingTexture.Apply();

        rawImage.texture = workingTexture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Eraser(eventData.position);
    }

    private void Eraser(Vector2 clickPosition)
    {
        if (isImageErased)
            return;

        // �۾� �ؽ�ó�� �ʱ�ȭ�մϴ�.
        if (originalTexture == null || rawImage.texture == null)
            InitTexture();

        Texture2D workingTexture = (Texture2D)rawImage.texture;

        // Ŭ���� ������ UV ��ǥ�� ��ȯ�մϴ�.
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, clickPosition, null, out localPoint);
        Vector2 normalizedPoint = new Vector2((localPoint.x + rawImage.rectTransform.pivot.x * rawImage.rectTransform.rect.width) / rawImage.rectTransform.rect.width,
                                               (localPoint.y + rawImage.rectTransform.pivot.y * rawImage.rectTransform.rect.height) / rawImage.rectTransform.rect.height);

        // Ŭ���� ������ �߽����� ���찳 ũ�⸦ �����մϴ�.
        int centerX = Mathf.FloorToInt(normalizedPoint.x * workingTexture.width);
        int centerY = Mathf.FloorToInt(normalizedPoint.y * workingTexture.height);

        int halfEraserSize = Mathf.FloorToInt(eraserBrushSize / 2f);

        // �ȼ��� ��ȸ�ϸ鼭 Ŭ���� ������ ���찳 ũ�� ���� �ȼ��� �����ϰ� ����ϴ�.
        for (int y = centerY - halfEraserSize; y < centerY + halfEraserSize; y++)
        {
            for (int x = centerX - halfEraserSize; x < centerX + halfEraserSize; x++)
            {
                if (x >= 0 && x < workingTexture.width && y >= 0 && y < workingTexture.height)
                {
                    workingTexture.SetPixel(x, y, Color.clear);
                }
            }
        }

        workingTexture.Apply();

        isImageErased = true;
    }
}
