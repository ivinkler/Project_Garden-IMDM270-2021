using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTracker : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int total; //total number of objects collected
    [SerializeField] bool complete; //is the collection finished? have you collected all of the items in this set?

    [SerializeField] int maxTotal = 4; //the total number of items in this set

    [SerializeField] GameObject[] collectedItems; //the items that have been collected

    void Start()
    {
        total = 0;
        collectedItems = new GameObject[maxTotal];
    }


    void addItem(GameObject item)
    {
        //do not add item if the collection is complete
        if(complete)
        {
            return;
        }
        for(int i = 0; i < maxTotal; i++)
        {
            if(collectedItems[i] == null)
            {
                collectedItems[i] = item;
                break;
            }
        }
    }
    void completeCheck()
    {
        
    }
}
