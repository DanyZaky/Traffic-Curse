using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace MG.ChessMaze
{
    public class MapGrid
    {
        private int width, length;
        private Cell[,] cellGrid;

        public int Width { get => width; }
        public int Length { get => length; }

        public MapGrid(int width, int length)
        {
            this.width = width;
            this.length = length;
            CreateGrid();
        }

        private void CreateGrid()
        {
            cellGrid = new Cell[length, width];
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    cellGrid[row, col] = new Cell(col, row);
                }
            }
        }

        public void SetCell(int x, int y, CellObjectType objType, bool isTaken = false)
        {
            cellGrid[y, x].ObjType = objType;
            cellGrid[y, x].IsTaken = isTaken;
        }

        public void SetCell(float x, float y, CellObjectType objType, bool isTaken = false)
        {
            SetCell((int)x, (int)y, objType, isTaken);
        }

        public bool IsCellTaken(int x, int y)
        {
            return cellGrid[y, x].IsTaken;
        }

        public bool IsCellTaken(float x, float y)
        {
            return cellGrid[(int)y, (int)x].IsTaken; 
        }

        public bool IsCellValid(float x, float y)
        {
            if (x >= width || x < 0 || y >= length || y < 0)
            {
                return false;
            }
            return true;
        }

        public Cell GetCell(int x, int y)
        {
            if(IsCellValid(x,y) == false)
            {
                return null;
            }
            return cellGrid[y, x];
        }

        public Cell GetCell(float x, float y)
        {
            return GetCell((int)x, (int)y);
        }

        public int CalculateIndexFromCoordinates(int x, int y)
        {
            return x + y * width;
        }

        public int CalculateIndexFromCoordinates(float x, float y)
        {
            return (int)x + (int)y * width;
        }

        public void CheckCoordinates()
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                StringBuilder b = new StringBuilder();
                for (int j = 0; j < cellGrid.GetLength(1); j++)
                {
                    b.Append(j + "," + i + " ");
                }
                Debug.Log(b.ToString());
            }
        }
    }

}

