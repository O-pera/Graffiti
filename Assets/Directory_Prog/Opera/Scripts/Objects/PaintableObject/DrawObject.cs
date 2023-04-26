using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

namespace OperaHouse {
    public class DrawObject : Interactable {
        [Header("P3d Plugins")] 
        [SerializeField] [InspectorName("P3dPaintable")]
        private P3dPaintable _ptble = null;

        [SerializeField]
        private InteractionArea _interactArea = null;
        
        #region Unity Event Functions
        private void Awake() {
            _ptble = GetComponentInChildren<P3dPaintable>();

            _interactArea = GetComponentInChildren<InteractionArea>();
            if(_interactArea == null)
                return;

        }

        #endregion

        #region Interactable override functions
        //public bool CheckNofiForInteraction(InteractType type) {

        //}

        public override void ReadyInteract(Collider other) {
            base.ReadyInteract(other);
        }

        public override void UnReadyInteract(Collider other) {
            base.UnReadyInteract(other);
        }

        public override void StartInteract() {
            //TODO: ī�޶� �̵� �� �׸� �׸��� ��� ON
            _ptble.enabled = true;
            _interactCanvas.SetActive(false);
            _interactArea.SetColliderActivation(false);
            InteractionManager.Instance.StartedInteract(this);
            DrawManager.Instance.StartDrawing();
        }

        public override void FinishInteract() {
            //TODO: ī�޶� ���� �� �׸� �׸��� ��� OFF
            _ptble.enabled = false;
            _interactCanvas.SetActive(true);
            _interactArea.SetColliderActivation(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        #endregion
    }
}