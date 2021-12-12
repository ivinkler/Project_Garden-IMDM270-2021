using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 home;
    [SerializeField] Vector3 goTo;

    [SerializeField] GameObject collectable;
    private bool found;
    void Start()
    {
        collectable.GetComponent<Transform>().position = home;
        found = false;
    }

    void goHome()
    {
        collectable.GetComponent<Transform>().position = home;
        found = false;
    }

    void getFound()
    {
        collectable.GetComponent<Transform>().position = home;
        found = true;
        //play sound?
    }
}
