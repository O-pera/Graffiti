using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class DrawObject : Interactable {
        [Header("P3d Plugins")] 
        [SerializeField] [InspectorName("P3dPaintable")]
        private P3dPaintable _ptble = null;
        
        #region Unity Event Functions
        private void Awake() {
            _ptble = GetComponentInChildren<P3dPaintable>();


        }

        #endregion

        #region Interactable override functions
        public override void ReadyInteract() {
            base.ReadyInteract();

        }

        public override void UnReadyInteract() {
            base.UnReadyInteract();
        }

        public override void StartInteract() {
            //TODO: ī�޶� �̵� �� �׸� �׸��� ��� ON
            _ptble.enabled = true;
        }

        public override void FinishInteract() {
            //TODO: ī�޶� ���� �� �׸� �׸��� ��� OFF
            _ptble.enabled = false;
        }

        #endregion
    }
}