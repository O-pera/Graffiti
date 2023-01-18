using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using static Define;

public class PlayerInputHandler : MonoBehaviour {
    [SerializeField] private PlayerMovement e_playerMovement = null;
    [SerializeField] private PlayerBrain    e_playerBrain    = null;

    #region InputSystem General Events
    public void IS_General_OnMove(InputAction.CallbackContext value) {
        if(e_playerMovement == null)
            return;
        Vector3 inputRaw = value.ReadValue<Vector2>();
        e_playerMovement.InputVector = inputRaw;
    }

    public void IS_General_OnRun(InputAction.CallbackContext value) {
        //TODO: 2022-12-28 �Է¹��� �� �������, Ȱ��ȭ���� üũ�� �ǵ� performed�� �ִ°� �´��� üũ�ϱ�
        e_playerMovement.MoveInput(Status.Run, value.performed, value.canceled);
    }

    public void IS_General_OnCrouch(InputAction.CallbackContext value) {
        //TODO: 2022-12-28 �Է¹��� �� �������, Ȱ��ȭ���� üũ�� �ǵ� performed�� �ִ°� �´��� üũ�ϱ�
        e_playerMovement.MoveInput(Status.Crouch, value.performed, value.canceled);
    }

    public void IS_General_OnJump(InputAction.CallbackContext value) {

    }

    #endregion

    #region InputSystem Wander Events

    #endregion

    #region InputSystem Draw Events
    public void IS_Draw_OnFocus(InputAction.CallbackContext value) {
        e_playerBrain.OnFocus(value.performed);
        e_playerMovement.OnFocus(value.performed);
    }

    public void IS_Draw_OnLeftClick(InputAction.CallbackContext value) {
        //Debug.LogError($"IS_Draw_OnLeftClick: Not Implemented!");
        Debug.LogWarning($"IS_Draw_OnLeftClick Should be Changed!");

        e_playerBrain.OnLeftClick(value.performed);
    }

    public void IS_Draw_OnRightClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnRightClick: Not Implemented!");
    }

    public void IS_Draw_OnMiddleClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnMiddleClick: Not Implemented!");
    }

    public void IS_Draw_OnScroll(InputAction.CallbackContext value) {
        if(value.phase != InputActionPhase.Started)
            return;

        float scrollDelta = value.ReadValue<float>() < 0 ? -0.1f : 0.1f;
        e_playerBrain.OnWheelScroll(scrollDelta);
    }

    public void IS_General_OnEscape(InputAction.CallbackContext value) {
        if(value.phase != InputActionPhase.Started && value.phase != InputActionPhase.Canceled)
            return;

        e_playerBrain.OffInteract();
        e_playerBrain.OnFocus(false, true);
        e_playerMovement.OnFocus(false, true);
    }

    public void IS_Draw_SwitchUIActivation(InputAction.CallbackContext value) {
        if(!value.performed)
            return;

        e_playerBrain.SwitchUIActivation();
    }

    #endregion
}
