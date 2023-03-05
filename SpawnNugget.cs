using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnNugget : MonoBehaviour
{
    private GameObject baseNug;
    private GameObject cloneNug;

    public bool canSpawn = true;
    public int nugID;

    public float spawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        baseNug = GameObject.Find("Ref Gold Nuggie");
        createNugget();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if (canSpawn == true && spawnTime > 20.0f)
        {
            createNugget();
            spawnTime = 0f;
        }

        else if (canSpawn == false && spawnTime > 20.0f)
        {
            spawnTime = 0f;
        }
    }

    void createNugget()
    {
        spawnTime = 0f;
        canSpawn = false;
        nugID++;
        cloneNug = (GameObject)Instantiate(baseNug, transform.position, Quaternion.identity);
        cloneNug.name = "Gold Nuggie " + nugID;
    }
}
