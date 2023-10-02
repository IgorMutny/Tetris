using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    private CellStruct[] cell = new CellStruct[4];

    //sx, sy - start coordinate of the left upper corner of the figure
    private const int sx = 3, sy = 19;
    //cx, cy - coordinates of the center of the figure
    private int cx, cy;

    private bool justInited = true;
    private bool gameIsOn = false;
    private bool gameIsPaused = false;

    private Container container;
    private FixedCells fixedCells;

    public Action FigureFixed;
    public Action FigureUpdated;
    public Action GameOver;

    private struct CellStruct
    {
        public int x;
        public int y;
        public int color;

        public CellStruct(int y, int x, int color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }

    public void Init()
    {
        container = GetComponent<Container>();
        fixedCells = GetComponent<FixedCells>();
        GetComponent<Timer>().Step += Renew;
        GetComponent<KeyboardController>().Move += Move;
        GetComponent<KeyboardController>().Turn += Turn;
        GetComponent<KeyboardController>().Drop += Renew;
        GetComponent<KeyboardController>().Pause += () => gameIsPaused = !gameIsPaused;
    }

    public void NewGameHandle()
    {
        gameIsOn = true;
    }

    public void Spawn(int type)
    {
        switch (type)
        {
            case Cell.TypeI:
                cell[0] = new CellStruct(sy, sx, Cell.ColorI);
                cell[1] = new CellStruct(sy, sx + 1, Cell.ColorI);
                cell[2] = new CellStruct(sy, sx + 2, Cell.ColorI);
                cell[3] = new CellStruct(sy, sx + 3, Cell.ColorI);
                cx = sx + 1;
                cy = sy;
                break;
            case Cell.TypeJ:
                cell[0] = new CellStruct(sy, sx, Cell.ColorJ);
                cell[1] = new CellStruct(sy - 1, sx, Cell.ColorJ);
                cell[2] = new CellStruct(sy - 1, sx + 1, Cell.ColorJ);
                cell[3] = new CellStruct(sy - 1, sx + 2, Cell.ColorJ);
                cx = sx + 1;
                cy = sy - 1;
                break;
            case Cell.TypeL:
                cell[0] = new CellStruct(sy - 1, sx, Cell.ColorL);
                cell[1] = new CellStruct(sy - 1, sx + 1, Cell.ColorL);
                cell[2] = new CellStruct(sy - 1, sx + 2, Cell.ColorL);
                cell[3] = new CellStruct(sy, sx + 2, Cell.ColorL);
                cx = sx + 1;
                cy = sy - 1;
                break;
            case Cell.TypeO:
                cell[0] = new CellStruct(sy, sx + 1, Cell.ColorO);
                cell[1] = new CellStruct(sy, sx + 2, Cell.ColorO);
                cell[2] = new CellStruct(sy - 1, sx + 1, Cell.ColorO);
                cell[3] = new CellStruct(sy - 1, sx + 2, Cell.ColorO);
                cx = sx + 1;
                cy = sy;
                break;
            case Cell.TypeZ:
                cell[0] = new CellStruct(sy, sx, Cell.ColorZ);
                cell[1] = new CellStruct(sy, sx + 1, Cell.ColorZ);
                cell[2] = new CellStruct(sy - 1, sx + 1, Cell.ColorZ);
                cell[3] = new CellStruct(sy - 1, sx + 2, Cell.ColorZ);
                cx = sx + 1;
                cy = sy;
                break;
            case Cell.TypeT:
                cell[0] = new CellStruct(sy - 1, sx, Cell.ColorT);
                cell[1] = new CellStruct(sy, sx + 1, Cell.ColorT);
                cell[2] = new CellStruct(sy - 1, sx + 1, Cell.ColorT);
                cell[3] = new CellStruct(sy - 1, sx + 2, Cell.ColorT);
                cx = sx + 1;
                cy = sy - 1;
                break;
            case Cell.TypeS:
                cell[0] = new CellStruct(sy - 1, sx, Cell.ColorS);
                cell[1] = new CellStruct(sy - 1, sx + 1, Cell.ColorS);
                cell[2] = new CellStruct(sy, sx + 1, Cell.ColorS);
                cell[3] = new CellStruct(sy, sx + 2, Cell.ColorS);
                cx = sx + 1;
                cy = sy;
                break;
        }

        SendNewCells();

        if (IsSpawnedInFixedCells() == true)
        {
            GameOver?.Invoke();
            gameIsOn = false;
        }
    }

    private bool IsSpawnedInFixedCells()
    {
        for (int i = 0; i < 4; i++)
        {
            if (fixedCells.IsCellFilled(cell[i].y, cell[i].x))
            {
                return true;
            }
        }

        return false;
    }

    private void Renew()
    {
        if (gameIsOn == true && gameIsPaused == false)
        {
            if (justInited == false)
            {
                EraseOldCells();
                if (IsLowerPositionsFree() == true)
                {
                    MoveDown();
                }
                else
                {
                    SendCellsToFixed();
                }
                SendNewCells();
            }
            else
            {
                justInited = false;
                SendNewCells();
            }
        }
    }

    private void EraseOldCells()
    {
        for (int i = 0; i < 4; i++)
        {
            container.SetCell(cell[i].y, cell[i].x, 0);
        }
    }

    private void MoveDown()
    {
        for (int i = 0; i < 4; i++)
        {
            cell[i].y--;
        }

        cy--;
    }

    private void SendNewCells()
    {
        for (int i = 0; i < 4; i++)
        {
            container.SetCell(cell[i].y, cell[i].x, cell[i].color);
        }

        FigureUpdated?.Invoke();
    }

    private bool IsLowerPositionsFree()
    {
        for (int i = 0; i < 4; i++)
        {
            if (cell[i].y - 1 < 0)
                return false;

            if (fixedCells.IsCellFilled(cell[i].y - 1, cell[i].x))
                return false;
        }

        return true;
    }

    private void SendCellsToFixed()
    {
        for (int i = 0; i < 4; i++)
        {
            fixedCells.SetCell(cell[i].y, cell[i].x, cell[i].color);
        }

        FigureFixed?.Invoke();
    }

    private void Move(int direction)
    {
        if (gameIsOn == true && gameIsPaused == false)
        {
            if (IsSidePositionsFree(direction) == true)
            {
                EraseOldCells();
                MoveAside(direction);
                SendNewCells();
            }
        }
    }

    private void Turn()
    {
        if (gameIsOn == true && gameIsPaused == false)
        {
            if (IsPositionsForTurnFree() == true)
            {
                EraseOldCells();
                MakeTurn();
                SendNewCells();
            }
        }
    }

    private bool IsPositionsForTurnFree()
    {
        for (int i = 0; i < 4; i++)
        {
            int relX = cell[i].x - cx;
            int relY = cell[i].y - cy;

            int newY = -relX;
            int newX = relY;

            newX += cx;
            newY += cy;

            if (newX < 0 || newX >= Container.Width || newY < 0 || newY >= Container.Height)
            {
                return false;
            }
            else if (fixedCells.IsCellFilled(newY, newX))
            {
                return false;
            }
        }

        return true;
    }

    private void MakeTurn()
    {
        for (int i = 0; i < 4; i++)
        {
            int relX = cell[i].x - cx;
            int relY = cell[i].y - cy;

            int newY = -relX;
            int newX = relY;

            newX += cx;
            newY += cy;

            cell[i].x = newX;
            cell[i].y = newY;
        }
    }

    private bool IsSidePositionsFree(int direction)
    {
        for (int i = 0; i < 4; i++)
        {
            if (cell[i].x + direction < 0 || cell[i].x + direction >= Container.Width)
                return false;

            if (fixedCells.IsCellFilled(cell[i].y, cell[i].x + direction))
                return false;
        }

        return true;
    }

    private void MoveAside(int direction)
    {
        for (int i = 0; i < 4; i++)
        {
            cell[i].x += direction;
        }

        cx += direction;
    }
}
