using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent CollisionStartEvent;


    [Header("�̵���ų ������Ʈ(���� ��κ� �÷��̾�) �� ���� ��")]
    public Transform targetObject;
    public Vector3 newPosition;
    public Quaternion newRotation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            CollisionStartEvent.Invoke();

        }
    }

    public void MoveObject()
    {
        targetObject.position = newPosition;
        targetObject.rotation = newRotation;
    }


}
