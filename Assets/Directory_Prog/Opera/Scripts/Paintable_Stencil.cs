using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    /// <summary>
    /// 2D Texture ������Ʈ���� ����
    /// </summary>
    public class Paintable_Stencil : Paintable, Mountable, Interactable{


        #region Mountable Interface
        public void StartMount() {
            //TODO: �������ϰ� �����, �ڷ�ƾ�Ἥ Paintable_Wall�� ����ĳ����.

            StartCoroutine(CoStartMount());
        }

        public void MountOn() {
            //TODO: ���� �Ƹ� ���� �浹�ϰ� �ִ� Paintable_Wall�̶� �浹�� ��ġ��ǥ �ָ� �ɵ�.
            SetP3dPaintable(true);

            StopCoroutine(CoStartMount());
        }

        public void FinishMount() {
            //TODO: �ٽ� �����ϰ� ����� �ڷ�ƾ ����

            StopCoroutine(CoStartMount());
        }

        private IEnumerator CoStartMount() {

            yield break;
        }

        #endregion

        #region Interactable Interface


        public void StartInteract() {
            
        }

        public void FinishInteract() {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}