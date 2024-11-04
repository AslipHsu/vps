using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this; 
    }

    public float moveSpeed;
    public Animator anim;

    public float pickupRange = 1.5f;
    private SpriteRenderer sp;

    //public Weapon activeWeapon;
    public List<Weapon> unassignedWeapons, assignedWeapons;

    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        sp = GameObject.Find("spriteL").GetComponent<SpriteRenderer>();
        Debug.Log("1");

        AddWeapon(Random.Range(0, unassignedWeapons.Count));
        Debug.Log("5");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f,0f,0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        transform.position += moveInput* moveSpeed* Time.deltaTime;

        //if (moveInput != Vector3.zero)
        //{
        //    anim.SetBool("isMoving", true);
        //}
        //else
        //{
        //    anim.SetBool("isMoving", false);
        //}

        if (moveInput.x > 0 || moveInput.x < 0)
        {
            if (moveInput.x > 0)
            {
                sp.flipX = true;
            }
            else
            {
                sp.flipX = false;
            }
            anim.SetBool("isMovingL", true);
            anim.SetBool("isMovingU", false);
            anim.SetBool("isMoving", false);

        }
        else if (moveInput.y < 0)
        {
            anim.SetBool("isMovingL", false);
            anim.SetBool("isMovingU", false);
            anim.SetBool("isMoving", true);
        }
        else if (moveInput.y > 0)
        {
            anim.SetBool("isMovingL", false);
            anim.SetBool("isMovingU", true);
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMovingL", false);
            anim.SetBool("isMovingU", false);
            anim.SetBool("isMoving", false);
        }
    }
    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            Debug.Log("2");
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            Debug.Log("3");
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            Debug.Log("4");
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
