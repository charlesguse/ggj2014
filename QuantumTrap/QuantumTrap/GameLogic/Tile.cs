using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic
{
    public class Tile : TileBase
    {
        public TileType TileType { get; set; }
        private Position2 _drawablePosition;
        private Texture2D _greyTexture, _greenTexture, _redTexture, _blueTexture, _yellowTexture, _blackTexture;

        public Tile(Position2 position)
        {
            _drawablePosition = ConvertToDrawablePosition(position, TileSize);
        }

        public void Update(GameTime gameTime)
        {
            
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(_drawablePosition.X, _drawablePosition.Y, TileSize.X, TileSize.Y);

            spriteBatch.Draw(GetColorTexture(TileType), rectangle, Color.White);
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
    }
}
