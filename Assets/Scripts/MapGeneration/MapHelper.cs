using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MG.ChessMaze
{
    public static class MapHelper
    {
        public static void RandomlyChooseAndSetStartAndExit(MapGrid grid, ref Vector3 startPosition, ref Vector3 exitPosition, bool randomPlacement, Direction startPositionEdge = Direction.Left, Direction exitPositionEdge = Direction.Right)
        {
            if(randomPlacement)
            {
                startPosition = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, startPosition);
                exitPosition = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, startPosition);
            } else
            {
                startPosition = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, startPosition, startPositionEdge);
                exitPosition = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, startPosition, exitPositionEdge);
            }
            grid.SetCell(startPosition.x, startPosition.y, CellObjectType.Start);
            grid.SetCell(exitPosition.x, exitPosition.y, CellObjectType.Exit);
        }

        private static Vector3 RandomlyChoosePositionOnTheEdgeOfTheGrid(MapGrid grid, Vector3 startPosition, Direction direction = Direction.None)
        {
            if(direction == Direction.None)
            {
                direction = (Direction)Random.Range(1, 5);
            }

            Vector3 position = Vector3.zero;
            switch (direction)
            {
                case Direction.Right:
                    do
                    {
                        position = new Vector3(grid.Width - 1, Random.Range(0, grid.Length), 0);
                    } while (Vector3.Distance(position, startPosition) <= 1);
                    break;
                case Direction.Left:
                    do
                    {
                        position = new Vector3(0, Random.Range(0, grid.Length), 0);
                    } while (Vector3.Distance(position, startPosition) <= 1);
                    break;
                case Direction.Up:
                    do
                    {
                        position = new Vector3(Random.Range(0, grid.Length), grid.Length - 1, 0);
                    } while (Vector3.Distance(position, startPosition) <= 1);
                    break;
                case Direction.Down:
                    do
                    {
                        position = new Vector3(Random.Range(0, grid.Length), 0, 0);
                    } while (Vector3.Distance(position, startPosition) <= 1);
                    break;
                default:
                    break;
            }
            return position;
        }
    }
}

