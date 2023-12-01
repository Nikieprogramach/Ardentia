using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    void Awake()
    {
        instance = this;
    }

    public Rigidbody2D cam;
    public Rigidbody2D rb;
    public Transform feetPos;

    public float maxHealth;
    public float maxBaseHealth;
    float currentHealth;
    public Slider health;
    public float maxMana;
    float currentMana;
    float regenMana;
    public Slider mana;

    public int money;
    public Text moneyAmount;

    public Animator animator;

    //PlayerMovement
    public float moveSpeed = 5;

    Vector2 movement;

    //Inventory
    private bool InventoryIsShowed = false;
    public GameObject InventoryUI;

    private bool EquipmentIsShowed = false;
    public GameObject EquipmentUI;

    //Stats
    public int Agility = 0;
    public int Strength = 0;
    public int Stamina = 0;
    public int Intelect = 0;
    public int Spirit = 0;

    public Text agilityText;
    public Text strengthText;
    public Text staminaText;
    public Text intelectText;
    public Text spiritText;

    Vector2 zeroZero;
    public string lastSceneEntered;
    Vector2 lastCordinates;

    //Checkpoints
    public Transform lastCheckpoint;
    public List<GameObject> enemiesThatAttackedBeforeDeath = new List<GameObject>();

    //Popup info
    public GameObject popupObj;
    public Text popupText;
    public float popupDuration = 5;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }

        //Stats
        agilityText.text = "Agility: " + Agility;
        strengthText.text = "Strength: " + Strength;
        staminaText.text = "Stamina: " + Stamina;
        intelectText.text = "Intelect: " + Intelect;
        spiritText.text = "Spirit: " + Spirit;

        //PlayerMovement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        //Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryIsShowed == false)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (EquipmentIsShowed == false)
            {
                EquipmentUI.SetActive(true);
                EquipmentIsShowed = true;
            }
            else
            {
                EquipmentUI.SetActive(false);
                EquipmentIsShowed = false;
                ItemInfo.instance.HideInfo();
            }
        }

        //Interaction
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact();
            }else if(hit.collider != null && hit.collider.gameObject.GetComponent<Vendor>() != null)
            {
                hit.collider.gameObject.GetComponent<Vendor>().OnClickOpenShop();
            }
            else if (hit.collider != null && hit.collider.gameObject.GetComponent<CraftingVendor>() != null)
            {
                hit.collider.gameObject.GetComponent<CraftingVendor>().OnClickOpenShop();
            }else if (hit.collider != null && hit.collider.gameObject.GetComponent<Chest>() != null)
            {
                hit.collider.gameObject.GetComponent<Chest>().OpenChest();
            }else if(hit.collider != null && hit.collider.gameObject.GetComponent<CheckPoint>() != null)
            {
                SetCheckpoint(hit.collider.GetComponent<Transform>());
            }else if(hit.collider != null && hit.collider.gameObject.GetComponent<QuestGiver>() != null)
            {
                hit.collider.gameObject.GetComponent<QuestGiver>().OpenQuestWindow();
            }else if (hit.collider != null && hit.collider.gameObject.GetComponent<Enemy>() != null)
            {
                hit.collider.gameObject.GetComponent<Enemy>().OnClickSetTarget();
            }
        }

        if (currentMana < maxMana)
        {
            regenMana += 1;
            if (regenMana >= 10)
            {
                currentMana += 1;
                mana.value = currentMana;
                regenMana = 0;
            }
        }
        if (popupDuration > 0)
        {
            popupDuration -= Time.deltaTime;
        }
        else
        {
            popupObj.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        //PlayerMovement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        cam.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Show()
    {
        InventoryUI.SetActive(true);
        InventoryIsShowed = true;
    }

    public void Hide()
    {
        InventoryUI.SetActive(false);
        InventoryIsShowed = false;
        ItemInfo.instance.HideInfo();
    }

    void Init()
    {
        zeroZero.x = 0;
        zeroZero.y = 0;
        maxBaseHealth = maxHealth;
        currentHealth = maxBaseHealth;
        currentMana = maxMana;

        health.maxValue = maxBaseHealth;
        health.value = currentHealth;
        mana.maxValue = maxMana;
        mana.value = currentMana;

        moneyAmount.text = "" + money;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<HouseEnter>() != null)
        {
            if(collider.GetComponent<HouseEnter>().houseName == lastSceneEntered)
            {
                Vector2 temp = transform.position;
                transform.position = lastCordinates;
                lastCordinates = temp;
                lastSceneEntered = collider.GetComponent<HouseEnter>().houseName;
            }

            lastCordinates = transform.position;
            lastSceneEntered = collider.GetComponent<HouseEnter>().houseName;
            transform.position = zeroZero;
            Debug.Log("Entering house");
            SceneManager.LoadScene(collider.GetComponent<HouseEnter>().houseName);
        }
    }

    public void TakeDamage(float damage, GameObject enemy)
    {
        
        if(enemy != null)
        {
            enemiesThatAttackedBeforeDeath.Add(enemy.gameObject);
        }
        currentHealth -= damage;
        health.value = currentHealth;
    }

    public bool UseMana(float manaUsed)
    {
        if (manaUsed > currentMana)
        {
            return false;
        }
        else
        {
            currentMana -= manaUsed;
            mana.value = currentMana;
            return true;
        }
    }

    public bool GiveMoney(int spentMoney)
    {
        if(spentMoney > money)
        {
            return false;
        }
        else
        {
            money -= spentMoney;
            moneyAmount.text = "" + money;
            return true;
        }
    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        moneyAmount.text = "" + money;
    }

    public void UpdateStats()
    {
        if(currentHealth == maxHealth)
        {
            maxHealth = maxBaseHealth + Stamina * 10;
            health.maxValue = maxHealth;
            currentHealth = maxHealth;
            health.value = currentHealth;
        }
        else
        {
            maxHealth = maxBaseHealth + Stamina * 10;
            health.maxValue = maxHealth;
        }
    }

    public void SetCheckpoint(Transform checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }

    void Die()
    {
        transform.position = lastCheckpoint.position;

        foreach(GameObject enemy in enemiesThatAttackedBeforeDeath)
        {
            if(enemy)
            {
                enemy.GetComponent<Enemy>().ResetAgro();
            }
        }

        currentHealth = maxHealth;
        health.value = currentHealth;
    }

    public void UsePotion(Potion potion)
    {
        if(currentHealth + potion.regenHealth < maxHealth)
        {
            currentHealth += potion.regenHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }

        if (currentMana + potion.regenMana < maxMana)
        {
            currentMana += potion.regenMana;
        }
        else
        {
            currentMana = maxMana;
        }

        health.value = currentHealth;
        mana.value = currentMana;
    }

    public void SetPopup(string text)
    {
        popupDuration = 5;
        popupObj.SetActive(true);
        popupText.text = text;
    }
}
