using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int snapOffset = 60; // ���� ������ ��ġ�� ���߱� ���� ������ �Ÿ�
    public JigsawPuzzle puzzle; // JigsawPuzzle ��ũ��Ʈ�� ���� ����
    public int piece_no; // ���� ������ ��Ÿ���� ��ȣ

    void Awake()
    {
        puzzle = FindObjectOfType<JigsawPuzzle>(); 
    }

    void Start()
    {
        piece_no = gameObject.name[gameObject.name.Length - 1] - '0'; 
    }

    void Update()
    {
        //...

    }

    bool CheckSnapPuzzle()
    {

        for (int i = 0; i < puzzle.puzzlePosSet.transform.childCount; i++)
        {
            //��ġ�� �ڽĿ�����Ʈ�� ������ �̹� ���������� ������ ���Դϴ�.
            if (puzzle.puzzlePosSet.transform.GetChild(i).childCount != 0)
            {
                continue;
            }
            else if (Vector2.Distance(puzzle.puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
            {
                if(piece_no == i)
                {
                    transform.SetParent(puzzle.puzzlePosSet.transform.GetChild(i).transform);
                    transform.localPosition = Vector3.zero;
                    return true;
                }
            }
        }
        return false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // �巡���ϴ� ���� ���� ������ ��ġ�� ������Ʈ�մϴ�.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //��ġ�ϴ� ��ġ�� ���� ��� �θ��ڽ� ���踦 �����մϴ�.
        if (!CheckSnapPuzzle())
        {
            transform.SetParent(puzzle.puzzlePieceSet.transform);
        }

        if (puzzle.IsClear())
        {
            Debug.Log("����~ Clear");
        }

    }

}
