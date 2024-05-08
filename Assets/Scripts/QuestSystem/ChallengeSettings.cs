using UnityEngine;

namespace LogicTower.QuestSystem
{
    [CreateAssetMenu(menuName = "Logic Tower/Challenge Settings")]
    public class ChallengeSettings : ScriptableObject
    {
        [SerializeField] private string expression;

        public string Expression => expression;
    }
}