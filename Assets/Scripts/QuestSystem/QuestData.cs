using System.Collections.Generic;
using LogicTower.ExpressionParsing;

namespace LogicTower.QuestSystem
{
    public struct QuestData
    {
        public List<Token> tokens;
        public Dictionary<Formula, bool> variables;

        public QuestData(List<Token> tokens, Dictionary<Formula,bool> variables)
        {
            this.tokens = tokens;
            this.variables = variables;
        }

        public bool GetFormulaValue(Formula formula)
        {
            return variables.ContainsKey(formula) && variables[formula];
        }
    }
}