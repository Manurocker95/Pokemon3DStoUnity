using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace P3DS2U
{
   


    [Serializable]
   public class P3ds2UShaderProperties
   {
      [SerializeField] public Shader bodyShader;
      [SerializeField] public Shader irisShader;
      [SerializeField] public Shader fireCoreShader;
      [SerializeField] public Shader fireStencilShader;
      
      // public string BaseMap = Shader.PropertyToID ("_BaseMap");
      public string BaseMap =  ("_BaseMap");
      public string BaseMapTiling =  ("_BaseMapTiling");
      public string BaseMapOffset = ("_BaseMapOffset");
      public string NormalMap =  ("_NormalMap");
      public string NormalMapTiling =  ("_NormalMapTiling");
      public string NormalMapOffset = ("_NormalOffset");
      public string OcclusionMap =  ("_OcclusionMap");
      public string OcclusionMapTiling =  ("_OcclusionMapTiling");
      public string OcclusionMapOffset =  ("_OcclusionMapOffset");
      public string Constant4Color =  ("_Constant4Color");
      public string Constant3Color =  ("_Constant3Color");

      public string Tex0TranslateX = "material._BaseMapOffset.x";
      public string Tex1TranslateX = "material._OcclusionMapOffset.x";
      public string Tex2TranslateX = "material._NormalMapOffset.x";
      public string Tex0TranslateY = "material._BaseMapOffset.y";
      public string Tex1TranslateY = "material._OcclusionMapOffset.y";
      public string Tex2TranslateY = "material._NormalMapOffset.y";
   }
   
   [Serializable]
   public class MergedBinary : PropertyAttribute
   {
      [SerializeField] public List<string> PokemonMergedBinary;
   }
 

   [Serializable]
   public class AnimationImportOptions: SerializableDictionary<string, bool>{}

    [Serializable]
    public class P3ds2UAnimatorProperties
    {
#if UNITY_EDITOR
        [SerializeField] public UnityEditor.Animations.AnimatorController baseController;
#else

#endif
#region Fight Animations
        public string Fight_Idle = ("Idle");
        public string Appear = ("Appear");
        public string Transform = ("Transform");
        public string Release = ("Release");
        public string Dropping = ("Dropping");
        public string Landing = ("Landing");
        public string Release_without_Landing = ("Release_without_Landing");
        public string Mega_Upgraded = ("Mega_Upgraded");
        public string Attack = ("Attack");
        public string Attack_2 = ("Attack_2");
        public string Attack_3 = ("Attack_3");
        public string Attack_4 = ("Attack_4");
        public string No_Touch_Attack = ("No_Touch_Attack");
        public string No_Touch_Attack_2 = ("No_Touch_Attack_2");
        public string No_Touch_Attack_3 = ("No_Touch_Attack_3");
        public string No_Touch_Attack_4 = ("No_Touch_Attack_4");
        public string Be_Attacked = ("Be_Attacked");
        public string Lost = ("Lost");
        public string Fight_Empty = ("Empty");
        public string Fight_Eye_Emotion = ("Eye_Emotion");
        public string Fight_Eye_2_Emotion = ("Eye_2_Emotion");
        public string Fight_Eye_3_Emotion = ("Eye_3_Emotion");
        public string Fight_Mouth_Emotion = ("Mouth_Emotion");
        public string Fight_Mouth_2_Emotion = ("Mouth_2_Emotion");
        public string Fight_Mouth_3_Emotion = ("Mouth_3_Emotion");
        public string Fight_State = ("State");
        public string Fight_State_2 = ("State_2");
        public string Fight_State_3 = ("State_3");
        public string Fight_State_4 = ("State_4");
#endregion

#region Pet Animations
        public string Pet_Idle = ("Idle");
        public string Turn = ("Turn");
        public string Look_Back = ("Look_Back");
        public string Look_Back_Happily = ("Look_Back_Happily");
        public string Falling_Asleep = ("Falling_Asleep");
        public string Sleepy = ("Sleepy");
        public string Sleepy_Awaken = ("Sleepy_Awaken");
        public string Sleeping = ("Sleeping");
        public string Awaken = ("Awaken");
        public string Refuse = ("Refuse");
        public string Thinking = ("Thinking");
        public string Agree = ("Agree");
        public string Happy = ("Happy");
        public string Very_Happy = ("Very_Happy");
        public string Look_Around = ("Look_Around");
        public string Rub_Eyes = ("Rub_Eyes");
        public string Comfortable = ("Comfortable");
        public string Relax = ("Relax");
        public string Sad = ("Sad");
        public string Salutate = ("Salutate");
        public string Happy_2 = ("Happy_2");
        public string Angry = ("Angry");
        public string Begin_Eating = ("Begin_Eating");
        public string Eating = ("Eating");
        public string Eating_Finished = ("Eating_Finished");
        public string No_Eating = ("No_Eating");
        public string Pet_Empty = ("Empty");
        public string Pet_Eye_Emotion = ("Eye_Emotion");
        public string Pet_Eye_2_Emotion = ("Eye_2_Emotion");
        public string Pet_Eye_3_Emotion = ("Eye_3_Emotion");
        public string Pet_Mouth_Emotion = ("Mouth_Emotion");
        public string Pet_Mouth_2_Emotion = ("Mouth_2_Emotion");
        public string Pet_Mouth_3_Emotion = ("Mouth_3_Emotion");
        public string Pet_State = ("State");
        public string Pet_State_2 = ("State_2");
        public string Pet_State_3 = ("State_3");
        public string Pet_State_4 = ("State_4");
#endregion

#region Movement Animations
        public string Movement_Idle = ("Idle");
        public string Movement_Empty = ("Empty");
        public string Walk = ("Walk");
        public string Run = ("Run");
        public string Empty_2 = ("Empty_2");
        public string Start_Walk = ("Start_Walk");
        public string End_Walk = ("End_Walk");
        public string Empty_3 = ("Empty_3");
        public string Start_Run = ("Start_Run");
        public string End_Run = ("End_Run");
        public string Empty_4 = ("Empty_4");
        public string Start_Run_2 = ("Start_Run_2");
        public string End_Run_2 = ("End_Run_2");
        public string Empty_5 = ("Empty_5");
        public string Movement_Eye_Emotion = ("Eye_Emotion");
        public string Movement_Eye_2_Emotion = ("Eye_2_Emotion");
        public string Movement_Eye_3_Emotion = ("Eye_3_Emotion");
        public string Movement_Mouth_Emotion = ("Mouth_Emotion");
        public string Movement_Mouth_2_Emotion = ("Mouth_2_Emotion");
        public string Movement_Mouth_3_Emotion = ("Mouth_3_Emotion");
        public string Movement_State = ("State");
        public string Movement_State_2 = ("State_2");
        public string Movement_State_3 = ("State_3");
        public string Movement_State_4 = ("State_4");
#endregion

        //public string test = AnimationNaming.animationNames["Fight"][0];
    }

    [Serializable]
   public class WhatToImport
   {
      [Header("Models import range")]
      [Min(0)] 
      public int StartIndex;
      [Min(0)]
      public int EndIndex;
      [Min(0)]
      public float ModelScale = 1;
      [Space(10)]
      public bool ImportModel;
      public bool ImportTextures;
      public bool ImportMaterials;
      public bool ImportShinyMaterials;
      public bool ApplyMaterials;
      public bool ApplyShinyMaterials;
      public bool ImportFireMaterials;
      public bool ExportGLB;
      public bool ExportGLTF;
      
      public bool SkeletalAnimations;
      public AnimationImportOptions FightAnimationsToImport;
      public AnimationImportOptions PetAnimationsToImport;
      public AnimationImportOptions MovementAnimationsToImport;
      [Tooltip("Feature in Progress")]
      public bool MaterialAnimations;
      public bool VisibilityAnimations;
      public bool InterpolateAnimations;
      public bool RenameGeneratedAnimationFiles;
      [HideInInspector] public string ExportPath;
      [HideInInspector] public string ImportPath;
      [HideInInspector] public string ExportGLTFPath;
   }
   
   public class P3ds2USettingsScriptableObject : P3ds2USettingsScriptableObjectBase
    {
      [HideInInspector] public string packageVersion = "";

         private bool _generated;
      [SerializeField] public WhatToImport ImporterSettings;
      
      [SerializeField] private List<MergedBinary> mergedBinariesPreview;
      
      public P3ds2UShaderProperties customShaderSettings;

      [Tooltip("Name clips same as in custom controller.")] public P3ds2UAnimatorProperties customAnimatorSettings;

      public static P3ds2USettingsScriptableObject Instance;


      [HideInInspector] public int chosenFormat; // 0 or 1
      public static bool ImportInProgress;
      
        public string ExportPath { get { return ImporterSettings.ExportPath; } }
        public string GLTFExportPath { get { return ImporterSettings.ExportGLTFPath; } }
        public string ImportPath { get { return ImporterSettings.ImportPath; } }
        public bool GetRenameGeneratedAnimationFiles() { return ImporterSettings.RenameGeneratedAnimationFiles; }

        public string PackageVersion => string.IsNullOrEmpty(packageVersion) ? "2.5.0" : packageVersion;

        [Serializable]
        public class PackageJsonObject
        {
           public string name;
           public string displayName;
           public string version;
           public string unity;
           public string description;
           public List<string> keywords;
           public Author originalAuthor;
           public Author author;
           public string category;
            
            [Serializable]
            public class Author
            {
                public string name;
                public string url;
            }
            
        }
        
      private P3ds2USettingsScriptableObject ()
      {
         try {
            var packageJsonObject =
               JsonUtility.FromJson<PackageJsonObject> (File.ReadAllText (Application.dataPath+ "/Pokemon3DStoUnity/package.json"));
            packageVersion = packageJsonObject.version;
         }
         catch (Exception) {
                packageVersion = "2.5.0"; 
            //ignored
         }
            Instance = this;
            InitSettings();
        
        }
      
      public void InitSettings()
        {
            customShaderSettings = new P3ds2UShaderProperties();
            ImporterSettings = new WhatToImport
            {
                StartIndex = 0,
                EndIndex = 0,
                ImportModel = true,
                ImportTextures = true,
                ImportMaterials = true,
                ApplyMaterials = true,
                SkeletalAnimations = true,
                MaterialAnimations = true,
                ImportFireMaterials = false,
                InterpolateAnimations = false,
                VisibilityAnimations = true,
                RenameGeneratedAnimationFiles = true,
                FightAnimationsToImport = new AnimationImportOptions(),
                PetAnimationsToImport = new AnimationImportOptions(),
                MovementAnimationsToImport = new AnimationImportOptions(),
                ExportPath = P3DS2UConfig.ExportPath,
                ImportPath = P3DS2UConfig.ImportPath,
                ExportGLTFPath = P3DS2UConfig.GLTFExportPath,
                ExportGLB = true,
                ExportGLTF = false
            };
            foreach (string animationName in AnimationNaming.animationNames["Fight"])
            {
                ImporterSettings.FightAnimationsToImport.Add(animationName, true);
            }
            foreach (string animationName in AnimationNaming.animationNames["Pet"])
            {
                ImporterSettings.PetAnimationsToImport.Add(animationName, true);
            }
            foreach (string animationName in AnimationNaming.animationNames["Movement"])
            {
                ImporterSettings.MovementAnimationsToImport.Add(animationName, true);
            }
            P3DS2UConfig.RenameGeneratedAnimationFilesFunc = GetRenameGeneratedAnimationFiles;
            RegeneratePreview();
            chosenFormat = 0;
        }

      private void OnEnable ()
      {
         if (!_generated) {
            customShaderSettings.bodyShader = Shader.Find ("Shader Graphs/LitPokemonShader");
            customShaderSettings.irisShader = Shader.Find ("Shader Graphs/LitPokemonIrisShader");
            customShaderSettings.fireCoreShader = Shader.Find ("Shader Graphs/LitPokemonFireCoreShader");
            customShaderSettings.fireStencilShader = Shader.Find ("Shader Graphs/LitPokemonFireStencil");
            _generated = true;
         }
      }

      private Dictionary<string, List<string>> ScenesDict = new Dictionary<string, List<string>> ();

         public void StartImporting (bool _export = false)
         {
             ImportInProgress = true;
             P3DS2UConfig.StartImportingBinaries (this, ScenesDict, _export);
             ImportInProgress = false;
         }

        public void SetImportPath()
        {
            string path = P3DS2UConfig.SaveFolderPanel("Choose Import folder", "Assets/", "Bin3DS");
            if (!string.IsNullOrEmpty(path))
            {
                SetImportPath("Assets" + path.Replace(Application.dataPath+"/", "/")+"/");
            }
        }

        public void SetImportPath(string path)
        {
            ImporterSettings.ImportPath = path;
        }

        public void ResetPaths(int _path = 0)
        {
            switch (_path)
            {
                case 0:
                    ImporterSettings.ImportPath = P3DS2UConfig.ImportPath;
                    break;
                case 1:
                    ImporterSettings.ExportPath = P3DS2UConfig.ExportPath;
                    break;
                case 2:
                    ImporterSettings.ExportGLTFPath = P3DS2UConfig.GLTFExportPath;
                    break;
                default:
                    ImporterSettings.ImportPath = P3DS2UConfig.ImportPath;
                    ImporterSettings.ExportPath = P3DS2UConfig.ExportPath;
                    ImporterSettings.ExportGLTFPath = P3DS2UConfig.GLTFExportPath;
                    break;
            }
        }

        public void SetExportPath()
        {
            string path = P3DS2UConfig.SaveFolderPanel("Choose Export folder", "Assets/", "Exported");
            if (!string.IsNullOrEmpty(path))
            {
                SetExportPath("Assets" + path.Replace(Application.dataPath + "/", "/") + "/");
            }
        }

        public void SetExportPath(string path)
        {
            ImporterSettings.ExportPath = path;
        }

        public void SetGLTFExportPath()
        {
            string path = P3DS2UConfig.SaveFolderPanel("Choose GLTF Export folder", "Assets/", "Exported");
            if (!string.IsNullOrEmpty(path))
            {
                SetGLTFExportPath("Assets" + path.Replace(Application.dataPath + "/", "/") + "/");
            }
        }

        public void SetGLTFExportPath(string path)
        {
            ImporterSettings.ExportGLTFPath = path;
        }

        public void RegeneratePreview ()
      {
         if (ImportInProgress) 
                return;

         if (!System.IO.Directory.Exists(ImportPath))
            Directory.CreateDirectory(ImportPath);

         if (!System.IO.Directory.Exists(ExportPath))
            Directory.CreateDirectory(ExportPath);
 
         ScenesDict = new Dictionary<string, List<string>> ();
         if (chosenFormat == 1) {
            var allFiles = DirectoryUtils.GetAllFilesRecursive (ImporterSettings.ImportPath);
            foreach (var singleFile in allFiles) {
               var trimmedName = Path.GetFileName (singleFile);
               if (!ScenesDict.ContainsKey (trimmedName)) {
                  ScenesDict.Add (trimmedName, new List<string> {singleFile});
               } else {
                  ScenesDict[trimmedName].Add (singleFile);
               }
            }  
         } else {
            var allFolders = Directory.GetDirectories (ImporterSettings.ImportPath);
            foreach (var singleFolder in allFolders) {
               var allFiles = Directory.GetFiles (singleFolder).ToList ();
               for (var i = allFiles.Count - 1; i >= 0; i--) {
                  if (allFiles[i].Contains (".meta")) {
                     allFiles.RemoveAt (i);
                  }
               }

               var trimmedFolderName = Path.GetFileName (singleFolder);
               foreach (var singleFile in allFiles) {
                  if (!ScenesDict.ContainsKey (trimmedFolderName)) {
                     ScenesDict.Add (trimmedFolderName, new List<string> {singleFile});
                  } else {
                     ScenesDict[trimmedFolderName].Add (singleFile);
                  }
               }
            }
         }
         
         mergedBinariesPreview = new List<MergedBinary> ();
         foreach (var scene in ScenesDict) {
            mergedBinariesPreview.Add (new MergedBinary {
               PokemonMergedBinary = scene.Value
            });
         }

         ImporterSettings.EndIndex = mergedBinariesPreview.Count - 1;
      }
   }

   
}
