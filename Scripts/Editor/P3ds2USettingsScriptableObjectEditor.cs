using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace P3DS2U.Editor
{
    [CustomEditor(typeof(P3ds2USettingsScriptableObject))]
    public class P3ds2USettingsScriptableObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var settingsTarget = target as P3ds2USettingsScriptableObject;
            if (settingsTarget != null)
            {

                var wti = settingsTarget.ImporterSettings;
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Version: " + settingsTarget.PackageVersion + "\n");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Paths");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("...", GUILayout.Width(50), GUILayout.Width(50)))
                {
                    settingsTarget.SetImportPath();
                }
                GUILayout.Label("Import path: " + wti.ImportPath);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("...", GUILayout.Width(50), GUILayout.Width(50)))
                {
                    settingsTarget.SetExportPath();
                }

                GUILayout.Label("Export path: " + wti.ExportPath);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("...", GUILayout.Width(50), GUILayout.Width(50)))
                {
                    settingsTarget.SetGLTFExportPath();
                }

                GUILayout.Label("GLTF Export path: " + wti.ExportGLTFPath);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Reset Path", GUILayout.Width(200), GUILayout.Width(150)))
                {
                    settingsTarget.ResetPaths(0);
                }
                EditorGUILayout.Space();
                if (GUILayout.Button("Reset Path", GUILayout.Width(200), GUILayout.Width(150)))
                {
                    settingsTarget.ResetPaths(1);
                }       
                EditorGUILayout.Space();
                if (GUILayout.Button("Reset Path", GUILayout.Width(200), GUILayout.Width(150)))
                {
                    settingsTarget.ResetPaths(2);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(400), GUILayout.Height(140));

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Import", GUILayout.Width(100), GUILayout.Height(50)))
                {
                    RefreshTab(settingsTarget, wti, ()=>settingsTarget.StartImporting());
                }
                if (GUILayout.Button("Import & Export as GLTF", GUILayout.Width(200), GUILayout.Height(50)))
                {
                    RefreshTab(settingsTarget, wti, ()=> settingsTarget.StartImporting(true));
                }
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Refresh", GUILayout.Width(100), GUILayout.Height(50)))
                {
                    RefreshTab(settingsTarget, wti);
                }
                GUILayout.Label("If you are not sure what settings to use, try the defaults!", GUILayout.ExpandWidth(true));
                EditorGUILayout.EndScrollView();
                EditorGUI.BeginChangeCheck();

                settingsTarget.chosenFormat = GUILayout.SelectionGrid(settingsTarget.chosenFormat, new[] { "Each pokemon is in a separate folder", "Each folder has a type of binary ('Mdls','Tex','Etc')" }, 2, GUI.skin.toggle);
                if (EditorGUI.EndChangeCheck())
                {
                    settingsTarget.RegeneratePreview();
                }

                EditorGUI.BeginChangeCheck();
                DrawDefaultInspector();
                if (EditorGUI.EndChangeCheck())
                {

                    if (!wti.ImportModel)
                    {
                        wti.ApplyMaterials = false;
                        wti.SkeletalAnimations = false;
                        wti.MaterialAnimations = false;
                        wti.RenameGeneratedAnimationFiles = false;
                        wti.VisibilityAnimations = false;
                        wti.ImportFireMaterials = false;
                      
                    }

                    if (!wti.ImportTextures)
                    {
                        wti.ImportMaterials = false;
                        wti.ImportShinyMaterials = false;
                        wti.ImportFireMaterials = false;
                    }

                    if (!wti.ImportMaterials)
                    {
                        wti.ApplyMaterials = false;
                    }

                    if (!wti.ImportShinyMaterials)
                    {
                        wti.ApplyShinyMaterials = false;
                    }

                    if (!wti.ImportMaterials && !wti.ImportShinyMaterials)
                    {
                        wti.MaterialAnimations = false;
                        wti.ImportFireMaterials = false;
                    }

                    if (!wti.ApplyMaterials && !wti.ApplyShinyMaterials)
                    {
                        wti.MaterialAnimations = false;
                    }

                    if (!wti.SkeletalAnimations)
                    {
                        wti.MaterialAnimations = false;
                        wti.RenameGeneratedAnimationFiles = false;
                        wti.VisibilityAnimations = false;
                    }

                    if (wti.ImportFireMaterials)
                    {
                        FireImporterSettingsUtil.ThrowWarningAndChangeSettings();
                    }
                }
                EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(100), GUILayout.Height(10));
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }
            serializedObject.Update();
            GUILayout.ExpandHeight(true);
            GUILayout.ExpandWidth(true);

            serializedObject.ApplyModifiedProperties();
        }

        private void RefreshTab(P3ds2USettingsScriptableObject settingsTarget, WhatToImport wti, UnityAction _callback = null)
        {
            CheckFolders(wti);
            if (P3DS2UConfig.NeedToImportFunc == null)
            {
                P3DS2UConfig.NeedToImportFunc = PokemonImporter.NeedToImportAnimation;
            }

            if (P3DS2UConfig.StartImportingBinariesEvent == null)
            {
                P3DS2UConfig.StartImportingBinariesEvent = PokemonImporter.StartImportingBinaries;
            }
            settingsTarget.RegeneratePreview();
            _callback?.Invoke();
        }

        private void CheckFolders(WhatToImport wti)
        {
            if (wti != null)
            {
                if (!string.IsNullOrEmpty(wti.ExportGLTFPath) && !System.IO.Directory.Exists(wti.ExportGLTFPath))
                    System.IO.Directory.CreateDirectory(wti.ExportGLTFPath);

                if (!string.IsNullOrEmpty(wti.ImportPath) && !System.IO.Directory.Exists(wti.ImportPath))
                    System.IO.Directory.CreateDirectory(wti.ImportPath);

                if (!string.IsNullOrEmpty(wti.ExportPath) && !System.IO.Directory.Exists(wti.ExportPath))
                    System.IO.Directory.CreateDirectory(wti.ExportPath);

                AssetDatabase.Refresh();
            }
        }
    }
}
