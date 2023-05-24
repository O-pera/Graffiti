using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Events;


public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ���� ������Ʈ
    public Button skipButton; // ��ŵ ��ư
    [SerializeField] float skipDuratioin = 3.0f;
    private bool isPlaying;
    [SerializeField] private GameObject cutSceneObject;

    public VideoClip[] cutScenes; // ���� �ƾ����� �����ϴ� �迭
    private int currentSceneIndex = 0; // ���� �ƾ� �ε���

    public UnityEvent cutSceneFinishEvent; // �ƾ� ���� �� �̺�Ʈ

    private void Start()
    {
        if (cutSceneObject == null)
        {
            Debug.LogError("CutScene ������Ʈ ���Ҵ�");
        }
        else
        {
            cutSceneObject.SetActive(false);
            PlayCutScene(currentSceneIndex); // ù ��° �ƾ� ���

            videoPlayer.loopPointReached += OnCompletionReached;
        }

    }

    void OnCompletionReached(VideoPlayer vp)
    {
        currentSceneIndex++;

    }

    private void Update()
    {
        if (!isPlaying) return;

        if (videoPlayer.isPlaying)
        {
            return;
        }


        if (currentSceneIndex < cutScenes.Length) // �迭 �� �ȳ������� �ƾ� ���� 
        {

            PlayCutScene(currentSceneIndex);
        }
        else // �� �������� ����
        {
            HandleAllCutScenesFinished();
        }

    }

    private void PlayCutScene(int index)
    {
        isPlaying = true;
        cutSceneObject.SetActive(true);
        videoPlayer.clip = cutScenes[index];
        videoPlayer.Play();

        // ���� �ð� �Ŀ� ��ŵ ��ư Ȱ��ȭ
        float duration = skipDuratioin;
        Invoke("EnableSkipButton", duration);


    }

    private void EnableSkipButton()
    {
        skipButton.gameObject.SetActive(true);
        skipButton.interactable = true;
    }

    private void DisableSkipButton()
    {
        skipButton.gameObject.SetActive(false);
        skipButton.interactable = false;
    }

    public void SkipCutScene()
    {
        videoPlayer.Stop();
        skipButton.interactable = false;
        currentSceneIndex++;

        if (currentSceneIndex < cutScenes.Length)
        {
            PlayCutScene(currentSceneIndex);
        }
        else
        {
            // ��� �ƾ� ���� �Ŀ� �̺�Ʈ ó��
            HandleAllCutScenesFinished();
        }
    }

    private void HandleAllCutScenesFinished()
    {
        // �ƾ� ���� �̺�Ʈ ó��
        Debug.Log("�ƾ� ���� �� ����");
        cutSceneFinishEvent.AddListener(DisableSkipButton); // ��ư �ݱ�... 
        cutSceneObject.SetActive(false);
        cutSceneFinishEvent.Invoke();
    }
}
