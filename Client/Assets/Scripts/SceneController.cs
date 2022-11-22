using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private SceneController _instance = null;
    public SceneController Instance { get { return _instance; } }

    [Tooltip("DontDestroyObjects' or static Objects' Operations")]
    public Action<bool> PostLoadingAction = null;

    private WaitForSeconds m_sceneLoadingWait = new WaitForSeconds(0.1f);
    private float m_FakeLoadingThreshold = 4.0f;
    private float m_SceneLoadindgProgress = 0.0f;
    public float SceneLoadingProgress { get => m_SceneLoadindgProgress; }

    private void Awake() {
        if(_instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        _instance = this;
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            ChangeSceneTo(1);
        }
    }

    public void ChangeSceneTo(int sceneIndex) {
        //TODO: ���� ���̵���/�ƿ� �ҰŸ� ���⼭ Completed�� �ڷ�ƾ �־��ְ� ���̵� ��/�ƿ� ȣ��
        StartCoroutine(CoStartLoading(sceneIndex));
    }

    private IEnumerator CoStartLoading(int sceneIndex) {
        float fakeLoading = 0.0f;
        bool isDone = false;

        AsyncOperation loadSceneAsyncOper = SceneManager.LoadSceneAsync("TestScene2");
        loadSceneAsyncOper.allowSceneActivation = false;

        while(isDone == false) {
            fakeLoading += 0.1f;
            m_SceneLoadindgProgress = loadSceneAsyncOper.progress;

            isDone = ( fakeLoading >= m_FakeLoadingThreshold ) &&
                     ( loadSceneAsyncOper.progress >= 0.9f );

            yield return m_sceneLoadingWait;
        }

        loadSceneAsyncOper.allowSceneActivation = true;
        m_SceneLoadindgProgress = 0.0f;
        //TODO: ���� ���̵���/�ƿ� �ҰŸ� ���⼭ ���̵� �ƿ� ����

        yield break;
    }
}
