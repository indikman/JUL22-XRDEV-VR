using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTip : MonoBehaviour
{
    private MeshRenderer rend;

    public Material currentMaterial;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("paintcolor"))
        {
            currentMaterial = other.GetComponent<Renderer>().material;
            rend.material = currentMaterial;
        }
    }
}
