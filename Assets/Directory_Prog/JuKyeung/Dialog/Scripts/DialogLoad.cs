using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogLoad : MonoBehaviour
{
    // ����� ��ȭ ������ ������ DialogSave ��ũ��Ʈ�� �ν��Ͻ�
    public DialogSave dialogSave;

    public string eventID_Setting;

    public string[] dialogArray;

    // �̺�Ʈ ID�� ���� ID�� ���ڷ� �����ϸ� �ش� ��ȭ ������ ��ȯ�մϴ�.
    private string[] GetDialog(string eventID)
    {
        Dictionary<string, List<string>> currentDialog;
        if (dialogSave.save.TryGetValue(eventID, out currentDialog))
        {
            List<string> dialogList = new List<string>();
            // �ش� �̺�Ʈ�� ���� ��� ��ȭ ������ ��ȯ�մϴ�.

            foreach (KeyValuePair<string, List<string>> pair in currentDialog)
            {
                string actorID = pair.Key;
                foreach (string context in pair.Value)
                {
                    dialogList.Add(actorID + " | " + context);
                }
            }

            return dialogList.ToArray();
        }

        // �ش� �̺�Ʈ�� ������ �� �迭�� ��ȯ�մϴ�.
        return new string[0];
    }

    private void Start()
    {
        // DialogSave Ŭ���� �ν��Ͻ��� �Ҵ��մϴ�.
        dialogSave = FindObjectOfType<DialogSave>();

        // eventID�� �ش��ϴ� ��ȭ ������ �����ͼ� ���� ���ڷ� ���е� ���ڿ��� ����մϴ�.
        //Debug.Log(string.Join("\n", GetDialog(eventID)));
        

    }

    private void Update()
    {
        LoadDialog();
    }

    /// <summary>
    /// �ϴ� �ҷ��� ��
    /// </summary>
    public void LoadDialog()
    {
        dialogArray = GetDialog(eventID_Setting);
        for (int i = 0; i < /*GetDialog(eventID_Setting).Length*/ dialogArray.Length; i++)
        {
            Debug.Log(GetDialog(eventID_Setting)[i]);

        }
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