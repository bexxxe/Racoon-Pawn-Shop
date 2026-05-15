using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
public class DialogueManager : MonoBehaviour
{
   public GameObject dialogueBox;
   public TMP_Text dialogueText;
   public GameObject requestDialogueBox;
   public TMP_Text requestDialogueText;
   public Transform customer; 
   public Transform matSlot;
   public string requestDialogue;
    public Animator customerAnimator;
   private GameManager gameManager;

   void Start() 
   {
       gameManager = FindFirstObjectByType<GameManager>();
      
   }

   public void ShowDialogue(bool correct, string dialogue)
    {
         Debug.Log("Showing dialogue: " + customer.name);
         requestDialogueBox.SetActive(false);
         dialogueBox.SetActive(true);
         dialogueText.text = dialogue;
         
         if (customerAnimator != null) 
             customerAnimator.SetBool("isTalking", true);

         if (correct) 
         {   
              StartCoroutine(AcceptAndLeave());
         } 
         else 
         {
              StartCoroutine(HideAfterDelay(2f));
         }
    }

   public void TriggerLeave() 
   {
       StartCoroutine(AcceptAndLeave());
   }

   public void ShowRequestDialogue() 
   {
       requestDialogueBox.SetActive(true);
       requestDialogueText.text = requestDialogue;
   }

   IEnumerator AcceptAndLeave() 
   {
       yield return new WaitForSeconds(2f);
       dialogueBox.SetActive(false);
       
       if (customerAnimator != null) 
           customerAnimator.SetBool("isTalking", false);
       

       if (matSlot.childCount > 0) 
       {
           Transform item = matSlot.GetChild(0);
           item.SetParent(customer);
       }

       Transform leavingCustomer = customer;
       StartCoroutine(SlideOut(leavingCustomer)); 
       gameManager.OnCustomerServed(true);
   }

   IEnumerator HideAfterDelay(float delay) 
   {
       yield return new WaitForSeconds(delay);
       dialogueBox.SetActive(false);
       
       if (customerAnimator != null) 
           customerAnimator.SetBool("isTalking", false);
   }

   IEnumerator SlideOut(Transform target) 
   {
       Debug.Log("Sliding out customer: " + target.name);
       float duration = 1.5f;
       Vector3 startPos = target.position;
       Vector3 endPos = startPos + new Vector3(10f, 0, 0); 
       float elapsed = 0;

       while (elapsed < duration) 
       {
           target.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
           Debug.Log("Customer position: " + target.position);
           elapsed += Time.deltaTime;
           yield return null;
       }
       
       target.gameObject.SetActive(false);
   }

}
