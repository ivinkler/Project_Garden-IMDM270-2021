using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavior : MonoBehaviour, TriggerableObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject ob;

    public void activateSequence()
    {
        ob.GetComponent<Transform>().position += new Vector3(0,5,0);
    }

    public void deactivateSequence()
    {
        ob.GetComponent<Transform>().position += new Vector3(0,-5,0);
    }
}
