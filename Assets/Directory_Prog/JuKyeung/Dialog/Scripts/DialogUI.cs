using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using DG.DemiLib;

public class DialogUI : MonoBehaviour
{
    [Header("���̾�α� UI ����")]
    public GameObject dialog_bubble; // ���̾�α� ������Ʈ
    [SerializeField] TextMeshProUGUI dialog_Context = null; // ���̾�α� �ؽ�Ʈ UI

    [Header("Actor ���� ���̾�α� ��ġ")]
    public GameObject titi_Dialog_HeadUpPos; // ���̾�α� ��ġ ������

    [Header("UI ��� ī�޶�")]
    [SerializeField] Camera lookCamera;

    [SerializeField] bool playerCheck ;

    //string dialog_FileName; // csv_Dialoog �� ���� �̸�
    //[SerializeField] private TextAsset csv_Dialog = null;

    //List<Dictionary<string, object>> dialog_Data;

    [Header("���� �̺�Ʈ ID")]
    public int nowEventID;

    int nowIndex = 0;

    private void Awake()
    {
        dialog_bubble.SetActive(false);
        //dialog_FileName = csv_Dialog.name;
        //dialog_Data = CSVParser.Read(dialog_FileName);
    }

    private void Update()
    {
        DialogPosition();
    }

    public void GetContext()  // TextMeshProUGUI nowText
    {
        dialog_bubble.SetActive(true);
        //dialog_Context.text = dialog_Data[nowIndex]["Context"].ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            //dialog_Context.text = dialog_Data[nowIndex]["Context"].ToString();
            TMProUGUIDoText.DoText(dialog_Context, 30f);
            Debug.Log(dialog_Context.text);
            nowIndex++;
        }
        
    }

    /// <summary>
    /// ���̾�α״� ī�޶� �׻� �ٶ󺸵��� �����Ǿ� �ֽ��ϴ�. 
    /// </summary>
    void DialogPosition()
    {
        dialog_bubble.transform.position = titi_Dialog_HeadUpPos.transform.position;
        dialog_bubble.transform.LookAt(lookCamera.transform);
    }

    /// <summary>
    /// ���� �̺�Ʈ ����� actorID �� ���� ��µǴ� ���̾�α� �ڽ��� ��ġ�� ã�ƿɴϴ�. 
    /// </summary>
    public void FindActorPos()
    {
        // ���õ� ���� ID�� �´� GameObject�� ã�ƿɴϴ�.
        GameObject actorObject = actorDialogPos.FirstOrDefault(x => x.name.Contains(EactorList.ToString()));

        // ���� GameObject�� �ڽ� �� ���̾�α� �ڽ� ��ġ�� ��Ÿ���� "DialogPos" �±׸� ���� ������Ʈ�� ã�ƿɴϴ�.
        Transform dialogPosition = actorObject.transform.Find("DialogPos");

        // ���̾�α� �ڽ� ��ġ�� dialogPosition ��ġ�� �̵��մϴ�.
        dialog_bubble.transform.position = dialogPosition.position;

        // ���̾�α� �ڽ��� ī�޶� �ٶ󺸵��� �մϴ�.
        dialog_bubble.transform.LookAt(lookCamera.transform);
    }


    /// <summary>
    /// richText(���̾�α��� ���� �ڵ� �� )�� �����ϱ� ���� �޼���
    /// </summary>
    /// <param name="richText"></param>
    /// <returns></returns>
    public float TextLenght(string richText)
    {
        float len = 0;
        bool inTag = false;

        foreach (var ch in richText)
        {
            if (ch == '<')
            {
                inTag = true;
                continue;
            }
            else if (ch == '>')
            {
                inTag = false;
            }
            else if (!inTag)
            {
                len++;
            }
        }
        Debug.Log(len);
        return len;
    }
}

public static class TMProUGUIDoText
{
    public static void DoText(this TextMeshProUGUI _text, float _duration)
    {
        _text.maxVisibleCharacters = 0;
        DOTween.To(x => _text.maxVisibleCharacters = (int)x, 0f, _text.text.Length, _duration / _text.text.Length);
    }
}
