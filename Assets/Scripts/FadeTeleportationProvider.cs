using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FadeTeleportationProvider : TeleportationProvider
{
    bool m_HasExclusiveLocomotion;
    float m_TimeStarted = -1f;

    [SerializeField] private RawImage fadeImage;
    [SerializeField] private float timerValue;

    private float timer = 0;


    void Start()
    {
        
    }

    public override bool QueueTeleportRequest(TeleportRequest teleportRequest)
    {
        // Fade in!
        StartCoroutine(FadeInAndStartRequest(teleportRequest));
        return true;
    }

    IEnumerator FadeInAndStartRequest(TeleportRequest teleportRequest)
    {
        timer = 0;

        while(timer <= 1)
        {
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer);
            timer += timerValue;
            yield return new WaitForEndOfFrame();
        }

        fadeImage.color = Color.black;

        currentRequest = teleportRequest;
        validRequest = true;
    }

    IEnumerator FadeOutAndEndLocomotion()
    {
        timer = 0;

        while (timer <= 1)
        {
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer);
            timer += timerValue;
            yield return new WaitForEndOfFrame();
        }

        fadeImage.color = Color.clear;

        EndLocomotion();
        m_HasExclusiveLocomotion = false;
        validRequest = false;
        locomotionPhase = LocomotionPhase.Done;
    }


    // Update is called once per frame
    protected override void Update()
    {
        if (!validRequest)
        {
            locomotionPhase = LocomotionPhase.Idle;
            return;
        }

        if (!m_HasExclusiveLocomotion)
        {
            if (!BeginLocomotion())
                return;

            m_HasExclusiveLocomotion = true;
            locomotionPhase = LocomotionPhase.Started;
            m_TimeStarted = Time.time;
        }


        locomotionPhase = LocomotionPhase.Moving;

        var xrOrigin = system.xrOrigin;
        if (xrOrigin != null)
        {
            switch (currentRequest.matchOrientation)
            {
                case MatchOrientation.WorldSpaceUp:
                    xrOrigin.MatchOriginUp(Vector3.up);
                    break;
                case MatchOrientation.TargetUp:
                    xrOrigin.MatchOriginUp(currentRequest.destinationRotation * Vector3.up);
                    break;
                case MatchOrientation.TargetUpAndForward:
                    xrOrigin.MatchOriginUpCameraForward(currentRequest.destinationRotation * Vector3.up, currentRequest.destinationRotation * Vector3.forward);
                    break;
                case MatchOrientation.None:
                    // Change nothing. Maintain current origin rotation.
                    break;
                default:
                    Assert.IsTrue(false, $"Unhandled {nameof(MatchOrientation)}={currentRequest.matchOrientation}.");
                    break;
            }

            var heightAdjustment = xrOrigin.Origin.transform.up * xrOrigin.CameraInOriginSpaceHeight;

            var cameraDestination = currentRequest.destinationPosition + heightAdjustment;

            xrOrigin.MoveCameraToWorldLocation(cameraDestination);
        }

        StartCoroutine(FadeOutAndEndLocomotion());
    }

    
}
