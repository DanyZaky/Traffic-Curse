using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MG
{
    public class GridVisualizer : MonoBehaviour
    {
        public GameObject groundPrefabs;

        public void VisualizeGrid(int width, int length)
        {
            Vector3 position = new Vector3(width / 2f, 0, length / 2f);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            var board = Instantiate(groundPrefabs, position, rotation);
            board.transform.localScale = new Vector3(width, length, 1);
        }
    }
}

