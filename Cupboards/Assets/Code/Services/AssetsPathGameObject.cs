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
            { GameObjectType.Point, "Prefabs/Point" }
        };

        public static readonly Dictionary<ScreenType, string> Screens = new Dictionary<ScreenType, string>()
        {
            {ScreenType.LoadLevel, "Prefabs/UI/Screen/Prefabs_UI_Screen_GameOver"},//uncorrect
        };

        #endregion
    }
}