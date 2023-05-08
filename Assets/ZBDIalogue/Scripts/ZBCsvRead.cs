using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ZB.CSV
{
    public class ZBCsvRead : MonoBehaviour
    {
        [SerializeField] string m_path;

        public List<List<Dictionary<string, string>>> m_datas;
        public string[] m_fileNames;

        //[ContextMenu("LoadData")]
        public void LoadData()
        {
            m_datas = ReadAll(m_path, out m_fileNames);
        }

        List<List<Dictionary<string, string>>> ReadAll(string path, out string[] fileNames)
        {
            List<List<Dictionary<string, string>>> result = new List<List<Dictionary<string, string>>>();
            List<Dictionary<string, string>> tempList;

            TextAsset[] sourceFiles = Resources.LoadAll<TextAsset>(path);
            StringReader sr;

            fileNames = new string[sourceFiles.Length];
            for (int i = 0; i < sourceFiles.Length; i++)
            {
                sr = new StringReader(sourceFiles[i].text);
                fileNames[i] = sourceFiles[i].name;

                tempList = new List<Dictionary<string, string>>();

                if (sr != null)
                {
                    string temp = "";
                    int index = 0;
                    string text = sr.ReadToEnd();

                    //�� �и���ų �ؽ�Ʈ ������
                    while (text[index + 1] != '\n')
                    {
                        temp += text[index++];
                    }
                    string[] textKeys = temp.Split(',');
                    index += 2;

                    //����Ʈ �߰� ����
                    bool endLoop = false;
                    while (!endLoop)
                    {
                        tempList.Add(new Dictionary<string, string>());
                        for (int j = 0; j < textKeys.Length; j++)
                        {
                            temp = "";

                            //�ٹٲ� �ִ� �ؽ�Ʈ�� ���
                            if (text[index] == '\"')
                            {
                                index++;
                                while (text[index] != '\"')
                                {
                                    temp += text[index++];
                                }

                                if (j + 1 >= textKeys.Length)
                                    index += 3;
                                else
                                    index += 2;
                            }

                            //�ٹٲ� ���� �ؽ�Ʈ�� ���
                            else
                            {
                                while (text[index] != ',' && text[index] != '\n')
                                {
                                    temp += text[index++];
                                }
                                index++;
                            }
                            tempList[tempList.Count - 1].Add(textKeys[j], temp);

                            if (index + 1 >= text.Length)
                            {
                                endLoop = true;
                                break;
                            }
                        }
                    }
                }

                result.Add(tempList);
            }

            return result;
        }
    }
}