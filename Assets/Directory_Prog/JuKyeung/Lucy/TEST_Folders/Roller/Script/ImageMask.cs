using UnityEngine;
using UnityEngine.Events;

public class ImageMask : MonoBehaviour
{
    public Texture2D maskTexture;  // ����ũ�� ����� �̹��� �ؽ�ó
    public UnityEvent onStartMasking;  // ����ŷ ���۽� ȣ���� �̺�Ʈ
    public UnityEvent onEndMasking;  // ����ŷ ����� ȣ���� �̺�Ʈ

    private Material maskMaterial;  // ����ũ�� ������ ����
    private RenderTexture maskRenderTexture;  // ����ũ�� �׸� ���� �ؽ�ó
    private bool isMasking = false;  // ����ŷ ������ ����
    private Vector2 previousMousePosition;  // ���� ���콺 ��ġ

    private void Start()
    {
        // ����ũ�� ������ ���� ����
        maskMaterial = new Material(Shader.Find("Unlit/Transparent"));

        // ����ũ �ؽ�ó�� ������ �Ҵ�
        maskMaterial.SetTexture("_MainTex", maskTexture);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ����ŷ ����
            StartMasking();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ����ŷ ����
            EndMasking();
        }

        // ����ŷ ���� ��, ���콺 �������� �����Ͽ� ����ũ�� ������Ʈ�մϴ�.
        if (isMasking)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            UpdateMask(currentMousePosition);
            previousMousePosition = currentMousePosition;
        }
    }

    private void StartMasking()
    {
        if (!isMasking)
        {
            isMasking = true;
            onStartMasking.Invoke();
            maskRenderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            maskRenderTexture.Create();
            maskMaterial.SetTexture("_MaskTex", maskRenderTexture);
            Graphics.Blit(maskTexture, maskRenderTexture, maskMaterial);
            previousMousePosition = Input.mousePosition;
        }
    }

    private void EndMasking()
    {
        if (isMasking)
        {
            isMasking = false;
            onEndMasking.Invoke();
            Destroy(maskRenderTexture);
            maskMaterial.SetTexture("_MaskTex", null);
        }
    }

    private void UpdateMask(Vector2 currentMousePosition)
    {
        Vector2 deltaMousePosition = currentMousePosition - previousMousePosition;
        Vector2 normalizedMousePosition = new Vector2(currentMousePosition.x / Screen.width, currentMousePosition.y / Screen.height);
        maskMaterial.SetVector("_MaskPos", normalizedMousePosition);
        maskMaterial.SetVector("_MaskSize", deltaMousePosition);
        Graphics.Blit(maskTexture, maskRenderTexture, maskMaterial);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
    }
}
