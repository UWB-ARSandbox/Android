using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

namespace UWB_Texturing
{
    /// <summary>
    /// Handles Unity Editor hooks for bundling and interpreting asset bundles 
    /// related to the Hololens room mesh texturing.
    /// </summary>
    public static class BundleMenu
    {
#if UNITY_EDITOR
        /// <summary>
        /// Unity Editor hook for packing room texture bundle. Gathers the 
        /// textures, camera matrices, room mesh text file, and supplementary 
        /// room mesh information together into an asset bundle for convenient 
        /// storage and transportation between Unity nodes.
        /// 
        /// NOTE: This logic should not be separated from this method, as 
        /// bundling asset bundles can ONLY OCCUR INSIDE OF THE UNITY EDITOR
        /// /WITH THE UNITY EDITOR NAMESPACE. This means that this logic cannot 
        /// ever operate on any ASL node outside of a client that is running 
        /// the editor.
        /// </summary>
        [UnityEditor.MenuItem("Room Texture/Intermediary Processing/Export Asset Bundle", false, 0)]
        public static void BundleRoomTexture_WindowsStandalone()
        {
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

            // Bundle room texture together
            buildMap[0] = new AssetBundleBuild();
            buildMap[0].assetBundleName = CrossPlatformNames.AssetBundle.IntermediaryProcessingName;

            // Gather number of assets to place into asset bundle
            int numTextures = MaterialManager.GetNumTextures();
            int numMeshFiles = 1;
            int numSupplementaryMeshInfoFiles = 1;
            int numMatrixFiles = 1;
            int numAssets = numTextures + numMeshFiles + numSupplementaryMeshInfoFiles + numMatrixFiles;
            string[] textureAssets = new string[numAssets];

            // Assign assets to asset bundle
            int index = 0;
            // Textures
            for (; index < numTextures; index++)
            {
                textureAssets[index] = Path.Combine(CrossPlatformNames.AssetBundle.InputFolder, CrossPlatformNames.Images.CompileFilename(index));
            }
            // Mesh
            textureAssets[index++] = Path.Combine(CrossPlatformNames.AssetBundle.InputFolder, CrossPlatformNames.Mesh.OutputFilename);
            // Mesh Supplementary Info
            textureAssets[index++] = Path.Combine(CrossPlatformNames.AssetBundle.InputFolder, CrossPlatformNames.Mesh.SupplementaryOutputFilename);
            // Matrix Arrays
            textureAssets[index++] = Path.Combine(CrossPlatformNames.AssetBundle.InputFolder, CrossPlatformNames.Matrices.Filename);
            buildMap[0].assetNames = textureAssets;

            // Write asset bundle
            try
            {
                BuildPipeline.BuildAssetBundles(CrossPlatformNames.AssetBundle.OutputFolder, buildMap, BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);
            }
            catch (System.ArgumentException)
            {
                Directory.CreateDirectory(CrossPlatformNames.AssetBundle.OutputFolder);
                Debug.Log("Asset Bundle folder created: " + CrossPlatformNames.AssetBundle.OutputFolder);
                BuildPipeline.BuildAssetBundles(CrossPlatformNames.AssetBundle.OutputFolder, buildMap, BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);
            }

            BundleHandler.CleanAssetBundleGeneration();

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Unity Editor hook for unpacking room texture bundle and pushing through
        /// logic of generating material/meshes appropriately.
        /// </summary>
        [UnityEditor.MenuItem("Room Texture/Intermediary Processing/Import Asset Bundle", false, 0)]
        public static void UnbundleRoomTexture()
        {
            BundleHandler.UnpackRoomTextureBundle();
        }
        
        [UnityEditor.MenuItem("Room Texture/Remove Assets/All")]
        public static void ClearRoomAssets()
        {
            BundleHandler.RemoveRoomObject();
            BundleHandler.RemoveRoomResources();
            PrefabHandler.DeletePrefabs();
        }

        [UnityEditor.MenuItem("Room Texture/Remove Assets/Room Object")]
        public static void ClearRoomObject()
        {
            BundleHandler.RemoveRoomObject();
        }

        [UnityEditor.MenuItem("Room Texture/Remove Assets/Stored Assets")]
        public static void ClearRoomResources()
        {
            BundleHandler.RemoveRoomResources();
        }

        [UnityEditor.MenuItem("Room Texture/Remove Assets/Prefabs")]
        public static void ClearRoomPrefabs()
        {
            PrefabHandler.DeletePrefabs();
        }

        // ERROR TESTING - UPDATE W/ CORRECT LOGIC
        [UnityEditor.MenuItem("Room Texture/Build Finished Room Bundle")]
        public static void BundleRoom_WindowsStandalone()
        {
            GameObject room = GameObject.Find(CrossPlatformNames.RoomObject.GameObjectName);
            if (room != null)
            {

                AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

                // Bundle room texture items together
                buildMap[0] = new AssetBundleBuild();
                buildMap[0].assetBundleName = CrossPlatformNames.AssetBundle.StandaloneRoomName;

                // Gather number of assets to place into asset bundle
                int numMaterials = MaterialManager.GetNumMaterials();
                int numMeshFiles = CustomMesh.GetNumMeshes();
                int numTexArrays = 1;
                int numRoomPrefabs = 1;
                int numShaders = 1;
                int numMatrixFiles = 1;
                int numAssets = numMaterials + numMeshFiles + numTexArrays + numRoomPrefabs + numShaders + numMatrixFiles;
                string[] roomAssets = new string[numAssets];

                // Assign assets to asset bundle
                int index = 0;
                // Materials
                for (int i = 0; i < numMaterials; i++)
                {
                    string materialName = CrossPlatformNames.Material.CompileAssetName(i);
                    roomAssets[index++] = CrossPlatformNames.Material.CompileAbsoluteAssetPath(materialName);
                }
                // Meshes
                for(int i = 0; i < numMeshFiles; i++)
                {
                    string meshName = CrossPlatformNames.Mesh.CompileMeshName(i);
                    roomAssets[index++] = CrossPlatformNames.Mesh.CompileAbsoluteAssetPath(meshName);
                }
                // Texture2DArray
                roomAssets[index++] = CrossPlatformNames.Texture2DArray.CompileAbsoluteAssetPath();
                // Room Prefab
                roomAssets[index++] = CrossPlatformNames.Prefab.CompileAbsoluteAssetPath();
                // Shaders
                roomAssets[index++] = CrossPlatformNames.Shader.CompileAbsoluteAssetPath();
                // Matrix Files
                roomAssets[index++] = CrossPlatformNames.Matrices.Filepath;
                buildMap[0].assetNames = roomAssets;

                // Write asset bundle
                try
                {
                    BuildPipeline.BuildAssetBundles(CrossPlatformNames.AssetBundle.OutputFolder, buildMap, BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);
                }
                catch (System.ArgumentException)
                {
                    Directory.CreateDirectory(CrossPlatformNames.AssetBundle.OutputFolder);
                    Debug.Log("Asset Bundle folder created: " + CrossPlatformNames.AssetBundle.OutputFolder);
                    BuildPipeline.BuildAssetBundles(CrossPlatformNames.AssetBundle.OutputFolder, buildMap, BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);
                }

                BundleHandler.CleanAssetBundleGeneration();

                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log("Asset bundle of processed room failed. Does the room object exist in the scene?");
            }
        }

        /// <summary>
        /// Mashes together the menu and submenu names, separated by '/' for 
        /// use with Unity's interface.
        /// </summary>
        /// <param name="menuAndSubMenus">
        /// The menu name and submenu strings, not including any delimiters.
        /// </param>
        /// <returns>
        /// A string that is parsable by Unity's interface to properly name a 
        /// menu item.
        /// </returns>
        public static string CompileMenuName(string[] menuAndSubMenus)
        {
            if(menuAndSubMenus == null
                || menuAndSubMenus.Length < 1)
            {
                return string.Empty;
            }
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(menuAndSubMenus[0]);
            for(int i = 1; i < menuAndSubMenus.Length; i++)
            {
                sb.Append('/');
                sb.Append(menuAndSubMenus[i]);
            }

            return sb.ToString();
        }
#endif
    }
}