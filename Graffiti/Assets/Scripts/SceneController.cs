using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Singleton
    private SceneController _instance = null;
    public SceneController Instance { get { return _instance; } }

    #endregion

    #region Local Variables
    private WaitForSeconds m_sceneLoadingWait = null;
    [SerializeField] private float m_loadWaitInterval = 0.1f;
    [SerializeField] private float m_FakeLoadingThreshold = 4.0f;
                     private float m_SceneLoadindgProgress = 0.0f;

    #endregion

    #region Properties
    /// <summary>
    /// �ܺ� ������Ʈ�� �� �ε� �Ŵ����κ��� �ε� ���൵�� �ޱ� ���� Property
    /// </summary>
    public float SceneLoadingProgress { 
        get => m_SceneLoadindgProgress; 
    }

    #endregion

    private void Awake() {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);

        m_sceneLoadingWait = new WaitForSeconds(m_loadWaitInterval);
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            ChangeSceneTo(1);
        }
    }

    /// <summary>
    /// ���� �� �ε� ��û�� �޴� �Լ�
    /// </summary>
    /// <param name="sceneIndex">Next Scene's Build Index to load</param>
    public void ChangeSceneTo(int sceneIndex) {
        //TODO: ���� ���̵���/�ƿ� �ҰŸ� ���⼭ Completed�� �ڷ�ƾ �־��ְ� ���̵� ��/�ƿ� ȣ��
        StartCoroutine(CoStartLoading(sceneIndex));
    }

    /// <summary>
    /// ���� ���� �񵿱������� �ε��ϱ� ���� Coroutine�Լ�
    /// </summary>
    /// <param name="sceneIndex">Next Scene's Build Index to load</param>
    private IEnumerator CoStartLoading(int sceneIndex) {
        float fakeLoading = 0.0f;
        bool isDone = false;

        AsyncOperation loadTask = SceneManager.LoadSceneAsync("TestScene2");
        loadTask.allowSceneActivation = false;

        while(true) {
            fakeLoading += m_loadWaitInterval;
            m_SceneLoadindgProgress = loadTask.progress;

            isDone = ( fakeLoading >= m_FakeLoadingThreshold ) &&
                     ( loadTask.progress >= 0.9f );

            if(isDone) break;
            yield return m_sceneLoadingWait;
        }

        loadTask.allowSceneActivation = true;
        m_SceneLoadindgProgress = 0.0f;
        //TODO: ���� ���̵���/�ƿ� �ҰŸ� ���⼭ ���̵� �ƿ� ����

        yield break;
    }
}
