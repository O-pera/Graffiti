using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class PuzzleManager : MonoBehaviour
{
    public enum PuzzleType
    {
        Street,
        Building,
        Note
    }

    [Serializable]
    public class PuzzlePiece
    {
        [Header("���� ���� ������Ʈ")]
        public GameObject pieceObject;
        [Header("���� ������Ʈ ")]
        public GameObject correctAnswer;
        [Header("���� �Ÿ� ����")]
        public float snapDistance; // ������ ������Ű�� ���� �Ÿ�
        [Header("���� ������ ���� ��ġ�� ��ġ���� �� �̺�Ʈ")]
        public UnityEvent puzzleSolvedEvent;
    }

    public int totalPieces;
    [Header("���� ����")]
    public List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    [Header("������ ���� ���� ��ġ�� ���������� �� �Ͼ �̺�Ʈ")]
    public UnityEvent endPuzzleEvent;

    private int correctPieces;
    private int activePageIndex; // ���� Ȱ��ȭ�� ������ �ε���
    private bool puzzleComplete;

    public PuzzleType puzzleType;

    private void Start()
    {
        correctPieces = 0;
        activePageIndex = 0;
        puzzleComplete = false;
    }

    private void Update()
    {
        if (!puzzleComplete && correctPieces == totalPieces)
        {
            if(puzzleType == PuzzleType.Note)
            {
                puzzleComplete = true;
                endPuzzleEvent.Invoke();
            }
            else if(puzzleType == PuzzleType.Street)
            {
                puzzleComplete = true;
                endPuzzleEvent.Invoke();
            }  
            
        }
    }

    public void SetActivePageIndex(int index)
    {
        activePageIndex = index;
    }

    public void OnPieceDragged(GameObject piece)
    {
        PuzzlePiece puzzlePiece = GetPuzzlePieceByGameObject(piece);

        if(puzzleType == PuzzleType.Note)
        {
            // ���� �������� Ȱ��ȭ�� ���������� Ȯ��
            if (IsPieceInCorrectPosition(piece, puzzlePiece) && activePageIndex == puzzlePieces.IndexOf(puzzlePiece))
            {
                Draggable draggable = piece.GetComponent<Draggable>();
                draggable.enabled = false;
                draggable.transform.SetParent(puzzlePiece.correctAnswer.transform, false);
                draggable.transform.localPosition = Vector3.zero;

                correctPieces++;
                puzzlePiece.puzzleSolvedEvent.Invoke();
            }
            else
            {
                Draggable draggable = piece.GetComponent<Draggable>();
                draggable.draggableTransform.anchoredPosition = draggable.originalTransform;
            }
        }
        else
        {
            if (IsPieceInCorrectPosition(piece, puzzlePiece))
            {
                Draggable draggable = piece.GetComponent<Draggable>();
                draggable.enabled = false;
                draggable.transform.SetParent(puzzlePiece.correctAnswer.transform, false);
                draggable.transform.localPosition = Vector3.zero;

                correctPieces++;
                puzzlePiece.puzzleSolvedEvent.Invoke();
            }

        }


    }

    private bool IsPieceInCorrectPosition(GameObject piece, PuzzlePiece puzzlePiece)
    {
        float distance = Vector3.Distance(piece.transform.position, puzzlePiece.correctAnswer.transform.position);
        return distance <= puzzlePiece.snapDistance;
    }

    private PuzzlePiece GetPuzzlePieceByGameObject(GameObject piece)
    {
        return puzzlePieces.Find(p => p.pieceObject == piece);
    }



}
