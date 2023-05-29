using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private bool isChanging = false; // ��ȯ���ΰ�
    private string targetSceneName; // ��ȯ �� �̸�


    public void ChangeScene(string _sceneName)
    {
        if (isChanging)
        {
            Debug.Log("�� ��ȯ����");
            return;
        }

        targetSceneName = _sceneName;
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        isChanging = true;

        yield return SceneManager.LoadSceneAsync(targetSceneName);

        isChanging = false;
    }
}
