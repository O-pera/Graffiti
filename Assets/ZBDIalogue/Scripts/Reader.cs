using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class Reader : MonoBehaviour
    {
        [SerializeField] private int m_targetLayer;
        [SerializeField] private bool m_inputWaiting;

        private DialogueMachine m_machine;
        private ReadableShower m_readableShower;
        private Holder m_holder;

        private void OnTriggerEnter(Collider other)
        {
            //Holder Ŭ���� ����ִ� ������Ʈ�� �浹
            if (other.gameObject.layer == m_targetLayer)
            {
                //Holder Ŭ���� �������µ��� ����
                if(other.TryGetComponent(out m_holder))
                {
                    //���� ���̾�α� ����
                    if (m_holder.m_ReadType == Holder.ReadType.enforce)
                    {
                        NewExport();
                    }

                    //��ưŬ�� ���̾�α� ���� ���
                    else if (m_holder.m_ReadType == Holder.ReadType.btnInput)
                    {
                        m_inputWaiting = true;
                        m_readableShower.ShowStart();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == m_targetLayer)
            {
                m_inputWaiting = false;
                m_readableShower.ShowStop();
            }
        }

        public void OnBtnInput()
        {
            //�� ���̾�α� ����
            if (m_inputWaiting && !m_machine.Interacting)
            {
                NewExport();
            }

            //���̾�α� ��� ����
            else if (m_machine.Interacting)
            {
                m_machine.TryNext();
            }
        }

        private void NewExport()
        {
            //���̾�α� ����
            UnityEvent uEvent_OnEscape;
            m_machine.NewExport(m_holder.GetEventID(out uEvent_OnEscape));
            m_machine.AddEscapeEvent(uEvent_OnEscape.Invoke);
        }

        private void Awake()
        {
            m_machine = FindObjectOfType<DialogueMachine>();
            m_readableShower = FindObjectOfType<ReadableShower>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnBtnInput();
                m_readableShower.ShowStop();
            }

            if(!m_machine.Interacting && m_inputWaiting && !m_readableShower.m_Showing)
            {
                m_readableShower.ShowStart();
            }
        }
    }
}