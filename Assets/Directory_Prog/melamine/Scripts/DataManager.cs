using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // ---�̱������� ����--- //
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    // --- ���� ������ �����̸� ���� ("���ϴ� �̸�(����).json") --- //
    string GameDataFileName = "GameData.json";

    // --- ����� Ŭ���� ���� --- //
    public Data data = new Data();


    // �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(FromJsonData);
            print("�ҷ����� �Ϸ�");

            //SceneManager.LoadScene(data.sceneIndex);

            GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject obj in objects)
            {
                string objName = obj.name;
                bool isActive;

                if(data.activeObjectState.TryGetValue(objName, out isActive))
                {
                    obj.SetActive(isActive);
                }
            }
        }
    }


    // �����ϱ�
    public void SaveGameData()
    {
        data.playerPosition = GameObject.Find("Player").transform.position;
        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        data.board = Minigame_Tel.isBoard;


        // Ȱ��ȭ�� ��� ������Ʈ ����
        data.activeObjectState.Clear();
        // ��Ÿ�� �� �ε�� ������ ��� ������Ʈ ��ȯ
        GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in objects)
        {
            bool isActive = obj.activeSelf;
            string objName = obj.name;

            data.activeObjectState[objName] = isActive;
        }


        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);

        // �ùٸ��� ����ƴ��� Ȯ�� (�����Ӱ� ����)


       
    }
}