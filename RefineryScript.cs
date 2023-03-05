using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefineryScript : MonoBehaviour
{
    Player inventory;
    RaycastHit hit;
    public Camera cam;
    public LayerMask refinery;

    public bool canRefine;
    public bool canInteract;

    public float interactionTime = 0f;

    public int amount = 0;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("PlayerObject").GetComponent<Player>();
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        canRefine = Physics.Raycast(cam.transform.position, cam.transform.forward, 2, refinery);

        interacting(canRefine, canInteract);
    }

    void interacting(bool inRange, bool isActive)
    {
        if (inRange == true && isActive == true)
        {
            Debug.Log("This is working");

            if (Input.GetKey("e"))
            {
                inventory.canMove = true;
                inventory.canLook = true;
                interactionTime += Time.deltaTime;

                if (interactionTime >= 10.0f)
                {
                    interactionTime = 0f;
                    canInteract = false;
                    Debug.Log("This thing will happen once hopefully");
                    takeGold();
                    inventory.canMove = false;
                    inventory.canLook = false;
                }

                else
                {
                    Debug.Log("Waiting");
                }
            }

            else
            {
                interactionTime = 0f;
                inventory.canMove = false;
                inventory.canLook = false;
            }
        }

        else if (inRange == false && isActive == false)
        {
            canInteract = true;
        }
    }

    void takeGold()
    {
        if (amount >= 25)
        {
            amount = 0;
            rechargeItems();
        }

        else if (amount < 25)
        {
            if (inventory.goldNum < 25)
            {
                Debug.Log("Not Enough Gold");
            }

            else
            {
                inventory.goldNum -= 25;
                amount += 25;
                inventory.itemPW += amount;
            }
        }
    }

    void rechargeItems()
    {

    }
}
