using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using QuantumTrap.ScreenManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumTrap.GameLogic
{
    public class PlayerBase : TileBase
    {
        public const int Speed = 2;
        public Position2 Direction { get; set; }
        public Position2 DrawablePosition { get; set; }
        public Position2 Position { get; set; }
        public int DistanceToTravel { get { return 64; } }
        public int DistanceLeftToTravel { get; set; }

        public void Move()
        {
            DrawablePosition += Direction * Speed;

            DistanceLeftToTravel -= Speed;

            if (DistanceLeftToTravel <= 0)
            {
                DistanceLeftToTravel = 0;
                Position += Direction;
                DrawablePosition = ConvertToDrawablePosition(Position, TileSize);
                Direction = Position2.Zero;
            }
        }

        public void SetDrawablePosition(Position2 Position)
        {
            DrawablePosition = ConvertToDrawablePosition(Position, TILE_SIZE);
        }

        protected static Position2 GetMovementVector(InputState input, PlayerIndex playerIndex)
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

        protected static Position2 HandleDPadMovement(InputState input, PlayerIndex playerIndex)
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

        protected static Position2 HandleKeyboardMovement(InputState input, PlayerIndex playerIndex)
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

        protected static Position2 HandleJoystickMovement(InputState input, PlayerIndex playerIndex)
        {
            var gamePadState = input.CurrentGamePadStates[(int)playerIndex];
            var thumbstick = gamePadState.ThumbSticks.Left;
            var movement = Position2.Zero;

            movement.X += Math.Abs(thumbstick.X) < .15 ? 0 : Math.Sign(thumbstick.X) * 1;
            movement.Y += Math.Abs(thumbstick.Y) < .15 ? 0 : Math.Sign(thumbstick.Y) * 1;

            return movement;
        }
    }
}
