using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photonButtons : MonoBehaviour {

    public InputField createRoomInput, joinRoomInput;
    public photonHandler pHandler;

    public void OnClickCreateRoom()
    {
        if(createRoomInput.text.Length>0)
        pHandler.createNewRoom(createRoomInput.text);
    }

    public void OnClickJoinRoom()
    {
        pHandler.joinOrCreateRoom(joinRoomInput.text);
    }
  
}
