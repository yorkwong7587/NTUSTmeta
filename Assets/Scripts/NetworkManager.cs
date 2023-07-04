using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        //PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server.");
        //base.OnConnectedToMaster();
        //RoomOptions roomOptions = new RoomOptions();
        //roomOptions.MaxPlayers = 20;
        //roomOptions.IsVisible = true;
        //roomOptions.IsOpen = true;

        //PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Joined a Room.");
        //base.OnJoinedRoom();
    }

    public void OnPlayerEnteredRoom()
    {
        Debug.Log("New Player Joined the Room.");
        //base.OnPlayerEnteredRoom(newplayer);
    }
}
