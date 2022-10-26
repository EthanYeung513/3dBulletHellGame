using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{

    public static BossBulletManager bulletMangerInstance;


    [SerializeField] private GameObject BossBullet; //Holds all bullets in empty game obj
    [SerializeField] bool notFull = true; //BossBullets obj is not full


    private List<GameObject> bullets; //Hold bullets

    void Awake()
    {
        bulletMangerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();  //Instantiate the list
        notFull = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject supplyBullet() //Returns one active bullet
    {
        if (bullets.Count > 0) //If bullet list not empty
        {
            for (int i = 0; i < bullets.Count; i++) //Loop through list
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }


        }

        if (notFull == true)

        {

            GameObject bul = Instantiate(BossBullet);
            bul.SetActive(false);
            bullets.Add(bul); //Add to list
            return bul; //Return inactive bullet
        }

        return null;
    }
}
