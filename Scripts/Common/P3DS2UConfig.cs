using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace P3DS2U
{
    public static class P3DS2UConfig
    {
        public const string ImportPath = "Assets/Bin3DS/";
        public const string ExportPath = "Assets/Exported/";

        public static Func<int, string, bool> NeedToImportFunc;
        public static Func<bool> RenameGeneratedAnimationFilesFunc;
        public static UnityAction<P3ds2USettingsScriptableObjectBase, Dictionary<string, List<string>>> StartImportingBinariesEvent;

        public static bool GetRenameGeneratedAnimationFiles() => RenameGeneratedAnimationFilesFunc.Invoke();
        public static bool NeedToImportBasedOnAnimationImportOptions(int _animFilesCount, string _animationName) => NeedToImportFunc.Invoke(_animFilesCount, _animationName);
        public static void StartImportingBinaries(P3ds2USettingsScriptableObjectBase settings, Dictionary<string, List<string>> sceneDict) => StartImportingBinariesEvent.Invoke(settings, sceneDict);
        public static string SaveFolderPanel(string title, string defaultPath, string folderName)
        {
            var items = SFB.StandaloneFileBrowser.OpenFolderPanel(title, defaultPath, false);

            if (items == null || items.Count == 0 || string.IsNullOrEmpty(items[0].Name))
                return "";

            return items[0].Name;
        }
    }
}
