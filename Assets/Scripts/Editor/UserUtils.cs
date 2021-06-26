using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.IO;

public class UserUtils : Editor
{
//    [MenuItem("User Utils/Clear PlayerPrefs Data")]
//    static void ClearData()
//    {
//        UserData.Reset();
//    }

//    [MenuItem("User Utils/Switch to/GameScene")]
//    static void ShowGamescene()
//    {
//        EditorSceneManager.OpenScene("Assets/_Application/Scenes/MainScene.unity");
//    }
//
//    [MenuItem("User Utils/Switch to/LevelEditor")]
//    static void ShowLeveleditor()
//    {
//        EditorSceneManager.OpenScene("Assets/_Application/Scenes/LevelEditor.unity");
//    }
//
//    [MenuItem("User Utils/- Align animator")]
//    static void AlignAnimator()
//    {
//        EditorWindow animatorWindow = getFirstEditorWindowOfType("UnityEditor.Graphs.AnimatorControllerTool");
//        if (animatorWindow == null)
//        {
//            return;
//        }
//
//        animatorWindow.position = new Rect(2000, 150, 800, 800);
//        animatorWindow.maximized = true;
//        animatorWindow.maximized = true;
//    }

//	[MenuItem("User Utils/Create/GameSettings")]
//	static void CreateGameSettings()
//	{
//		createScriptableObject<GameSettings>();
//	}

    [MenuItem("User Utils/Create/Cube Settings")]
    static void CreateCubeSettings()
    {
        createScriptableObject<CubeSettings>();
    }
    
    public static void createScriptableObject<TScriptableObj>() where TScriptableObj : ScriptableObject
    {
        TScriptableObj sobj = ScriptableObject.CreateInstance<TScriptableObj>();

        AssetDatabase.CreateAsset(sobj, getSelectionPath() + "/" + typeof(TScriptableObj) + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = sobj;
    }

    static string getSelectionPath()
    {
        string path = "";
        if (Selection.activeObject != null)
        {
            path = AssetDatabase.GetAssetPath(Selection.activeObject);
        }

        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        return path;
    }


//    static EditorWindow getNextWindow(EditorWindow prev)
//    {
//        if (!prev) return null;
//        IEnumerable<EditorWindow> wins;
//        wins = getEditorWindowsOfType(prev);
//        //add a second copy so we "loop around"
//        return wins.Concat(wins).SkipWhile(x => x != prev).Skip(1).FirstOrDefault();
//    }
//
//    static EditorWindow getFirstEditorWindowOfType(string type)
//    {
//        return getEditorWindowsOfType(type).FirstOrDefault();
//    }
//
//    static IEnumerable<EditorWindow> getEditorWindowsOfType(EditorWindow win)
//    {
//        return getEditorWindowsOfType(win.GetType().ToString());
//    }

//    static IEnumerable<EditorWindow> getEditorWindowsOfType(string type)
//    {
////		EditorWindow[] windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
////		for (int i = 0; i < windows.Length; i++)
////		{
////			Debug.Log(i + " " + windows[i].name + " " + windows[i].GetType());
////		}
////		
//        return (Resources.FindObjectsOfTypeAll(typeof(EditorWindow)) as EditorWindow[]).Where(x =>
//            x.GetType().ToString() == type);
//    }
}