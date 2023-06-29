using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public Image tutorialImage;
    public Sprite[] tutorialSprites;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private int currentTutorialIndex = 0;
    private int index = 0;
    [SerializeField] private bool isUIActive = true;

    [System.Serializable]
    public class TutorialCompletedEvent : UnityEvent { }
    public TutorialCompletedEvent onTutorialCompleted;

    public PlayerMove_SIDE playerMoveSide;

    [SerializeField] private bool isGraffitiActive = false;

    private void OnEnable()
    {
        currentTutorialIndex = 0;

        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();

        if (tutorialSprites.Length <= 1) // ���� �� ��������Ʈ ������ 1���� �۰ų� ������ �ƹ��� ��ư�� Active ���� ���� 
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
        }
        else // �װ� �ƴ϶�� �ϴ� 
        {
            prevButton.gameObject.SetActive(currentTutorialIndex > 0); // currentTutorialIndex ��ȣ�� 0���� ū ���¶�� prevButton �� Ȱ��ȭ 

            exitButton.gameObject.SetActive(false);

        }

        isUIActive = true;

    }


    private void Start()
    {
        DisplayTutorialImage(currentTutorialIndex); // ���÷��� �̹��� �ʱ�ȭ

        isGraffitiActive = false;

        playerMoveSide = FindObjectOfType<PlayerMove_SIDE>();
    }

    private void Update()
    {
        if (isUIActive)
        {
            if (currentTutorialIndex > 0)
            {
                prevButton.gameObject.SetActive(true);
            }

            else
            {
                prevButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);
            }

            playerMoveSide.enabled = false;

        }
    }

    private void UpdateButtonInteractivity()
    {
        prevButton.interactable = currentTutorialIndex > 0;
        nextButton.interactable = currentTutorialIndex < tutorialSprites.Length - 1;
    }

    private void ShowNextTutorialImage() // ���� �̹��� 
    {
        currentTutorialIndex++;
        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();

        if (currentTutorialIndex >= tutorialSprites.Length - 1)
        {
            exitButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
    }

    private void ShowPreviousTutorialImage() // �ڷ� �̹��� 
    {
        currentTutorialIndex--;
        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();
        exitButton.gameObject.SetActive(false);
    }

    private void DisplayTutorialImage(int index)
    {
        tutorialImage.sprite = tutorialSprites[index];
    }

    public void OnNextButtonClicked()
    {
        ShowNextTutorialImage();
    }

    public void OnPrevButtonClicked() // ��°�� �۵��� ������? 
    {
        ShowPreviousTutorialImage();
    }

    public void OnExitButtonClicked()
    {
        // Ʃ�丮�� �Ϸ� �̺�Ʈ ȣ��
        onTutorialCompleted?.Invoke();

        gameObject.SetActive(false);

        isUIActive = false;

        if (isGraffitiActive == true) // �׷���Ƽ ���¶���� 
        {
            playerMoveSide.enabled = false;
            isGraffitiActive = false;
        }
        else
        {
            playerMoveSide.enabled = true;
        }

    }

    public void SetGraffitiActive(bool active)
    {
        isGraffitiActive = active;
    }

    //private void OnGraffitiButtonClicked()
    //{
    //    SetGraffitiActive(true);
    //}

}