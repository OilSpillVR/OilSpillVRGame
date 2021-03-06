using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerOilSpill _networkManagerOilSpill = null;

    [Header("UI")] [SerializeField] private GameObject landingPagePanel = null;

    public void HostLobby()
    {
        _networkManagerOilSpill.StartHost();
        landingPagePanel.SetActive(true);
    }
}