using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace P3DS2U.Editor
{
    public class P3DS2UConfigProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (P3DS2UConfig.NeedToImportFunc == null)
            {
                P3DS2UConfig.NeedToImportFunc = PokemonImporter.NeedToImportAnimation;
            }

            if (P3DS2UConfig.StartImportingBinariesEvent == null)
            {
                P3DS2UConfig.StartImportingBinariesEvent = PokemonImporter.StartImportingBinaries;
            }
        }
    }
}
