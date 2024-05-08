using LogicTower.InteractionSystem;
using LogicTower.QuestSystem;
using UnityEngine;

namespace LogicTower.Interactions
{
    public class ToggleQuestVariableInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool currentValue;
        [SerializeField] private QuestManager.QuestVariable questVariable; 

        private void Start()
        {
            QuestManager.SetChallengeVariable(questVariable, currentValue);
        }

        public void Interact()
        {
            currentValue = !currentValue;
            QuestManager.SetChallengeVariable(questVariable, currentValue);
        }
    }
}