using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    //Object that will spin
    [SerializeField] GameObject spinner;

    //Rotation rates
    [SerializeField] float xSpin = 4f;
    [SerializeField] float ySpin = 4f;
    [SerializeField] float zSpin = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinner.GetComponent<Transform>().localEulerAngles += new Vector3(xSpin,ySpin,zSpin);
    }
}
