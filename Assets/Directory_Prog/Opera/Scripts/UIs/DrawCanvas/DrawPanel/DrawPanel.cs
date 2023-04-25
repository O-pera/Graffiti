using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class DrawPanel : UIPanel {
        [SerializeField] private Slider _remainSlider = null;

        public Slider RemainSlider { get => _remainSlider; }

        protected override void OnEnablePanel() {
            //DrawPanel�� �׸� �׸� �� ��� ��µǾ���ϱ� ������ �ƹ� �͵� ���� �ʴ´�.
            gameObject.SetActive(true);
        }

        protected override void OnDisablePanel() {
            //DrawPanel�� �׸� �׸� �� ��� ��µǾ���ϱ� ������ �ƹ� �͵� ���� �ʴ´�.
            gameObject.SetActive(false);
        }
    }
}