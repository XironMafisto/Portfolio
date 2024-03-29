﻿using UnityEngine;
using System.Collections;
// using EpPathFinding.cs;
using System.Collections.Generic;

public class GridBase : MonoBehaviour
{
    public GameObject nodePrefab;

    public int sizeX;
    public int sizeZ;
    public int offset = 1;

    public Node[,] grid;

    private static GridBase instance = null;
    public static GridBase GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        CreateGrid();
        CreateMouseCollision();
    }

    void CreateGrid()
    {
        grid = new Node[sizeX, sizeZ];

        for(int x = 0; x < sizeX; x++)
        {
            for (int z = 0;  z < sizeZ; z++)
            {
                float posX = x * offset;
                float posZ = z * offset;

                GameObject go = Instantiate(nodePrefab, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;
                go.transform.parent = transform.GetChild(1).transform;

                NodeObject nodeObj = go.GetComponent<NodeObject>();
                nodeObj.posX = x;
                nodeObj.posZ = z;

                Node node = new Node();
                node.vis = go;
                node.tileRenderer = node.vis.GetComponentInChildren<MeshRenderer>();
                node.isWalkable = true;
                node.nodePosX = x;
                node.nodePosZ = z;
                grid[x, z] = node;
            }

        }
    }

    void CreateMouseCollision()
    {
        GameObject go = new GameObject();
        go.AddComponent<BoxCollider>();
        go.GetComponent<BoxCollider>().size = new Vector3(sizeX * offset, 0.1f, sizeZ * offset);
        go.transform.position = new Vector3((sizeX * offset) / 2 - 1, 0, (sizeZ * offset) / 2 - 1);

    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float worldX = worldPosition.x;
        float worldZ = worldPosition.y;

        worldX /= offset;
        worldZ /= offset;

        int x = Mathf.RoundToInt(worldX);
        int z = Mathf.RoundToInt(worldZ);

        if (x > sizeX)
            x = sizeX;
        if (z > sizeZ)
            z = sizeZ;
        if (x < 0)
            x = 0;
        if (z < 0)
            z = 0;

        return grid[x, z];
    }
}
