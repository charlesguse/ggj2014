using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Graphics.ES20;

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
            _greenTexture = content.Load<Texture2D>("img/blank");
            _redTexture = content.Load<Texture2D>("img/blank");
            _blueTexture = content.Load<Texture2D>("img/blank");
            _yellowTexture = content.Load<Texture2D>("img/blank");
            _blackTexture = content.Load<Texture2D>("img/blank");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(_drawablePosition.X, _drawablePosition.Y, TileSize.X, TileSize.Y);

            spriteBatch.Draw(GetColorTexture(TileType), rectangle, GetColor(TileType));
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
            switch (tileType)
            {
                case TileType.Green:
                    return Color.Green;
                case TileType.Red:
                    return Color.Red;
                case TileType.Blue:
                    return Color.Blue;
                case TileType.Yellow:
                    return Color.Yellow;
                case TileType.Black:
                    return Color.DarkGray;
                default:
                    return Color.LightGray;
            }
        }
    }
}
