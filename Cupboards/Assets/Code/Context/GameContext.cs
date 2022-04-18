using System.Collections.Generic;


namespace CupBoards
{
    public sealed class GameContext : Contexts
    {
        #region Properties

        public List<LevelData> Levels { get; set; }
        public List<PointBehaviour> Points { get; set; }
        public LevelData CurrentLevel { get; set; }
        public LoadMenuBehaviour LoadMenu { get; set; }
        public int[,] AdjacencyMatrix { get; set; }
        public float CupSpeed { get; set; }

        #endregion
    }
}
