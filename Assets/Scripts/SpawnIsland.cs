using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIsland : MonoBehaviour {

    [SerializeField]
    private GameObject GameTilePrefab;
    GameObject GameTile;
    [SerializeField]
    public enum mapSize
    {
        tiny, //16
        small, //32
        medium, //64
        large, //128
        huge //256
    }
    [SerializeField]
    private GameObject[] allTiles; //
    private GameObject focusedTile;
    [SerializeField]
    private int islandsize;
    [SerializeField]
    private GameObject targetTile;

    // Use this for initialization
    void Start () {
        islandsize = 16; //temporary manual map sizing
        CreateIsland();
    }

    public void CreateIsland()
    {
        //creates first tile
        if (allTiles.Length < 1)
        {
            SpawnTile(0, 0, 0);
        }
        while (allTiles.Length < islandsize) 
        {
            targetTile = GetRandomTile();  //get random tile from array.
            int newSide = Random.Range(0, 5); //get random side on random tile.
            decimal newX = GetLocation(targetTile, "x", newSide); //find the x axis to place new tile.
            int newY = (int)targetTile.GetComponent<Transform>().position.y + Random.Range(-2, 2); // randomize height.
                if (newY > 16 || newY < -16) //but not too random.
                {
                    newY = (int)targetTile.GetComponent<Transform>().position.y;
                }
            decimal newZ = GetLocation(targetTile, "z", newSide);
            SpawnTile(newX, newY, newZ); //find the z axis to place new tile
        }
    }

    private void SpawnTile(decimal locX, int locY, decimal locZ)
    {
        bool spawnTile = true;
        // go through every current tile and make sure one is not already located where new tile is being placed
        foreach (GameObject obj in allTiles)
        {
            if (decimal.Round((decimal)obj.GetComponent<Transform>().position.x, 2) == decimal.Round(locX, 2) 
                && decimal.Round((decimal)obj.GetComponent<Transform>().position.z, 2) == decimal.Round(locZ, 2))
            {
                spawnTile = false;
                break;
            }
        }
        //if no tile is already placed here, go ahead and place new tile.
        if (spawnTile == true)
        {
            GameTile = Instantiate(GameTilePrefab) as GameObject;
            GameTile.GetComponent<Transform>().position = new Vector3((float)locX, locY, (float)locZ);
            GameTile.tag = "GameTile";

            allTiles = GameObject.FindGameObjectsWithTag("GameTile");
        }
    }

    private GameObject GetRandomTile()
    {
        int i = Random.Range(0, allTiles.Length - 1);
        return allTiles[i];
    }

    //crazy and ineficiant. Returns x and z coordinates for placing new tiles.
    private decimal GetLocation(GameObject targetTile, string XZ, int newSide)
    {
        decimal side1 = 0;
        decimal side2 = 0;
        decimal side3 = 0;
        decimal side4 = 0;
        decimal side5 = 0;
        decimal side6 = 0;

        decimal newXYZ = 0;
        decimal a = 0;
        Vector3 b  = targetTile.GetComponent<Transform>().position;

        if (XZ == "x")
        {
            side1 = 0;
            side2 = 6;
            side3 = 6;
            side4 = 0;
            side5 = -6;
            side6 = -6;
        }
        else if (XZ == "z")
        {
            side1 = 6.9282m;
            side2 = 3.4642m;
            side3 = -3.4642m;
            side4 = -6.9282m;
            side5 = -3.4642m;
            side6 = 3.4642m;
        }
        switch (newSide)
        {
            case 0:
                a = side1;
                break;
            case 1:
                a = side2;
                break;
            case 2:
                a = side3;
                break;
            case 3:
                a = side4;
                break;
            case 4:
                a = side5;
                break;
            case 5:
                a = side6;
                break;
        }
        if (XZ == "x") { newXYZ = (decimal)b.x + a; }
        if (XZ == "z") { newXYZ = (decimal)b.z + a; }

        return newXYZ;
    }
}
