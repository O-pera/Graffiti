using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public UnityEvent sceneChangeEvent;
    public string targetSceneName;

    private bool isInRange;
    private bool isEventExecuted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && !isEventExecuted)
        {
            // 클릭하거나 'E' 키를 누르면 이벤트 실행
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                ExecuteEvent();
            }
        }
    }

    public void ExecuteEvent()
    {
        StartCoroutine(ExecuteEventCoroutine());
    }

    private IEnumerator ExecuteEventCoroutine()
    {
        isEventExecuted = true;

        sceneChangeEvent.Invoke();

        int eventCount = sceneChangeEvent.GetPersistentEventCount();
        for (int i = 0; i < eventCount; i++)
        {
            string methodName = sceneChangeEvent.GetPersistentMethodName(i);
            MonoBehaviour effectComponent = sceneChangeEvent.GetPersistentTarget(i) as MonoBehaviour;

            if (effectComponent != null && !string.IsNullOrEmpty(methodName))
            {
                yield return StartCoroutine(effectComponent.GetType().GetMethod(methodName).Invoke(effectComponent, null) as IEnumerator);
            }
        }

        yield return new WaitForSeconds(1f); // 예시로 1초 대기

        LoadTargetScene();
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

    // 마우스 클릭으로 씬 전환하기 위한 코드
    /*
    private void Update()
    {
        if (isInRange && !isEventExecuted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ExecuteEvent();
            }
        }
    }
    */
}
