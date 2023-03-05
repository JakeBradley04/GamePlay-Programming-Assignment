using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    RayCastScript torchScript;
    ItemController playerInput;
    GameObject enemyObject;
    Player playerPW;

    public float timeUntilAction = 0f;
    public float timeUntilDeath = 0f;
    public float timeUntilAppear;

    public bool hidden;


    // Start is called before the first frame update
    void Start()
    {
        torchScript = GameObject.Find("Torch").GetComponent<RayCastScript>();
        playerInput = GameObject.Find("Drill").GetComponent<ItemController>();
        enemyObject = GameObject.Find("Enemy");
        playerPW = GameObject.Find("PlayerObject").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hidden == false)
        {
            timeUntilDeath += Time.deltaTime;
        }

        if (torchScript.rangeCheckTorch(torchScript.torchRange) && playerInput.torchItem.activeItem(playerInput.torch, playerInput.itemCond) && playerPW.itemPW > 0)
        {
            timeUntilAction += Time.deltaTime;

            if (timeUntilAction > 15)
            {
                timeUntilAction = 0f;
                timeUntilAppear = Random.Range(20.0f, 60.0f);
                enemyObject.GetComponent<Renderer>().enabled = false;
                hidden = true;
                timeUntilDeath = 0f;
            }
        }

        else {}

        Appear();
        kill();
    }

    void Appear()
    {
        if (hidden == true)
        {
            timeUntilAction += Time.deltaTime;

            if (timeUntilAction > timeUntilAppear)
            {
                timeUntilAction = 0f;
                enemyObject.GetComponent<Renderer>().enabled = true;
                hidden = false;
            }
        }

        else {}
    }

    void kill()
    {
        if (timeUntilDeath >= 120.0f)
        {
            Debug.Log("You died!");
            Application.Quit();
        }
    }
}
