using System;
using UnityEngine;

[Serializable]
public sealed class StoryMission
{
    public string id = "mission_01";
    public string title = "Nova missão";
    [TextArea(2, 6)] public string description = "Descreva a missão.";
    [TextArea(3, 10)] public string[] objectives = { "Objetivo 1" };
    public bool completed;
    public int currentObjectiveIndex;
}

public sealed class StoryManager : MonoBehaviour
{
    [SerializeField] private StoryMission[] missions;
    [SerializeField] private DialogueUI dialogueUI;

    private int currentMissionIndex;

    public StoryMission CurrentMission => currentMissionIndex >= 0 && currentMissionIndex < missions.Length ? missions[currentMissionIndex] : null;
    public bool IsFinished => currentMissionIndex >= missions.Length;

    private void Start()
    {
        if (missions != null && missions.Length > 0)
            BeginMission(0);
    }

    public void BeginMission(int index)
    {
        if (missions == null || index < 0 || index >= missions.Length)
            return;

        currentMissionIndex = index;
        var mission = missions[currentMissionIndex];
        mission.completed = false;
        mission.currentObjectiveIndex = 0;
        if (dialogueUI != null)
            dialogueUI.ShowMissionBanner(mission.title, mission.description);
    }

    public void BeginMission(string id)
    {
        if (missions == null)
            return;

        for (int i = 0; i < missions.Length; i++)
        {
            if (missions[i].id == id)
            {
                BeginMission(i);
                return;
            }
        }
    }

    public void AdvanceObjective()
    {
        var mission = CurrentMission;
        if (mission == null)
            return;

        if (mission.currentObjectiveIndex < mission.objectives.Length - 1)
        {
            mission.currentObjectiveIndex++;
            if (dialogueUI != null)
                dialogueUI.ShowMissionBanner(mission.title, mission.objectives[mission.currentObjectiveIndex]);
            return;
        }

        CompleteMission();
    }

    public void CompleteMission()
    {
        var mission = CurrentMission;
        if (mission == null)
            return;

        mission.completed = true;
        if (dialogueUI != null)
            dialogueUI.ShowMissionBanner(mission.title, "Missão concluída!");

        if (currentMissionIndex + 1 < missions.Length)
            BeginMission(currentMissionIndex + 1);
    }
}
