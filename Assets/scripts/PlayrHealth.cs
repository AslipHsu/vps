using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayrHealth : MonoBehaviour
{
    public static PlayrHealth Instance;
    private void Awake()
    {
        Instance = this;
    }
    public float currentHealth,maxHealth ;

    public Slider healthSlider;

    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerStatsController.instance.health[0].value;
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damageToTake)
    {

        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(deathEffect, transform.position, transform.rotation);
            LevelManger.instance.EndLevel();

        }
        healthSlider.value = currentHealth;
    }
}