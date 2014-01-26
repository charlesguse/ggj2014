namespace QuantumTrap.GameLogic.Managers
{
    public class WinManager
    {
        public bool GameWon { get; set; }

        public void Update(PlayerManager playerManager)
        {
            GameWon = playerManager.Player.Position == playerManager.Shadow.Position;
        }
    }
}