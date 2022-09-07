using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightGrabInteractable : XRGrabInteractable
{
    [SerializeField] private GameObject spotLight;
    [SerializeField] private MeshRenderer flashLightGlass;

    [SerializeField] private Material glassMaterial, flashMaterial;

    private bool isOn = false;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        if(isOn)
        {
            // We need to turn off
            flashLightGlass.material = glassMaterial;
            spotLight.SetActive(false);
            isOn = false;
        }
        else
        {
            // We need to turn on
            flashLightGlass.material = flashMaterial;
            spotLight.SetActive(true);
            isOn = true;
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
