using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int scoreValue = 240;
    public GameObject explosion;
    public Animator anim;
    public bool isDead = false;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        Debug.Log("Take Damage Enemy " + damage);
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }


    public void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        // Instantiate(explosionParticles, transform.position, Quaternion.identity);
        //audioManager.PlayExplosion();
        isDead = true;
        Destroy(gameObject);
    }
    
}