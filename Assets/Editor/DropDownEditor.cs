using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(PlayAudio))]
public class DropDownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayAudio script = (PlayAudio)target;

        GUIContent arrayLabel = new GUIContent("AudioClips");
        script.arrayIdx = EditorGUILayout.Popup(arrayLabel, script.arrayIdx, script.audioClipToPlay);
    }
}
