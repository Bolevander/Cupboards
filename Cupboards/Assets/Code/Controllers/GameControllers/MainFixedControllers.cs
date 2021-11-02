namespace CupBoards
{
    public sealed class MainFixedControllers : Controllers
    {
        #region ClassLifeCycles

        public MainFixedControllers(GameContext context)
        {
            Add(new NavigateController(context));
        }

        #endregion
    }
}