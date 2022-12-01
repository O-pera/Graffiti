using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class InputManager : MonoBehaviour {
    #region Singleton
    private static InputManager _instance = null;
    public static InputManager Instance { get => _instance; }

    #endregion

    #region Local Variables
    [SerializeField] private InputableObject _currentInputHandler = null;
                     private InputableObject _prevInputHandler = null;
    #endregion

    #region Properties


    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        _currentInputHandler.HandleInput();
    }

    #endregion

    #region User Defined Functions
    /// <summary>
    /// Input���� ������ <see cref="InputableObject"/> �ν��Ͻ��� �����մϴ�.
    /// Start() �̺�Ʈ �Լ� ���ĺ��� ȣ�� �����մϴ�.
    /// </summary>
    /// <returns>true if Interruption successed, else false.</returns>
    public bool InterruptHandleInput(InputableObject input) {
        if(input.Equals(_currentInputHandler))
            return false;

        if(_currentInputHandler != null && input.Priority < _currentInputHandler.Priority)
            return false;

        _prevInputHandler = _currentInputHandler;
        _currentInputHandler = input;
        return true;
    }

    /// <summary>
    /// ���� Input���� ���� ���� �ν��Ͻ��� �����ϰ�, ���� �ν��Ͻ��� �ٽ� �����ɴϴ�.
    /// </summary>
    /// <returns> true if _currentInputHandler successfully switched to _prevInputController, else false</returns>
    public bool ReleaseHandleInput() {
        if(_prevInputHandler.Equals(null) || 
           _prevInputHandler.Equals(_currentInputHandler))
            return false;

        _currentInputHandler = _prevInputHandler;
        return true;
    }

    #endregion
}
