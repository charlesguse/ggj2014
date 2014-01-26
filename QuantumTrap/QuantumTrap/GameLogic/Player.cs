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
    public class Player : TileBase
    {
        private int _currentColor;
        private List<PlayerColor> _colorsAvailable;
        public PlayerColor PlayerColor { get { return _colorsAvailable[_currentColor]; } }
        public Position2 Position { get; set; }
        int DistanceToTravel { get { return 64; } }
        int DistanceLeftToTravel { get; set; }
        Position2 Direction { get; set; }
        private Texture2D _defaultTexture, _greenTexture, _redTexture, _blueTexture, _yellowTexture;
        private Position2 _drawablePosition;

        public Player(Position2 startingLocation)
        {
            _colorsAvailable = new List<PlayerColor> { PlayerColor.Grey };
            _colorsAvailable.Add(PlayerColor.Green);
            _colorsAvailable.Add(PlayerColor.Red);
            _colorsAvailable.Add(PlayerColor.Blue);
            _colorsAvailable.Add(PlayerColor.Yellow);
            _currentColor = 0;
            Position = startingLocation;
            _drawablePosition = ConvertToDrawablePosition(Position, TileSize);
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
            if (DistanceLeftToTravel > 0)
            {
                var potentialPostion = Position + Direction;
                if (levelManager.CanMoveTo(potentialPostion, PlayerColor))
                {
                    Move();
                }
            }
            else if (DistanceLeftToTravel == 0 && Direction.Sum() != 0)
            {
                var potentialPostion = Position + Direction;
                if (levelManager.CanMoveTo(potentialPostion, PlayerColor))
                {
                    DistanceLeftToTravel = DistanceToTravel;
                    Move();
                }
            }
        }

        private void Move()
        {
            _drawablePosition += Direction;

            DistanceLeftToTravel -= 1;

            if (DistanceLeftToTravel <= 0)
            {
                DistanceLeftToTravel = 0;
                Position += Direction;
                _drawablePosition = ConvertToDrawablePosition(Position, TileSize);
                Direction = Position2.Zero;
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

        private static Position2 GetMovementVector(InputState input, PlayerIndex playerIndex)
        {
            var movement = HandleDPadMovement(input, playerIndex);

            if (movement.X == 0 && movement.Y == 0)
            {
                movement = HandleKeyboardMovement(input, playerIndex);
            }
            if (movement.X == 0 && movement.Y == 0)
            {
                movement = HandleJoystickMovement(input, playerIndex);
            }
            return movement;
        }

        private static Position2 HandleDPadMovement(InputState input, PlayerIndex playerIndex)
        {
            var gamePadState = input.CurrentGamePadStates[(int)playerIndex];
            var movement = Position2.Zero;

            if (gamePadState.IsButtonDown(Buttons.DPadLeft))
                movement.X--;
            else if (gamePadState.IsButtonDown(Buttons.DPadRight))
                 movement.X++;
            else if (gamePadState.IsButtonDown(Buttons.DPadUp))
                 movement.Y--;
            else if (gamePadState.IsButtonDown(Buttons.DPadDown))
                movement.Y++;

            return movement;
        }

        private static Position2 HandleKeyboardMovement(InputState input, PlayerIndex playerIndex)
        {
            var keyboardState = input.CurrentKeyboardStates[(int)playerIndex];
            var movement = Position2.Zero;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                 movement.X--;
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                 movement.X++;
            else if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                 movement.Y--;
            else if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                movement.Y++;

            return movement;
        }

        private static Position2 HandleJoystickMovement(InputState input, PlayerIndex playerIndex)
        {
            var gamePadState = input.CurrentGamePadStates[(int)playerIndex];
            var thumbstick = gamePadState.ThumbSticks.Left;
            var movement = Position2.Zero;

            movement.X += Math.Abs(thumbstick.X) < .15 ? 0 : Math.Sign(thumbstick.X) * 1;
            movement.Y += Math.Abs(thumbstick.Y) < .15 ? 0 : Math.Sign(thumbstick.Y) * 1;

            return movement;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetColorTexture(PlayerColor), _drawablePosition, Color.White);
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