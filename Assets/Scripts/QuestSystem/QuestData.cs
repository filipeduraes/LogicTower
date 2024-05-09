using System.Collections.Generic;
using LogicTower.Data;
using LogicTower.ExpressionParsing;

namespace LogicTower.QuestSystem
{
    public readonly struct QuestData
    {
        public readonly List<Token> tokens;
        public readonly ChallengeSettings challengeSettings;
        private readonly Dictionary<Formula, bool> _variables;

        public QuestData(List<Token> tokens, Dictionary<Formula, bool> variables, ChallengeSettings challengeSettings)
        {
            this.tokens = tokens;
            this.challengeSettings = challengeSettings;
            _variables = variables;
        }

        public bool GetFormulaValue(Formula formula)
        {
            return _variables.ContainsKey(formula) && _variables[formula];
        }
    }
}