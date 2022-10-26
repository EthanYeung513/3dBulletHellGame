using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBullets : MonoBehaviour
{
    [SerializeField] int bulletCount = 15;

    [SerializeField] float initialAngle = 0f;
    [SerializeField] float finalAngle = 360f;

    private Vector3 bulletDir;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot", 0f, 2f); //Call shoot every few secs
    }

    // Update is called once per frame
    void Update()
    {

    }

    void shoot()
    {
        float incrementAmount = (finalAngle - initialAngle) / bulletCount; //Calculate the amount angle gets incremented by


        float currentAngle = initialAngle; //Gets incremented. Starts as initial

        for (int i = 0; i < bulletCount; i++) //Should a bullet (bulletCount) times
        {


            float bulletDirX = transform.position.x + Mathf.Sin((currentAngle * Mathf.PI) / 180f); //Using trig to calc direction
            float bulletDirY = transform.position.y + Mathf.Cos((currentAngle * Mathf.PI) / 180f);
            float bulletDirZ = transform.position.z + Mathf.Tan((currentAngle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulletDirX, bulletDirY, bulletDirZ);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject currentBullet = BossBulletManager.bulletMangerInstance.supplyBullet(); //Get a bullet
            currentBullet.transform.position = transform.position; //Set position and rotation
            currentBullet.transform.rotation = Quaternion.Euler(transform.position);
            currentBullet.SetActive(true);
            currentBullet.GetComponent<BossBulletBehaviour>().setMoveDir(bulDir);


            currentAngle = incrementAmount + currentAngle; //Increment current angle    



        }
    }
}
