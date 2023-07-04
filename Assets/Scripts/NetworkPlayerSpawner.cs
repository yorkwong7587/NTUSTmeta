using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviour
{
    private GameObject spawnedPlayerPrefab;

    public void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);

    }

    public void OnLeftRoom()
    {
        //base.OnLeftRoom();
        //PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
