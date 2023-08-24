using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//namespace ZB.Dialogue.Graffiti
//{
//    public class Reader : MonoBehaviour
//    {
//        [SerializeField] private int m_targetLayer;
//        [SerializeField] private bool m_inputWaiting;

//        private DialogueMachine m_machine;
//        private ReadableShower m_readableShower;
//        private Holder m_holder;

//        private Transform m_currentHolder;
//        // �÷��̾� ��ü �߰�..
//        private Transform m_playerTransform;

//        private void OnTriggerEnter(Collider other)
//        {
//            //Holder Ŭ���� ����ִ� ������Ʈ�� �浹
//            if (other.gameObject.layer == m_targetLayer)
//            {
//                //Holder Ŭ���� �������µ��� ����
//                if (other.TryGetComponent(out m_holder) && !m_machine.m_Interacting)
//                {
//                    //���� ���̾�α� ����
//                    if (m_holder.m_ReadType == Holder.ReadType.enforce)
//                    {
//                        NewExport();
//                    }

//                    //��ưŬ�� ���̾�α� ���� ���
//                    else if (m_holder.m_ReadType == Holder.ReadType.btnInput)
//                    {
//                        m_inputWaiting = true;
//                        m_currentHolder = other.transform;
//                        m_readableShower.ShowStart(m_currentHolder);
//                    }
//                }
//            }
//        }

//        private void OnTriggerExit(Collider other)
//        {
//            if (other.gameObject.layer == m_targetLayer)
//            {
//                m_inputWaiting = false;
//                m_readableShower.ShowStop();
//            }
//        }

//        public void OnBtnInput()
//        {
//            //�� ���̾�α� ����
//            if (m_inputWaiting && !m_machine.m_Interacting)
//            {
//                NewExport();
//                m_readableShower.ShowStop();
//            }

//            //���̾�α� ��� ����
//            else if (m_machine.m_Interacting)
//            {
//                m_machine.TryNext();
//            }
//        }

//        private void NewExport()
//        {
//            //���̾�α� ����
//            UnityEvent uEvent_OnEscape;
//            m_machine.NewExport(m_holder.GetEventID(out uEvent_OnEscape));
//            m_machine.AddEscapeEvent(uEvent_OnEscape.Invoke);

//            /*
//            float currentPlayerPositionY = m_playerTransform.position.y;

//            // ��� �߰��� �κ�... Holder �� ��ġ�� �÷��̾ �̵� , �׸��� Holder ���� ������ target ������Ʈ�� ���ؼ� �ٶ�(�������ָ� ��)
//            m_playerTransform.position = m_currentHolder.position;
//            // �̵���ų �� �̵��� �κ��� y ���� �÷��̾��� y �ຸ�� �۰ų� ũ�ٸ� �̵��� �������� y �� ���� �÷��̾��� y ���������� �����Ѵ�. 
//            if(m_currentHolder.position.y > currentPlayerPositionY || m_currentHolder.position.y < currentPlayerPositionY)
//            {
//                Vector3 playerPosition = m_playerTransform.position;
//                playerPosition.y = currentPlayerPositionY;
//                m_playerTransform.position = playerPosition;
//            }

//            Vector3 targetPosition = m_currentHolder.GetComponent<Holder>().targetObject.position;
//            targetPosition.y = m_playerTransform.position.y;
//            m_playerTransform.LookAt(targetPosition);

//            m_playerTransform.gameObject.GetComponentInChildren<Animator>().SetFloat("moveWeight_Side", 0f);
//            */

//            float currentPlayerPositionY = m_playerTransform.position.y; // �ϴ� currentPlayerPositionY �� ���̾�α׸� �����ϴ� ������ �÷��̾� �������� ����
//            Transform holderPosition = m_currentHolder.GetComponent<Holder>().playerPosition;  // �÷��̾��� �������� currentHolder �� playerPosition ���� �̵� �ϵ�, y ���� currentPlayerpositionY�� �����Ѵ�. 
//            Debug.Log(m_currentHolder.gameObject.name);

//            Vector3 exportPlayerPos = new Vector3(holderPosition.position.x, currentPlayerPositionY, holderPosition.position.z);
//            m_playerTransform.position = exportPlayerPos;

//            Vector3 targetPosition = m_currentHolder.GetComponent<Holder>().targetObject.position; // �÷��̾�� targetPosition �� ���� �ٶ󺸵�, y ���� ������ �ʴ´�. 
//            targetPosition.y = m_playerTransform.position.y;
//            m_playerTransform.LookAt(targetPosition);

//            m_playerTransform.gameObject.GetComponentInChildren<Animator>().SetFloat("moveWeight_Side", 0f); // ���̾�α׷� �������� ��쿡�� �÷��̾��� �ִϸ����͸� Idle ���·� �����Ѵ� 

//        }

//        private void Awake()
//        {
//            m_machine = FindObjectOfType<DialogueMachine>();
//            m_readableShower = FindObjectOfType<ReadableShower>();
//            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
//        }

//        private void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
//            {
//                OnBtnInput();
//            }

//            if (!m_machine.m_Interacting && m_inputWaiting && !m_readableShower.m_Showing)
//            {
//                m_readableShower.ShowStart(m_currentHolder);
//            }
//        }
//    }
//}

namespace ZB.Dialogue.Graffiti
{
    public class Reader : MonoBehaviour
    {
        [SerializeField] private int m_targetLayer;
        [SerializeField] private bool m_inputWaiting;

        private DialogueMachine m_machine;
        private ReadableShower m_readableShower;
        private Holder m_holder;

        private Transform m_currentHolder;
        private Transform m_playerTransform;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == m_targetLayer)
            {
                if (other.TryGetComponent(out m_holder) && !m_machine.m_Interacting)
                {
                    if (m_holder.m_ReadType == Holder.ReadType.enforce)
                    {
                        NewExport(other.transform);
                    }
                    else if (m_holder.m_ReadType == Holder.ReadType.btnInput)
                    {
                        m_inputWaiting = true;
                        m_currentHolder = other.transform;
                        m_readableShower.ShowStart(m_currentHolder);
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
            if (m_inputWaiting && !m_machine.m_Interacting)
            {
                if (m_currentHolder != null)
                {
                    NewExport(m_currentHolder);
                    m_readableShower.ShowStop();
                }
            }
            else if (m_machine.m_Interacting)
            {
                m_machine.TryNext();
            }
        }

        private void NewExport(Transform currentHolder)
        {
            UnityEvent uEvent_OnEscape;
            m_machine.NewExport(m_holder.GetEventID(out uEvent_OnEscape));
            m_machine.AddEscapeEvent(uEvent_OnEscape.Invoke);

            float currentPlayerPositionY = m_playerTransform.position.y;
            Transform holderPosition = currentHolder.GetComponent<Holder>().playerPosition;

            Vector3 exportPlayerPos;
            if (holderPosition != null)
            {
                exportPlayerPos = new Vector3(holderPosition.position.x, currentPlayerPositionY, holderPosition.position.z);
            }
            else
            {
                exportPlayerPos = m_playerTransform.position;
            }
            m_playerTransform.position = exportPlayerPos;

            Vector3 targetPosition = currentHolder.GetComponent<Holder>().targetObject.position;
            targetPosition.y = m_playerTransform.position.y;
            m_playerTransform.LookAt(targetPosition);

             m_playerTransform.gameObject.GetComponentInChildren<Animator>().SetFloat("moveWeight_Side", 0f);

            //Animator animator = m_playerTransform.gameObject.GetComponentInChildren<Animator>();
            //animator.SetFloat("moveWeight_Side", 0f);

            //// Stop other animations if they are playing
            //foreach (AnimatorControllerParameter parameter in animator.parameters)
            //{
            //    if (parameter.type == AnimatorControllerParameterType.Float)
            //    {
            //        animator.SetFloat(parameter.name, 0f);
            //    }
            //}
        }

        private void Awake()
        {
            m_machine = FindObjectOfType<DialogueMachine>();
            m_readableShower = FindObjectOfType<ReadableShower>();
            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if(m_machine == null)
                return;

            if (/*Input.GetKeyDown(KeyCode.E) ||*/ Input.GetMouseButtonDown(0))
            {
                OnBtnInput();
            }

            if (!m_machine.m_Interacting && m_inputWaiting && !m_readableShower.m_Showing)
            {
                m_readableShower.ShowStart(m_currentHolder);
            }
        }
    }
}