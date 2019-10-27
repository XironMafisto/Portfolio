using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class ResourceManager : MonoBehaviour
    {
        public List<LevelGameObjectBase> LevelGameObjects = new List<LevelGameObjectBase>();
        public List<LevelStackedObjsBase> LevelGameObjects_Stacking = new List<LevelStackedObjsBase>();
        public List<Material> LevelMaterials = new List<Material>();
        public GameObject wallPrefab;

        private static ResourceManager instance = null;

        void Awake()
        {
            instance = this;
        }

        public static ResourceManager GetInstance()
        {
            return instance;
        }

        public LevelGameObjectBase GetObjBase(string objId)
        {
            LevelGameObjectBase retVal = null;

            for(int i  = 0; i < LevelGameObjects.Count; i++)
            {
                if (objId.Equals(LevelGameObjects[i].obj_id))
                {
                    retVal = LevelGameObjects[i];
                    break;
                }
            }

            return retVal;
        }

        public LevelStackedObjsBase GetStackedObjsBase(string stacked_id)
        {
            LevelStackedObjsBase retVal = null;

            for(int i = 0; i < LevelGameObjects_Stacking.Count; i++)
            {
                if (stacked_id.Equals(LevelGameObjects_Stacking[i].stack_id))
                {
                    retVal = LevelGameObjects_Stacking[i];
                    break;
                }
            }
            return retVal;
        }

        public Material GetMaterial(int matId)
        {
            Material retVal = null;

            for(int i = 0; i <  LevelMaterials.Count; i++)
            {
                if(matId == i)
                {
                    retVal = LevelMaterials[i];
                    break;
                }
            }
            return retVal;
        }

        public int GetMaterialId(Material mat)
        {
            int id = -1;

            for(int i = 0; i < LevelMaterials.Count; i++)
            {
                if (mat.Equals(LevelMaterials[i]))
                {
                    id = i;
                    break;
                }
                
            }

            return id;
        }

    }

    [System.Serializable]
    public class LevelGameObjectBase
    {
        public string obj_id;
        public GameObject objPrefab;
    }

    [System.Serializable]
    public class LevelStackedObjsBase
    {
        public string stack_id;
        public GameObject objPrefab;
    }
}