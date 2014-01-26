using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic.Sprite
{
    public abstract class AnimationBase
    {
        /// <summary>
        /// All frames in the animation arranged horizontally.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets the size of a frame in the animation.
        /// </summary>
        public Rectangle Frame { get; protected set; }

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public int FrameCount { get; protected set; }
        
        /// <summary>
        /// Duration of time to show each frame.
        /// </summary>
        public float FrameTime { get; protected set; }

        /// <summary>
        /// When the end of the animation is reached, should it
        /// continue playing from the beginning?
        /// </summary>
        public bool IsLooping { get; protected set; }
    }
}
