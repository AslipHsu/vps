using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;
   
    public Transform holder, fireballToSpawnn;

    public float timeBetweenSpawn;
    private float spawnCounter;

    public EnemyDamager damager;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        //UIController.instance.levelUpButton[0].UpdateButtonDisplay(this);
    }

    // Update is called once per frame
    void Update()
    {
        holder.transform.rotation = Quaternion.Euler(0f, 0f, holder.transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));


        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            //Instantiate(fireballToSpawnn,fireballToSpawnn.position,fireballToSpawnn.rotation,holder).gameObject.SetActive(true);

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(fireballToSpawnn, fireballToSpawnn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }

    
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        damager.lifeTime = stats[weaponLevel].duration;

    }
}
