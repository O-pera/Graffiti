using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Events;


public class TimelineEventHandler : MonoBehaviour
{
    public PlayableDirector timeline;
    public UnityEvent timelineFinishedEvent;
    public GameObject targetObject;

    private void Start()
    {
        // Ÿ�Ӷ��� ��� �Ϸ� �̺�Ʈ ������ ���� �Լ� ����
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Ÿ�Ӷ��� ��� �Ϸ� �̺�Ʈ ȣ��
        timelineFinishedEvent.Invoke();

        // Ÿ�Ӷ����� ���� ���� ���� ��ġ�� ������Ʈ�� ��ġ ����
        Vector3 currentPosition = targetObject.transform.position;
        targetObject.transform.position = currentPosition;
    }

    public void SetPositionObject(GameObject objectToMove, Vector3 newPosition)
    {
        // ������Ʈ�� ��ġ ����
        objectToMove.transform.position = newPosition;
    }

    public void SetTargetObject(GameObject _targetObject)
    {
        targetObject = _targetObject;
    }
}
