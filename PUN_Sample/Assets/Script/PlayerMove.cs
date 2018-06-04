using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : Photon.MonoBehaviour {

    public Boolean devTesting = false;

    public PhotonView photonView;

    public float moveSpeed = 3f;

    public float jumpForce = 800;

    private Vector3 selfPos;
    private GameObject sceneCam;
    public GameObject plCam;
    public Rigidbody2D rigidbody2D;
    public bool isGrounded = false;
    public SpriteRenderer spriteRenderer;

    public Text playerName;

    private void Awake()
    {
        if (!devTesting && photonView.isMine)
        {
            sceneCam = GameObject.Find("Main Camera");
            sceneCam.SetActive(false);
            plCam.SetActive(true);

            playerName.text = PhotonNetwork.playerName;
        }

        if(!photonView.isMine)
        {
            playerName.text = photonView.owner.name;
        }
    }

    private void Update()
    {
        if (!devTesting)
        {
            if (photonView.isMine)
                checkInput();
            else
                smoothNetMovement();
        }
        else { checkInput(); }
    }


    private void checkInput()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            performJump();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            photonView.RPC("onSpriteFlipFalse", PhotonTargets.Others);
        }

        else if(Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            photonView.RPC("onSpriteFlipTrue", PhotonTargets.Others);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!devTesting)
        {
            if (photonView.isMine)
                if (collision.gameObject.tag == "Ground")
                    isGrounded = true;
        }
        else
        {
            if (collision.gameObject.tag == "Ground")
                isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!devTesting)
        {
            if (photonView.isMine)
                if (collision.gameObject.tag == "Ground")
                    isGrounded = false;
        }
        else
        {
            if (collision.gameObject.tag == "Ground")
                isGrounded = false;
        }
    }

    void performJump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    [PunRPC]
    private void onSpriteFlipTrue()
    {
        spriteRenderer.flipX = true;
    }

    [PunRPC]
    private void onSpriteFlipFalse()
    {
        spriteRenderer.flipX = false;
    }

    private void smoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
    }


    private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            selfPos = (Vector3)stream.ReceiveNext();
        }
    }

   
}
