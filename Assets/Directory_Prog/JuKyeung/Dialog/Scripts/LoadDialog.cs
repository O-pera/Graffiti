using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialog : MonoBehaviour
{
    private DialogLoad dialogLoad;
    void Start()
    {
        dialogLoad = GameObject.FindObjectOfType<DialogLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        // getEventID ���� �о�ͼ� ���
        string getEventID = dialogLoad.eventID_Setting;
    }

    /// <summary>
    /// �̷������� �̺�Ʈ�� �Ҵ��ؼ� ������ �ҷ��� �� ���� -> ��� ��ü�� dialogArray �� ������� �ϳ��� ��ȣ�ۿ��� �� ������ �ҷ����� ������ ������
    /// </summary>
    public void Event101()
    {
        dialogLoad.eventID_Setting = ChangeEventID("100101");
    }
    public void Event401()
    {

        dialogLoad.eventID_Setting = ChangeEventID("100401"); // 
    }
    

    string ChangeEventID(string _currentEventID)
    {
        return _currentEventID;
    }
}
