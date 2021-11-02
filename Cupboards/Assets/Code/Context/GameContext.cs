namespace CupBoards
{
    public sealed class GameContext : Contexts
    {
        public CupBehaviour CurrentCup { get; set; }
        public PointBehaviour PreviousPoint { get; set; }
    }
}
