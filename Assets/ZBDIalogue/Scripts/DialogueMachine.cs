using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    public class DialogueMachine : MonoBehaviour
    {
        [SerializeField] DialogueContentsPool m_pool;
        [SerializeField] ActorConnector m_actorConnector;
        [SerializeField] SpeechBubble m_speechBubble;

        Interact nowInteract;
        int index;

        public void NewExport(int EventID)
        {
            nowInteract = m_pool.GetInteract(EventID);
            index = -1;
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
                index++;
                //������ ��ȭ�� �ִ�
                if (index < nowInteract.m_Acts.Count)
                {
                    Act nowAct = nowInteract.m_Acts[index];

                    //��ǳ���� ������ �ൿ
                    m_actorConnector.Actor_Do(nowAct);

                    //��ǳ��
                    m_speechBubble.AppearNew(m_actorConnector.GetActorPos(nowAct), nowAct.m_Context);
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

        }
    }
}