namespace QuantumTrap.GameLogic.Managers
{
    public class WinManager
    {
        private int _framesTogether;
        public bool GameWon { get; set; }

        public void Update(PlayerManager playerManager)
        {
            if (playerManager.Player.Position == playerManager.Shadow.Position)
                _framesTogether++;
            else
                _framesTogether = 0;

            if (_framesTogether >= 3)
                GameWon = playerManager.Player.Position == playerManager.Shadow.Position;
        }
    }
}