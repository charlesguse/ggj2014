using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuantumTrap.GameLogic.Managers;
using QuantumTrap.ScreenManagers;

namespace QuantumTrap.GameLogic
{
    public class Player
    {
        Position2 Position { get; set; }
        int DistanceToTravel { get { return 64; } }
        int DistanceLeftToTravel { get; set; }
        Position2 Direction { get; set; }
        private Texture2D _playerTexture;
        private Position2 _drawablePosition;

        public Player(Position2 startingLocation)
        {
            Position = startingLocation;
            _drawablePosition = ConvertToDrawablePosition(Position);
        }

        public void LoadContent(ContentManager content)
        {
            _playerTexture = content.Load<Texture2D>("Content/img/bozon-default");
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            if (DistanceLeftToTravel > 0)
            {
                Move();
            }
            else if (DistanceLeftToTravel == 0 && Direction.Sum() > 0)
            {
                DistanceLeftToTravel = DistanceToTravel;
                Move();
            }
        }

        private void Move()
        {
            _drawablePosition.X += Direction.X;

            DistanceLeftToTravel -= Direction.Sum();

            if (DistanceLeftToTravel <= 0)
            {
                DistanceLeftToTravel = 0;
                Position += Direction;
                _drawablePosition = ConvertToDrawablePosition(Position);
                Direction = Position2.Zero;
            }
        }

        public void HandleInput(GameTime gameTime, InputState input)
        {
            if (DistanceLeftToTravel == 0)
            {
                Direction = GetMovementVector(input, PlayerIndex.One);
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

            if (keyboardState.IsKeyDown(Keys.A))
                movement.X--;

            if (keyboardState.IsKeyDown(Keys.D))
                movement.X++;

            if (keyboardState.IsKeyDown(Keys.W))
                movement.Y--;

            if (keyboardState.IsKeyDown(Keys.S))
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

        private static Position2 ConvertToDrawablePosition(Position2 position)
        {
            var drawablePosition = position;
            drawablePosition.X *= 64;
            drawablePosition.Y *= 64;

            return drawablePosition;
        }
    }
}