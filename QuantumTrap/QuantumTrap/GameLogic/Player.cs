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
        public bool PlayingCantMoveAnimation { get; set; }
        private bool _incrementingColor { get; set; }
        private bool _decrementingColor { get; set; }

        private Position2 _drawableDirection { get; set; }
        private TimeSpan _cantMoveAnimTimeElapsed { get; set; }
        private const int _cantMoveAnimDurationMilliseconds = 500;

        private Texture2D _defaultTexture, _greenTexture, _redTexture, _blueTexture, _yellowTexture;
        private DelayedSoundEffect _cantMoveSfx;
        private SoundEffect _movingSfx, _changeColorSfx, _cantChangeColorSfx;

        public Player(List<PlayerColor> colorsAvailable)
        {
            _cantMoveAnimTimeElapsed = TimeSpan.Zero;
            PlayingCantMoveAnimation = false;
            _drawableDirection = Position2.Zero;
            _colorsAvailable = colorsAvailable;
            
            _currentColor = 0;
        }

        public void LoadContent(ContentManager content)
        {
            _movingSfx = content.Load<SoundEffect>("sfx/Bozon Moving");
            _changeColorSfx = content.Load<SoundEffect>("sfx/Color Change");
            _cantChangeColorSfx = content.Load<SoundEffect>("sfx/blip");
            _cantMoveSfx = new DelayedSoundEffect(content, "sfx/Can't move there sound", 500);
            _defaultTexture = content.Load<Texture2D>("img/bozon-default");
            _greenTexture = content.Load<Texture2D>("img/bozon-green");
            _redTexture = content.Load<Texture2D>("img/bozon-red");
            _blueTexture = content.Load<Texture2D>("img/bozon-blue");
            _yellowTexture = content.Load<Texture2D>("img/bozon-yellow");
        }

        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            if (PlayingCantMoveAnimation)
            {
                _cantMoveAnimTimeElapsed += gameTime.ElapsedGameTime;
                DrawablePosition = CalculateCantMoveAnimDrawablePosition(Position, _drawableDirection, _cantMoveAnimTimeElapsed);
                if (CantMoveAnimFinished(gameTime))
                {
                    PlayingCantMoveAnimation = false;
                    _cantMoveAnimTimeElapsed = TimeSpan.Zero;
                    DrawablePosition = ConvertToDrawablePosition(Position, Tile.TILE_SIZE);
                }
            }
            else
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
                            _movingSfx.Play();
                        }
                        Move();
                    }
                    else
                    {
                        CanMove = false;
                        PlayingCantMoveAnimation = true;
                        _drawableDirection = Direction;
                        _cantMoveAnimTimeElapsed = new TimeSpan();

                        Direction = Position2.Zero;

                        _cantMoveSfx.Play(gameTime);
                    }
                }


                var canChangeColor = CanChangeColor(levelManager, potentialPosition);
                if ((_incrementingColor || _decrementingColor) && canChangeColor)
                {
                    if (_colorsAvailable.Count > 1)
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
            }

            _decrementingColor = _incrementingColor = false;
        }

        private bool CantMoveAnimFinished(GameTime gameTime)
        {
            return (_cantMoveAnimTimeElapsed.Milliseconds >= _cantMoveAnimDurationMilliseconds);
        }

        private Position2 CalculateCantMoveAnimDrawablePosition(Position2 Position, Position2 _drawableDirection, TimeSpan cantMoveAnimTimeElapsed)
        {
            const int _cantMoveShakeDistance = 4;

            Position2 baseDrawablePosition = ConvertToDrawablePosition(Position, Tile.TILE_SIZE);
            double animationPlayDurationFrames = (_cantMoveAnimDurationMilliseconds / 1000) * Game1.TargetFrameRate; 
            double animationPlayElapsedFrames = (cantMoveAnimTimeElapsed.TotalSeconds * Game1.TargetFrameRate);

            double framesBeforeBouncing = (animationPlayDurationFrames);
            double moveOffsetDistance = 48 * Math.Min(animationPlayElapsedFrames, framesBeforeBouncing);
            double bounceDistance = (animationPlayElapsedFrames >= framesBeforeBouncing) ? (Math.Sin(animationPlayElapsedFrames - framesBeforeBouncing) * _cantMoveShakeDistance) : 0;
            
            int distanceToMove = (int)(moveOffsetDistance + bounceDistance);
            Position2 newPosition = baseDrawablePosition + (_drawableDirection * distanceToMove);

            return newPosition;
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