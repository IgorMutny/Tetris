using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCells : MonoBehaviour
{
    private int[,] cell = new int[Container.Height, Container.Width];

    private Container container;

    public Action FixedCellsUpdated;
    public Action<int> LinesFilled;

    public void Init()
    {
        GetComponent<FigureController>().FigureFixed += Renew;
        container = GetComponent<Container>();
    }

    public void NewGameHandle()
    {
        for (int h = 0; h < Container.Height; h++)
            for (int w = 0; w < Container.Width; w++)
            {
                cell[h, w] = 0;
            }

        Renew();
    }

    public void SetCell(int h, int w, int color)
    {
        cell[h, w] = color;
    }

    public bool IsCellFilled(int h, int w)
    {
        return cell[h, w] > 0;
    }

    private void Renew()
    {
        DeleteFilledLines();
        SendCells();
    }

    private void SendCells()
    {
        for (int h = 0; h < Container.Height; h++)
            for (int w = 0; w < Container.Width; w++)
            {
                container.SetCell(h, w, cell[h, w]);
            }
    }

    private void DeleteFilledLines()
    {
        int filledLines = 0;

        for (int h = 0; h < Container.Height; h++)
        {
            int filledCells = 0;

            for (int w = 0; w < Container.Width; w++)
            {
                if (cell[h, w] > 0)
                {
                    filledCells++;
                }
            }

            if (filledCells == Container.Width)
            {
                filledLines++;

                for (int w = 0; w < Container.Width; w++)
                {
                    cell[h, w] = 0;

                    for (int h1 = h; h1 < Container.Height; h1++)
                    {
                        if (h1 < Container.Height - 1)
                        {
                            cell[h1, w] = cell[h1 + 1, w];
                        }
                        else if (h1 == Container.Height - 1)
                        {
                            cell[h1, w] = 0;
                        }
                    }
                }

                h--;
            }
        }

        if (filledLines > 0)
        {
            LinesFilled?.Invoke(filledLines);
        }
    }
}

