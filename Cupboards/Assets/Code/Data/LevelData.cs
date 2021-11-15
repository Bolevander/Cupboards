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
        public (int, int)[] linksBetweenPoints;

        public int[] startCupPoints;
        public int[] finishCupPoints;

        private int _linksCount;
        private int _cupsCount;
        private int _pointsCount;

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
                result._cupsCount = Convert.ToInt32(fileLines[step++]);

                result._pointsCount = Convert.ToInt32(fileLines[step++]);

                result.pointsCoordinates = new Vector2[result._pointsCount];
                for (int i = 0; i < result._pointsCount; i++)
                {
                    fileLines[step].Trim(' ');
                    result.pointsCoordinates[i].x = Convert.ToInt32(fileLines[step].Split(',')[0]);
                    result.pointsCoordinates[i].y = Convert.ToInt32(fileLines[step++].Split(',')[1]);
                }

                result.startCupPoints = new int[result._cupsCount];
                fileLines[step].Trim(' ');
                var temp = fileLines[step++].Split(',');
                for (int i = 0; i < temp.Length; i++)
                {
                    result.startCupPoints[i] = Convert.ToInt32(temp[i]);
                }

                result.finishCupPoints = new int[result._cupsCount];
                fileLines[step].Trim(' ');
                temp = fileLines[step++].Split(',');
                for (int i = 0; i < temp.Length; i++)
                {
                    result.finishCupPoints[i] = Convert.ToInt32(temp[i]);
                }

                result._linksCount = Convert.ToInt32(fileLines[step++]);

                result.linksBetweenPoints = new (int, int)[result._linksCount];
                for (int i = 0; i < result._linksCount; i++)
                {
                    fileLines[step].Trim(' ');
                    result.linksBetweenPoints[i].Item1 = Convert.ToInt32(fileLines[step].Split(',')[0]);
                    result.linksBetweenPoints[i].Item2 = Convert.ToInt32(fileLines[step++].Split(',')[1]);
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
