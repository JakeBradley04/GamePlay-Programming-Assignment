using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    public bool inRange;

    public float drillRange;
    public float torchRange;

    private GameObject target;
    public LayerMask gold;
    public LayerMask enemy;
    public Camera cam;

    UI userInterface;
    ItemController usingItem;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        userInterface = GameObject.Find("Crosshair").GetComponent<UI>();
        usingItem = GameObject.Find("ItemSelect").GetComponent<ItemController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool rangeCheckDrill(float distance)
    {
        inRange = Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, gold);

        if (inRange == true && usingItem.Switch == true)
        {
            userInterface.canMine();
            target = hit.collider.gameObject;
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
            return true;
        }

        else
        {
            userInterface.noMine();
            return false;
        }
    }

    public bool rangeCheckTorch(float distance)
    {
        inRange = Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, enemy);

        if (inRange == true && usingItem.Switch == false)
        {
            target = hit.collider.gameObject;
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
            return true;
        }

        else
        {
            return false;
        }
    }

    public GameObject returnTarget()
    {
        return target;
    }
}
