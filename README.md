![chx](https://user-images.githubusercontent.com/3764951/110683736-5b67b200-8202-11eb-8801-1ba969f6714c.gif)

Compatibility:
- https://docs.google.com/spreadsheets/d/1-3NI6vUMltge9d57bbdNkJ9jBEYdKAf59i2BfbgTiGs/edit#gid=1937181896

Note these important things:
- This is a fork of Opeious work, so this is meant only for adding functionalities. 
- The whack runtime version requires NET 4.x so NET Framework must be selected under Edit > Project Settings > Player. The reason is the usage of Reflection for future standalone import and export.
- This plugin mainly works with the Universal Render Pipeline 12+ at the moment. You can use it for Built-in with 2021.2 and above but you need to install Shader Graph through PackageManager. For HDRP, add that target to every 3DS2Unity shader. 
- Exported GLTF/GLB will need you to make the custom shaders and material configuration in Blender, Unreal... They won't work out of the box outside Unity but you can now export them in batch.
- Exporting as GLTF creates the .bin required to handle the buffers. This might cause Unity to lock them until you close the editor and might not let you to delete them, so use GLB as your main export output.

Installation:

- Clone with git or download and put the entire project into your Assets Folder, under Pokemon3DS2Unity folder (Assets/Pokemon3DS2Unity/).
- In Releases section you can find a .zip with the .unitypackage inside.

Usage:
-
![2021-02-27 22_53_24-Pokemon3DStoUnity - SampleScene - PC, Mac   Linux Standalone - Unity 2019 4 18f1](https://user-images.githubusercontent.com/3764951/109395128-c8509180-7950-11eb-8b5b-2243dcf1f899.png)

- From the top bar click 3DStoUnity and hit import
- Place your 3DS files in `Assets/Bin3DS` that would've been created
- Hit import again
- Exported files and prefabs get added to  `Assets/Exported` by default. You can set a custom folder inside your Assets folder by clicking on `[...]` button.
- Exported files and prefabs get added to  `Assets/GLTF_Exported` by default. You can set a custom folder inside your Assets folder by clicking on `[...]` button.

For a step-by-step guide / more info: https://gbatemp.net/threads/tutorial-export-your-pokemon-animated-models-from-3ds-to-unity-engine-3d.532962/ 

Troubleshooting:
-
If you face any issues or just looking for a community of likeminded developers:  https://discord.gg/pYBWwAa (Discord Server)

Contributions:
-
If you would like to contribute to this project, I would recommend going through the TODOs at the top of `PokemonImporter.cs` in the `main` branch of the original repository:
https://github.com/opeious/Pokemon3DStoUnity/blob/main/Editor/PokemonImporter.cs


Settings to change for Fire to work:
-
![2021-03-11 00_01_26-NVIDIA GeForce Overlay](https://user-images.githubusercontent.com/3764951/110681966-69b4ce80-8200-11eb-9334-269ffc47ba27.png)
- Add two new layers called "FireCore" and "FireMask"

- Either create a new ForwardRenderer (sample attached in root of this repo), or select your existing ForwardRenderer
- The default ForwardRenderer will be with URP settings in `Assets/Setting`
- In the ForwardRenderer: From Opaque Layer Mask remove FireCore and FireMask.
- Add two render objects one for each layer, for their individual settings refer the image below
- In your project settings. (URP Renderer Settings) If you created a new ForwardRenderer, set the new one as default

![2021-03-10 23_58_49-NVIDIA GeForce Overlay](https://user-images.githubusercontent.com/3764951/110682434-ec3d8e00-8200-11eb-977a-b309efb81cce.png)


What does the package do at the moment:
-
- Open the Binary using SPICA's H3D parser
- Translate the 3D model to Unity Mesh system and skinned mesh renderers
- Generate material files from the textures 
- Automatically copy over some of the shader settings to the newly created materials
- Skins and generates the skeleton for the mesh
- Generates skeletal animations from the binaries
- Generate material / vis animations and append them to skeletal animations
- Renames animations, skips repeated animations
- Saves the translated mon as prefab
- Supports bulk processing

Updating:
-
- Unity package manager doesn't currently support version of git packages, for now just remove the project as a dependency and add it again

ChangeLog:
- v2.3-2.5: support for automatized exporter with UnityGLTF dev branch and some refactorization. NET 4.x is now mandatory as I use reflection for future runtime app.
- v2.2: Skip null data (E.g Altaria)
- v2.1: Ability to override animation controller during import, List<uint> fix (2019.1 and below)
- v2.0: Added option to generate and apply shiny materials
- v1.9: Fix (Changed parser to allow reading of Binaries that are set to read only.)
- v1.8: Fixes, delete settings to regenerate defaults, custom shader support for material animations
- v1.7: Fire Shader
- v1.6: Full material / vis animation support, bunch of fixes related to animations and skipping corrupt files etc.
- v1.5: Added Visiblity animations, fixed iris/body shaders, Material aniamtions (preview)
- v1.4: Added UI, made the plugin customizable, animation renaming, etc
- v1.3: Skeletal animation generation
- v1.2: Skinning and rigging fixes
- v1.1: sample toon shader, asset creation
- v1: basic skeleton, model, texture generation

![2021-02-27 22_43_19-NVIDIA GeForce Overlay](https://user-images.githubusercontent.com/3764951/109395153-e4543300-7950-11eb-8351-e42af713c374.png)
![2021-02-27 23_07_57-PokeUnity - SampleScene - PC, Mac   Linux Standalone - Unity 2019 4 8f1 Personal](https://user-images.githubusercontent.com/3764951/109395156-e918e700-7950-11eb-83c9-4923417450f1.png)![syl2](https://user-images.githubusercontent.com/3764951/110213468-330c4a80-7ec6-11eb-8f94-02aa35e54abc.gif)



