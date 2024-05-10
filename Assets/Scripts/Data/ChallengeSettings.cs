using System;
using LogicTower.ExpressionParsing;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LogicTower.Data
{
    [CreateAssetMenu(menuName = "Logic Tower/Challenge Settings")]
    public class ChallengeSettings : ScriptableObject
    {
        [SerializeField] private string expression;
        [SerializeField] private FormulaDescription[] formulaDescriptions;
        [SerializeField] private AssetReferenceGameObject levelPrefab; 

        public string Expression => expression;
        public AssetReferenceGameObject LevelPrefab => levelPrefab;
        public FormulaDescription[] FormulaDescriptions => formulaDescriptions;

        [Serializable]
        public struct FormulaDescription
        {
            public Formula formula;
            public string description;
        }
    }
}