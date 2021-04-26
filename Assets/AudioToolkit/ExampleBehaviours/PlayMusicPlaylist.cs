using UnityEngine;
using System.Collections;

namespace ClockStone
{
    public class PlayMusicPlaylist : AudioTriggerBase
    {
        public string playListName = "Default";

        protected override void _OnEventTriggered()
        {
            AudioController.PlayMusicPlaylist( playListName );
        }
    }
}