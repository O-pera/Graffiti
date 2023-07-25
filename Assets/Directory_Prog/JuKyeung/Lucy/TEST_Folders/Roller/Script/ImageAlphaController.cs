using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageAlphaController : MonoBehaviour, IDragHandler
{
    public float alphaDecreaseSpeed = 0.1f; // ���İ��� ���ҽ�Ű�� �ӵ�
    public float minAlphaPercentage = 0.2f; // ���İ��� 0�� �ȼ��� ������ �� �� �̻��̸� ���� ����
    public int eraseRange = 5; // ���콺 �巡�׷� ���İ��� ������ ����

    private Image image;
    private Texture2D texture;
    private Color32[] originalPixels;
    private bool isGameRunning = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        texture = (Texture2D)image.mainTexture;
        originalPixels = texture.GetPixels32();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isGameRunning)
            return;

        // �巡���� ���콺�� ��ġ���� �ȼ� ��ǥ ���
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            image.rectTransform, eventData.position, null, out Vector2 localPoint);

        // �̹����� �ؽ�ó ���������� �ȼ� ��ǥ ���
        float normalizedX = (localPoint.x + image.rectTransform.rect.width * 0.5f) / image.rectTransform.rect.width;
        float normalizedY = (localPoint.y + image.rectTransform.rect.height * 0.5f) / image.rectTransform.rect.height;
        int pixelX = Mathf.FloorToInt(normalizedX * texture.width);
        int pixelY = Mathf.FloorToInt(normalizedY * texture.height);

        // ���İ��� ������ ���� ���
        int startX = Mathf.Clamp(pixelX - eraseRange, 0, texture.width - 1);
        int endX = Mathf.Clamp(pixelX + eraseRange, 0, texture.width - 1);
        int startY = Mathf.Clamp(pixelY - eraseRange, 0, texture.height - 1);
        int endY = Mathf.Clamp(pixelY + eraseRange, 0, texture.height - 1);

        // ���� ���� ������ ���İ� ����
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                int index = y * texture.width + x;
                Color32 pixel = texture.GetPixel(x, y);
                pixel.a = (byte)Mathf.Max(pixel.a - Mathf.FloorToInt(alphaDecreaseSpeed * 255), 0);
                texture.SetPixel(x, y, pixel);
            }
        }

        texture.Apply();

        // ���İ��� 0�� �ȼ��� ���� �˻�
        float totalPixels = texture.width * texture.height;
        float transparentPixels = 0f;
        foreach (Color32 pixel in texture.GetPixels32())
        {
            if (pixel.a == 0)
                transparentPixels++;
        }

        float alphaPercentage = transparentPixels / totalPixels;
        if (alphaPercentage >= minAlphaPercentage)
        {
            // ���� ���� ���� �߰�
            Debug.Log("Alpha threshold reached. Game over!");
            isGameRunning = false;
        }
    }

    public void ResetAlpha()
    {
        texture.SetPixels32(originalPixels);
        texture.Apply();
        isGameRunning = true;
    }

    private void OnDisable()
    {
        ResetAlpha();
    }
}
