using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public interface Interactable {
        public void StartInteract();
        public void FinishInteract();
    }

    public interface Mountable {
        /// <summary>
        /// ��ġ�� ������ �� ȣ���ϴ� �Լ�
        /// </summary>
        public void StartMount();

        //TODO: �Ƹ� ��ġ ��� ������Ʈ�� Ŀ���� ������Ʈ�� ��ġ ��������ߵɵ�?
        /// <summary>
        /// ��ġ�� ��ġ�� ���������� �������� �� ȣ���ϴ� �Լ�
        /// </summary>
        public void MountOn();

        /// <summary>
        /// ��ġ�� �����߰ų� ������� �� ȣ���ϴ� �Լ�
        /// </summary>
        public void FinishMount();
    }

    public interface QuestReceiver{
        
    }
}