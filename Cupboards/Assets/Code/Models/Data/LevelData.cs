using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace CupBoards
{
    [Serializable]
    public sealed class LevelData
    {
        #region Fields

        public Action OnMove = () => { };
        public Vector2[] pointsCoordinates;

        public int[] startCupPoints;
        public int[] finishCupPoints;

        #endregion


        #region Propeties

        public int LinksCount { get; private set; }
        public int PointsCount { get; private set; }
        public int CupsCount { get; private set; }
        public int[,] AdjacencyMatrix { get; set; }

        #endregion


        #region Methods

        public static LevelData Deserialize(string filePath)
        {
            List<string> fileLines = new List<string>();
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }

            var result = new LevelData();
            var step = 0;

            try
            {
                result.CupsCount = Convert.ToInt32(fileLines[step++]);

                result.PointsCount = Convert.ToInt32(fileLines[step++]);

                result.pointsCoordinates = new Vector2[result.PointsCount];
                for (int i = 0; i < result.PointsCount; i++)
                {
                    fileLines[step].Trim(' ');
                    result.pointsCoordinates[i].x = Convert.ToInt32(fileLines[step].Split(',')[0]);
                    result.pointsCoordinates[i].y = Convert.ToInt32(fileLines[step++].Split(',')[1]);
                }

                result.startCupPoints = new int[result.CupsCount];
                fileLines[step].Trim(' ');
                var temp = fileLines[step++].Split(',');
                for (int i = 0; i < temp.Length; i++)
                {
                    result.startCupPoints[i] = Convert.ToInt32(temp[i]);
                }

                result.finishCupPoints = new int[result.CupsCount];
                fileLines[step].Trim(' ');
                temp = fileLines[step++].Split(',');
                for (int i = 0; i < temp.Length; i++)
                {
                    result.finishCupPoints[i] = Convert.ToInt32(temp[i]);
                }

                result.LinksCount = Convert.ToInt32(fileLines[step++]);

                int rows = result.PointsCount + 1;
                int columns = rows;
                result.AdjacencyMatrix = new int[rows, columns];
                for (int i = 0; i < result.LinksCount; i++)
                {
                    fileLines[step].Trim(' ');
                    var row = Convert.ToInt32(fileLines[step].Split(',')[0]);
                    var column = Convert.ToInt32(fileLines[step++].Split(',')[1]);
                    result.AdjacencyMatrix[row, column] = 1;
                }

                return result;
            }
            catch (Exception e)
            {
                Debug.Log($"Invalid level data, error {e}");
                return null;
            }
        }

        #endregion
    }
}
