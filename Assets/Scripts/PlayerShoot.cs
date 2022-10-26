using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform bulletSpawn;
    public GameObject bullet;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            bulletShoot();
        }
    }

    void bulletShoot()
    {
        GameObject bul = Instantiate(bullet, bulletSpawn.position, bullet.transform.rotation);
        Rigidbody rb = bul.GetComponent<Rigidbody>();

        rb.AddForce(bulletSpawn.forward * 40, ForceMode.Impulse);
    }
}
