using System.Collections.Generic;


namespace CupBoards
{
    public sealed class AssetsPathGameObject
    {
        #region Fields

        public static readonly Dictionary<GameObjectType, string> Object = new Dictionary<GameObjectType, string>()
        {
            { GameObjectType.Cup, "Prefabs/Cup" },
            { GameObjectType.PathLine, "Prefabs/PathLine" },
            { GameObjectType.Point, "Prefabs/Point" },
            { GameObjectType.LoadButton, "Prefabs/UI/LoadButton" }
        };

        #endregion
    }
}