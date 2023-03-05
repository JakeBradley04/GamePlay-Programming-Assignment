using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    RefineryScript powerGain;
    ItemController itemSelect;
    CameraMovement viewLock;
    PlayerMovement moveLock;

    public int goldNum = 0;
    public int itemPW = 100;

    public float powerDrain = 0.0f;

    public bool canMove;
    public bool canLook;

    // Start is called before the first frame update
    void Start()
    {
        powerGain = GameObject.Find("Refinery").GetComponent<RefineryScript>();
        viewLock = GameObject.Find("Camera").GetComponent<CameraMovement>();
        moveLock = GameObject.Find("Player Container").GetComponent<PlayerMovement>();
        itemSelect = GameObject.Find("ItemSelect").GetComponent<ItemController>();
    }

    // Update is called once per frame
    void Update()
    {
        viewLock.CameraLock = canLook;
        moveLock.MoveLock = canMove;

        if (itemPW < 0)
        {
            itemPW = 0;
        }

        if (goldNum > 25)
        {
            goldNum = 25;
        }

        if (itemPW > 100)
        {
            itemPW = 100;
        }

        if (Input.GetMouseButton(0))
        {
            powerDrain += Time.deltaTime;

            itemTime();
        }

        else
        {
            powerDrain = 0f;
        }
    }

    void itemTime()
    {
        if (powerDrain > 10)
        {
            powerDrain = 0f;
            Debug.Log("Lost 10% power");
            itemPW -= 10;
        }
    }
}
