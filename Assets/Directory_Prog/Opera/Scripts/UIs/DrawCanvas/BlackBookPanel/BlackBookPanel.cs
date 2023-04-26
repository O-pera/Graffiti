using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OperaHouse {

    public class BlackBookPanel : UIPanel {
        [SerializeField] private RectTransform _blackBookGroup = null;
        [SerializeField] private TextMeshProUGUI _pageText = null;
        [SerializeField] private StencilInstaller _stencilInstaller = null;
        [SerializeField] private PageGroupUI _leftPage = null;
        [SerializeField] private PageGroupUI _rightPage = null;
        private int _curPageNum = 0;

        private StencilData _curStencils = null;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                OnClick_ExitBlackBook();
            }
        }

        protected override void InitPos() {
            _blackBookGroup.position =_blackBookGroup.position + new Vector3(0, _blackBookGroup.rect.height, 0);
            _blackBookGroup.gameObject.SetActive(true);
        }

        public override void OpenPanel() {
            if(DrawManager.Instance.IsAnyPanelOpened() && IsOpened == false)
                return;
            if(IsOpened)
                base.ClosePanel();
            else
                base.OpenPanel();
        }

        public override void ClosePanel() {
            base.ClosePanel();
        }

        protected override void OnEnablePanel() {
            _blackBookGroup.DOMove(_blackBookGroup.position - new Vector3(0, _blackBookGroup.rect.height, 0), 1f);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            base.OnEnablePanel();
        }

        protected override void OnDisablePanel() {
            _blackBookGroup.DOMove(_blackBookGroup.position + new Vector3(0, _blackBookGroup.rect.height, 0), 1f);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            base.OnDisablePanel();
        }

        public void OnClick_NextPage() {
            if(_curStencils._stencils.Count <= ( _curPageNum + 1 ) * 12)
                return;

            _curPageNum++;
            SetPageGroupsWithCurrentPage();
        }

        public void OnClick_PrevPage() {
            if(_curPageNum == 0)
                return;

            _curPageNum--;
            SetPageGroupsWithCurrentPage();
        }

        /// <summary>
        /// ������ �±׸� �������� �� ó�� �����ϴ� �Լ�
        /// </summary>
        /// <param name="data"></param>
        public void OnClick_SetBlackBookTag(StencilData data) {
            if(data == null)
                return;

            _curStencils = data;
            _curPageNum = 0;

            SetPageGroupsWithCurrentPage();
        }

        public void OnClick_ExitBlackBook() {
            ClosePanel();
        }

        /// <summary>
        /// ����/������ PageGroup�� ���� �������� �����ͷ� �ʱ�ȭ�ϴ� �Լ�.
        /// </summary>
        private void SetPageGroupsWithCurrentPage() {
            SetCurrentPageUI();
            _leftPage.InitializePage(GetStencilsInPage(_curPageNum));
            _rightPage.InitializePage(GetStencilsInPage((_curPageNum + 1) * 6));
        }

        /// <summary>
        /// ���ٽ��� �������� �� ó�� �����ϴ� �Լ�.
        /// </summary>
        /// <param name="data"></param>
        public void InstallStencil(Stencil data) {
            if(data.MaskSprite == null)
                return;

            _stencilInstaller.StartInstallStencil(data.MaskSprite, data.MaskOutlineSprite);
            ClosePanel();
        }

        /// <summary>
        /// _curStencils._stencils�� ������ �ִ� Stencil�����͸� �ִ� 6������ Slice�ؼ� ��ȯ�ϴ� �Լ�.
        /// </summary>
        /// <param name="offset"> ������ Stencil�� offset. </param>
        /// <returns> offset���� �ִ� 6���� Stencil�� ������ List�� ��ȯ <returns>
        private List<Stencil> GetStencilsInPage(int offset) {
            if(_curStencils._stencils.Count <= offset)
                return null;

            int count = Mathf.Min(_curStencils._stencils.Count - offset, 6);
            List<Stencil> ret = _curStencils._stencils.GetRange(offset, count);

            return ret;
        }

        /// <summary>
        /// ���� �������� ����ϴ� TMPro TextUI�� �����ϴ� �Լ�.
        /// </summary>
        private void SetCurrentPageUI() {
            int lastPage = _curStencils._stencils.Count % 12 > 0 ? _curStencils._stencils.Count / 12 + 1 : _curStencils._stencils.Count / 12;
            _pageText.text = $"{_curPageNum + 1}/{lastPage}";
        }
    }
}
