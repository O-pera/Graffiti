using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSave : MonoBehaviour
{
    // CSV ���� �̸�
    private string fileName;
    [SerializeField] private TextAsset csv_data = null;

    // CSV ���Ͽ��� ������ �����͸� ������ ����Ʈ
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

    // EventID�� �������� ActorID�� Context�� �׷�ȭ�Ͽ� ������ ��ųʸ�
    public Dictionary<string, Dictionary<string, List<string>>> save = new Dictionary<string, Dictionary<string, List<string>>>();
    // Start �޼ҵ忡�� CSV ������ �о �����͸� �����ϰ� DialogSave ��ųʸ��� ���� �����մϴ�.
    void Start()
     {
         fileName = csv_data.name;
         data = CSVParser.Read(fileName);

         foreach (var row in data)
         {

             string eventID = row["EventID"].ToString();
             string actorID = row["ActorID"].ToString();
             string context = row["Context"].ToString();

            if (!save.ContainsKey(eventID))
            {
                save[eventID] = new Dictionary<string, List<string>>();
            }

            // ActorID�� ��ųʸ��� ������ ���ο� List�� �߰��մϴ�.
            if (!save[eventID].ContainsKey(actorID))
            {
                save[eventID][actorID] = new List<string>();
            }

            save[eventID][actorID].Add(context);
        }
     }

    public void SaveDialog(string eventID, string actorID, string context)
    {
        if (!save.ContainsKey(eventID))
        {
            save[eventID] = new Dictionary<string, List<string>>();
        }
        if (!save[eventID].ContainsKey(actorID))
        {
            save[eventID][actorID] = new List<string>();
        }

        save[eventID][actorID].Add(context);
    }
}