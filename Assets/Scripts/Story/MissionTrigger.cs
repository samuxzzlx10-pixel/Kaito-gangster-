using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class MissionTrigger : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private string missionId;
    [SerializeField] private bool triggerOnce = true;
    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || (triggerOnce && hasTriggered))
            return;

        hasTriggered = true;
        if (storyManager != null)
            storyManager.BeginMission(missionId);
    }
}
