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
    [Header("Camera Objects")]
    [SerializeField] private Transform t_mainCam = null;

    [Header("Current Interactable Object")]
    [SerializeField] private Interactable _interactable = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    private MovementType m_curMoveType    = MovementType.Walk;
    private Vector3      m_inputDirNormal = Vector3.zero;
    private Vector3      m_moveDir        = Vector3.zero;
    private Vector3      m_VerticalForce  = Vector3.down;

    private float        m_currentSpeed   = 0.0f;
    private byte         _isBraking = 0;

    #endregion

    #region Unity Event Functions

    private void Update() {
        //TODO: ���� �߰� �� ��� ���� �ʿ�.

        byte isInputVoid   = (byte)( 1 - m_inputDirNormal.magnitude );  
        // Input���� Vector3.zero�� ��� true, �������� false.

        byte isNotOpposite = (byte)( m_moveDir.normalized + m_inputDirNormal ).normalized.magnitude;    
        // ���� Input���� ���� m_moveDir���� ���ݴ� ������ ��� true, �������� false.

        _isBraking = (byte)(( _isBraking.Not() * isInputVoid.Not() * isNotOpposite.Not() * m_currentSpeed > m_Stat.moveType[(byte)MovementType.Walk].speed ? 1 : 0 ) + 
                            ( _isBraking       * m_currentSpeed > 1f ? 1 : 0)); 
        //Input���� m_moveDir������ ���ݴ�ų� ���� Braking ������ ��� true, �������� false.

        byte isDeceleration = (byte)Mathf.Min((m_currentSpeed > m_Stat.moveType[(byte)m_curMoveType].speed ? 1 : 0) + isNotOpposite.Not(), 1);  
        //���� �ӵ��� MovementType���� ���� �ӵ��� MovementType���� �Ѿ���� �� ���� �ӵ��� ���� MovementType�� �ְ� �ӵ����� ���� ��� true, �������� false.

        {
            float targetAngle = Mathf.Atan2(isInputVoid.Not() * _isBraking.Not() * m_inputDirNormal.x + isInputVoid * m_moveDir.x + _isBraking * -m_moveDir.x,
                                        isInputVoid.Not() * _isBraking.Not() * m_inputDirNormal.z + isInputVoid * m_moveDir.z + _isBraking * -m_moveDir.z
                                        ) * Mathf.Rad2Deg + t_mainCam.eulerAngles.y;
            targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }// Player Rotation By Input Direction

        {
            m_currentSpeed += m_Stat.moveType[(byte)m_curMoveType].acceleration * isInputVoid.Not() * isNotOpposite * isDeceleration.Not() * _isBraking.Not()              // Input���� ���� ��� 0, ���� ��� ���� �̵������ ���ؼ� �ݴ������ ��� ����, �������� ���� ���.
                            + m_Stat.moveType[(byte)m_curMoveType].damping * isInputVoid * -1   // Input���� ���� ��� -damping �ӵ��� ����.
                            + m_Stat.moveType[(byte)m_curMoveType].damping * isInputVoid.Not() * isNotOpposite.Not() * _isBraking.Not() * -1
                            + m_Stat.moveType[(byte)m_curMoveType].damping * _isBraking * -1
                            + m_Stat.moveType[(byte)m_curMoveType].damping * isDeceleration * -1;
            m_currentSpeed = Mathf.Clamp(m_currentSpeed, 0.0f, m_Stat.moveType[(byte)MovementType.Run].speed);
            

            m_moveDir = m_moveDir * isInputVoid +                                                                // Input���� ���� �� m_moveDir�� �״�� �����Ѵ�.
                        m_inputDirNormal * isInputVoid.Not() * isNotOpposite +                                          // Input���� ���� ����� ����� �帧�� ��� Input�������� �ٲ۴�.
                        m_moveDir * isInputVoid.Not() * isNotOpposite.Not() +                                    // Input���� ���ݴ��� ��� �״�� �����Ѵ�.
                        m_inputDirNormal * isInputVoid.Not() * isNotOpposite.Not() * (1 - _isBraking) * ( m_currentSpeed <= 0.5f ? 1 : 0 );   // ������ �ݴ��� �� Damping�Ǿ ���� �ӵ��� 1f ���Ϸ� �������� Input�������� �ٲ۴�.
        }// Calculating Player's Current Speed and Absolute moving direction 

        _controller.Move(m_moveDir.normalized * m_currentSpeed * Time.deltaTime + m_VerticalForce * 9.8f * Time.deltaTime);
    }

    #endregion

    #region User Defined Functions

    public void Notify_OnInteractArea(Interactable onRanged) {
        if(_interactable == onRanged)
            return;

        _interactable = onRanged;
    }

    public void Notify_OffInteractArea(Interactable outRanged) {
        if(_interactable != outRanged)
            return;

        _interactable = null;
    }

    #endregion
}
