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

   public void ShowDialogue(bool isCorrect) 
   {
       dialogueBox.SetActive(true);
       if (isCorrect) 
       {
           dialogueText.text = "Woof! That's exactly what I needed. Thanks!";
           StartCoroutine(AcceptAndLeave());
       } 
       else 
       {
           dialogueText.text = "Hmm... that's not quite right.";
           StartCoroutine(HideAfterDelay(2f));
       }
      
   }

   IEnumerator AcceptAndLeave() 
   {
       yield return new WaitForSeconds(2f);
       dialogueBox.SetActive(false);

       if (matSlot.childCount > 0) 
       {
           Transform item = matSlot.GetChild(0);
           item.SetParent(customer);
       }
       
       StartCoroutine(SlideOut()); 
   }

   IEnumerator HideAfterDelay(float delay) 
   {
       yield return new WaitForSeconds(delay);
       dialogueBox.SetActive(false);
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
