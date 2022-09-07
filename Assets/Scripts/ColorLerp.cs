using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    [SerializeField] private float interpolationAmount = 0; // The value that goes in between 0 and 1
    [SerializeField] private float speed;

    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;



    void Update()
    {
        if (interpolationAmount > 1)
        {
            interpolationAmount = 0;
        }
        interpolationAmount += speed;

        Color newColor = Color.Lerp(startColor, endColor, interpolationAmount);

        GetComponent<MeshRenderer>().material.color = newColor;
    }
}
