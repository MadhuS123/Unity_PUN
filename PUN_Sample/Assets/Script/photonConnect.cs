using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonConnect : MonoBehaviour {

    public string versionName = "0.1";
    [SerializeField]
    GameObject sectionView1, sectionView2, sectionView3;
    public void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);

        m("Connecting to Photon");
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        sectionView1.SetActive(false);
        sectionView2.SetActive(true);
        m("On Joined Lobby");
    }

    public void OnDisconnectedFromPhoton()
    {
        if (sectionView1.activeSelf)
            sectionView1.SetActive(false);

        if (sectionView2.activeSelf)
            sectionView2.SetActive(false);

        sectionView3.SetActive(true);
    }

    public void OnFailedToConnectPhoton()
    {

    }

    public static void m(string mess)
    {
        Debug.Log(mess);
    }
}
