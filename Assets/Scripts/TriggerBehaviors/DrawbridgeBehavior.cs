using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawbridgeBehavior : MonoBehaviour, TriggerableObject  
{
    [SerializeField] GameObject bridge;
    [SerializeField] GameObject[] colliders;

    // Start is called before the first frame update
    void Start()
    {
      deactivateSequence();  
    }

    public void activateSequence()
    {
        bridge.GetComponent<Transform>().localEulerAngles = new Vector3(0,0,0);
    }

    public void deactivateSequence()
    {
        bridge.GetComponent<Transform>().localEulerAngles = new Vector3(-85,0,0);
    }
}
