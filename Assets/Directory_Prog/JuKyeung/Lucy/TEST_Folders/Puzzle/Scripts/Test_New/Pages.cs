using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pages : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [NonSerialized] public int currentPageIndex = 0;  // ���� ������ �ε���

    public PuzzleManager puzzleManager;

    public void ShowNextPage()
    {
        pages[currentPageIndex].SetActive(false);  // ���� ������ ��Ȱ��ȭ
        currentPageIndex++;
        if (currentPageIndex >= pages.Length)
            currentPageIndex = 0;

        pages[currentPageIndex].SetActive(true);  // ���� ������ Ȱ��ȭ
        puzzleManager.SetActivePageIndex(currentPageIndex);
    }

    public void ShowPreviousPage()
    {
        pages[currentPageIndex].SetActive(false);  // ���� ������ ��Ȱ��ȭ
        currentPageIndex--;
        if (currentPageIndex < 0)
            currentPageIndex = pages.Length - 1;

        pages[currentPageIndex].SetActive(true);  // ���� ������ Ȱ��ȭ
        puzzleManager.SetActivePageIndex(currentPageIndex);
    }
}
