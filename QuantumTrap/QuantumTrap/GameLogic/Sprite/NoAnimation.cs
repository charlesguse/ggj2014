using Microsoft.Xna.Framework;

namespace QuantumTrap.GameLogic.Sprite
{
    public class NoAnimation : AnimationBase
    {
        /// <summary>
        /// Constructors a new animation.
        /// </summary>        
        public NoAnimation()
        {
            this.Texture = null;
            this.Frame = new Rectangle(0, 0, 0, 0);
            this.FrameCount = 0;
            this.FrameTime = 1.0f; // Doesn't matter because it is a single frame
            this.IsLooping = true;
        }
    }
}
