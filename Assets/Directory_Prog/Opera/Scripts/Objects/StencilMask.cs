using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Insomnia {
    public class StencilMask : MonoBehaviour {
        [SerializeField] MeshRenderer _maskPreview = null;
        [SerializeField] private P3dMask _mask = null;
        [SerializeField] private SpriteRenderer _maskVisual = null;
        [SerializeField] private DrawPanel _drawPanel = null;
        private Material _mat = null;
        [SerializeField] private int _curMaskPixelCount = 0;
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
                _drawPanel.SetStencilVisible(ChangeMaskChannel());
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

            Debug.Log($"Mask's Pixel Count: {_curMaskPixelCount}");
        }

        /// <summary>
        /// ��ġ ������ ����� �� ȣ���ϴ� �Լ�.
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetMaskPreviewVisiblity(bool isVisible) {
            _maskPreview.gameObject.SetActive(isVisible);
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
            _curMaskPixelCount = (int)(_curMaskPixelCount * gameObject.transform.localScale.x );
            SetMaskTransform(position, rotation);
            _maskPreview.gameObject.SetActive(false);
            DrawManager.Instance.Bag.ActivateRemoveButtonWithMask(this);
            _drawPanel.SetStencilVisible(true);
            _drawPanel.OpenStencilVisualizer();
            DrawManager.Instance.Draw.Percent.SetExitMethod(true);
        }

        /// <summary>
        /// ����ũ�� ���� ����߰ų� ����ũ ������ ������� �� ȣ���ϴ� �Լ�.
        /// </summary>
        public void ReleaseMask() {
            _isMaskEnabled = false;
            _mask.Channel = P3dChannel.Alpha;
            _mask.Texture = null;
            _maskVisual.sprite = null;
            _maskPreview.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _drawPanel.CloseStencilVisualizer();
            SetMaskPreviewVisiblity(true);
            DrawManager.Instance.Draw.Percent.SetExitMethod(false);
        }

        private bool ChangeMaskChannel() {
            if(_mask.Channel == P3dChannel.Green) {
                _mask.Channel = P3dChannel.Alpha;
                return true;
            }
            else {
                _mask.Channel = P3dChannel.Green;
                return false;
            }
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