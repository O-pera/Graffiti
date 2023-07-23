using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpriteEraser : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Image spriteImage; // ��������Ʈ �̹���
    public Color eraserColor = new Color(1f, 1f, 1f, 0f); // ���찳�� �������� �κ��� ���� (����)
    public float eraserSize = 50f; // ���찳�� ũ��

    private Texture2D originalTexture; // ������ �ؽ�ó ����
    private Sprite originalSprite; // ������ ��������Ʈ ����

    private void Awake()
    {
        // ���� ���� �� ������ �ؽ�ó�� ��������Ʈ�� ����
        originalTexture = Instantiate(spriteImage.sprite.texture);
        originalSprite = spriteImage.sprite;
    }

    private void OnDestroy()
    {
        // ���� ���� �� ������ �ؽ�ó�� ��������Ʈ�� ����
        spriteImage.sprite = originalSprite;
        Destroy(originalTexture);
    }

    private void EraseSprite(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(spriteImage.rectTransform, position, null, out Vector2 localPos);

        // ��������Ʈ�� ���� ���� ���
        float normalizedX = (localPos.x + spriteImage.rectTransform.rect.width / 2) / spriteImage.rectTransform.rect.width;
        float normalizedY = (localPos.y + spriteImage.rectTransform.rect.height / 2) / spriteImage.rectTransform.rect.height;
        float offsetX = normalizedX * spriteImage.sprite.texture.width;
        float offsetY = normalizedY * spriteImage.sprite.texture.height;
        float halfSize = eraserSize / 2f;
        Rect eraserRect = new Rect(offsetX - halfSize, offsetY - halfSize, eraserSize, eraserSize);

        // ��������Ʈ �̹����� Texture2D ��������
        Texture2D spriteTexture = spriteImage.sprite.texture;

        // ����� ������ �ؽ�ó�� ������ �ؽ�ó�� ����
        spriteTexture.SetPixels(originalTexture.GetPixels());
        spriteTexture.Apply();

        // �ȼ� ���� ����
        Color[] colors = spriteTexture.GetPixels();
        for (int x = 0; x < spriteTexture.width; x++)
        {
            for (int y = 0; y < spriteTexture.height; y++)
            {
                if (eraserRect.Contains(new Vector2(x, y)))
                {
                    float distanceX = Mathf.Abs(x - offsetX);
                    float distanceY = Mathf.Abs(y - offsetY);
                    float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
                    float alpha = Mathf.Clamp01(1f - (distance / halfSize));
                    Color color = colors[y * spriteTexture.width + x];
                    colors[y * spriteTexture.width + x] = Color.Lerp(color, eraserColor, alpha);
                }
            }
        }

        // ����� �ȼ� ������ Texture2D�� ����
        spriteTexture.SetPixels(colors);
        spriteTexture.Apply();

        // ��������Ʈ �̹��� ������Ʈ
        spriteImage.sprite = Sprite.Create(spriteTexture, spriteImage.sprite.rect, spriteImage.sprite.pivot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EraseSprite(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        EraseSprite(eventData.position);
    }
}
