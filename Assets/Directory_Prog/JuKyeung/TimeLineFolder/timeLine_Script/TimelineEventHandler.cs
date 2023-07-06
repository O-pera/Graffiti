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

    private bool isTimelinePlayed = false;


    private void Start()
    {
        // Ÿ�Ӷ��� ��� �Ϸ� �̺�Ʈ ������ ���� �Լ� ����
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Ÿ�Ӷ��� ��� �Ϸ� �̺�Ʈ ȣ��
        timelineFinishedEvent.Invoke();
        Debug.Log(timeline.name + "�� ��ϵ� �̺�Ʈ�� ����");

        // Ÿ�Ӷ��� ��� ���� ����
        isTimelinePlayed = true;
    }

    public void PlayTimeline()
    {
        if (!isTimelinePlayed)
        {
            timeline.Play();
        }
    }
}
