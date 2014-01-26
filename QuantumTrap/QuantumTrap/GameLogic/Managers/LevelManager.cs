using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic.Managers
{
    public class LevelManager
    {
        private readonly string _levelFile;
        public Level Level { get; set; }

        public LevelManager(string levelFile)
        {
            _levelFile = levelFile;
            Level = new Level();
        }

        public void LoadContent(ContentManager content, PlayerManager player)
        {
            var importer = new LevelImporter();
            Level = importer.Import(_levelFile);
            Level.LoadContent(content);
            player.Player.Position = Level.PlayerStart;
            player.Player.SetDrawablePosition(player.Player.Position);
            player.Shadow.Position = Level.ShadowStart;
            player.Shadow.SetDrawablePosition(player.Shadow.Position);
        }

        public void LoadLevel(string levelName, PlayerManager playerManager)
        {
        }

        public void Update(GameTime gameTime, Player player)
        {
            Level.Update(gameTime, player);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Player player, Shadow shadow)
        {
            Level.Draw(gameTime, spriteBatch, player, shadow);
        }

        internal bool CanMoveTo(Position2 potentialPostion, PlayerColor playerColor)
        {
            return Level.CanMoveTo(potentialPostion, playerColor);
        }
    }
}