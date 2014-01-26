using System;
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
        private PlayerColor _playerColor;
        Position2 Position { get; set; }
        int DistanceToTravel { get { return 64; } }
        int DistanceLeftToTravel { get; set; }
        Position2 Direction { get; set; }
        private Texture2D _playerTexture;
        private Position2 _drawablePosition;

        public Player(Position2 startingLocation)
        {
            Position = startingLocation;
            _playerColor = PlayerColor.Grey;
            _drawablePosition = ConvertToDrawablePosition(Position, TileSize);
        }

        public void LoadContent(ContentManager content)
        {
            _playerTexture = content.Load<Texture2D>("img/bozon-default");
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            if (DistanceLeftToTravel > 0)
            {
                var potentialPostion = Position + Direction;
                if (levelManager.CanMoveTo(potentialPostion, _playerColor))
                {
                    Move();
                }
            }
            else if (DistanceLeftToTravel == 0 && Direction.Sum() != 0)
            {
                var potentialPostion = Position + Direction;
                if (levelManager.CanMoveTo(potentialPostion, _playerColor))
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
            _playerColor = GetPlayerColor(input, playerIndex, _playerColor);

        }

        private static PlayerColor GetPlayerColor(InputState input, PlayerIndex playerIndex, PlayerColor playerColor)
        {
            PlayerIndex unused;

            if (input.IsNewKeyPress(Keys.Q, playerIndex, out unused) 
                || input.IsNewButtonPress(Buttons.LeftShoulder, playerIndex, out unused))
                playerColor = DecrementPlayerColor(playerColor);

            if (input.IsNewKeyPress(Keys.E, playerIndex, out unused)
                || input.IsNewButtonPress(Buttons.RightShoulder, playerIndex, out unused))
                playerColor = IncrementPlayerColor(playerColor);

            return playerColor;
        }

        private static PlayerColor DecrementPlayerColor(PlayerColor playerColor)
        {
            return (PlayerColor)(((int)playerColor - 1) % (int)PlayerColor.Yellow);
        }

        private static PlayerColor IncrementPlayerColor(PlayerColor playerColor)
        {
            return (PlayerColor)(((int)playerColor + 1) % (int)PlayerColor.Yellow);
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

            if (gamePadState.IsButtonDown(Buttons.DPadRight))
                movement.X++;

            if (gamePadState.IsButtonDown(Buttons.DPadUp))
                movement.Y--;

            if (gamePadState.IsButtonDown(Buttons.DPadDown))
                movement.Y++;

            return movement;
        }

        private static Position2 HandleKeyboardMovement(InputState input, PlayerIndex playerIndex)
        {
            var keyboardState = input.CurrentKeyboardStates[(int)playerIndex];
            var movement = Position2.Zero;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                movement.X--;

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                movement.X++;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                movement.Y--;

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
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
            spriteBatch.Draw(_playerTexture, _drawablePosition, Color.White);
        }
    }
}