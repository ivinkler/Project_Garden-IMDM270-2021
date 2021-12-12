using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] bool toggleState;
    /**
    Array of all objects to be "triggered" when the interactable is toggled
    */
    [SerializeField] GameObject[] actives; 
    void Start()
    {
        toggleState = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle()
    {
        if(!toggleState) //object is NOT activated
        {
            foreach(GameObject active in actives)
            {
                active.GetComponent<TriggerableObject>().activateSequence();
            }
            toggleState = true;
        }
        else
        {
            foreach(GameObject active in actives) //object IS activated
            {
                active.GetComponent<TriggerableObject>().deactivateSequence();
            }
            toggleState = false;
        }
    }
}
