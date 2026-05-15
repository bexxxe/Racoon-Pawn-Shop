using UnityEngine;
using UnityEngine.Playables;
using TMPro;



public class GameManager : MonoBehaviour
{
    public CustomerData[] customers;
    public GameObject[] customerObjects;
    public ItemSlot matSlot;
    public DialogueManager dialogueManager;
    public TMP_Text moneyText;
    public GameObject dayEndPanel;
    public TMP_Text dayEndText;
    

    private int currentCustomerIndex = 0;
    private int totalMoney = 0;
    private Vector3[] customerStartPosition;
    private GameObject currentCustomer;
    void Start()
    {
        customerStartPosition = new Vector3[customerObjects.Length];
        for (int i = 0; i < customerObjects.Length; i++)
        {
            customerStartPosition[i] = customerObjects[i].transform.position;
            customerObjects[i].SetActive(false);
        }
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

       if (currentCustomer != null) 
            currentCustomer.gameObject.SetActive(false);
        
        currentCustomer = customerObjects[index];
        
        currentCustomer.transform.position = customerStartPosition[index];

        PlayableDirector director = currentCustomer.GetComponent<PlayableDirector>();
        director.Stop();
        director.playableAsset = data.timeline;

        matSlot.requiredItem = data.requestedItem;
        matSlot.isInteractable = false;
        dialogueManager.requestDialogue = data.requestDialogue;
        dialogueManager.customer = currentCustomer.transform;
        currentCustomer.gameObject.SetActive(true);
        director.Play();

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
