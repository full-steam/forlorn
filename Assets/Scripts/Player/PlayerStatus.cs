using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private const int HUNGER_STEPS = 45;

    [HideInInspector] public float maxHealth = 5;
    [HideInInspector] public float maxHunger = 5;
    public float health;
    public float hunger;

    private int money;
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            GameManager.Instance.Blackboard.VariableStorage.SetVariable("$VAR_Money", new Yarn.Value(money));
        }
    }
    
    public bool starving;
    public int steps;
    public List<ItemObject> itemList;

    [SerializeField]
    private Vector2 lastPos;
    [SerializeField]
    private float distanceSum;
    private Image[] healthIcons;
    private Image[] hungerIcons;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        //UpdateUI();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        distanceSum += Vector3.Distance(lastPos, transform.position);
        lastPos = transform.position;
        if (distanceSum >= 1.0f)
        {
            distanceSum = 0;
            steps++;
            if (steps >= HUNGER_STEPS)
            {
                steps = 0;
                ModifyHunger(-0.5f);
            }
        }
    }

    public void AssignPlayerStatus(SaveObject stat, bool skipPosition = false)
    {
        health = stat.health;
        hunger = stat.hunger;
        distanceSum = stat.distanceSum;
        steps = stat.steps;
        starving = stat.starving;
        Money = stat.money;
        if (!skipPosition) transform.position = stat.posInScene.GetData();

        itemList = new List<ItemObject>();
        foreach (var item in stat.itemList)
        {
            var io = ItemObject.CopyByValue(item);
            itemList.Add(io);
        }

        lastPos = transform.position;
        UpdateUI();
    }

    public void GetPlayerStatus(ref SaveObject stat)
    {
        stat.health = health;
        stat.hunger = hunger;
        stat.distanceSum = distanceSum;
        stat.steps = steps;
        stat.starving = starving;
        stat.money = Money;
        stat.posInScene.SetData(transform.position);

        stat.itemList = new List<ItemObject>();
        foreach (var item in itemList)
        {
            var io = ItemObject.CopyByValue(item);
            stat.itemList.Add(io);
        }
    }

    public void SetIcons(Image[] health, Image[] hunger)
    {
        healthIcons = health;
        hungerIcons = hunger;
        UpdateUI();
    }

    public void ModifyHealth(float amount)
    {
        health += amount;
        CheckHealth();
        UpdateUI();
    }

    public void ModifyHunger(float amount)
    {
        hunger += amount;
        CheckHunger();
        UpdateUI();
    }

    public void ModifyMoney(int amount)
    {
        Money += amount;
    }

    public void AddItem(ItemObject newItem)
    {
        int sameItemIndex = -1;
        if (GameManager.Instance.Blackboard.ItemLibrary.GetItem(newItem.itemID).stackable)  //check if item is stackable
        {                                                                                   //if so then check if player already has them before
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].itemID == newItem.itemID) { sameItemIndex = i; break; }
            }
        }

        if (sameItemIndex == -1) //no same item in itemlist / item unstackable, add new
        {
            itemList.Add(newItem);
            Debug.Log("new item added");
        }
        else                    //add the amount from the obtained item to previously owned stacks
        {
            itemList[sameItemIndex].count += newItem.count;
            Debug.Log("item amount added");
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Dead();
        }
        else if (health > maxHealth) health = maxHealth;
    }

    private void CheckHunger()
    {
        if (hunger <= 0)
        {
            hunger = 0;
            if (starving) return;
            else
            {
                //Debug.Log("Starving started");
                starving = true;
                InvokeRepeating("Starving", 3.0f, 3.0f);
            }
        }
        else if (hunger > 0)
        {
            if (hunger > maxHunger) hunger = maxHunger;
            if (starving)
            {
                //Debug.Log("Starving stopped");
                starving = false;
                CancelInvoke("Starving");
            }
        }
    }

    private void Starving()
    {
        //Debug.Log("Health reduced from starving");
        ModifyHealth(-0.5f);
    }

    private void Dead()
    {
        Debug.Log("Player ded. F");
        GetComponent<PlayerMovement>().ToggleMovement(false);
        GameManager.Instance.Dead();
    }

    private void UpdateUI()
    {
        //Health Icons
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i + 1 <= (health * 2)) healthIcons[i].enabled = true;
            else healthIcons[i].enabled = false;
        }

        //Hunger Icons
        for (int i = 0; i < hungerIcons.Length; i++)
        {
            if (i + 1 <= (hunger * 2)) hungerIcons[i].enabled = true;
            else hungerIcons[i].enabled = false;
        }
    }
}
