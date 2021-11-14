namespace CupBoards
{
    public sealed class MainLateControllers : Controllers
    {
        #region ClassLifeCycles

        public MainLateControllers(GameContext context)
        {
            Add(new NavigateController(context));
        }

        #endregion
    }
}