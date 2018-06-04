using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photonHandler : MonoBehaviour {
    public GameObject mainplayer;
    private void Awake()
    {
        DontDestroyOnLoad(this.transform);

        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 20;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public void createNewRoom(string name)
    {
        PhotonNetwork.CreateRoom(name, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public void joinOrCreateRoom(string name)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(name, roomOptions, TypedLobby.Default);
    }

    public void moveScene()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    private void OnJoinedRoom()
    {
        moveScene();
        Debug.Log("Now we are joined Let Rockzzzz");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Am New");
        if (scene.name == "GameScene")
        {
            spawnPlayer();
        }
    }

    private void spawnPlayer()
    {
        Debug.Log("Am New");
        PhotonNetwork.Instantiate(mainplayer.name, mainplayer.transform.position, mainplayer.transform.rotation, 0);
    }
}
