using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager{
    [SerializeField] private Interactable _curInteractingObj = null;
    public Interactable CurInteractObj { get => _curInteractingObj; }
    private PlayerTest Player {
        get {
            if(_player == null)
                _player = Managers.Player;
            return _player;
        }
    }
    private PlayerTest _player = null;

    public bool EnterInteractWithCurObj(Interactable interactedObject) {
        if(_curInteractingObj == null)
            return false;

        if(_curInteractingObj.Equals(interactedObject) == false)
            return false;

        //TODO: �׸��׸��� �� �ٸ� ��ȣ�ۿ��� ����� Interactable�� Ÿ�Կ� ���� �ø��� ���̾ �����ϵ��� ���� �ʿ�
        //Managers.Cam.DisableLayerMask(LayerMask.NameToLayer("PlayerBody"));
        Player.ChangeInputTypeTo(Define.InputType.Player_Draw);
        return true;
    }

    public bool ExitInteractWithCurObj() {
        if(_curInteractingObj == null)
            return false;

        _curInteractingObj.OffInteract();
        //TODO: �׸��׸��� �� �ٸ� ��ȣ�ۿ��� ����� Interactable�� Ÿ�Կ� ���� �ø��� ���̾ �����ϵ��� ���� �ʿ�
        //Managers.Cam.EnableLayerMask(LayerMask.NameToLayer("PlayerBody"));
        Player.ChangeInputTypeTo(Define.InputType.Player_Wander);
        return true;
    }

    public void ChangeCurIntObj(Interactable newIntObj) {
        if(_curInteractingObj == null) {
            _curInteractingObj = newIntObj;
            return;
        }

        if(_curInteractingObj.Equals(newIntObj))
            return;

        _curInteractingObj = newIntObj;
    }

    /// <summary>
    /// �� ��ȯó�� ��� ��ȣ�ۿ��� ������ �� ����մϴ�.
    /// </summary>
    public void ResetCurIntObj() { _curInteractingObj = null; }

    public void ExtractFromCurIntObj(Interactable disabled) {
        if(_curInteractingObj == null)
            return;

        if(_curInteractingObj.Equals(disabled)) {
            _curInteractingObj = null;
        }

        return;
    }
}
