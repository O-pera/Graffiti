using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �� ���� ���� ��ġ�� ���� �̹��� ������ ���� ���� ��� ������� ������� 
/// </summary>
public class PeopleFadeOut : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 1f; // �̹����� ������ ������� �� �ɸ��� �ð�

    public Image[] image;
    private float currentFadeTime;
    private bool fadingOut;

    private int currentIndex;

    public void EventCheck()
    {
        Debug.Log("�̺�Ʈ üũ" );
        Color color = image[currentIndex].color;

        for(int i =0; i <image.Length; i++)
        {

            color.a = 0f;
        }
    }
}
