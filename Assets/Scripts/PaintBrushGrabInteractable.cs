using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaintBrushGrabInteractable : XRGrabInteractable
{
    [SerializeField] private GameObject paintPrefab; // We instantiate a paint prefab with trigger
    [SerializeField] private Transform paintTip; // Reference to the paint tip for the paint prefab to follow
    [SerializeField] private PaintTip tip;

    private GameObject tempPaint;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        tempPaint = Instantiate(paintPrefab, paintTip.position, paintTip.rotation);
        tempPaint.transform.SetParent(paintTip);

        if(tip.currentMaterial != null)
        {
            tempPaint.GetComponent<TrailRenderer>().material = tip.currentMaterial;
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);

        if(tempPaint != null)
        {
            // Release the paint from the paint tip
            tempPaint.transform.SetParent(null);
            tempPaint = null;
        }
    }
}
