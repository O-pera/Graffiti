using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace OperaHouse {

    public struct EventFunc {
        public bool NeedToReUse;
        public Func<bool> EventFunction;
    }

    /// <summary>
    /// ��ȣ�ۿ� ������ ������Ʈ�� ���� �θ� Ŭ����
    /// </summary>
    public class Interactable : MonoBehaviour {
        [Header("Interactable UIs")]
        [SerializeField] GameObject _interactCanvas = null;

        //[Header("Delegate Functions' Queue for Any Events")]


        /// <summary>
        /// OnTriggerEnter()���� ȣ���ϴ� �Լ�.
        /// </summary>
        public virtual void ReadyInteract() {
            _interactCanvas.SetActive(true);
        }

        /// <summary>
        /// OnTriggerExit()���� ȣ���ϴ� �Լ�.
        /// </summary>
        public virtual void UnReadyInteract() {
            _interactCanvas.SetActive(false);
        }

        /// <summary>
        /// ��ȣ�ۿ� ���� ��ư Ŭ�� �� ȣ���ϴ� �Լ�.
        /// </summary>
        public virtual void StartInteract() {
            Debug.Log("Interactable.StartInteract(): Not overrided");
        }

        /// <summary>
        /// ��ȣ�ۿ� ���� ��ư Ŭ�� �� ȣ���ϴ� �Լ�.
        /// </summary>
        public virtual void FinishInteract() {
            Debug.Log("Interactable.FinishInteract(): Not overrided");
        }
    }
}
