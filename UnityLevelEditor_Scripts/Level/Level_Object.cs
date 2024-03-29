﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{

    public class Level_Object : MonoBehaviour
    {
        public string obj_Id;
        public int gridPosX;
        public int gridPosZ;
        public GameObject nodeVisualization;
        public Vector3 worldPositionOffset;
        public Vector3 worldRotation;

        public bool isStackableObj = false;
        public bool isWallObject = false;

        public float rotateDegrees = 90;

        public void UpdateNode(Node[,] grid)
        {
            Node node = grid[gridPosX, gridPosZ];

            Vector3 worldPosistion = node.vis.transform.position;
            worldPosistion += worldPositionOffset;
            transform.rotation = Quaternion.Euler(worldRotation);
            transform.position = worldPosistion;
        }

        public void ChangeRotation()
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, rotateDegrees, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }

        public SaveableLevelObject GetSaveableObject()
        {
            SaveableLevelObject savedObj = new SaveableLevelObject();
            savedObj.obj_Id = obj_Id;
            savedObj.posX = gridPosX;
            savedObj.posZ = gridPosZ;

            worldRotation = transform.localEulerAngles;

            savedObj.rotX = worldRotation.x;
            savedObj.rotY = worldRotation.y;
            savedObj.rotZ = worldRotation.z;
            savedObj.isWallObject = isWallObject;
            savedObj.isStackable = isStackableObj;

            return savedObj;
        }
    }

    [System.Serializable]
    public class SaveableLevelObject
    {
        public string obj_Id;
        public int posX;
        public int posZ;

        public float rotX;
        public float rotY;
        public float rotZ;

        public bool isWallObject = false;
        public bool isStackable = false;

    }
}