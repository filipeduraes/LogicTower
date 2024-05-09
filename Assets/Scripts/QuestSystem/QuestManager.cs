using System;
using System.Collections.Generic;
using LogicTower.Data;
using LogicTower.ExpressionParsing;
using UnityEngine;

namespace LogicTower.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private ChallengeSettings challengeSettings;

        public static event Action<QuestData> OnQuestDataChanged = delegate { }; 
        public static event Action<bool> OnQuestFinishedChanged = delegate { };
        
        private static QuestManager questManager;
        private readonly Dictionary<Formula, bool> _challengeVariables = new();
        private ExpressionParser _parser;
        private bool _challengeIsFinished;

        private void Awake()
        {
            questManager = this;
            _parser = new ExpressionParser(challengeSettings.Expression);
        }

        private void Start()
        {
            OnQuestDataChanged(new QuestData(_parser.GetTokens(), _challengeVariables, challengeSettings));
        }

        public static void SetChallengeVariable(QuestVariable questVariable, bool value)
        {
            questManager.SetChallengeVariableInternal(questVariable, value);
        }

        private void SetChallengeVariableInternal(QuestVariable questVariable, bool value)
        {
            Formula formula = (Formula)(int)questVariable;
            
            _challengeVariables[formula] = value;
            bool solved = _parser.Solve(_challengeVariables);
            
            OnQuestDataChanged(new QuestData(_parser.GetTokens(), _challengeVariables, challengeSettings));

            if (_challengeIsFinished != solved)
                OnQuestFinishedChanged(solved);
            
            _challengeIsFinished = solved;
        }
        
        public enum QuestVariable
        {
            P, Q, R, S, P1, P2, P3
        }
    }
}