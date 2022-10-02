using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.ChessMaze
{
    public class Cell
    {
        private int x, y;
        private bool isTaken;
        private CellObjectType objType;

        public int Y { get => y;}
        public int X { get => x;}
        public bool IsTaken { get => isTaken; set => isTaken = value; }
        public CellObjectType ObjType { get => objType; set => objType = value; }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.objType = CellObjectType.Empty;
            IsTaken = false;
        }
    }

    public enum CellObjectType
    {
        Empty,
        Road,
        Obstacle,
        Start,
        Exit
    }
}

