using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumTrap.GameLogic.Sprite
{
    public class StillAnimation : AnimationBase
    {
        /// <summary>
        /// Constructors a new animation.
        /// </summary>        
        public StillAnimation(Texture2D texture)
        {
            this.Texture = texture;
            this.Frame = new Rectangle(0, 0, Texture.Width, Texture.Height);
            this.FrameCount = 1;
            this.FrameTime = 1.0f; // Doesn't matter because it is a single frame
            this.IsLooping = true;
        }
    }
}
