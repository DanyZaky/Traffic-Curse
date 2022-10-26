using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MG.AI
{
    public class VertexPosition : IEquatable<VertexPosition>, IComparable<VertexPosition>
    {
        public static List<Vector2Int> possibleNeightbours = new List<Vector2Int>
        {
            new Vector2Int(0, -1),
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0)
        };

        public float totalCost, estimateCost;
        public VertexPosition previousVertex = null;
        private Vector3 position;
        private bool isTaken;

        public int x { get => (int)Position.x; }
        public int y { get => (int)Position.y; }
        public Vector3 Position { get => position; }
        public bool IsTaken { get => isTaken; }

        public VertexPosition(Vector3 position, bool isTaken = false)
        {
            this.position = position;
            this.isTaken = isTaken;
            this.estimateCost = 0;
            this.totalCost = 1;
        }

        public int GetHashCode(VertexPosition obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }

        public int CompareTo(VertexPosition other)
        {
            if (this.estimateCost < other.estimateCost) return -1;
            if (this.estimateCost > other.estimateCost) return 1;
            return 0;
        }

        public bool Equals(VertexPosition other)
        {
            return Position == other.Position;
        }
    }
}


