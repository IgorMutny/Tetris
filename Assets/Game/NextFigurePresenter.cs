using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFigurePresenter : MonoBehaviour
{
    [SerializeField] private GameObject cellObjectPrefab;

    private const int height = 2;
    private const int width = 4;

    private const float startHeight = 16.5f;
    private const float startWidth = 11;

    private int[,] cell = new int[height, width];
    private Cell[,] cellObject = new Cell[height, width];

    public void Init()
    {
        GetComponent<FigureSpawner>().NextFigureSet += DrawNextFigure;

        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                cellObject[h, w] = Instantiate(cellObjectPrefab, this.transform).GetComponent<Cell>();
                cellObject[h, w].transform.position = new Vector3(startWidth + w, startHeight + h, 0);
                cellObject[h, w].Init();
            }
    }

    private void DrawNextFigure(int type)
    {
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                cell[h, w] = 0;
            }

        switch (type)
        {
            case Cell.TypeI:
                cell[1, 0] = Cell.ColorI;
                cell[1, 1] = Cell.ColorI;
                cell[1, 2] = Cell.ColorI;
                cell[1, 3] = Cell.ColorI;
                break;
            case Cell.TypeJ:
                cell[1, 0] = Cell.ColorJ;
                cell[0, 0] = Cell.ColorJ;
                cell[0, 1] = Cell.ColorJ;
                cell[0, 2] = Cell.ColorJ;
                break;
            case Cell.TypeL:
                cell[0, 0] = Cell.ColorL;
                cell[0, 1] = Cell.ColorL;
                cell[0, 2] = Cell.ColorL;
                cell[1, 2] = Cell.ColorL;
                break;
            case Cell.TypeO:
                cell[1, 1] = Cell.ColorO;
                cell[1, 2] = Cell.ColorO;
                cell[0, 1] = Cell.ColorO;
                cell[0, 2] = Cell.ColorO;
                break;
            case Cell.TypeZ:
                cell[1, 0] = Cell.ColorZ;
                cell[1, 1] = Cell.ColorZ;
                cell[0, 1] = Cell.ColorZ;
                cell[0, 2] = Cell.ColorZ;
                break;
            case Cell.TypeT:
                cell[0, 0] = Cell.ColorT;
                cell[0, 1] = Cell.ColorT;
                cell[1, 1] = Cell.ColorT;
                cell[0, 2] = Cell.ColorT;
                break;
            case Cell.TypeS:
                cell[0, 0] = Cell.ColorS;
                cell[0, 1] = Cell.ColorS;
                cell[1, 1] = Cell.ColorS;
                cell[1, 2] = Cell.ColorS;
                break;
        }

        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                cellObject[h, w].GetComponent<Cell>().SetState(cell[h, w]);
            }
    }
}
