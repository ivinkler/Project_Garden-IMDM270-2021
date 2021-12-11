using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField]float scrollX = 0.0f;
    [SerializeField]float scrollY = 0.5f;

    // Update is called once per frame
    void Update()
    {
        float xOffset = scrollX * Time.time;
        float yOffset = scrollY * Time.time;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(xOffset,yOffset);
    }
}
