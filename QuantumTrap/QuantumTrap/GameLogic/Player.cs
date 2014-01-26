using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuantumTrap.GameLogic.Managers;
using QuantumTrap.ScreenManagers;
using Microsoft.Xna.Framework.Audio;

namespace QuantumTrap.GameLogic
{
    public class Player : PlayerBase
    {
        private int _currentColor;
        private List<PlayerColor> _colorsAvailable;
        public PlayerColor PlayerColor { get { return _colorsAvailable[_currentColor]; } }
        public bool CanMove { get; set; }
        private bool _incrementingColor { get; set; }
        private bool _decrementingColor { get; set; }

        private Texture2D _defaultTexture, _greenTexture, _redTexture, _blueTexture, _yellowTexture;
        private DelayedSoundEffect _cantMoveSfx;
        private SoundEffect _changeColorSfx, _cantChangeColorSfx;

        public Player(Position2 startingLocation)
        {
            _colorsAvailable = new List<PlayerColor> { PlayerColor.Grey };
            _colorsAvailable.Add(PlayerColor.Green);
            _colorsAvailable.Add(PlayerColor.Red);
            _colorsAvailable.Add(PlayerColor.Blue);
            _colorsAvailable.Add(PlayerColor.Yellow);
            _currentColor = 0;
            Position = startingLocation;
            SetDrawablePosition(Position);
        }

        public void LoadContent(ContentManager content)
        {
            _changeColorSfx = content.Load<SoundEffect>("sfx/blip");
            _cantChangeColorSfx = content.Load<SoundEffect>("sfx/blip");
            _cantMoveSfx = new DelayedSoundEffect(content, "sfx/blip", 2000);
            _defaultTexture = content.Load<Texture2D>("img/bozon-default");
            _greenTexture = content.Load<Texture2D>("img/bozon-green");
            _redTexture = content.Load<Texture2D>("img/bozon-red");
            _blueTexture = content.Load<Texture2D>("img/bozon-blue");
            _yellowTexture = content.Load<Texture2D>("img/bozon-yellow");
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            var potentialPosition = Position + Direction;

            CanMove = true;

            if (Direction.Sum() != 0)
            {
                if (levelManager.CanMoveTo(potentialPosition, PlayerColor))
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
                    _cantMoveSfx.Play(gameTime);
                }
            }

            var canChangeColor = CanChangeColor(levelManager, potentialPosition);
            if ((_incrementingColor || _decrementingColor) && canChangeColor)
            {
                _changeColorSfx.Play();

                if (_incrementingColor)
                    IncrementPlayerColor();
                else if (_decrementingColor)
                    DecrementPlayerColor();
            }
            else if (_incrementingColor || _decrementingColor)
            {
                _cantChangeColorSfx.Play();
            }

            _decrementingColor = _incrementingColor = false;
        }

        private bool CanChangeColor(LevelManager levelManager, Position2 potentialPosition)
        {
            var currentPositionTileType = levelManager.Level.TileMap[Position.X][Position.Y].TileType;
            var potentialPositionTileType = levelManager.Level.TileMap[potentialPosition.X][potentialPosition.Y].TileType;
            return (!CanMove || 
                Position == potentialPosition || 
                (currentPositionTileType == TileType.White && potentialPositionTileType == TileType.White));
        }

        public void HandleInput(InputState input, PlayerIndex playerIndex)
        {
            if (DistanceLeftToTravel == 0)
            {
                Direction = GetMovementVector(input, playerIndex);
            }
            SetPlayerColor(input, playerIndex, PlayerColor);

        }

        private void SetPlayerColor(InputState input, PlayerIndex playerIndex, PlayerColor playerColor)
        {
            PlayerIndex unused;

            if (input.IsNewKeyPress(Keys.Q, playerIndex, out unused)
                || input.IsNewButtonPress(Buttons.LeftShoulder, playerIndex, out unused))
                _decrementingColor = true;

            if (input.IsNewKeyPress(Keys.E, playerIndex, out unused)
                || input.IsNewButtonPress(Buttons.RightShoulder, playerIndex, out unused))
                _incrementingColor = true;
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