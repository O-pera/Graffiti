using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZB.Dialogue
{
    public abstract class IImageSwap : MonoBehaviour
    {
        [SerializeField] SingleContent[] m_contents;
        public SingleContent m_focusingContent { get => m_contents[m_index]; }

        [SerializeField] protected int m_index = 0;

        protected abstract void AppearEvent();
        protected abstract void DisappearEvent();
        protected abstract void ResetState();

        [SerializeField] bool imageOverlaping;

        public void Appear(bool appearNew)
        {
            if (appearNew) imageOverlaping = false;

            //�տ� �ִ� �̹��� ������
            if (imageOverlaping)
            {
                //�ִ� �̹��� ����
                Disappear();
            }

            //�� �̹��� ����
            AppearEvent();

            imageOverlaping = true;
        }
        public void Disappear()
        {
            if (imageOverlaping)
            {
                //�ִ� �̹��� ����
                DisappearEvent();
                Swap();

                imageOverlaping = false;
            }
        }

        private void Swap()
        {
            m_index = (m_index + 1) % 2;
        }

        protected void Awake()
        {
            ResetState();
        }

        [System.Serializable]
        public class SingleContent
        {
            public Image m_Img;
            public RectTransform m_Rtf;
            public Animator m_Ani;
            public TextMeshProUGUI m_Tmp;
        }
    }
}