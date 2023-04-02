using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum EActorList
{
    Titi,
    Red,
    Blue,
    Yellow,
    NPC1
};

public class DialogLoad : MonoBehaviour
{
    public DialogSave dialogSave;

    // �̺�Ʈ ���̵� 
    [HideInInspector]
    public string eventID_Setting;

    // ���� �ҷ��� �̺�Ʈ�� ��� �迭 
    public string[] dialogArray;

    public EActorList EactorList = EActorList.Titi;

    // �̺�Ʈ ID�� ���� ID�� ���ڷ� �����ϸ� �ش� ��ȭ ������ ��ȯ.
    private string[] GetDialog(string eventID, string actorID)
    {
        Dictionary<string, List<string>> currentDialog;
        if (dialogSave.save.TryGetValue(eventID, out currentDialog))
        {
            List<string> dialogList = new List<string>();
            if (currentDialog.TryGetValue(actorID, out List<string> actorDialog))
            {
                // �ش� �̺�Ʈ�� ���� �ش� ������ ��� ��ȭ ������ ��ȯ�մϴ�.
                foreach (string context in actorDialog)
                {
                    dialogList.Add(context);
                }
            }

            return dialogList.ToArray();
        }

        // �ش� �̺�Ʈ�� ���ų� �ش� ������ ��ȭ ������ ������ �� �迭�� ��ȯ.
        return new string[0];
    }

    private void Start()
    {
        dialogSave = FindObjectOfType<DialogSave>();
    }


    /// <summary>
    /// �̺�Ʈ ���̵� ������ �� �ִ� �޼��� 
    /// </summary>
    /// <param name="_eventID"></param>
    /// <returns></returns>
    public string SetEventID
    {
        get { return eventID_Setting; }
        set { eventID_Setting = value; }
    }

}
