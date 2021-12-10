using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] float sensitivity = 200.0f;
    //[SerializeField] bool lockHoriz = false;
    //[SerializeField] bool lockVert = false;
    [SerializeField] float lookDownLimit = -85.0f;
    [SerializeField] float lookUpLimit = 85.0f;
    [SerializeField] float vRotation = 0.0f;
    [SerializeField] GameObject player;

    [SerializeField] Transform playerBody;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player");
        playerBody = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float turnX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float turnY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        vRotation -= turnY;
        vRotation = Mathf.Clamp(vRotation, lookDownLimit, lookUpLimit);

        transform.localRotation = Quaternion.Euler(vRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * turnX);
    }
}
