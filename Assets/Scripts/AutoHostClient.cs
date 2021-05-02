using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoHostClient : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;

    /// <summary>
    ///  Auto joins the server located in the url   
    /// </summary>
    /// <param name="url">IP</param>
    public void JoinLocal(string url)
    {
        networkManager.networkAddress = url;
        networkManager.StartClient();
    }
}