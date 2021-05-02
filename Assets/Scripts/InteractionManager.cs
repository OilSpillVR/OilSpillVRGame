using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mirror;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionManager : MonoBehaviour
{
    private static GameObject[] playersInScene;

    public void Awake()
    {
        playersInScene = GameObject.FindGameObjectsWithTag("Player");
    }


    public void CanBeActivated()
    {
        foreach (GameObject player in playersInScene)
        {
            NetworkIdentity networkIdentity = player.GetComponent<NetworkIdentity>();
            if (Vector3.Distance(player.transform.position, transform.position) < 0.3f && networkIdentity.isLocalPlayer)
            {
                ActivateAndDisableComponentsFromPlayer(player, true);
            }

            else if (Vector3.Distance(player.transform.position, transform.position) < 0.3f &&
                     !networkIdentity.isLocalPlayer)
            {
                ActivateAndDisableComponentsFromPlayer(player, false);
            }
        }
    }

    public void ActivateAndDisableComponentsFromPlayer(GameObject player, bool activate)
    {
        //Activate components from Left Hand
        player.transform.GetChild(0).transform.GetChild(1).GetComponent<XRRayInteractor>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(1).GetComponent<LineRenderer>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(1).GetComponent<XRInteractorLineVisual>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(1).GetComponent<XRController>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<HandController>()
            .enabled = activate;

        //Activate components from Right Hand
        player.transform.GetChild(0).transform.GetChild(2).GetComponent<XRRayInteractor>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(2).GetComponent<LineRenderer>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(2).GetComponent<XRInteractorLineVisual>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(2).GetComponent<XRController>().enabled = activate;
        player.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<HandController>()
            .enabled = activate;
    }
}