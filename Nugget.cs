using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour
{
    //public int goldAmount;
    public int goldGen;

    SpawnNugget spawner;
    RayCastScript drillScript;
    ItemController playerInput;
    Player giveGold;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("Gold Spawner").GetComponent<SpawnNugget>();
        drillScript = GameObject.Find("Drill").GetComponent<RayCastScript>();
        playerInput = GameObject.Find("Drill").GetComponent<ItemController>();
        giveGold = GameObject.Find("PlayerObject").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drillScript.rangeCheckDrill(drillScript.drillRange) && playerInput.drillItem.activeItem(playerInput.drill, playerInput.itemCond))
        {
            destroySelf();
        }
    }

    public int generateGold()
    {
        goldGen = Random.Range(10, 25);
        return goldGen;
    }

    void destroySelf()
    {
        spawner.canSpawn = true;
        giveGold.goldNum += generateGold();
        Destroy(drillScript.returnTarget());
    }
}
