using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class DialogueMachine : MonoBehaviour
    {
        [Header("���̾�α� ������ �� �̺�Ʈ")]
        [SerializeField] private UnityEvent m_fixedEvent_OnEnter;
        [Header("���̾�α� ������ �� �̺�Ʈ")]
        [SerializeField] private UnityEvent m_fixedEvent_OnEscape;

        private UnityEvent m_uEvent_OnEscape;
        [SerializeField] private DialogueContentsPool m_pool;
        [SerializeField] private ActorConnector m_actorConnector;
        [SerializeField] private SpeechBubble m_speechBubble;

        public bool m_Interacting { get => m_interacting; }

        Interact m_nowInteract;
        int m_index;
        bool m_interacting;

        // �Ű������� �̺�Ʈ ID ������ ���̾�α� �ٷ� �ҷ��� �� �ִ�.. -> TimeLine ���� ����ϱ� 
        public void NewExport(int EventID)
        {
            m_fixedEvent_OnEnter.Invoke();

            m_nowInteract = m_pool.GetInteract(EventID);
            m_index = -1;
            m_interacting = true;
            TryNext();
        }

        public void TryNext()
        {
            //���� ���� ���� ���̴�. ��ŵ
            if (m_speechBubble.m_TextAppearing)
            {
                m_speechBubble.SkipCurrent();
            }

            //���� ������ ������. ������ȭ
            else
            {
                m_index++;
                //������ ��ȭ�� �ִ�
                if (m_index < m_nowInteract.m_Acts.Count)
                {
                    Act nowAct = m_nowInteract.m_Acts[m_index];

                    //��ǳ���� ������ �ൿ
                    m_actorConnector.Actor_Do(nowAct);

                    //��ǳ��
                    m_speechBubble.AppearNew(m_actorConnector.GetActorTransform(nowAct), nowAct.m_Context);
                }

                //������ ��ȭ�� ����
                else
                {
                    m_speechBubble.Disappear();
                    EscapeDialogue();
                }
            }
        }

        public void EscapeDialogue()
        {
            m_interacting = false;
            m_uEvent_OnEscape.Invoke();
            m_fixedEvent_OnEscape.Invoke();
            m_uEvent_OnEscape.RemoveAllListeners();
        }

        public void AddEscapeEvent(UnityAction action)
        {
            m_uEvent_OnEscape.AddListener(action);
        }

        private void Awake()
        {
            m_uEvent_OnEscape = new UnityEvent();
        }
    }
}