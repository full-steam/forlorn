using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ObjectiveManager))]
public class ObjectiveManagerEditor : Editor
{
    private SerializedProperty objectives;
    private ReorderableList reoderableList;

    private void OnEnable()
    {
        objectives = serializedObject.FindProperty("objectives");

        reoderableList = new ReorderableList(serializedObject, objectives, true, true, true, true);

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
        foldout.boolValue = EditorGUI.Foldout(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), foldout.boolValue, foldout.boolValue ? "" : element.FindPropertyRelative("text").stringValue);
        EditorGUI.indentLevel--;

        if (foldout.boolValue)
        {
            pos.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Start Flag");

            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("startFlag"), GUIContent.none);

            pos.y += EditorGUIUtility.singleLineHeight + 2f;

            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "End Flag");

            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("endFlag"), GUIContent.none);

            pos.y += EditorGUIUtility.singleLineHeight + 2f;

            EditorGUI.LabelField(new Rect(pos.x, pos.y, 100, EditorGUIUtility.singleLineHeight), "Text");

            EditorGUI.PropertyField(new Rect(pos.x + 100, pos.y, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("text"), GUIContent.none);
        }
    }

    private void DrawHeader(Rect rect)
    {
        string name = "Objectives";
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
