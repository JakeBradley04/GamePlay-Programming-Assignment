using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject drill;
    public GameObject torch;

    public bool Switch;
    public bool itemCond;

    UI userInterface;
    RayCastScript drillScript;
    RayCastScript torchScript;
    Player inventory;

    public class CurrentItem
    {
        public virtual void useItem(GameObject thisObject, bool state){}

        public bool activeItem(GameObject thisObject, bool state)
        {
            if (state == true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public void hideItem(GameObject thisObject)
        {
            thisObject.GetComponent<Renderer>().enabled = false;

        }
        public void showItem(GameObject thisObject)
        {
            thisObject.GetComponent<Renderer>().enabled = true;
        }
    }

    public class Torch : CurrentItem
    {
        public override void useItem(GameObject thisObject, bool state)
        {
            thisObject.GetComponent<Light>().enabled = state;
        }
    }

    public class Drill : CurrentItem
    {
        public override void useItem(GameObject thisObject, bool state)
        {
            float z = 0.0f;
            float rotationSpeed = 500.0f;

            if (state == true)
            {
                z += Time.deltaTime * rotationSpeed;

                thisObject.transform.Rotate(0, 0, z, Space.Self);
            }

            else
            {
                thisObject.transform.Rotate(0, 0, 0, Space.Self);
            }
        }
    }

    public CurrentItem drillItem;
    public CurrentItem torchItem;

    // Start is called before the first frame update
    void Start()
    {
        drillItem = new Drill();
        torchItem = new Torch();

        drill = GameObject.Find("Drill");
        torch = GameObject.Find("Torch");
        userInterface = GameObject.Find("Crosshair").GetComponent<UI>();
        inventory = GameObject.Find("PlayerObject").GetComponent<Player>();

        torchItem.useItem(torch, false);
        drillItem.hideItem(drill);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            itemCond = true;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            itemCond = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Switch = !Switch;
        }

        if (Switch == false)
            {
            userInterface.disableCrosshair();
            torchItem.showItem(torch);
            drillItem.hideItem(drill);

            if (inventory.itemPW > 0)
            {
                torchItem.useItem(torch, itemCond);
            }

            else
            {
                Debug.Log("Out of torch power");
                torchItem.useItem(torch, false);
            }

        }
        else
        {
            userInterface.enableCrosshair();
            torchItem.hideItem(torch);
            torch.GetComponent<Light>().enabled = false;
            drillItem.showItem(drill);

            if (inventory.itemPW > 0)
            {
                drillItem.useItem(drill, itemCond);
            }

            else
            {
                Debug.Log("Out of drill power");
                drillItem.useItem(drill, false);
            }
        }
    }
}