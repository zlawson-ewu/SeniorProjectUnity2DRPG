using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour, ISaveManager
{
    public static QuestManager Instance = null;

    public SerializableDictionary<string, bool> questGivers;
    public SerializableDictionary<string, int> questGiversCount;

    public List<QuestGiver> givers;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        givers = FindObjectsOfType<QuestGiver>().ToList();
        foreach (QuestGiver giver in givers)
        {
            if (data.questGivers.TryGetValue(giver.transform.parent.name, out bool output))
            {
                giver.questCompleted = output;
            }
            if (data.questGiversCount.TryGetValue(giver.transform.parent.name, out int outCount))
            {
                giver.objectivesCompleted = outCount;
            }
            if (data.questGiversRewarded.TryGetValue(giver.transform.parent.name, out bool reward))
            {
                giver.playerRewarded = reward;
            }
            giver.CompleteQuestIfObjectivesDone();
            if (giver.playerRewarded)
            {
                giver.GetComponent<DialogueTrigger>().dialogue = giver.DialogueAfterReward;
            }
        }
    }

    public void SaveData(GameData data)
    {
        givers = FindObjectsOfType<QuestGiver>().ToList();
        foreach (QuestGiver giver in givers)
        {
            if (!data.questGivers.TryAdd(giver.transform.parent.name, giver.questCompleted))
            {
                data.questGivers[giver.transform.parent.name] = giver.questCompleted;
            }
            if (!data.questGiversCount.TryAdd(giver.transform.parent.name, giver.objectivesCompleted))
            {
                data.questGiversCount[giver.transform.parent.name] = giver.objectivesCompleted;
            }
            if (!data.questGiversRewarded.TryAdd(giver.transform.parent.name, giver.playerRewarded))
            {
                data.questGiversRewarded[giver.transform.parent.name] = giver.playerRewarded;
            }
        }
    }
}
