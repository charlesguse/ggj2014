using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace QuantumTrap.GameLogic
{
    public class Tile : TileBase
    {
        public TileType TileType { get; set; }

        private const int _tileChangeFlickerTimeMilliseconds = 200;
        private TimeSpan _elapsedTimeSinceFlickerStart;

        private Position2 _position;
        private Position2 _drawablePosition;
        private Texture2D _greyTexture, _greenTexture, _redTexture, _blueTexture, _yellowTexture, _blackTexture;

        public Tile(Position2 position)
        {
            _elapsedTimeSinceFlickerStart = TimeSpan.Zero;
            _position = position;
            _drawablePosition = ConvertToDrawablePosition(position, TileSize);
        }

        public void Update(GameTime gameTime, Player player)
        {
            PlayerColor tilePlayerColor = GetPlayerColor(this.TileType);
            TimeSpan? timeSwitched = player.PlayerColorSwitchTimes[tilePlayerColor];

            if (timeSwitched.HasValue)
            {
                _elapsedTimeSinceFlickerStart = gameTime.TotalGameTime - timeSwitched.Value;

                if (_elapsedTimeSinceFlickerStart.Milliseconds >= _tileChangeFlickerTimeMilliseconds)
                {
                    player.PlayerColorSwitchTimes[tilePlayerColor] = null;
                    _elapsedTimeSinceFlickerStart = TimeSpan.Zero;
                }
            }
            else
            {
                _elapsedTimeSinceFlickerStart = TimeSpan.Zero;
            }
        }

        public void LoadContent(ContentManager content)
        {
            _greyTexture = content.Load<Texture2D>("img/blank");
            _greenTexture = content.Load<Texture2D>("img/green-block");
            _redTexture = content.Load<Texture2D>("img/red-block");
            _blueTexture = content.Load<Texture2D>("img/blue-block");
            _yellowTexture = content.Load<Texture2D>("img/yellow-block");
            _blackTexture = content.Load<Texture2D>("img/black-block");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Player player, Shadow shadow)
        {
            if (TileType != GameLogic.TileType.White)
            {
                Rectangle rectangle = new Rectangle(_drawablePosition.X, _drawablePosition.Y, TileSize.X, TileSize.Y);

                float opacity;

                if (TileIsTransparent(player, shadow) && (player.Position != _position) && (shadow.Position != _position))
                {
                    opacity = (float)((Math.Sin(_elapsedTimeSinceFlickerStart.Milliseconds / 4) / 2) + 0.5);
                }
                else if (TileIsTransparent(player, shadow)) // player or shadow is on it so dont flicker
                {
                    opacity = 0.5f;
                }
                else
                {
                    opacity = 1.0f;
                }

                spriteBatch.Draw(GetColorTexture(TileType), rectangle, Color.White * opacity);
            }
        }

        private bool TileIsTransparent(Player player, Shadow shadow)
        {
            return TileIsPlayerColor(TileType, player.PlayerColor) || (player.Position == _position) || (shadow.Position == _position) || (TileType != TileType.Black && _elapsedTimeSinceFlickerStart > TimeSpan.Zero);
        }

        private bool TileIsPlayerColor(TileType tileType, PlayerColor playerColor)
        {
            if (tileType == TileType.Blue && playerColor == PlayerColor.Blue)
                return true;
            if (tileType == TileType.Green && playerColor == PlayerColor.Green)
                return true;
            if (tileType == TileType.Yellow && playerColor == PlayerColor.Yellow)
                return true;
            if (tileType == TileType.Red && playerColor == PlayerColor.Red)
                return true;

            return false;
        }

        private Texture2D GetColorTexture(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Green:
                    return _greenTexture;
                case TileType.Red:
                    return _redTexture;
                case TileType.Blue:
                    return _blueTexture;
                case TileType.Yellow:
                    return _yellowTexture;
                case TileType.Black:
                    return _blackTexture;
                default:
                    return _greyTexture;
            }
        }

        private Color GetColor(TileType tileType)
        {
            Color color;
            switch (tileType)
            {
                case TileType.Green:
                    color = Color.Green;
                    break;
                case TileType.Red:
                    color = Color.Red;
                    break;
                case TileType.Blue:
                    color = Color.Blue;
                    break;
                case TileType.Yellow:
                    color = Color.Yellow;
                    break;
                case TileType.Black:
                    color = Color.DarkGray;
                    break;
                default:
                    color = Color.LightGray;
                    break;
            }
            return color;
        }

        private PlayerColor GetPlayerColor(TileType tileType)
        {
            PlayerColor color;
            switch (tileType)
            {
                case TileType.Green:
                    color = PlayerColor.Green;
                    break;
                case TileType.Red:
                    color = PlayerColor.Red;
                    break;
                case TileType.Blue:
                    color = PlayerColor.Blue;
                    break;
                case TileType.Yellow:
                    color = PlayerColor.Yellow;
                    break;
                case TileType.Black:
                    color = PlayerColor.Grey;
                    break;
                default:
                    color = PlayerColor.Grey;
                    break;
            }
            return color;
        }
    }
}
