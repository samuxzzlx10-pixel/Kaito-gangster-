using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField] private string speakerName = "NPC";
    [TextArea(2, 6)] [SerializeField] private string dialogueText = "Olá, Kaito.";
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private bool triggerOnce = true;
    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || (triggerOnce && hasTriggered))
            return;

        hasTriggered = true;

        if (dialogueUI != null)
            dialogueUI.ShowDialogue(speakerName, dialogueText);
    }
}
