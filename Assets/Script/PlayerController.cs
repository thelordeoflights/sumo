using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRb;
    public PowerUpType currentPowerUp = PowerUpType.None;
   
    public GameObject powerupindicator;
    private GameObject focalpoint;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
   
    private Coroutine powerupCountdown; 

    public bool haspowerup = false;
    
    public float speed = 5.0f;
    public float hangTime = 1;
    public float smashSpeed = 75;
    public float explosionForce = 50;
    public float explosionRadius = 50;
    private float powerupstrength = 15f;
    public float uplimit = 10f;
    
    bool smashing = false;
    float floorY;
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        focalpoint = GameObject.Find("Focal Point");
    }
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        PlayerRb.AddForce(focalpoint.transform.forward * speed * forwardInput);
        powerupindicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if(currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
        if(currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space))
        {
            smashing = true;
            StartCoroutine(Smash());
            if (transform.position.y>uplimit)
            {transform.position = new Vector3(transform.position.x, uplimit ,transform.position.z );}
        }
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        floorY = transform.position.y; //Store the y position before taking off
        float jumpTime = Time.time + hangTime;
        
        while(Time.time < jumpTime) //move the player up while still keeping their x velocity.
        {
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, smashSpeed);
            yield return null;
        }
        while(transform.position.y > floorY)
        {
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        for(int i = 0; i< enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
        smashing = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUP"))
        {
            haspowerup = true;
            currentPowerUp = other.gameObject.GetComponent<Powerup>().powerUpType;
            powerupindicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(Powerupcountdowntimer());
        }
    }
    IEnumerator Powerupcountdowntimer()
    {
        yield return new WaitForSeconds(7);
        haspowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupindicator.gameObject.SetActive(false);
    }
    void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayfromPlayer * powerupstrength, ForceMode.Impulse);
            Debug.Log("Player Collided with:" + collision.gameObject.name + "with power setup to" + currentPowerUp.ToString());
        }
    }
    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
}

