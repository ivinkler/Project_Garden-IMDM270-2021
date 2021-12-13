using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] GameObject playerCam;
    [SerializeField] float playerReach = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCam.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
            
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, playerReach))
            {
                Interactable interaction = hit.collider.GetComponent<Interactable>();
                Collectable collectable = hit.collider.GetComponent<Collectable>();
                if(interaction != null)
                {
                    interaction.toggle();
                }

                if(collectable != null)
                {
                    collectable.collect();
                }
            }
        }
    }
}
