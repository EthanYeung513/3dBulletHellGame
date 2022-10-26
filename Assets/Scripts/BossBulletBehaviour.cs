using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private float bulletSpeed = 30f;

    private void OnEnable()
    {
         Invoke("remove", 3f); //In 3 seconds after enabled, destroy bullet
    }


    void Start()
    {
        bulletSpeed = 30f;
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(moveDir * bulletSpeed * Time.deltaTime);
         transform.position =  new Vector3(transform.position.x, 4f, transform.position.z); //Keep y  axis constant
    }

    public void setMoveDir(Vector3 direction)
    {
        moveDir = direction; //Set direction it moves in
    }

    public void remove()
    {
        gameObject.SetActive(false); //Set inactivee
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}

