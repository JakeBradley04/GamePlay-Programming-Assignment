using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject minePrompt;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        minePrompt = GameObject.Find("Text Bar");
        minePrompt.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableCrosshair()
    {
        crosshair.GetComponent<Canvas>().enabled = true;
    }

    public void disableCrosshair()
    {
        crosshair.GetComponent<Canvas>().enabled = false;
    }

    public void canMine()
    {
        minePrompt.GetComponent<Canvas>().enabled = true;
    }

    public void noMine()
    {
        minePrompt.GetComponent<Canvas>().enabled = false;
    }
}
