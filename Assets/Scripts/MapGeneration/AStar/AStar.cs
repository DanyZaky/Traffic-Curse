using MG.ChessMaze;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.AI
{
    public static class AStar
    {
        public static List<Vector3> GetPath(Vector3 start, Vector3 exit, bool[] obstaclesArray, MapGrid grid)
        {
            VertexPosition startVertex = new VertexPosition(start);
            VertexPosition exitVertex = new VertexPosition(exit);

            List<Vector3> path = new List<Vector3>();

            List<VertexPosition> openedList = new List<VertexPosition>();
            HashSet<VertexPosition> closedList = new HashSet<VertexPosition>();

            startVertex.estimateCost = ManhatanDistance(startVertex, exitVertex);

            openedList.Add(startVertex);

            VertexPosition currentVertex = null;

            while(openedList.Count > 0)
            {
                openedList.Sort();
                currentVertex = openedList[0];

                if(currentVertex.Equals(exitVertex))
                {
                    while(currentVertex != startVertex)
                    {
                        path.Add(currentVertex.Position);
                        currentVertex = currentVertex.previousVertex;
                    }
                    path.Reverse();
                    break;
                }
                var arrayOfNeightbours = FindNeightboursFor(currentVertex, grid, obstaclesArray);

                foreach (var neightbour in arrayOfNeightbours)
                {
                    if(neightbour ==  null || closedList.Contains(neightbour))
                    {
                        continue;
                    }
                    if(neightbour.IsTaken == false)
                    {
                        var totalCost = currentVertex.totalCost + 1;
                        var neightbourEstimatedCost = ManhatanDistance(neightbour, exitVertex);
                        neightbour.totalCost = totalCost;
                        neightbour.previousVertex = currentVertex;
                        neightbour.estimateCost = totalCost + neightbourEstimatedCost;

                        if(openedList.Contains(neightbour) == false)
                        {
                            openedList.Add(neightbour);
                        }
                    }
                }
                closedList.Add(currentVertex);
                openedList.Remove(currentVertex);
            }

            return path;
        }

        private static VertexPosition[] FindNeightboursFor(VertexPosition currentVertex, MapGrid grid, bool[] obstaclesArray)
        {
            VertexPosition[] arrayOfNeightbours = new VertexPosition[4];

            int arrayIndex = 0;
            foreach (var possibleNeightbor in VertexPosition.possibleNeightbours)
            {
                Vector3 position = new Vector3(currentVertex.x + possibleNeightbor.x, 0, currentVertex.y + possibleNeightbor.y);

                if(grid.IsCellValid(position.x, position.y))
                {
                    int index = grid.CalculateIndexFromCoordinates(position.x, position.y);
                    arrayOfNeightbours[arrayIndex] = new VertexPosition(position, obstaclesArray[index]);
                    arrayIndex++;
                }
            }
            return arrayOfNeightbours;
        }

        private static float ManhatanDistance(VertexPosition startVertex, VertexPosition exitVertex)
        {
            return Mathf.Abs(startVertex.x - exitVertex.x) + Mathf.Abs(startVertex.y - exitVertex.y);
        }
    }
}

