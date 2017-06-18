using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_WSA_10_0
using Windows.Storage;
using System.Threading.Tasks;
using System;
#endif

namespace UWB_Texturing
{
    /// <summary>
    /// Handles logic with unbundling asset bundle.
    /// </summary>
    public static class BundleHandler
    {
        #region Methods
        /// <summary>
        /// Runs through the logic of unpacking the room texture bundle, then 
        /// takes the information extracted to generate the room mesh/room 
        /// mesh material appropriately. Assumes certain asset names, asset 
        /// bundle names, and folder locations.
        /// 
        /// NOTE: There is some issue with using constants when specifying 
        /// asset names in a bundle, so names are HARDCODED in the method.
        /// </summary>
        public static void UnpackRoomTextureBundle()
        {
            // Ensure that previous room items (resources & game objects) are deleted
            RemoveRoomObject();
            
            AssetBundle roomTextureBundle = AssetBundle.LoadFromFile(CrossPlatformNames.AssetBundle.FilePath);

            // Extract specific text file assets
            // NOTE: Asset name has to be hardcoded.
            TextAsset roomMatricesTextAsset = roomTextureBundle.LoadAsset("RoomMatrices".ToLower()) as TextAsset;

            // Extract camera matrices
            Matrix4x4[] WorldToCameraMatrixArray;
            Matrix4x4[] ProjectionMatrixArray;
            Matrix4x4[] LocalToWorldMatrixArray;
            MatrixArray.LoadMatrixArrays_FromAssetBundle(roomMatricesTextAsset, out WorldToCameraMatrixArray, out ProjectionMatrixArray, out LocalToWorldMatrixArray);

            // Extract room mesh & info
            // NOTE: Asset names have to be hardcoded.
            TextAsset supplementaryInfoTextAsset = roomTextureBundle.LoadAsset("SupplementaryInfo".ToLower()) as TextAsset;
            TextAsset roomMeshTextAsset = roomTextureBundle.LoadAsset("RoomMesh".ToLower()) as TextAsset;
#if UNITY_EDITOR
            GameObject RoomMesh = CustomMesh.InstantiateRoomObject(roomMeshTextAsset, supplementaryInfoTextAsset, true);
#else
            GameObject RoomMesh = CustomMesh.InstantiateRoomObject(roomMeshTextAsset, supplementaryInfoTextAsset, false);
#endif

            // Extract textures
            //Texture2D[] bundledTexArray = roomTextureBundle.LoadAllAssets<Texture2D>();
            Texture2D[] rawBundledTexArray = roomTextureBundle.LoadAllAssets<Texture2D>();
            Texture2D[] bundledTexArray = new Texture2D[rawBundledTexArray.Length];
            for(int i = 0; i < rawBundledTexArray.Length; i++)
            {
                int imageIndex = int.Parse(rawBundledTexArray[i].name.Substring(CrossPlatformNames.Images.Prefix.Length));
                bundledTexArray[imageIndex] = rawBundledTexArray[i];
            }

            if (bundledTexArray == null)
            {
                Debug.Log("Null tex array");
            }
            else
            {
                Debug.Log("Bundled tex array size = " + bundledTexArray.Length);
            }

            // Create Texture2DArray, copy items from text asset into array, 
            // and push into shader
            Texture2DArray TextureArray = new Texture2DArray(bundledTexArray[0].width, bundledTexArray[0].height, bundledTexArray.Length, bundledTexArray[0].format, false);
            if (WorldToCameraMatrixArray != null
                && ProjectionMatrixArray != null
                && LocalToWorldMatrixArray != null
                && bundledTexArray != null
                && RoomMesh != null
                && TextureArray != null)
            {
                for (int i = 0; i < bundledTexArray.Length; i++)
                {
                    Graphics.CopyTexture(bundledTexArray[i], 0, 0, TextureArray, i, 0);
                }

#if UNITY_EDITOR
                if (!Directory.Exists(CrossPlatformNames.Texture2DArray.AbsoluteAssetFolder))
                {
                    Directory.CreateDirectory(CrossPlatformNames.Texture2DArray.AbsoluteAssetFolder);
                }

                // Save Texture2DArray as asset if appropriate
                AssetDatabase.CreateAsset(TextureArray, CrossPlatformNames.Texture2DArray.LocalAssetPath);
                AssetDatabase.SaveAssets();
#endif

                RoomModel roomManager = RoomMesh.AddComponent<RoomModel>();
                roomManager.FirstTimeSetup(CrossPlatformNames.RoomObject.RecommendedShaderRefreshTime, TextureArray, WorldToCameraMatrixArray, ProjectionMatrixArray, LocalToWorldMatrixArray);

                //GameObject.Find(RoomModel.GameObjectName).GetComponent<RoomModel>().BeginShaderRefreshCycle(RoomModel.RecommendedShaderRefreshTime, TextureArray, WorldToCameraMatrixArray, ProjectionMatrixArray, LocalToWorldMatrixArray);

                // ERROR TESTING - REMOVE
                //RoomModel.SetShaderParams(TextureArray, WorldToCameraMatrixArray, ProjectionMatrixArray, LocalToWorldMatrixArray);
            }
            else
            {
                roomTextureBundle.Unload(true);
                throw new System.Exception("Asset bundle unload failed.");
            }

            // Unload asset bundle so future loads will not fail
            roomTextureBundle.Unload(true);
        }
        
#region Helper Functions

        public static void CleanAssetBundleGeneration()
        {
            // Clean up erroneous asset bundle generation
            string[] bundleFilepaths = Directory.GetFiles(CrossPlatformNames.AssetBundle.OutputFolder);
            for (int i = 0; i < bundleFilepaths.Length; i++)
            {
                string bundleFilepath = bundleFilepaths[i];
                string bundleFilename = Path.GetFileNameWithoutExtension(bundleFilepath);
                if (bundleFilename.Equals(CrossPlatformNames.AssetBundle.OutputSubFolder))
                {
                    File.Delete(bundleFilepath);
                }
            }
        }
        
        public static void RemoveRoomObject()
        {
            GameObject room = GameObject.Find(CrossPlatformNames.RoomObject.GameObjectName);
            if(room != null)
            {
                GameObject.DestroyImmediate(room);
            }
        }

        // Assumes all resources sit in a subfolder in the resources folder
        public static void RemoveRoomResources()
        {
            // Remove materials
            string materialAssetFolder = CrossPlatformNames.Material.AbsoluteAssetFolder;
            if (Directory.Exists(materialAssetFolder))
            {
                string[] files = Directory.GetFiles(materialAssetFolder);
                for(int i = 0; i < files.Length; i++)
                {
                    if (files[i].Contains(CrossPlatformNames.Material.MaterialName))
                    {
                        File.Delete(files[i]);
                    }
                }
            }

            // Remove meshes
            string meshAssetFolder = CrossPlatformNames.Mesh.AbsoluteAssetFolder;
            if (Directory.Exists(meshAssetFolder))
            {
                string[] files = Directory.GetFiles(meshAssetFolder);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Contains(CrossPlatformNames.Mesh.MeshPrefix))
                    {
                        File.Delete(files[i]);
                    }
                }
            }

            // Remove Texture2DArray
            string textureArrayFolder = CrossPlatformNames.Texture2DArray.AbsoluteAssetFolder;
            if (Directory.Exists(textureArrayFolder))
            {
                string[] files = Directory.GetFiles(textureArrayFolder);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Contains(CrossPlatformNames.Texture2DArray.ArrayName))
                    {
                        File.Delete(files[i]);
                    }
                }
            }

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
#endregion
#endregion
    }
}