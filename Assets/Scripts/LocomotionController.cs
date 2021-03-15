using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRRayInteractor leftTeleportRay;
    public XRRayInteractor rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationTreshold = 0.1f;
      

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(CheckIfActivated(null));
        }
        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(null));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationTreshold);
        return isActivated;
    }
}
