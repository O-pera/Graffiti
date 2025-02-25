using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour {
    [Header("Player Input Component")]
    [SerializeField] private PlayerInputHandler e_input = null;

    [Header("Player Cameras")]
    [SerializeField] private CinemachineVirtualCameraBase e_sideCam = null;
    [SerializeField] private CinemachineVirtualCameraBase e_tpsCam = null;

    [Header("Current Interactable Object")]
    [SerializeField] private Interactable e_curInteract = null;

    [Header("OnClickEvent Receiver")]
    [SerializeField] SprayController e_spray = null;

    [Header("UIPanel Object")]
    [SerializeField] private GameObject e_uiPanel = null;

    private bool i_canInput = true;

    #region Unity Event Functions

    #endregion

    #region User Defined Functions


    #endregion

    #region Interaction Event Functions
    public void OnInteract() {
        if(e_curInteract == null)
            return;

        e_curInteract.OnInteract();

        //TODO: 여기서 자기 자신의 컴포넌트들 & 객체들 관리 실행.

        e_input.ChangeInputState(Define.InputType.Player_Draw);
        e_sideCam.enabled = false;
    }

    public void OffInteract() {
        if(e_curInteract == null)
            return;

        //TODO: 여기서 자기 자신의 컴포넌트들 & 객체들 관리 우선적으로 실행.
        e_input.ChangeInputState(Define.InputType.Player_Wander);

        e_sideCam.enabled = true;

        e_curInteract.OffInteract();
    }

    public void Notify_OnInteractArea(Interactable onRanged) {
        if(e_curInteract == onRanged)
            return;

        e_curInteract = onRanged;
    }

    public void Notify_OffInteractArea(Interactable outRanged) {
        if(e_curInteract != outRanged)
            return;

        e_curInteract = null;
    }

    #endregion

    #region Action Event Functions

    public void OnFocus(bool performed, bool sudoExit = false) {
        if(i_canInput == false)
            return;

        if(sudoExit) {
            e_spray.OnFocus(false);
            e_tpsCam.enabled = false;
            return;
        }

        if(e_spray.IsFocusing == false) {
            CursorManager.ChangeCursorModeTo(CursorLockMode.Locked);
            e_spray.OnFocus(true);
            e_tpsCam.enabled = true;
        }
            
        else if(e_spray.IsFocusing == true) {
            CursorManager.ChangeCursorModeTo(CursorLockMode.None);
            e_spray.OnFocus(false);
            e_tpsCam.enabled = false;
        }
    }

    public void OnLeftClick(bool performed) {
        if(i_canInput == false)
            return;

        e_spray.OnLeftClick(performed);
    }

    public void OnWheelScroll(float scrollDelta) {
        if(i_canInput == false)
            return;

        e_spray.ChangeSprayNozzleSize(scrollDelta);
    }

    public bool SwitchUIActivation(bool sudoExit = false) {
        if(e_uiPanel == null)
            return true;

        if(sudoExit) {
            e_uiPanel.SetActive(false);
            i_canInput = true;
            return true;
        }

        if(e_uiPanel.activeSelf) {  //UI를 끌 때
            e_uiPanel.SetActive(false);
            i_canInput = true;
            CursorManager.ChangeCursorModeTo(CursorLockMode.Locked);
            ( e_tpsCam as CinemachineFreeLook ).m_XAxis.m_MaxSpeed = 100f;
            ( e_tpsCam as CinemachineFreeLook ).m_YAxis.m_MaxSpeed = 2.5f;
        }
        else {//UI를 켤 때
            e_uiPanel.SetActive(true);
            i_canInput = false;
            CursorManager.ChangeCursorModeTo(CursorLockMode.None);

            ( e_tpsCam as CinemachineFreeLook ).m_XAxis.m_MaxSpeed = 0f;
            ( e_tpsCam as CinemachineFreeLook ).m_YAxis.m_MaxSpeed = 0f;
        }

        return i_canInput;
    }

    public void OnMouseDelta(Vector2 value) {
        e_spray.ShakingSpray(value);
    }

    public void OnShakeModeClicked(bool performed) {
        bool isPerformed = e_spray.ShakeModeActivation(performed);

        if(isPerformed) {//흔들기 시작
            ( e_tpsCam as CinemachineFreeLook ).m_XAxis.m_MaxSpeed = 10f;
            ( e_tpsCam as CinemachineFreeLook ).m_YAxis.m_MaxSpeed = 0.5f;
        }
        else {//흔들기 종료
            ( e_tpsCam as CinemachineFreeLook ).m_XAxis.m_MaxSpeed = 100f;
            ( e_tpsCam as CinemachineFreeLook ).m_YAxis.m_MaxSpeed = 2.5f;
        }
    }

    #endregion
}
