using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class StencilMask : MonoBehaviour {
        [SerializeField] MeshRenderer _maskPreview = null;
        [SerializeField] private P3dMask _mask = null;
        [SerializeField] private SpriteRenderer _maskVisual = null;
        [SerializeField] private DrawPanel _drawPanel = null;
        private Material _mat = null;
        private int _curMaskPixelCount = 0;
        bool _isMaskEnabled = false;

        public bool MaskEnabled { get => _isMaskEnabled; }
        public int MaskPixelCount { get => _curMaskPixelCount; }
        public float ScaleRatio { get => transform.localScale.x; }

        [SerializeField] private Color availColor, nonAvailColor;

        private void Awake() {
            _mat = _maskPreview.material;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Z)) {
                bool isActive = _mask.enabled;
                _mask.enabled = !isActive;
                _maskVisual.gameObject.SetActive(!isActive);
                _drawPanel.SetStencilVisible(_mask.enabled);
            }
        }

        /// <summary>
        /// ����ũ�� ó�� ������ �� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="maskTexture">TextureŸ���� �̹������� ����</param>
        /// <param name="maskVisualSprite">SpriteŸ���� �̹������� ����</param>
        public void InitializeMask(Texture2D maskTexture, Sprite maskVisualSprite) {
            if(MaskEnabled)
                ReleaseMask();
            _mask.Texture = maskTexture;
            _curMaskPixelCount = GetPixelCount(maskTexture);
            _maskVisual.sprite = maskVisualSprite;
            _maskPreview.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// ��ġ ������ ����� �� ȣ���ϴ� �Լ�.
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetMaskVisible(bool isVisible) {
            if(isVisible) {
                _maskPreview.gameObject.SetActive(true);
            }
            else {
                _maskPreview.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// ����ũ�� ��ġ�� �� ��ġ ������ ��� ���, �Ұ����� ��� �������� �絵�� �������ִ� �Լ�.
        /// </summary>
        /// <param name="isAvailable">true: Green / false: Red</param>
        public void SetMaskInstallationAvailable(bool isAvailable) {
            if(isAvailable == true)
                _mat.color = availColor;
            else
                _mat.color = nonAvailColor;
        }

        /// <summary>
        /// ����ũ�� ��ġ�� �����ϰų� ������ �� ȣ���ϴ� �Լ�.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SetMaskTransform(Vector3 position, Vector3 rotation) {
            transform.position = position + new Vector3(0, 0, -0.03f);
            transform.rotation = Quaternion.Euler(rotation);
        }

        /// <summary>
        /// ����ũ�� ���������� ��ġ�� �� ȣ���ϴ� �Լ�.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void InstallMask(Vector3 position, Vector3 rotation) {
            _isMaskEnabled = true;
            SetMaskTransform(position, rotation);
            _maskPreview.gameObject.SetActive(false);
            DrawManager.Instance.Bag.ActivateRemoveButtonWithMask(this);
            _drawPanel.OpenStencilVisualizer();
            DrawManager.Instance.Draw.Percent.SetDrawFinishMode(true);
        }

        /// <summary>
        /// ����ũ�� ���� ����߰ų� ����ũ ������ ������� �� ȣ���ϴ� �Լ�.
        /// </summary>
        public void ReleaseMask() {
            _isMaskEnabled = false;
            _mask.Texture = null;
            _maskVisual.sprite = null;
            _maskPreview.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _drawPanel.CloseStencilVisualizer();
            DrawManager.Instance.Draw.Percent.SetDrawFinishMode(false);
        }

        private int GetPixelCount(Texture2D texture) {
            var tw = texture.width;
            var th = texture.height;
            var source = texture.GetPixels();
            var pixels = texture.GetPixels();
            int result = 0;

            int i1 = 0;

            for(int iy = 0; iy < th; iy++) {
                for(int ix = 0; ix < tw; ix++) {
                    if(source[i1++].a >= 0.9f)
                        result++;
                }
            }

            return (int)(result * 0.1f);
        }
    }
}