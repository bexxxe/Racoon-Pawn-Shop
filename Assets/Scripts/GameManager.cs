using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public CustomerData[] customers;
    public GameObject customerObjects;
    public ItemSlot matSlot;
    public DialogueManager dialogueManager;
    public TMP_Text moneyText;
    public GameObject dayEndPanel;
    public TMP_Text dayEndText;

    private int currentCustomerIndex = 0;
    private int totalMoney = 0;
    void Start()
    {
        LoadCustomer(0);
    }

    public void LoadCustomer(int index) 
    {
        if (index >= customers.Length) 
        {
            EndDay();
            return;
        } 
        
        CustomerData data = customers[index];
        customerObjects.GetComponent<SpriteRenderer>().sprite = data.customerSprite;
        customerObjects.GetComponent<Animator>().runtimeAnimatorController 
            = data.animatorController;
        matSlot.requiredItem = data.requestedItem;
        dialogueManager.requestDialogue = data.requestDialogue;
        customerObjects.SetActive(true);
        customerObjects.GetComponent<CustomerEvents>().PlayTimeline();

    }

    public void OnCustomerServed(bool correct) 
    {
        if (correct) 
        {
            totalMoney += matSlot.requiredItem.value; 
            moneyText.text = "$" + totalMoney;
        }
        
        currentCustomerIndex++;
        Invoke("LoadNextCustomer", 2f);
    }

    void LoadNextCustomer() 
    {
        LoadCustomer(currentCustomerIndex);
    }

    void EndDay() 
    {
        dayEndPanel.SetActive(true);
        dayEndText.text = "You Made $" + totalMoney + " today!";
    }

   
}
