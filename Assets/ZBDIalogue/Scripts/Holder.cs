using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class Holder : MonoBehaviour
    {
        //Ű���� �Է����� ���̾�α� �����ϴ���, ������ �����ϴ��� ����
        public enum ReadType
        {
            btnInput,   //��ư��ǲ�޾Ƽ� ��ȭ
            enforce     //������ ��ȭ
        }

        //���� ���̾�α� �ϳ��� ��� �������, �迭�� ���ִ� ���̾�α� ID ������� �о������
        public enum ExportType
        {
            single,     //����
            sequence    //������� (����Ʈ ���ҵ� ������� ����)
        }

        public ReadType m_ReadType { get => m_readType; }

        [Header("���ɾ ��ȭ / ������ ������ȭ")]
        [SerializeField] private ReadType m_readType;
        [Header("�ϳ��� ��ȭ�� / ������ ��ȭ ����������")]
        [SerializeField] private ExportType m_exportType;
        [Header("ID ���� �Է��ʿ�")]
        [SerializeField] private IDSets[] m_idSets;
        [SerializeField] private int currentIndex;

        [Header("���̾�α� ���� �� �÷��̾ �ٶ� �� / ��ġ�� �� ")]
        public Transform targetObject; // �ٶ� ���� (������Ʈ ) �� ����
        public Transform holder_playerPos; // ���̾�α� ���� �� �÷��̾ ��ġ�� ���� ����

        private void Start()
        {
            Transform parent = transform.parent;
            holder_playerPos = transform.GetChild(1);
            targetObject = transform.GetChild(0);

            //// ���࿡ targetObject�� �θ� �ִٸ� �θ������ �ٶ󺸰� holder �ڽ� ������Ʈ �� targetObject �� ��Ȱ��ȭ
            //if(parent != null )
            //{
            //    targetObject = parent;
            //    transform.GetChild(0).gameObject.SetActive(false);
            //}
            //else
            //{
            //targetObject = transform.GetChild(0);
            //}

        }
        public int GetEventID(out UnityEvent uEvent_OnEscape)
        {
            switch(m_exportType)
            {
                case ExportType.single:
                    uEvent_OnEscape = m_idSets[0].m_uEvent_OnEscape;
                    return m_idSets[0].m_EventID;

                case ExportType.sequence:
                    int result = m_idSets[currentIndex].m_EventID;
                    uEvent_OnEscape = m_idSets[currentIndex].m_uEvent_OnEscape;
                    currentIndex = currentIndex + 1 < m_idSets.Length ? currentIndex + 1 : currentIndex;
                    return result;
            }

            uEvent_OnEscape = null;
            return -1;
        }

        [System.Serializable]
        public class IDSets
        {
            public int m_EventID;
            [Header("�ش� ��ȭ ������ �Ͼ �̺�Ʈ")]
            public UnityEvent m_uEvent_OnEscape;
        }
    }
}