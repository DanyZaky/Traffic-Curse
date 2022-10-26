using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MG.ChessMaze
{
    public class CandidateMap
    {
        private MapGrid grid;
        private int numberOfPieces = 0;
        private bool[] obstaclesArray = null;
        private Vector3 startPoint, exitPoint;
        private List<KnightPiece> knigtPiecesList = new List<KnightPiece>();

        public MapGrid Grid { get => grid; }
        public bool[] ObstaclesArray { get => obstaclesArray; }

        public CandidateMap(MapGrid grid, int numberOfPieces)
        {
            this.numberOfPieces = numberOfPieces;
            
            this.grid = grid;
        }

        public void CreateMap(Vector3 startPosition, Vector3 exitPosition, bool autoRepair = false)
        {
            this.startPoint = startPosition;
            this.exitPoint = exitPosition;
            obstaclesArray = new bool[grid.Width * grid.Length];
            this.knigtPiecesList = new List<KnightPiece>();
            RandomlyPlaceKnightPieces(this.numberOfPieces);

            PlaceObstacles();
        }

        private bool CheckIfPositionCanBeObstacle(Vector3 position)
        {
            if(position == startPoint || position == exitPoint)
            {
                return false;
            }
            int index = grid.CalculateIndexFromCoordinates(position.x, position.y);

            return obstaclesArray[index] == false;
        }

        private void RandomlyPlaceKnightPieces(int numberOfPieces)
        {
            var count = numberOfPieces;
            var knightPlacementTryLimit = 100;
            
            while(count > 0 && knightPlacementTryLimit > 0)
            {
                var randomIndex = Random.Range(0, obstaclesArray.Length);
                if(obstaclesArray[randomIndex] == false)
                {
                    var coordinates = grid.CalculateIndexFromCoordinates(randomIndex);
                    if(coordinates == startPoint || coordinates == exitPoint)
                    {
                        continue;
                    }
                    obstaclesArray[randomIndex] = true;
                    knigtPiecesList.Add(new KnightPiece(coordinates));

                    count--;
                }

                knightPlacementTryLimit--;
            }
        }

        private void PlaceObstaclesForThisKnight(KnightPiece knight)
        {
            foreach (var position in KnightPiece.listOfPosssibleMoves)
            {
                var newPosition = knight.Position + position;
                if (grid.IsCellValid(newPosition.x, newPosition.y) && CheckIfPositionCanBeObstacle(newPosition))
                {
                    obstaclesArray[grid.CalculateIndexFromCoordinates(newPosition.x, newPosition.y)] = true;
                }
            }
        }

        private void PlaceObstacles()
        {
            foreach (var knight in knigtPiecesList)
            {
                PlaceObstaclesForThisKnight(knight);
            }
        }

        public MapData ReturnMapData()
        {
            return new MapData
            {
                obstacleArray = this.obstaclesArray,
                knightPiecesList = knigtPiecesList,
                startPosition = startPoint,
                exitPosition = exitPoint
            };
        }
    }
}
