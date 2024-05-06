using System.Collections.Generic;
using UnityEngine;

namespace LogicTower.QuestSystem
{
    [CreateAssetMenu(menuName = "Logic Tower/Challenge Settings")]
    public class ChallengeSettings : ScriptableObject
    {
        [SerializeField] private string expression;

        private ExpressionParser _expressionParser;
        
        public bool Solve(Dictionary<Formula, bool> formulasValues)
        {
            _expressionParser ??= new ExpressionParser(expression);
            return _expressionParser.Solve(formulasValues);
        }
    }
}