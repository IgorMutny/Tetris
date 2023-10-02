using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    private List<int> figureBag = new List<int>();
    private int nextFigureType;
    private FigureController figure;

    public Action<int> NextFigureSet;

    public void Init()
    {
        figure = GetComponent<FigureController>();
        figure.FigureFixed += Spawn;
    }

    public void NewGameHandle()
    {
        figureBag.Clear();
        RefillFigureBag();
        SetNextFigure();
        Spawn();
    }

    private void Spawn()
    {
        figure.Spawn(nextFigureType);
        SetNextFigure();
    }

    private void RefillFigureBag()
    {
        for (int i = 0; i < 7; i++)
        {
            figureBag.Add(i);
        }
    }

    private void SetNextFigure()
    {
        int typeIndex = UnityEngine.Random.Range(0, figureBag.Count);
        int type = figureBag[typeIndex];

        figureBag.RemoveAt(typeIndex);
        if (figureBag.Count == 0)
        {
            RefillFigureBag();
        }

        nextFigureType = type;
        NextFigureSet?.Invoke(type);
    }
}
