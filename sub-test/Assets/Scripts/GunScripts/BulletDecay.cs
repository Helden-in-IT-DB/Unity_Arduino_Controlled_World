using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public bool StickOnImpact;
    public float lifespan; // Time before the bullet disappears if not collided with anything
    private float timer;

    void Update()
    {
        // Increase the timer
        timer += Time.deltaTime;

        // Check if the bullet has been alive for longer than its lifespan
        if (timer >= lifespan)
        {
            DestroyBullet();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "bullet")
        {
            // Check if the bullet has collided with something
            if (StickOnImpact)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                DestroyBullet();
            }
        }
    }

    void DestroyBullet()
    {
        
        // Destroy the bullet
        Destroy(gameObject);
    } 
}