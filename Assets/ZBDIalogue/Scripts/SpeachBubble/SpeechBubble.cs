using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace ZB.Dialogue.Graffiti
{
    public class SpeechBubble : IImageSwap
    {
        [SerializeField] TextMeshProUGUI m_tmp_sizeCheck;
        [SerializeField] RectTransform m_rtf_sizeCheck;

        public bool m_TextAppearing { get { return m_textAppearing; } }
        private bool m_textAppearing;

        //�ؽ�Ʈ ���� �ӵ�
        private float m_textAppearDuration = 0.05f;
        private WaitForSeconds m_wfs_textAppearDuration;

        private Transform m_actorTF;        //��ǳ�� �����ؾ��� ����
        private string m_talkText;          //���

        public void AppearNew(Transform tf, string text)
        {
            //�ؽ�Ʈ ���� �߰�
            m_talkText = text;
            m_tmp_sizeCheck.text = text;

            m_actorTF = tf;

            Appear(false);
        }
        public void SkipCurrent()
        {
            //�ؽ�Ʈ ���� ��ŵ
            if (textAppear_C != null)
                StopCoroutine(textAppear_C);
            m_focusingContent.m_Tmp.text = m_talkText;
            m_textAppearing = false;
        }
        public void DisappearCurrent()
        {
            Disappear();
        }


        protected override void AppearEvent()
        {
            m_focusingContent.m_Rtf.gameObject.SetActive(true);
            m_focusingContent.m_Rtf.position = Camera.main.WorldToScreenPoint(m_actorTF.position);

            m_focusingContent.m_Rtf.DOKill();
            m_focusingContent.m_Rtf.sizeDelta = Vector2.zero;
            transform.DOMove(Vector2.zero, 0.05f).OnComplete(() =>
            {
                m_focusingContent.m_Rtf.DOKill();
                m_focusingContent.m_Rtf.sizeDelta = Vector2.one * 10;
                m_focusingContent.m_Rtf.DOSizeDelta(m_rtf_sizeCheck.sizeDelta + Vector2.one * 20, 0.2f);
            });

            if (textAppear_C != null)
                StopCoroutine(textAppear_C);
            textAppear_C = textAppear();
            StartCoroutine(textAppear_C);

            ShowPosFix_C = ShowPosFix();
            StartCoroutine(ShowPosFix_C);
        }
        protected override void DisappearEvent()
        {
            m_focusingContent.m_Rtf.gameObject.SetActive(false);

            if (textAppear_C != null)
                StopCoroutine(textAppear_C);

            StopCoroutine(ShowPosFix_C);
        }
        protected override void ResetState()
        {

        }


        private void Awake()
        {
            base.Awake();
            m_wfs_textAppearDuration = new WaitForSeconds(m_textAppearDuration);
        }
        IEnumerator textAppear_C;
        IEnumerator textAppear()
        {
            TextMeshProUGUI tmp = m_focusingContent.m_Tmp;
            tmp.text = "";

            m_textAppearing = true;
            for (int i = 0; i < m_talkText.Length; i++)
            {
                tmp.text += m_talkText[i];
                yield return m_wfs_textAppearDuration;
            }
            m_textAppearing = false;
        }
        IEnumerator ShowPosFix_C;
        IEnumerator ShowPosFix()
        {
            while(true)
            {
                m_focusingContent.m_Rtf.position =
                    Vector2.Lerp(m_focusingContent.m_Rtf.position, Camera.main.WorldToScreenPoint(m_actorTF.position), Time.deltaTime * 5);

                yield return null;
            }
        }
    }
}