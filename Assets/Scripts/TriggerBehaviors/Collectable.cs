using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Spot where the item should be before its found
    [SerializeField] Vector3 home;

    //The collection tracker
    [SerializeField] CollectionTracker tracker;

    //Spot where the item goes once its found
    [SerializeField] Vector3 goTo;
    [SerializeField] AudioSource fanfare;

    //the item to be picked up
    [SerializeField] GameObject collectable;
    private bool found;
    void Start()
    {
        collectable.GetComponent<Transform>().position = home;
        tracker = GameObject.Find("hub").GetComponent<CollectionTracker>();
        fanfare = collectable.GetComponent<AudioSource>();
        found = false;
    }

    void goHome()
    {
        collectable.GetComponent<Transform>().position = home;
        found = false;
    }

    public void collect()
    {
       if(!found)
       {
            collectable.GetComponent<Transform>().position = goTo;
            tracker.addItem();
            found = true;
            fanfare.Play();
       }
    }
}
