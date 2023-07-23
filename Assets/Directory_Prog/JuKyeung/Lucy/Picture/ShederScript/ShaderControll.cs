using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShaderControll : MonoBehaviour
{
    public GameObject targetObj;

    private Renderer Objrenderer;

    [Header("�����ų ���̴� ���׸���")]
    public Material blurMaterial;
    public Material unblurMaterial;

    [Header("���� ����")]
    [SerializeField] private float nowBlurIntensity; // ���� �� ����
    public float min_blurAmount = 0.0f;
    public float max_blurAmount = 5.0f;

    public void Awake()
    {
        nowBlurIntensity = max_blurAmount;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        blurMaterial.SetFloat("_Radius", nowBlurIntensity);
    }

}
