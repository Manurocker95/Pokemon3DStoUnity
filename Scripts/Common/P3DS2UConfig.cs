using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace P3DS2U
{
    public static class P3DS2UConfig
    {
        public const string ImportPath = "Assets/Bin3DS/";
        public const string ExportPath = "Assets/Exported/";
        public const string GLTFExportPath = "Assets/Exported_GLTF/";

        public static Func<int, string, bool> NeedToImportFunc;
        public static Func<bool> RenameGeneratedAnimationFilesFunc;
        public static UnityAction<P3ds2USettingsScriptableObjectBase, Dictionary<string, List<string>>, bool> StartImportingBinariesEvent;

        public static bool GetRenameGeneratedAnimationFiles() => RenameGeneratedAnimationFilesFunc.Invoke();
        public static bool NeedToImportBasedOnAnimationImportOptions(int _animFilesCount, string _animationName) => NeedToImportFunc.Invoke(_animFilesCount, _animationName);
        public static void StartImportingBinaries(P3ds2USettingsScriptableObjectBase settings, Dictionary<string, List<string>> sceneDict, bool _export = false) => StartImportingBinariesEvent.Invoke(settings, sceneDict, _export);
        public static string SaveFolderPanel(string title, string defaultPath, string folderName)
        {
#if USE_VP_CORE && USE_VP_SFB
            var items = VirtualPhenix.SFB.VP_StandaloneFileBrowser.OpenFolderPanel(title, defaultPath, false);

            if (items == null || items.Count == 0 || string.IsNullOrEmpty(items[0].Name))
                return "";

            return items[0].Name;
#else
#if UNITY_EDITOR
            string filePath = EditorUtility.SaveFolderPanel(title, defaultPath, folderName);
            if (string.IsNullOrEmpty(filePath))
            {
                return defaultPath;
            }

            return filePath;
#else
            return defaultPath;
#endif
#endif
        }
    }
}
