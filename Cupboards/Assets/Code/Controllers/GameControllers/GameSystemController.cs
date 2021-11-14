﻿namespace CupBoards
{
    public sealed class GameSystemsController : GameStateController
    {
        #region ClassLifeCycles

        public GameSystemsController(GameContext context)
        {
            AddUpdateFeature(new MainControllers(context));
            AddLateUpdateFeature(new MainLateControllers(context));
        }

        #endregion
    }
}
