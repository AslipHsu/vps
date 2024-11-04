using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;
    
    private List<DamageNumber> numberPool =new List<DamageNumber>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDamage(float damageInput,Vector3 location)
    {
        int rounded =Mathf.RoundToInt(damageInput);

        //DamageNumber newDamage= Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        DamageNumber newDamage = GetFromPool();

        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);
    
        newDamage.transform.position = location;
    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberTOoutput = null;

        if (numberPool.Count == 0) {
            numberTOoutput = Instantiate(numberToSpawn,numberCanvas);
        }
        else
        {
            numberTOoutput = numberPool[0];
            numberPool.RemoveAt(0);
        }
        
        return numberTOoutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);
        numberPool.Add(numberToPlace);
    }
}
