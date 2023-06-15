using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent CollisionStartEvent;

    [Header("�̵���ų ������Ʈ(���� ��κ� �÷��̾�) �� ���� ��")]
    public Transform targetObject;
    public Vector3 localOffset;  // �θ� ������Ʈ�κ����� ������� ������
    public Quaternion localRotationOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollisionStartEvent.Invoke();
        }
    }

    public void MoveObject()
    {
        // �θ� ������Ʈ�� ���� ��ǥ�踦 �������� ��ġ ����
        targetObject.localPosition = localOffset;
        targetObject.localRotation = localRotationOffset;  // �θ� ������Ʈ�� ȸ���� ������� �ʵ��� �׻� �ʱ� ȸ������ �Ҵ�
    }


}
