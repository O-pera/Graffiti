using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public delegate void PuzzleEventHandle();

    [Header("�̺�Ʈ ������ ��")]
    public UnityEvent StartPuzzleEvent; 
    [Header("�̺�Ʈ �ϴ� ��")]
    public UnityEvent InProgressPuzzleEvent;
    [Header("�̺�Ʈ�� ������")]
    public UnityEvent EndPuzzleEvent;


    [Header("������Ʈ��")]
    [SerializeField] private GameObject example_pictureObject;
    private void Start()
    {
        Transform objects = transform.Find("ExamplePicture");

        if(objects != null )
        {
            example_pictureObject = objects.gameObject;
        }
    }

    private void StartEventSignal() { Debug.Log("�̺�Ʈ ���� ��ȣ ����"); }

    // �̺�Ʈ ���� �� ȣ��Ǵ� �Լ�
    public void StartEvent()
    {
        Debug.Log("�̺�Ʈ�� ���۵Ǿ����ϴ�.");
        example_pictureObject.gameObject.SetActive(true);

        StartPuzzleEvent.Invoke();
         
        // �̺�Ʈ ���� ���� ��Ÿ���� ��ȣ ������
        EventInProgress();
    }
    


    // �̺�Ʈ ���� ���� ��Ÿ���� ��ȣ�� ������ �Լ�
    public void EventInProgress()
    {
        Debug.Log("�̺�Ʈ�� ���� ���Դϴ�.");
        InProgressPuzzleEvent.Invoke();

        // �̺�Ʈ�� �������� �˸��� �Լ� ȣ��
        EndEvent();
    }

    // �̺�Ʈ ���� �� ȣ��Ǵ� �Լ�
    public void EndEvent()
    {
        Debug.Log("�̺�Ʈ�� ����Ǿ����ϴ�.");

        // ���� �Ŀ� ��ȣ�� ������ �Լ� ȣ��
        SignalAfterEvent();
    }

    // �̺�Ʈ ���� �Ŀ� ��ȣ�� ������ �Լ�
    private void SignalAfterEvent()
    {
        EndPuzzleEvent.Invoke();
        Debug.Log("�̺�Ʈ ���� �� ��ȣ�� ���½��ϴ�.");
    }
}
