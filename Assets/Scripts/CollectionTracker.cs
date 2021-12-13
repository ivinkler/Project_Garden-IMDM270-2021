using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTracker : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int total; //total number of objects collected
    [SerializeField] bool complete; //is the collection finished? have you collected all of the items in this set?

    [SerializeField] int maxTotal = 4; //the total number of items in this set

    [SerializeField] GameObject finalDoor; //reference to the exit door

    bool exitOpen; //checks if the exit has already been toggled open

    void Start()
    {
        exitOpen = false;
        complete = false;
        total = 0;
    }


    public void addItem()
    {
        //do not add item if the collection is complete
        if(complete)
        {
            if(!exitOpen)
            {
                exitOpen = true;
                finalDoor.GetComponent<DespawnToggle>().activateSequence();
                finalDoor.GetComponent<Transform>().position = Vector3.zero;
            }
            return;
        }
        else
        {
            total++;
        }
        complete = completeCheck();
    }
    bool completeCheck()
    {
        return total >= maxTotal;
    }
}
