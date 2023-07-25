using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RollerMode : MonoBehaviour
{
    [Header("�ѷ� ���콺 ����Ʈ")]
    public Texture2D rollerCursur;


    private void Start()
    {
        RollerModeStart();
    }

    public void RollerModeStart()
    {
        Cursor.SetCursor(rollerCursur, Vector2.zero, CursorMode.Auto);
    }


}

