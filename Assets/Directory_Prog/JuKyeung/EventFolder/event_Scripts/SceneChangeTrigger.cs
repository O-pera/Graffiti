using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneChangeTrigger : MonoBehaviour
{

    public UnityEvent SceenChangeEvent;

    [Header("�� ������Ʈ�� ����� �� �̵��� ��")]
    public string targetSceneName;

    private SceneHandler sceneManager;

    private void Start()
    {
        sceneManager = FindObjectOfType<SceneHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sceneManager.ChangeScene(targetSceneName);

            SceenChangeEvent.Invoke();
        }
    }
}
