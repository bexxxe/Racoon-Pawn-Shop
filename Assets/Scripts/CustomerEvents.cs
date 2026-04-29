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
