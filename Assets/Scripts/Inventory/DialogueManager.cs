using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
public class DialogueManager : MonoBehaviour
{
   public GameObject dialogueBox;
   public TMP_Text dialogueText;
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
         dialogueBox.SetActive(true);
         dialogueText.text = dialogue;
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

   IEnumerator AcceptAndLeave() 
   {
       yield return new WaitForSeconds(2f);
       dialogueBox.SetActive(false);
       customerAnimator.SetBool("isTalking", false);

       if (matSlot.childCount > 0) 
       {
           Transform item = matSlot.GetChild(0);
           item.SetParent(customer);
       }

       gameManager.OnCustomerServed(true);

       StartCoroutine(SlideOut()); 
   }

   IEnumerator HideAfterDelay(float delay) 
   {
       yield return new WaitForSeconds(delay);
       dialogueBox.SetActive(false);
       customerAnimator.SetBool("isTalking", false);
   }

   IEnumerator SlideOut() 
   {
       float duration = 1.5f;
       Vector3 startPos = customer.position;
       Vector3 endPos = startPos + new Vector3(10f, 0, 0); 
       float elapsed = 0;

       while (elapsed < duration) 
       {
           customer.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
           elapsed += Time.deltaTime;
           yield return null;
       }
       
       Destroy(customer.gameObject);
   }

}
