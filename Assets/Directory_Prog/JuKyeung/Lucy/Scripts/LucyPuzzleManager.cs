using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LucyPuzzleManager : MonoBehaviour
{
    public UnityEvent LucyPuzzleCompletEvent;

    public bool isAllPuzzleClear;
    private int clearPuzzleIndex = 4;
    public int currentClearPuzzle = 0;

    private void Start()
    {
        currentClearPuzzle = 0;
        isAllPuzzleClear = false;
    }

    public void LucyPuzzleClearCheck(int _clear)
    {
        currentClearPuzzle += _clear;
    }

    public void Update()
    {
        if(currentClearPuzzle == clearPuzzleIndex)
        {
            isAllPuzzleClear = true;
            LucyPuzzleCompletEvent.Invoke();
        }
    }
}
