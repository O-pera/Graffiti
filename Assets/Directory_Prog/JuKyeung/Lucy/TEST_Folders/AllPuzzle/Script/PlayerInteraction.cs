using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int targerLayer; // 16 �� ���̾�
     
    private HavePicture havePicture;
    private noteBook _noteBook;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targerLayer)
        {
            if (other.TryGetComponent(out havePicture)) // ��ȣ�ۿ��� ��� ������Ʈ�� 16�� ���̾��� ��� HavePicture ��ũ��Ʈ�� ������ 
            {
                // �����ͼ� ? 
            }
        }
    }
}
