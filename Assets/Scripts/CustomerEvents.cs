using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Playables;

public class CustomerEvents : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public PlayableDirector playableDirector;
    public void OnEntryDone() 
    {
        CustomEvent.Trigger(gameObject, "EntryDone");
        Debug.Log("Customer has arrived!");
        FindFirstObjectByType<DialogueManager>().ShowRequestDialogue();
        
        ItemSlot[] slots = FindObjectsByType<ItemSlot>(FindObjectsSortMode.None);
        foreach (ItemSlot slot in slots) 
        {
            if (slot.isMat) 
            {
                Debug.Log("Mat slot is now interactable.");
                slot.isInteractable = true;
                break;
                
            }
        }
    }

    public void OnLeave() 
    {
        dialogueManager.TriggerLeave();
    }

    public void PlayTimeline() 
    {
        playableDirector.Play();
    }
}
