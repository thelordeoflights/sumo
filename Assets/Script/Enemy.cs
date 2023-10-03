using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody enemyRb;
    GameObject player;
    public float speed;
    public float uplim = 1.0f;
    public GameProperties gp;

    //PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //pc = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gp.isAlive == true && player)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;

            enemyRb.AddForce(lookDirection * speed);
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
            if (transform.position.y > uplim)
            {
                transform.position = new Vector3(transform.position.x, uplim, transform.position.z);
            }
        }
    }
}
