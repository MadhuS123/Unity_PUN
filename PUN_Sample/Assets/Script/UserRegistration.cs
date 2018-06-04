using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserRegistration : MonoBehaviour {

    private bool alreadyRegisterd = false;

    public bool removePref = false;

    public InputField userNameInput;

    public GameObject objParent;

    public GameObject createButton;


    public void Awake()
    {
        //if (removePref)
        //    PlayerPrefs.SetInt("registerd", 0);

        //if (PlayerPrefs.GetInt("registerd") == 0)
        checkRegister();
    }

    void checkRegister()
    {
        if (!alreadyRegisterd)
            objParent.SetActive(true);
    }

    public void UserNameInputChange()
    {
        if (userNameInput.text.Length >= 2)
            createButton.SetActive(true);
        else
            createButton.SetActive(false);
    }

    public void createUserName()
    {
        PhotonNetwork.playerName = userNameInput.text;
        //PlayerPrefs.SetInt("registerd", 1);
        objParent.SetActive(false);
    }
}
