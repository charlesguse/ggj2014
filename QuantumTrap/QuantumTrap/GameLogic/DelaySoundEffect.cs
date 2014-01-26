using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumTrap.GameLogic
{
    public class DelayedSoundEffect
    {
        private TimeSpan _lastPlayTime;
        private SoundEffect _soundEffect;
        private int _millisecondsToDelay;
        private int _millisecondsTilNextPlay;

        public DelayedSoundEffect(ContentManager content, string assetName, int millisecondsToDelay)
        {
            _soundEffect = content.Load<SoundEffect>(assetName);
            _millisecondsToDelay = millisecondsToDelay;
            _millisecondsTilNextPlay = 0;
        }

        public void Play(GameTime gameTime)
        {
            _millisecondsTilNextPlay -= (gameTime.TotalGameTime - _lastPlayTime).Milliseconds;
            if (_millisecondsTilNextPlay <= 0)
            {
                _soundEffect.Play();
                _millisecondsTilNextPlay = _millisecondsToDelay;
                _lastPlayTime = gameTime.TotalGameTime;
            }
        }
    }
}
