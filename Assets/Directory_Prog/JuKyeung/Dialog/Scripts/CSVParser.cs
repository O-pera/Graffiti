using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;
using System;

public class CSVParser : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file) // CSV ������ �о���� file �Ű������� �޾Ƽ�  List~~ �������� ��ȯ... 
    {
        var list = new List<Dictionary<string, object>>(); // list = ����Ʈ �ʱ�ȭ
        TextAsset data = Resources.Load(file) as TextAsset; // TextAsset data =  ���� �ȿ� CSV ������ �о�� 

        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // lines = �Ҵ�� CSV ������� �ٴ����� ����

        string csvText = file.Substring(0, file.Length - 1); // file �Ű��������� ������ ���ڸ� ������ ���ڿ��� ���� // �����ص� �ɵ� 

        if (lines.Length <= 1) return list; // lines �� ���ҵ� ���� ���� 1 ���϶�� �� ����Ʈ�� ��ȯ

        var header = Regex.Split(lines[0], SPLIT_RE); // header = lines�� 0��° ���� ���� EventName| ActorID|  Context|
        for (var i = 1; i < lines.Length; i++) // CSV ���� ������ ���� �����Ͽ� ��ųʸ� ���·� ���� 
        {
            var values = Regex.Split(lines[i], SPLIT_RE); // values �� 1��° �ٺ��� �ϳ��� �ش� ���� ������� �ٽ� �����Ѵ�. -> ��׶���  |  |  | 
            //if (values.Length == 0 || values[0] == "") continue; // ����ó��. values�� ������ ���ų� 0��°���� �ٽ� �ε� ?

            if (values.Length == 0) continue; // *** ���� �κ�  �ش� ���� ������� ��� �ǳʵ۴�. 

            var entry = new Dictionary<string, object>(); // entty = �� ���������� ���� ������ ��ųʸ� ����
            for (var j = 0; j < header.Length && j < values.Length; j++) // ���ҵ� CSV ������ ���پ� ó��, �� ���� ������� �ٽ� ����,�� ���� ��ųʸ� ���·� ����...
            {
                string value = values[j]; // CSV ���Ͽ��� ���ҵ� �� ���� ���� values �� �� �� �� ó���ϴ� �κ�
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value; // �ش� ó���� �Ϸ�� ���� ���ڿ��� object ���Ŀ� ����
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f)) // value ���ڿ��� ���� int ������������ ��ȯ���� f �� ���� 
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue; // ���ҵ� �� ���� ������ ��ųʸ� ���·� ���� [header[j]] �� �� ���� �̸�(���) 
            }
            list.Add(entry);
        }
        return list;
    }
}
