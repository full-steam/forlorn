using UnityEngine;
using System.Collections;

namespace ClockStone
{
    public class StopAllAudio : AudioTriggerBase
    {
        public float fadeOut = 0;

        protected override void _OnEventTriggered()
        {
            AudioController.StopAll( fadeOut );
        }
    }
}