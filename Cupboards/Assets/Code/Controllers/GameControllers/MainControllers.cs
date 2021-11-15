namespace CupBoards
{
    public sealed class MainControllers : Controllers
    {
        #region ClassLifeCycles

        public MainControllers(GameContext context)
        {
            Add(new LevelController(context));
            Add(new GameStageController(context));
        }

        #endregion
    }
}
