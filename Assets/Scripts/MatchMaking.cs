using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Mirror;
using Random = UnityEngine.Random;

namespace OilSpillVR
{
    [System.Serializable]
    public class Match
    {
        public string id;
        public SyncListGameObject players = new SyncListGameObject();

        public Match(string matchId, GameObject player)
        {
            id = matchId;
            players.Add(player);
        }

        public Match()
        {
        }
    }

    public class SyncListGameObject : SyncList<GameObject>
    {
    }

    public class SyncListMatch : SyncList<Match>
    {
    }

    public class MatchMaking : NetworkBehaviour
    {
        public static MatchMaking instance;
        public SyncListMatch matches = new SyncListMatch();
        public SyncListString matchIDs = new SyncListString();

        public void Start()
        {
            instance = this;
        }

        public bool HostGame(string matchId, GameObject player)
        {
            if (!matchIDs.Contains(matchId))
            {
                matchIDs.Add(matchId);
                matches.Add(new Match(matchId, player));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetRandomMatchID()
        {
            string id = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int random = UnityEngine.Random.Range(0, 36);
                if (random < 26)
                {
                    id += (char) (random + 65);
                }
                else
                {
                    id += (random - 26).ToString();
                }
            }

            Debug.Log($"Random Match ID: {id}");
            return id;
        }
    }

    public static class MatchExtensions
    {
        public static Guid ToGuid(this string id)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hasBytes = provider.ComputeHash(inputBytes);

            return new Guid(hasBytes);
        }
    }
}