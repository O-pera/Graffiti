using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


[Serializable]
public class Data
{
    public Vector3 playerPosition;
    public int sceneIndex;

    // ��ųʸ� ���·� Ȱ��ȭ ��Ȱ��ȭ �� ������Ʈ�� �̸��̶� bool �� ����
    public Dictionary<string, bool> activeObjectState;

    public Data()
    {
        activeObjectState = new Dictionary<string, bool>();
    }
}
