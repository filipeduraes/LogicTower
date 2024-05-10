using System;
using System.Collections.Generic;
using LogicTower.Data;
using LogicTower.ExpressionParsing;
using UnityEngine;

namespace LogicTower.QuestSystem
{
    public class QuestManager : MonoBehaviour, IChallengeHandler
    {
        public static event Action<QuestData> OnQuestDataChanged = delegate { }; 
        public static event Action OnNewQuestAvailable = delegate { }; 
        public static event Action<bool> OnQuestFinishedChanged = delegate { };
        
        private static QuestManager questManager;
        private readonly Dictionary<Formula, bool> _challengeVariables = new();
        private static readonly Queue<(QuestVariable, bool)> ChangesRequestedBeforeInitialization = new();
        
        private ChallengeSettings _challengeSettings;
        private ExpressionParser _parser;
        private bool _challengeIsFinished;

        public void PopulateSettings(ChallengeSettings settings)
        {
            questManager = this;
            _challengeSettings = settings;
            _parser = new ExpressionParser(_challengeSettings.Expression);

            while (ChangesRequestedBeforeInitialization.Count > 0)
            {
                (QuestVariable variable, bool value) = ChangesRequestedBeforeInitialization.Dequeue();
                SetChallengeVariableInternal(variable, value);
            }

            OnQuestDataChanged(new QuestData(_parser.GetTokens(), _challengeVariables, _challengeSettings));
            OnNewQuestAvailable();
        }

        public static void SetChallengeVariable(QuestVariable questVariable, bool value)
        {
            if (!questManager)
            {
                ChangesRequestedBeforeInitialization.Enqueue((questVariable, value));
                return;
            }
            
            questManager.SetChallengeVariableInternal(questVariable, value);
        }

        private void SetChallengeVariableInternal(QuestVariable questVariable, bool value)
        {
            Formula formula = (Formula)(int)questVariable;
            
            _challengeVariables[formula] = value;
            bool solved = _parser.Solve(_challengeVariables);
            
            OnQuestDataChanged(new QuestData(_parser.GetTokens(), _challengeVariables, _challengeSettings));

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