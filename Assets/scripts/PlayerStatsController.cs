using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController instance;

    private void Awake()
    {
        instance = this; 
    }
    public List<PlayerStatValue> moveSpeed, health, picupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount,pickupRangeLevelCount;

    public int moveSpeedLevel,healthLevel, picupRangeLevel, maxWeaponLevel;
    // Start is called before the first frame update
    void Start()
    {
        for(int i= moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        for (int i = picupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            picupRange.Add(new PlayerStatValue(picupRange[i].cost + picupRange[1].cost, picupRange[i].value + (picupRange[1].value - picupRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.instance.LevelUpPanel.activeSelf)
        {
            UpdateDisplay();
        }
    }
    public void UpdateDisplay()
    {
        if (moveSpeedLevel<moveSpeed.Count-1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        else
        {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1)
        {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }

        if (picupRangeLevel < picupRange.Count - 1)
        {
            UIController.instance.pickupRangeUpgradeDisplay.UpdateDisplay(picupRange[picupRangeLevel + 1].cost, picupRange[picupRangeLevel].value, picupRange[picupRangeLevel + 1].value);
        }
        else
        {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

        if (maxWeaponLevel < maxWeapons.Count - 1)
        {
            UIController.instance.maxWeaponUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponLevel + 1].cost, maxWeapons[maxWeaponLevel].value, maxWeapons[maxWeaponLevel + 1].value);
        }
        else
        {
            UIController.instance.maxWeaponUpgradeDisplay.ShowMaxLevel();
        }
    }

    public void PurechaseMoveSpeed()
    {
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();

        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;


    }
    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();

        PlayrHealth.Instance.maxHealth = health[healthLevel].value;
        PlayrHealth.Instance.currentHealth += health[healthLevel].value- health[healthLevel-1].value;
    }
    public void PurchasePickupRange()
    {
        picupRangeLevel++;
        CoinController.instance.SpendCoins(picupRange[picupRangeLevel].cost);
        UpdateDisplay();

        PlayerController.instance.pickupRange=picupRange[picupRangeLevel].value;
    }
    public void PurchaseMaxWeapons()
    {
        maxWeaponLevel++;
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponLevel].cost);
        UpdateDisplay();

        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponLevel].value);
    }


}

[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;
    public PlayerStatValue(int newCost,float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}