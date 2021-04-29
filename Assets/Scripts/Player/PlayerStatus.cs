using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{

    public float maxHealth;
    public float maxHunger;
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

    public void AssignPlayerStatus(SaveObject stat) { }

    public void GetPlayerStatus(/*out*/ SaveObject stat) { }

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
