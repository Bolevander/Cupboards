using System.IO;
using UnityEngine;


namespace CupBoards
{
    public static class LevelLoader
    {
        public const string FileFolderName = "LevelTextSeeds";
        public const string FileExtension = ".txt";

        public static readonly string folderPath = $"{Application.dataPath}/{FileFolderName}";

        public static LevelData LoadSave(string filePath)
        {
            //var filePath = $"{folderPath}/{filePath}{FileExtension}";

            if (File.Exists(filePath) == false)
            {
                return null;
            }

            return LevelData.Deserialize(filePath);
        }
        
        public static LevelData[] LoadAllSaves()
        {
            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("Save folder was created, now you need to fill it.");
                return null;
            }

            var fileNames = Directory.GetFiles(folderPath, "*.txt");
            var result = new LevelData[fileNames.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = LoadSave(fileNames[i]);
            }

            return result;
        }
    }
}
