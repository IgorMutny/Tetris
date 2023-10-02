using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private GameObject cellObjectPrefab;

    public const int Width = 10;
    public const int Height = 20;

    private int[,] cell = new int[Height, Width];
    private Cell[,] cellObject = new Cell[Height, Width];

    public void Init()
    {
        for (int h = 0; h < Height; h++)
            for (int w = 0; w < Width; w++)
            {
                cellObject[h, w] = Instantiate(cellObjectPrefab, this.transform).GetComponent<Cell>();
                cellObject[h, w].transform.position = new Vector3(w, h, 0);
                cellObject[h, w].Init();
            }

        GetComponent<FigureController>().FigureUpdated += Renew;
    }

    public void NewGameHandle()
    {
        Renew();
    }

    public void SetCell(int h, int w, int color)
    {
        cell[h, w] = color;
    }

    private void Renew()
    {
        for (int h = 0; h < Height; h++)
            for (int w = 0; w < Width; w++)
            {
                cellObject[h, w].SetState(cell[h, w]);
            }
    }
}
