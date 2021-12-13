using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnToggle : MonoBehaviour, TriggerableObject
{
    [SerializeField] GameObject thingy;
    [SerializeField] bool startTangible = true;
    [SerializeField] bool startVisible = false;
    [SerializeField] bool allowToggleIntangible = true;
    [SerializeField] bool allowToggleInvisible = true;

    // Start is called before the first frame update
    void Start()
    {
        thingy.GetComponent<MeshRenderer>().enabled = startVisible;
        thingy.GetComponent<Collider>().enabled = startTangible;
    }

    public void activateSequence()
    {
        if(allowToggleIntangible)
        {
            thingy.GetComponent<Collider>().enabled = !thingy.GetComponent<Collider>().enabled;
        }
        if(allowToggleInvisible)
        {
            thingy.GetComponent<MeshRenderer>().enabled = !thingy.GetComponent<MeshRenderer>().enabled;
        }
        if(thingy.GetComponent<Light>() != null)
        {
            thingy.GetComponent<Light>().enabled = !thingy.GetComponent<Light>().enabled;
        }
    }

    public void deactivateSequence()
    {
        activateSequence();
    }
}
