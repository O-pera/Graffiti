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
        private Material _mat = null;

        [SerializeField] private Color availColor, nonAvailColor;

        private void Awake() {
            _mat = _maskPreview.material;
        }

        /// <summary>
        /// ����ũ�� ó�� ������ �� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="maskTexture">TextureŸ���� �̹������� ����</param>
        /// <param name="maskVisualSprite">SpriteŸ���� �̹������� ����</param>
        public void InitializeMask(Texture maskTexture, Sprite maskVisualSprite) {
            _mask.Texture = maskTexture;
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
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
        }

        /// <summary>
        /// ����ũ�� ���������� ��ġ�� �� ȣ���ϴ� �Լ�.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void InstallMask(Vector3 position, Vector3 rotation) {
            SetMaskTransform(position, rotation);
            _maskPreview.gameObject.SetActive(false);
            DrawManager.Instance.Draw.ActivateRemoveButtonWithMask(this);
        }

        /// <summary>
        /// ����ũ�� ���� ����߰ų� ����ũ ������ ������� �� ȣ���ϴ� �Լ�.
        /// </summary>
        public void ReleaseMask() {
            _mask.Texture = null;
            _maskVisual.sprite = null;
            _maskPreview.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}