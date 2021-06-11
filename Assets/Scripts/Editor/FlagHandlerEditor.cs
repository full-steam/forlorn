using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(FlagHandler))]
public class FlagHandlerEditor : Editor
{
    private ReorderableList reoderableList;

    private SerializedProperty flagEvents;

    public void OnEnable()
    {
        flagEvents = serializedObject.FindProperty("flagEvents");

        reoderableList = new ReorderableList(serializedObject, flagEvents, true, true, true, true);

        reoderableList.drawElementCallback = DrawListItems;
        reoderableList.drawHeaderCallback = DrawHeader;
        reoderableList.elementHeightCallback = GetElementHeight;
    }

    private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        Rect pos = new Rect(rect);

        SerializedProperty element = reoderableList.serializedProperty.GetArrayElementAtIndex(index);

        SerializedProperty foldout = element.FindPropertyRelative("foldout");

        EditorGUI.indentLevel++;
        foldout.boolValue = EditorGUI.Foldout(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), foldout.boolValue, foldout.boolValue ? "" : element.FindPropertyRelative("flag").stringValue);
        EditorGUI.indentLevel--;

        if (foldout.boolValue)
        {
            pos.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Flag");

            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("flag"), GUIContent.none);

            pos.y += EditorGUIUtility.singleLineHeight + 2f;

            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Event Type");

            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("eventType"), GUIContent.none);

            pos.y += EditorGUIUtility.singleLineHeight + 2f;

            SerializedProperty foldoutTargets = element.FindPropertyRelative("foldoutTargets");

            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Targets");
            foldoutTargets.boolValue = EditorGUI.Foldout(new Rect(pos.x + 110, pos.y, 100, EditorGUIUtility.singleLineHeight), foldoutTargets.boolValue, GUIContent.none);

            pos.y += EditorGUIUtility.singleLineHeight + 2f;

            if (foldoutTargets.boolValue)
            {
                FlagEvent.FlagEventType eventType = (FlagEvent.FlagEventType)element.FindPropertyRelative("eventType").enumValueIndex;

                SerializedProperty targets;

                switch (eventType)
                {
                    case FlagEvent.FlagEventType.EnableObject:
                    case FlagEvent.FlagEventType.DisableObject:
                        targets = element.FindPropertyRelative("targets");

                        EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Size");

                        EditorGUI.BeginChangeCheck();
                        targets.arraySize = EditorGUI.IntField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), targets.arraySize);
                        EditorGUI.EndChangeCheck();

                        for (var i = 0; i < targets.arraySize; i++)
                        {
                            var target = targets.GetArrayElementAtIndex(i);

                            pos.y += EditorGUIUtility.singleLineHeight + 2f;

                            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Target " + i);
                            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), target, GUIContent.none);
                        }
                        break;
                    case FlagEvent.FlagEventType.MoveObject:
                        targets = element.FindPropertyRelative("moveTargets");

                        EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Size");

                        EditorGUI.BeginChangeCheck();
                        targets.arraySize = EditorGUI.IntField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), targets.arraySize);
                        EditorGUI.EndChangeCheck();

                        for (var i = 0; i < targets.arraySize; i++)
                        {
                            var target = targets.GetArrayElementAtIndex(i);

                            pos.y += EditorGUIUtility.singleLineHeight + 2f;

                            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Target " + i);
                            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), target.FindPropertyRelative("target"), GUIContent.none);

                            pos.y += EditorGUIUtility.singleLineHeight + 2f;

                            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), target.FindPropertyRelative("coords"), GUIContent.none);
                        }
                        break;
                }
            }
        }
    }

    private void DrawHeader(Rect rect)
    {
        string name = "Flag Events";
        EditorGUI.LabelField(rect, name);
    }

    private float GetElementHeight(int index)
    {
        SerializedProperty element = reoderableList.serializedProperty.GetArrayElementAtIndex(index);
        var foldout = element.FindPropertyRelative("foldout");

        var height = EditorGUIUtility.singleLineHeight;

        if (foldout.boolValue)
        {
            height += EditorGUIUtility.singleLineHeight * 3f + 2f;

            if (element.FindPropertyRelative("foldoutTargets").boolValue)
            {
                FlagEvent.FlagEventType eventType = (FlagEvent.FlagEventType)element.FindPropertyRelative("eventType").enumValueIndex;

                SerializedProperty targets = null;

                switch (eventType)
                {
                    case FlagEvent.FlagEventType.EnableObject:
                    case FlagEvent.FlagEventType.DisableObject:
                        targets = element.FindPropertyRelative("targets");
                        height += (EditorGUIUtility.singleLineHeight + 2f) * (targets.arraySize + 1);
                        break;
                    case FlagEvent.FlagEventType.MoveObject:
                        targets = element.FindPropertyRelative("moveTargets");
                        height += (EditorGUIUtility.singleLineHeight + 2f) * (targets.arraySize * 2 + 1);
                        break;
                }


            }
        }

        return height;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        reoderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}
