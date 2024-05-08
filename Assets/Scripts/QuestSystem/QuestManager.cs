using System;
using System.Collections.Generic;
using LogicTower.ExpressionParsing;
using UnityEngine;

namespace LogicTower.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private ChallengeSettings challengeSettings;

        public bool ChallengeIsFinished { get; private set; }

        public static event Action<QuestData> OnQuestDataChanged = delegate { }; 
        private readonly Dictionary<Formula, bool> _challengeVariables = new();
        private ExpressionParser _parser;
        private static QuestManager _questManager;

        private void Awake()
        {
            _questManager = this;
            _parser = new ExpressionParser(challengeSettings.Expression);
        }

        private void Start()
        {
            OnQuestDataChanged(new QuestData(_parser.GetTokens(), _challengeVariables));
        }

        public static void SetChallengeVariable(QuestVariable questVariable, bool value)
        {
            _questManager.SetChallengeVariableInternal(questVariable, value);
        }

        private void SetChallengeVariableInternal(QuestVariable questVariable, bool value)
        {
            Formula formula = (Formula)(int)questVariable;
            
            _challengeVariables[formula] = value;
            ChallengeIsFinished = _parser.Solve(_challengeVariables);
            
            OnQuestDataChanged(new QuestData(_parser.GetTokens(), _challengeVariables));
        }
        
        public enum QuestVariable
        {
            P, Q, R, S, P1, P2, P3
        }
    }
}