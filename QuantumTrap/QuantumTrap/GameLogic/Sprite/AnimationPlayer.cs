#region File Description
//-----------------------------------------------------------------------------
// AnimationPlayer.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic.Sprite
{
    /// <summary>
    /// Controls playback of an Animation.
    /// </summary>
    public class AnimationPlayer
    {
        /// <summary>
        /// Gets the animation which is currently playing.
        /// </summary>
        public AnimationBase Animation { get; private set; }


        /// <summary>
        /// Gets the index of the current frame in the animation.
        /// </summary>
        public int FrameIndex { get; private set; }

        /// <summary>
        /// The amount of time in seconds that the current frame has been shown for.
        /// </summary>
        private float time;

        /// <summary>
        /// Gets a texture origin at the center of each frame.
        /// </summary>
        private Vector2? origin;
        public Vector2 Origin
        {
            get
            {
                if (origin == null)
                    origin = new Vector2(Animation.Frame.Width / 2.0f, Animation.Frame.Height / 2.0f);
                return origin.Value;
            }
        }

        public float Scale { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public SpriteEffects SpriteEffect { get; set; }

        /// <summary>
        /// Begins or continues playback of an animation.
        /// </summary>
        public void PlayAnimation(AnimationBase animation)
        {
            // If this animation is already running, do not restart it.
            if (Animation == animation)
                return;

            // Start the new animation.
            this.Animation = animation;
            ResetAnimation();
        }

        public void ResetAnimation()
        {
            this.FrameIndex = 0;
            this.time = 0.0f;

            this.Color = Color.White;
            this.SpriteEffect = SpriteEffects.None;
        }

        //public bool AtEndOfAnimation()
        //{
        //    if (Animation == null)
        //        return false;

        //    return !Animation.IsLooping && FrameIndex == Animation.FrameCount - 1;
        //}

        /// <summary>
        /// Advances the time position and draws the current frame of the animation.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            this.Draw(gameTime, spriteBatch, position, 0.0f);
        }

        /// <summary>
        /// Advances the time position and draws the current frame of the animation.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float layerDepth)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            // Process passing time.
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > Animation.FrameTime)
            {
                time -= Animation.FrameTime;

                // Advance the frame index; looping or clamping as appropriate.
                if (Animation.IsLooping)
                {
                    FrameIndex = (FrameIndex + 1) % Animation.FrameCount;
                }
                else
                {
                    FrameIndex = Math.Min(FrameIndex + 1, Animation.FrameCount - 1);
                }
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(FrameIndex * Animation.Frame.Height, 0, Animation.Frame.Width, Animation.Frame.Height);

            // Draw the current frame.
            spriteBatch.Draw(Animation.Texture, position, source, Color, Rotation, Origin, Scale, SpriteEffect, layerDepth);
        }
    }
}