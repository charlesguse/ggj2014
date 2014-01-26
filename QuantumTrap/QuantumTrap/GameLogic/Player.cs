using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuantumTrap.GameLogic.Managers;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic
{
    public class Player : PlayerBase
    {
        private int _currentColor;
        private List<PlayerColor> _colorsAvailable;
        public PlayerColor PlayerColor { get { return _colorsAvailable[_currentColor]; } }
        public bool CanMove { get; set; }

        private Texture2D _defaultTexture, _greenTexture, _redTexture, _blueTexture, _yellowTexture;

        public Player(List<PlayerColor> colorsAvailable)
        {
            _colorsAvailable = colorsAvailable;
            
            _currentColor = 0;
        }

        public void LoadContent(ContentManager content)
        {
            _defaultTexture = content.Load<Texture2D>("img/bozon-default");
            _greenTexture = content.Load<Texture2D>("img/bozon-green");
            _redTexture = content.Load<Texture2D>("img/bozon-red");
            _blueTexture = content.Load<Texture2D>("img/bozon-blue");
            _yellowTexture = content.Load<Texture2D>("img/bozon-yellow");
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            CanMove = true;

            var potentialPostion = Position + Direction;
            if (levelManager.CanMoveTo(potentialPostion, PlayerColor))
            {
                if (DistanceLeftToTravel == 0 && Direction.Sum() != 0)
                {
                    DistanceLeftToTravel = DistanceToTravel;
                }
                Move();
            }
            else
            {
                Direction = Position2.Zero;
                CanMove = false;
            }
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            if (DistanceLeftToTravel == 0)
            {
                Direction = GetMovementVector(input, playerIndex);
            }
            SetPlayerColor(input, playerIndex, PlayerColor);

        }

        private PlayerColor SetPlayerColor(InputState input, PlayerIndex playerIndex, PlayerColor playerColor)
        {
            PlayerIndex unused;

            if (input.IsNewKeyPress(Keys.Q, playerIndex, out unused)
                || input.IsNewButtonPress(Buttons.LeftShoulder, playerIndex, out unused))
                DecrementPlayerColor();

            if (input.IsNewKeyPress(Keys.E, playerIndex, out unused)
                || input.IsNewButtonPress(Buttons.RightShoulder, playerIndex, out unused))
                IncrementPlayerColor();

            return playerColor;
        }

        private void DecrementPlayerColor()
        {
            _currentColor = _currentColor - 1;
            int maxColor = _colorsAvailable.Count;
            if (_currentColor < 0)
            {
                _currentColor = maxColor - 1;
            }
        }

        private void IncrementPlayerColor()
        {
            _currentColor = _currentColor + 1;
            int maxColor = _colorsAvailable.Count;
            if (_currentColor >= maxColor)
            {
                _currentColor = 0;
            }
        }

        

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetColorTexture(PlayerColor), DrawablePosition, Color.White);
        }

        private Texture2D GetColorTexture(PlayerColor playerColor)
        {
            switch (playerColor)
            {
                case PlayerColor.Green:
                    return _greenTexture;
                case PlayerColor.Red:
                    return _redTexture;
                case PlayerColor.Blue:
                    return _blueTexture;
                case PlayerColor.Yellow:
                    return _yellowTexture;
                default:
                    return _defaultTexture;
            }
        }
    }
}