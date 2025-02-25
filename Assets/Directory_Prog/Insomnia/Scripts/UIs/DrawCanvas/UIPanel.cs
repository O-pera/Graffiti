using UnityEngine;

namespace Insomnia {
    public class UIPanel : MonoBehaviour {
        [SerializeField] private bool _isOpened = false;
        public bool IsOpened { get => _isOpened; }

        public virtual bool IsPlayingAnimation { get; }

        private void Awake() {
            Init();
        }

        public virtual void OpenPanel() {
            OnEnablePanel();
        }

        public virtual void ClosePanel() {
            if(IsOpened == false)
                return;

            OnDisablePanel();
        }

        protected virtual void OnEnablePanel() {
            _isOpened = true;
        }

        protected virtual void OnDisablePanel() {
            _isOpened = false;
        }

        protected virtual void Init() {

        }
    }
}