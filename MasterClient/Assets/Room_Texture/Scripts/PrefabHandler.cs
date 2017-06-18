using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

namespace UWB_Texturing
{
    public class PrefabHandler : MonoBehaviour
    {
        public static class Messages
        {
            public static string GameObjectDoesNotExist = "Room prefab generation failed. Does Room object exist in the scene object hierarchy?";
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Room Texture/Prefab/Generate Finished Room Prefab", false, 0)]
        public static void CreatePrefab_Room()
        {
            GameObject roomModel = GameObject.Find(CrossPlatformNames.RoomObject.GameObjectName);
            if(roomModel != null)
            {
                CreatePrefab(roomModel);
            }
            else
            {
                Debug.Log(Messages.GameObjectDoesNotExist);
            }
        }

        private static void CreatePrefab(GameObject obj)
        {
            if (obj != null)
            {
                //AssetDatabase.CreateAsset(obj, "Assets/Room Texture/Resources/Test.obj");
                //AssetDatabase.SaveAssets();
                //AssetDatabase.Refresh();

                if (!Directory.Exists(CrossPlatformNames.Prefab.AbsoluteOutputFolder))
                {
                    Directory.CreateDirectory(CrossPlatformNames.Prefab.AbsoluteOutputFolder);
                    Debug.Log("Prefab folder created: " + CrossPlatformNames.Prefab.AbsoluteOutputFolder);
                }
                //var emptyPrefab = PrefabUtility.CreateEmptyPrefab(CrossPlatformNames.Prefab.CompileCompatibleOutputFolder() + '/' + CrossPlatformNames.Prefab.Filename);
                //PrefabUtility.ReplacePrefab()
                //PrefabUtility.CreatePrefab(CrossPlatformNames.Prefab.CompileCompatibleOutputFolder() + '/' + CrossPlatformNames.Prefab.Filename, AssetDatabase.Find)

                PrefabUtility.CreatePrefab(CrossPlatformNames.Prefab.CompileCompatibleOutputFolder() + '/' + CrossPlatformNames.Prefab.Filename, obj);
                //PrefabUtility.CreatePrefab(CrossPlatformNames.Prefab.OutputFilepath, obj);
                Debug.Log("Room prefab generated at " + CrossPlatformNames.Prefab.OutputFolder);
                //Debug.Log("Room path = " + CrossPlatformNames.Prefab.OutputFilepath);
                Debug.Log("Room path = " + CrossPlatformNames.Prefab.CompileCompatibleOutputFolder() + '/' + CrossPlatformNames.Prefab.Filename);
            }
            else
            {
                Debug.Log(Messages.GameObjectDoesNotExist);
            }
        }

        private static void CreatePrefab(string gameObjectName)
        {
            GameObject obj = GameObject.Find(gameObjectName);
            CreatePrefab(obj);
        }

        public static void DeletePrefabs()
        {
            string absoluteFilepath = Path.Combine(CrossPlatformNames.Prefab.AbsoluteOutputFolder, CrossPlatformNames.Prefab.Filename);
            File.Delete(absoluteFilepath);
            //absoluteFilepath = Path.Combine(CrossPlatformNames.Prefab.AbsoluteOutputFolder, CrossPlatformNames.Prefab.StandaloneRoom.Filename);
            //File.Delete(absoluteFilepath);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
#endif
    }
}