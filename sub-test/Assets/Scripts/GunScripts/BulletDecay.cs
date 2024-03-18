using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public bool StickOnImpact;
    public float lifespan; // Time before the bullet disappears if not collided with anything
    private float timer;
    private float multiplier = 1;

    void Update()
    {
        // Increase the timer
        timer += Time.deltaTime * multiplier;

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
                multiplier = 100;
            }
        }
    }

    void DestroyBullet()
    {
        
        // Destroy the bullet
        Destroy(gameObject);
    } 
}