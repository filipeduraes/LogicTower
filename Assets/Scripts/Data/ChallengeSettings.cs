using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LogicTower.Data
{
    [CreateAssetMenu(menuName = "Logic Tower/Challenge Settings")]
    public class ChallengeSettings : ScriptableObject
    {
        [SerializeField] private string expression;
        [SerializeField] private string[] prepositionDescriptions;
        [SerializeField] private AssetReferenceGameObject levelPrefab; 

        public string Expression => expression;
        public AssetReferenceGameObject LevelPrefab => levelPrefab;
    }
}