using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class DebugStarter
{
    // add menu named "Start" to "Tools" menu
    [MenuItem("Tools/Start")]
    public static void Init()
    {
        if (SceneManager.GetActiveScene().isDirty)
        {
            // cancels start if pop-up to save is cancelled
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }
        }

        EditorSceneManager.OpenScene("Assets/Scenes/Loading.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}