using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{

    [HideInInspector] public float maxHealth = 5;
    [HideInInspector] public float maxHunger = 5;
    public float health;
    public float hunger;
    public int money;
    public bool starving;
    public int steps;
    public List<ItemObject> itemList;

    private Vector2 lastPos;
    private float distanceSum;
    private Image[] healthIcons;
    private Image[] hungerIcons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignPlayerStatus(SaveObject stat) 
    {
        health = stat.health;
        hunger = stat.hunger;
        distanceSum = stat.distanceSum;
        steps = stat.steps;
        starving = stat.starving;
        transform.position = stat.posInScene.GetData();
        itemList = stat.itemList;
    }

    public void GetPlayerStatus(ref SaveObject stat) 
    {
        stat.health = health;
        stat.hunger = hunger;
        stat.distanceSum = distanceSum;
        stat.steps = steps;
        stat.starving = starving;
        stat.posInScene.SetData(transform.position);
        stat.itemList = itemList;
    }

    public void SetIcons(Image[] health, Image[] hunger)
    {
        healthIcons = health;
        hungerIcons = hunger;
    }

    public void ModifyHealth(float amount) { }
    
    public void ModifyHunger(float amount) { }
    
    public void ModifyMoney(int amount) { }
    
    public void AddItem(ItemObject newItem) { }

    private void CheckHealth() { }

    private void CheckHunger() { }

    private void Starving() { }

    private void Death() { }

    private void UpdateUI() { }
}
