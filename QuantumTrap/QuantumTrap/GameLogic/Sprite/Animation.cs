#region File Description
//-----------------------------------------------------------------------------
// Animation.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic.Sprite
{
    /// <summary>
    /// Represents an animated texture.
    /// </summary>
    /// <remarks>
    /// Currently, this class assumes that each frame of animation is
    /// as wide as each animation is tall. The number of frames in the
    /// animation are inferred from this.
    /// </remarks>
    public class Animation : AnimationBase
    {
        /// <summary>
        /// Constructors a new animation.
        /// </summary>        
        public Animation(Texture2D texture, float frameTime, bool isLooping)
        {
            this.Texture = texture;
            this.Frame = new Rectangle(0, 0, Texture.Height, Texture.Height);
            this.FrameCount = Texture.Width / Frame.Width;
            this.FrameTime = frameTime;
            this.IsLooping = isLooping;
        }
    }
}
