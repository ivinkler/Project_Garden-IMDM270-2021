using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour, TriggerableObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject lever;
    [SerializeField] AudioSource leverSound;

    public void activateSequence()
    {
        lever.GetComponent<Transform>().localEulerAngles *= -1;
        leverSound.Play();
    }
    
    public void deactivateSequence()
    {
        activateSequence();
    }
}
