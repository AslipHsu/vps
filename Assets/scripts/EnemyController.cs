using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    public Transform target;
    private SpriteRenderer sp;
    public float damage;

    public float hitWaitTime = 1f;
    private float hitCounter;

    public float health=10 ;

    public float knockBackTime = .5f;
    private float knockBackCounter;

    public int expToGive = 1;
    // Start is called before the first frame update
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayrHealth.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }
            if(knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);

            }
        }


        theRB.velocity = (target.position-transform.position).normalized*moveSpeed;

        if (theRB.velocity.x < 0){
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
        if(hitCounter > 0f) {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter<=0f)
        {
            PlayrHealth.Instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceLevelController.Instance.SpawnExp(transform.position,expToGive);
        }
        DamageNumberController.Instance.SpawnDamage(damageToTake,transform.position);
    }

    public void TakeDamage(float damageTotake,bool shouldKnockBack)
    {
        TakeDamage(damageTotake);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }
}

