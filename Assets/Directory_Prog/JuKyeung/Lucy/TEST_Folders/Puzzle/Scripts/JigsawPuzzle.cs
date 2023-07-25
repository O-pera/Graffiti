using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPuzzle : MonoBehaviour
{
    public GameObject puzzlePosSet; // ���� ��ġ�� �����ϴ� �θ� ������Ʈ�� ���� ����
    public GameObject puzzlePieceSet; // ���� ������ �����ϴ� �θ� ������Ʈ�� ���� ����

    public bool IsClear()
    {
        for (int i = 0; i < puzzlePosSet.transform.childCount; i++)
        {
            if (puzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                Debug.Log("JigSawPuzzle.IsClear(): ���� ������ �̵������� �ùٸ� ��ġ�� ��ġ���� �ʾҽ��ϴ�.");
                return false;
            }

            else if (puzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().piece_no != i)
            {
                Debug.Log("JigSawPuzzle.IsClear(): ���� ������ �ùٸ� ��ġ�� ��ġ���� �ʾҽ��ϴ�.");
                return false;
            }
        }
        Debug.Log("JigSawPuzzle.IsClear(): ������ �ϼ��Ǿ����ϴ�!");

        return true;
    }
}
