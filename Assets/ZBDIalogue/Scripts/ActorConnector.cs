using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    public class ActorConnector : MonoBehaviour
    {
        [SerializeField] List<Actor> m_finded_actors;

        public void Actor_Do(Act act)
        {
            FindActor(act.m_ActorID).Do(act);
        }
        public Transform GetActorTransform(Act act)
        {
            return FindActor(act.m_ActorID).transform;
        }
        public Vector3 GetActorPos(Act act)
        {
            return FindActor(act.m_ActorID).transform.position;
        }

        Actor FindActor(int actorID)
        {
            //����Ʈ �ȿ� �ִ��� ã�´�.
            for (int i = 0; i < m_finded_actors.Count; i++)
            {
                if (m_finded_actors[i].m_ActorID == actorID)
                {
                    return m_finded_actors[i];
                }
            }

            //����Ʈ�ȿ� ���ٸ�, ����Ʈ ������Ʈ, ����Ʈ�ȿ��� �ٽ� ã��
            bool checkNext = false;
            Actor[] newArray = FindObjectsOfType<Actor>();
            for (int i = 0; i < newArray.Length; i++)
            {
                for (int j = 0; j < m_finded_actors.Count; j++)
                {
                    if (newArray[i] == m_finded_actors[j])
                    {
                        checkNext = true;
                        break;
                    }
                }

                if (checkNext)
                {
                    checkNext = false;
                    continue;
                }
                m_finded_actors.Add(newArray[i]);
            }
            for (int i = 0; i < m_finded_actors.Count; i++)
            {
                if (m_finded_actors[i].m_ActorID == actorID)
                {
                    return m_finded_actors[i];
                }
            }

            //�����ߴٸ� LogError
            Debug.LogError($"ActorConnector / FindActor() / ã�� ���� / ���� �Ű����� : {actorID} / �Ű����� �Է¿��� �Ǵ� ã�����ϴ� Actor������Ʈ ��Ȱ��ȭ");
            return null;
        }
    }
}