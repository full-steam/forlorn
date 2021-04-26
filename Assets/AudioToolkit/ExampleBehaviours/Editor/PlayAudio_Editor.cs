using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ClockStone
{
    [CustomEditor( typeof( PlayAudio ) )]
    public class PlayAudio_Editor : EditorEx
    {
       
        public override void OnInspectorGUI()
        {
            DrawInspector();
        }

        private void DrawInspector()
        {
            var playAudioObj = (PlayAudio) target;

            BeginInspectorGUI();

            DrawDefaultInspector();

            if( GUILayout.Button( "Browse Audio Items", GUILayout.MaxWidth( 150 ) ) )
            {
                ShowItemOverview();
            }
            
            EndInspectorGUI();
        }

        private void VerticalSpace()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        void ShowItemOverview()
        {
            AudioItemOverview win = EditorWindow.GetWindow( typeof( AudioItemOverview ) ) as AudioItemOverview;
            win.Show( null );
        }
    }
}
