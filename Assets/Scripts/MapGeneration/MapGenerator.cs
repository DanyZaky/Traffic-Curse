using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.ChessMaze
{
    public class MapGenerator : MonoBehaviour
    {
        public GridVisualizer gridVisualizer;
        public Direction startEdge, exitEdge;
        public bool randomPlacement;
        private Vector3 startPosition, exitPosition;

        [Range(3,20)]
        public int width, length = 11;
        private MapGrid grid;
        
        private void Start()
        {
            grid = new MapGrid(width, length);
            gridVisualizer.VisualizeGrid(width, length);
            MapHelper.RandomlyChooseAndSetStartAndExit(grid, ref startPosition, ref exitPosition, randomPlacement, startEdge, exitEdge);
        }
    }
}

