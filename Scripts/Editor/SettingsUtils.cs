using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace P3DS2U.Editor
{
    public static class SettingsUtils
    {
        private const string SettingsFileName = "3DStoUnitySettings.asset";
        public static P3ds2USettingsScriptableObject GetOrCreateSettings(bool _focus = false)
        {
            var settings = FindSettingsInProject();
            if (settings == null)
            {
                string filePath = EditorUtility.SaveFilePanelInProject("Choose the folder where to save the new Import Settings", SettingsFileName, "asset", "Save", P3DS2UConfig.ImportPath);
                if (string.IsNullOrEmpty(filePath))
                    filePath = P3DS2UConfig.ImportPath + SettingsFileName;

                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<P3ds2USettingsScriptableObject>(), filePath);
                return GetOrCreateSettings(_focus);
            }

            if (_focus)
            {
                Selection.activeObject = settings;
            }

            Pokemon3DSToUnityEditorWindow.settings = settings;
            return settings;
        }

        public static void GetOrCreateSettingsInImporterPath()
        {
            const string filePath = P3DS2UConfig.ImportPath + SettingsFileName;
            if (System.IO.File.Exists(filePath))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<P3ds2USettingsScriptableObject>(filePath);
            }
            else
            {
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<P3ds2USettingsScriptableObject>(), filePath);
                GetOrCreateSettingsInImporterPath();
            }
        }

        public static P3ds2USettingsScriptableObject FindSettingsInProject()
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:P3ds2USettingsScriptableObject");
            foreach (string ttype in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(ttype);
                P3ds2USettingsScriptableObject settings = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(P3ds2USettingsScriptableObject)) as P3ds2USettingsScriptableObject;
                if (settings != null)
                {
                    return settings;
                }
            }

            return null;
        }

        public static List<P3ds2USettingsScriptableObject> FindAllSettingsInProject<T>()
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:P3ds2USettingsScriptableObject");
            List<P3ds2USettingsScriptableObject> finalList = new List<P3ds2USettingsScriptableObject>();
            foreach (string ttype in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(ttype);
                P3ds2USettingsScriptableObject val = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(P3ds2USettingsScriptableObject)) as P3ds2USettingsScriptableObject;
                if (val != null)
                {
                    finalList.Add(val);
                }
            }

            return finalList;
        }
    }

}
