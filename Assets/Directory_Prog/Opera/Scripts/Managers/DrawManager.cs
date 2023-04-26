using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace OperaHouse {
    public enum PanelUIType {
        Bag,
        BlackBook,
    }

    public class DrawManager :  Singleton<DrawManager>{
        [SerializeField] private DrawPanel _drawPanel = null;
        public DrawPanel Draw {
            get {
                if(_drawPanel == null)
                    _drawPanel = GetComponentInChildren<DrawPanel>();

                return _drawPanel;
            }
        }

        [SerializeField] private BagPanel _bagPanel = null;
        public BagPanel Bag {
            get {
                if(_bagPanel == null)
                    _bagPanel = GetComponentInChildren<BagPanel>();

                return _bagPanel;
            }
        }

        [SerializeField] private BlackBookPanel _blackBookPanel = null;
        public BlackBookPanel BlackBook {
            get {
                if(_blackBookPanel == null)
                    _blackBookPanel = GetComponentInChildren<BlackBookPanel>();



                return _blackBookPanel;
            }
        }

        [SerializeField] private Spray _spray = null;
        public Spray Spray { get => _spray; }


        protected override void Awake() {
            base.Awake();
        }


        private void Update() {
            if(InteractionManager.Instance.IsInteracting == false)
                return;

            if(Input.GetKeyDown(KeyCode.Tab)) {
                _bagPanel.OpenPanel();
            }

            if(Input.GetKeyDown(KeyCode.B)) {
                _blackBookPanel.OpenPanel();
            }
        }

        public void StartDrawing() {
            _blackBookPanel.ClosePanel();
            _bagPanel.ClosePanel();
            _drawPanel.OpenPanel();
        }

        /// <summary>
        /// ��� ���� DrawPanel�� ������ �ٸ� �г��� �����ִ��� Ȯ�� �� ���ٸ� true, �ִٸ� false�� ��ȯ.
        /// </summary>
        /// <returns></returns>
        public bool IsAnyPanelOpened() {
            return _bagPanel.IsOpened || _blackBookPanel.IsOpened;
        }

        public void FinishDrawing() {
            _blackBookPanel.ClosePanel();
            _bagPanel.ClosePanel();
            _drawPanel.ClosePanel();

            GameObject.Find("Player").GetComponent<Point_Of_View>().ForceChangeToSide();
        }
    }
}
