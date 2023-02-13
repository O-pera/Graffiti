using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graffiti.Dialogue;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using DG.DemiLib;

namespace Graffiti.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI scriptText;
        TextMeshPro textMeshPro;
        [SerializeField] Button nextButton;
        [SerializeField] Button dialogueExitButton;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);
            scriptText.text = playerConversant.GetText();
            UpdateUI();


        }

        private void Update()
        {
            NextInputKey();
        }

        /// <summary>
        /// �����̽� �ٷ� �ؽ�Ʈ �ѱ� �� �ְ� Ű �߰� 
        /// </summary>
        void NextInputKey()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Next();
            }
        }

        void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }
        void UpdateUI()
        {
            scriptText.text = playerConversant.GetText();

            TMProUGUIDoText.DoText(scriptText, /*TextLenght(scriptText.text.ToString())*/ 250f);
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }

        /// <summary>
        /// richText(���̾�α��� ���� �ڵ� �� )�� �����ϱ� ���� �޼���
        /// </summary>
        /// <param name="richText"></param>
        /// <returns></returns>
        public float TextLenght(string richText)
        {
            float len = 0;
            bool inTag = false;

            foreach (var ch in richText)
            {
                if (ch == '<')
                {
                    inTag = true;
                    continue;
                }
                else if (ch == '>')
                {
                    inTag = false;
                }
                else if (!inTag)
                {
                    len++;
                }
            }
            Debug.Log(len);
            return len;
        }
    }
}

public static class TMProUGUIDoText
{
    public static void DoText(this TextMeshProUGUI _text, float _duration)
    {
        _text.maxVisibleCharacters = 0;
        DOTween.To(x => _text.maxVisibleCharacters = (int)x, 0f, _text.text.Length, _duration / _text.text.Length);
    }
}
