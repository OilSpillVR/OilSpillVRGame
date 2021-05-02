using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

namespace OilSpillVR
{
    public class Player : NetworkBehaviour
    {
        public static Player localPlayer;
        [SyncVar] public string matchID;
        [SyncVar] public int playerIndex;
        private NetworkMatchChecker _networkMatchChecker;

        // Start is called before the first frame update
        void Start()
        {
            _networkMatchChecker = GetComponent<NetworkMatchChecker>();

            if (isLocalPlayer)
                localPlayer = this;
            else
                UILobby.instance.SpawnPlayerPrefab(this);
        }

        /*
        * Host Match
        */
        public void HostGame()
        {
            string matchID = MatchMaking.GetRandomMatchID();
            CmdHostGame(matchID);
        }


        [Command]
        public void CmdHostGame(string _matchId)
        {
            matchID = _matchId;
            if (MatchMaking.instance.HostGame(_matchId, gameObject, out playerIndex))
            {
                _networkMatchChecker.matchId = _matchId.ToGuid();
                TargetHostGame(true, _matchId);
                Debug.Log("Success");
            }

            else
            {
                TargetHostGame(false, _matchId);
                Debug.Log("error");
            }
        }

        [TargetRpc]
        void TargetHostGame(bool success, string matchId)
        {
            UILobby.instance.HostSuccess(success, matchId);
        }


        /*
         * JOIN MATCH
         */

        public void JoinGame(string matchID)
        {
            CmdJoinGame(matchID);
        }


        [Command]
        public void CmdJoinGame(string _matchId)
        {
            matchID = _matchId;
            if (MatchMaking.instance.JoinGame(_matchId, gameObject, out playerIndex))
            {
                _networkMatchChecker.matchId = _matchId.ToGuid();
                TargetJoinGame(true, _matchId);
                Debug.Log("Success");
            }

            else
            {
                TargetJoinGame(false, _matchId);
                Debug.Log("error");
            }
        }

        [TargetRpc]
        void TargetJoinGame(bool success, string matchId)
        {
            UILobby.instance.JoinSuccess(success, matchId);
        }


        /*
         * BEGIN GAME
         */
       
        /// <summary>
        /// Send the begin game functionality to the server
        /// </summary>
        public void BeginGame()
        {
            CmdBeginGame();
        }

        /// <summary>
        /// calls the method from matchmaking class with the correct match id
        /// </summary>
        [Command]
        public void CmdBeginGame()
        {
            MatchMaking.instance.BeginGame(matchID);
        }
        /// <summary>
        /// calls the method from matchmaking class with the correct match id
        /// </summary>
        public void StartGame()
        {
            TargetBeginGame();
        }
        /// <summary>
        /// Start
        /// </summary>
        [TargetRpc]
        void TargetBeginGame()
        {
            Debug.Log(matchID + "Beginning");
            SceneManager.LoadScene("Scenes/Oil Spill VR Main", LoadSceneMode.Additive);
        }
    }
}