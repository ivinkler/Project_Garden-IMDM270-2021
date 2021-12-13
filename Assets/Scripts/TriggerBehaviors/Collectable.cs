using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 home;
    [SerializeField] Vector3 goTo;
    [SerializeField] AudioSource fanfare;

    [SerializeField] GameObject collectable;
    private bool found;
    void Start()
    {
        collectable.GetComponent<Transform>().position = home;
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
            found = true;
            fanfare.Play();
       }
    }
}
