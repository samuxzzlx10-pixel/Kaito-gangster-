using TMPro;
using UnityEngine;

public sealed class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject panel;

    public void ShowDialogue(string speaker, string message)
    {
        if (panel != null)
            panel.SetActive(true);

        if (speakerText != null)
            speakerText.text = speaker;

        if (dialogueText != null)
            dialogueText.text = message;
    }

    public void HideDialogue()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void ShowMissionBanner(string missionTitle, string message)
    {
        ShowDialogue(missionTitle, message);
    }
}
