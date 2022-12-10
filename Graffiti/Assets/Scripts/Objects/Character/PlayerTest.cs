using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Define;


public class PlayerTest : MonoBehaviour {
    #region Components
    [SerializeField] private CharacterController _controller = null;

    #endregion

    #region External Objects
    [SerializeField] Transform _camTransform = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    [SerializeField] private Vector3 m_inputDirNormal = Vector3.zero;
    [SerializeField] private Vector3 m_moveDir = Vector3.zero;

    [SerializeField] private float m_currentSpeed = 0.0f;

    private byte isInputVoid = 0;
    private byte isNotOpposite = 0;
    #endregion


    #region Unity Event Functions
    private void Update() {
        isInputVoid   = (byte)( 1 - m_inputDirNormal.magnitude );
        isNotOpposite = (byte)( ( m_moveDir.normalized + m_inputDirNormal ).normalized.magnitude );

        float targetAngle = Mathf.Atan2(isInputVoid.Not() * m_inputDirNormal.x + 
                                        isInputVoid * m_moveDir.x, 
                                        isInputVoid.Not() * m_inputDirNormal.z + 
                                        isInputVoid * m_moveDir.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
        targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        m_currentSpeed += m_Stat.acceleration * isInputVoid.Not() * isNotOpposite              // Input���� ���� ��� 0, ���� ��� ���� �̵������ ���ؼ� �ݴ������ ��� ����, �������� ���� ���.
                        + m_Stat.damping      * isInputVoid                             * -1   // Input���� ���� ��� -damping �ӵ��� ����.
                        + m_Stat.damping      * isInputVoid.Not() * isNotOpposite.Not() * -1;

        m_currentSpeed = Mathf.Clamp(m_currentSpeed, 0.0f, m_Stat.speed);

        m_moveDir = m_moveDir        * isInputVoid +                                                                // Input���� ���� �� m_moveDir�� �״�� �����Ѵ�.
                    m_inputDirNormal * isInputVoid.Not() * isNotOpposite +                                          // Input���� ���� ����� ����� �帧�� ��� Input�������� �ٲ۴�.
                    m_moveDir        * isInputVoid.Not() * isNotOpposite.Not() +                                    // Input���� ���ݴ��� ��� �״�� �����Ѵ�.
                    m_inputDirNormal * isInputVoid.Not() * isNotOpposite.Not() * ( m_currentSpeed <= 1f ? 1 : 0);   // ������ �ݴ��� �� Damping�Ǿ ���� �ӵ��� 1f ���Ϸ� �������� Input�������� �ٲ۴�.

        if(m_currentSpeed <= 0.1f)
            return;
            
        _controller.Move(m_moveDir.normalized * m_currentSpeed * Time.deltaTime);
    }

    #endregion

    #region User Defined Functions
    /// <summary>
    /// �Էµ� ���� <see cref="UnityEvent"/>���Լ� �޾ƿɴϴ�.
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputAction.CallbackContext value) {
        if(_camTransform == null)
            return;

        Vector3 inputRaw = value.ReadValue<Vector2>();
        inputRaw = Quaternion.Euler(new Vector3(0, _camTransform.eulerAngles.y, 0)) * new Vector3(inputRaw.x, 0, inputRaw.y);
        inputRaw.x = (float)Math.Round(inputRaw.x, 0);
        inputRaw.y = (float)Math.Round(inputRaw.y, 0);
        inputRaw.z = (float)Math.Round(inputRaw.z, 0);

        m_inputDirNormal = inputRaw.normalized;

    }
    #endregion
}
