Quests v1

There are two scripts, QuestGiver and QuestTarget. 

QuestGiver should be attached to the InteractableCircle child of an NPC.

QuestTarget should be attached to the EnemySprite child of an Enemy. It could also be attached to anything else as long as the script of that attached object calls QuestTargetCompleted() on its QuestTarget whenever it's time for that condition to be met.

Questgivers have a list of QuestTargets assigned in the inspector. QuestTargets must be associated with a QuestGiver in the inspector. QuestGivers keep track of how many QuestTargets have been completed, and when the completed number equals the size of the list, the Dialogue for the attached NPC's DialogueTrigger will be switched to the Dialogue On Complete in QuestGiver.