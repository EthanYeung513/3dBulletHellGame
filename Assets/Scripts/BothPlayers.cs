using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothPlayers : MonoBehaviour
{

    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;

    [SerializeField] public bool activated = true; //True = player1, false = player2
    [SerializeField] public bool switchCooldown = false; //If true, then dont allow to switch


    [SerializeField] Camera cam1;
    [SerializeField] Camera cam2;


    [SerializeField] PlayerLook Playerlook1;
    [SerializeField] PlayerMovement PlayerMovement1;
    [SerializeField] PlayerManager playerManager1;

    [SerializeField] PlayerLook PlayerLook2;
    [SerializeField] PlayerMovement PlayerMovement2;
    [SerializeField] PlayerManager PlayerManager2;

    [SerializeField] string activeNameObj;
    // Start is called before the first frame update
    void Start()
    {
        activeNameObj = player1.gameObject.name; //Set this string to player1 cuz its active first
        getRelevantComponents(); //Get components of both playerrs
        disablePlayer2Components(); //Disable player 2 cuz player 1 is active
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && switchCooldown == false) //If active and they press k to switch
        {
            switchPlayers();
            switchCooldown = true; //Start cooldown
            StartCoroutine(startCoolDown());
        }
    }

    public IEnumerator startCoolDown()
    {
        yield return new WaitForSeconds(3);
        switchCooldown = false;
    }

    void getRelevantComponents()
    {
        cam1 = GetComponentInChildren<UnityEngine.Camera>(); //Get this camera     
        Playerlook1 = cam1.GetComponent<PlayerLook>();
        PlayerMovement1 = player1.gameObject.GetComponent<PlayerMovement>();
        playerManager1 = player1.gameObject.GetComponent<PlayerManager>();

        //
        cam2 = player2.GetComponentInChildren<UnityEngine.Camera>();
        PlayerLook2 = cam2.GetComponent<PlayerLook>();
        PlayerMovement2 = player2.GetComponent<PlayerMovement>();
        PlayerManager2 = player2.GetComponent<PlayerManager>();
    }

    void disablePlayer2Components()
    {
        cam2.enabled = false;
        PlayerLook2.enabled = false;
        PlayerMovement2.enabled = false;
    }



    void switchPlayers()
    {
        if (activeNameObj == player1.gameObject.name)
        {
            cam1.enabled = false;
            Playerlook1.enabled = false;
            PlayerMovement1.enabled = false;

            cam2.enabled = true;
            PlayerLook2.enabled = true;
            PlayerMovement2.enabled = true;

            activeNameObj = player2.gameObject.name;
        }
        else if (activeNameObj == player2.gameObject.name)
        {
            disablePlayer2Components();

            cam1.enabled = true;
            Playerlook1.enabled = true;
            PlayerMovement1.enabled = true;

            activeNameObj = player1.gameObject.name;
        }
    }
}
