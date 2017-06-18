using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UWB_Texturing
{
    /// <summary>
    /// Stores constants or logic for items that are, or could potentially be, 
    /// shared between different platforms. Also stores constants or logic 
    /// regarding items that are shared between different classes to avoid 
    /// potential dependency issues.
    /// 
    /// If you can't find a constant in a file, it's probably here.
    /// </summary>
    public static class CrossPlatformNames
    {
        public static string ResourcesSubFolder = "Room";
        public static string AssetSubFolder = "Room_Texture" + '/' + "Resources" + '/' + ResourcesSubFolder;
        public static string LocalAssetFolder = "Assets" + '/' + AssetSubFolder;
        public static string AbsoluteAssetFolder = Path.Combine(Application.dataPath, AssetSubFolder);

        /// <summary>
        /// Information regarding naming convention for room texture images 
        /// passed around.
        /// </summary>
        public static class Images
        {
            /// <summary>
            /// Prefix for the room texture images that will be saved/transported.
            /// </summary>
            public static string Prefix = "Room";
            /// <summary>
            /// Extension for the room texture images that will be 
            /// saved/transported.
            /// </summary>
            public static string Extension = ".png";
            
            /// <summary>
            /// Suffix for the room texture images that will be saved/transported. 
            /// Adjoins directly after the prefix with no delimiters in between 
            /// (i.e. fullFilename = prefix + suffix + extension;).
            /// </summary>
            /// <param name="imageIndex">
            /// The index of the image that will help uniquely identify it.
            /// </param>
            /// <returns>
            /// A string identifying the room's suffix
            /// </returns>
            public static string GetSuffix(int imageIndex)
            {
                return imageIndex.ToString();
            }

            /// <summary>
            /// Compile the full file name (without directories), given an image 
            /// index.
            /// </summary>
            /// <param name="imageIndex">
            /// The index of the image that will help uniquely identify it.
            /// </param>
            /// <returns>
            /// A string identifying the room texture image.
            /// </returns>
            public static string CompileFilename(int imageIndex)
            {
                return Prefix + GetSuffix(imageIndex) + Extension;
            }
        }

        public static class Mesh
        {
            public static string OutputFilename = "RoomMesh.txt";
            public static string SupplementaryOutputFilename = "SupplementaryInfo.txt";
#if UNITY_WSA_10_0
            public static string OutputFilepath = Application.persistentDataPath + "/" + OutputFilename;
            public static string SupplementaryOutputFilepath = Application.persistentDataPath + "/" + SupplementaryOutputFilename;
#else
            public static string OutputFilepath = Path.Combine(AssetBundle.InputFolder, OutputFilename);
            public static string SupplementaryOutputFilepath = Path.Combine(AssetBundle.InputFolder, SupplementaryOutputFilename);
#endif

            public static string ResourcesSubFolder = CrossPlatformNames.ResourcesSubFolder;
            public static string LocalAssetFolder = CrossPlatformNames.LocalAssetFolder;
            public static string AbsoluteAssetFolder = CrossPlatformNames.AbsoluteAssetFolder;
            public static string AssetExtension = ".obj";

            public static string MeshPrefix = "RoomMesh";
            public static string CompileMeshName(int index)
            {
                return MeshPrefix + '_' + index;
            }

            public static string CompileAssetPath(string meshName)
            {
                return LocalAssetFolder + '/' + meshName + AssetExtension;
            }
            public static string CompileAbsoluteAssetPath(string meshName)
            {
                return Path.Combine(AbsoluteAssetFolder, meshName + AssetExtension);
            }

            public static string GetResourcesLoadPath(string assetNameWithoutExtension)
            {
                return ResourcesSubFolder + '/' + assetNameWithoutExtension;
            }
        }

        public static class Material
        {
            public static string ResourcesSubFolder = CrossPlatformNames.ResourcesSubFolder;
            public static string LocalAssetFolder = CrossPlatformNames.LocalAssetFolder;
            public static string AbsoluteAssetFolder = CrossPlatformNames.AbsoluteAssetFolder;
            public static string AssetExtension = ".mat";

            public static string CompileAssetPath(string materialName)
            {
                return LocalAssetFolder + '/' + materialName + AssetExtension;
            }
            public static string CompileAbsoluteAssetPath(string materialName)
            {
                return Path.Combine(AbsoluteAssetFolder, MaterialName + AssetExtension);
            }
            
            /// <summary>
            /// Name of the material that will be generated. Will be followed 
            /// by a suffix specified during material generation.
            /// </summary>
            public static string MaterialName = "RoomMaterial";

            public static string CompileAssetName(int roomChildIndex)
            {
                return MaterialName + '_' + roomChildIndex;
            }
            public static string GetResourcesLoadPath(string assetNameWithoutExtension)
            {
                return ResourcesSubFolder + '/' + assetNameWithoutExtension;
            }
        }

        public static class Matrices
        {
            public static string Extension = ".txt";
            public static string FilenameWithoutExtension = "RoomMatrices";
            public static string Filename = FilenameWithoutExtension + Extension;
#if UNITY_WSA_10_0
            public static string Filepath = Application.persistentDataPath + "/" + Filename;
#else
            public static string Filepath = Path.Combine(AssetBundle.InputFolder, Filename);
#endif
        }

        public static class Texture2DArray
        {
            public static string ResourcesSubFolder = CrossPlatformNames.ResourcesSubFolder;
            public static string LocalAssetFolder = CrossPlatformNames.LocalAssetFolder;
            public static string AbsoluteAssetFolder = CrossPlatformNames.AbsoluteAssetFolder;
            public static string AssetExtension = ".asset";
            public static string ArrayName = "RoomTextureArray";
            public static string LocalAssetPath = LocalAssetFolder + '/' + ArrayName + AssetExtension;

            public static string CompileAbsoluteAssetPath()
            {
                return Path.Combine(AbsoluteAssetFolder, ArrayName + AssetExtension);
            }

            public static string GetResourcesLoadPath(string assetNameWithoutExtension)
            {
                return ResourcesSubFolder + '/' + assetNameWithoutExtension;
            }
        }

        public static class AssetBundle
        {
            /// <summary>
            /// Name for the room texture information asset bundle. Bundle 
            /// includes textures and text files representing the room mesh, 
            /// localToWorld matrices (transforms model coordinate space to 
            /// world coordinate space), worldToCamera matrices (transforms 
            /// world coordinate space to camera/view space), projection 
            /// matrices (transforms camera/view space to clip space), and 
            /// supplementary information regarding the mesh (positions & 
            /// rotations).
            /// 
            /// This information is designed to be used by the UWB_Texturing 
            /// namespace classes to generate a final RoomMesh object from 
            /// the components described.
            /// </summary>
            public static string IntermediaryProcessingName = "roomtexture";
            /// <summary>
            /// Name for the standalone, textured room mesh GameObject asset 
            /// bundle.
            /// </summary>
            public static string StandaloneRoomName = "room";


            public static string InputSubFolder = "TextureData";
            public static string InputFolder = Path.Combine(Path.Combine("Assets", "Room_Texture"), InputSubFolder);
            public static string OutputSubFolder = "Asset Bundles";
            public static string OutputFolder = Path.Combine(Path.Combine(Application.dataPath, "Room_Texture"), OutputSubFolder);
            public static string FilePath = Path.Combine(OutputFolder, IntermediaryProcessingName);
        }

        public static class Prefab
        {
            //public static string OutputSubFolder = "Resources"
            //public static string OutputFolder = Path.Combine(Path.Combine("Assets", "Room_Texture"), OutputSubFolder);
            public static string OutputFolder = CrossPlatformNames.LocalAssetFolder;
            //public static string AbsoluteOutputFolder = Path.Combine(Path.Combine(Application.dataPath, "Room_Texture"), OutputSubFolder);
            public static string CompileCompatibleOutputFolder()
            {
                string[] components = OutputFolder.Split('\\');
                if (components.Length > 0)
                {
                    string compatibleOutputFolder = components[0];
                    for (int i = 1; i < components.Length; i++)
                    {
                        compatibleOutputFolder += '/';
                        compatibleOutputFolder += components[i];
                    }

                    return compatibleOutputFolder;
                }
                else
                {
                    return string.Empty;
                }
            }

            public static string AbsoluteOutputFolder = CrossPlatformNames.AbsoluteAssetFolder;

            //public static class IntermediaryProcessing {
            public static string FilenameWithoutExtension = "Room";
            public static string FileExtension = ".prefab";
            public static string Filename = FilenameWithoutExtension + FileExtension;
            public static string CompileAbsoluteAssetPath()
            {
                return Path.Combine(AbsoluteOutputFolder, Filename);
            }
            //}

            //public static class StandaloneRoom
            //{
            //    public static string FilenameWithoutExtension = "Room";
            //    public static string FileExtension = ".prefab";
            //    public static string Filename = FilenameWithoutExtension + FileExtension;

            //    public static string CompileAbsoluteAssetPath()
            //    {
            //        return Path.Combine(AbsoluteOutputFolder, Filename);
            //    }
            //}

            //public static string GetAbsoluteOutputFolder()
            //{
            //    string[] directories = CrossPlatformNames.LocalAssetFolder.Split('/');

            //    string folder = "";

            //    // Assumes "Assets" is the first directory to come up
            //    if (directories.Length > 0)
            //    {
            //        folder = Path.Combine(Application.dataPath, directories[1]);
            //        for (int i = 2; i < directories.Length; i++)
            //        {
            //            Path.Combine(folder, directories[i]);
            //        }
            //    }

            //    return folder;
            //}
        }

        public static class RoomObject
        {
            public static string GameObjectName = "Room";
            public static float RecommendedShaderRefreshTime = 5.0f;
        }

        public static class Shader
        {
            public static string ShaderSubFolder = "Shaders";
            public static string AbsoluteAssetFolder = Path.Combine(CrossPlatformNames.AbsoluteAssetFolder, ShaderSubFolder);


            /// <summary>
            /// Name of the shader without the type listed or extension used.
            /// </summary>
            public static string ShaderName = "MyRoomShader";
            /// <summary>
            /// Name of the shader prefaced by the the type of shader it is. 
            /// Required when using Shader.Find();
            /// </summary>
            public static string ShaderName_Full = "Unlit/" + ShaderName;
            public static string Extension = ".shader";

            public static string CompileAbsoluteAssetPath()
            {
                return Path.Combine(AbsoluteAssetFolder, ShaderName + Extension);
            }
        }
    }
}