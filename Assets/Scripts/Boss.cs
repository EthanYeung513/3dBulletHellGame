using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{ 
    [SerializeField] int Health = 100;
    [SerializeField] float bossMoveSpeed;
    [SerializeField] bool reachedEndPoint;

    [SerializeField] private Transform cornerTransforms;
    // Start is called before the first frame update
    void Start()
    {

     bossMoveSpeed = 20;
        //Fill an array of all the different positions of the verticies
        Vector3[] Verticies = new Vector3[cornerTransforms.childCount];
        for (int i = 0; i < Verticies.Length; i++) // For loop because number of verticies can vary
        {
            Verticies[i] = cornerTransforms.GetChild(i).position;
            Verticies[i] = new Vector3(Verticies[i].x, transform.position.y, Verticies[i].z);


        }
        StartCoroutine(movePatrol(Verticies)); // Coroutine allows unity to wait for seconds
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 4, 0, 0));
    }

    public void damageTaken(int damage)
    {
        Health -= damage;
        Debug.Log("Boss Health is now: " + this.Health);

        if(Health <= 0)
        {
            Debug.Log("Boss killed ");
            Destroy(this.gameObject);
        }
    }

    IEnumerator movePatrol(Vector3[] verticies)
    {
        transform.position = verticies[0]; // Set patrol position as first corner
        int nextVertexIndex = 1; // Index of array patrol should move towards
        Vector3 nextVertex = verticies[nextVertexIndex]; // Transform patrol should move towards
        transform.LookAt(nextVertex);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextVertex, bossMoveSpeed * Time.deltaTime);
            if (transform.position == nextVertex) //If patrol reached vertex, change to different vertex
            {
                nextVertexIndex = (nextVertexIndex + 1) % verticies.Length; // Mod so index goes between 0 - length
                nextVertex = verticies[nextVertexIndex];
                yield return new WaitForSeconds(1); // Wait amount specified
                yield return StartCoroutine(turnPatrol(nextVertex));  // Star coroutin which changes patrol angle

            }
            yield return null;
        }

    }

    IEnumerator turnPatrol(Vector3 otherVertex)
    {
        Vector3 direction = (otherVertex - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {    //change angle
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, 180 * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }




}
