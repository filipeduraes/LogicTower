using System.Collections.Generic;
using UnityEngine;

namespace LogicTower.QuestSystem
{
    public class ChallengeTest : MonoBehaviour
    {
        [SerializeField] private ChallengeSettings challengeSettings;

        [SerializeField] private bool p;
        [SerializeField] private bool q;
        [SerializeField] private bool r;
        [SerializeField] private bool result;

        [ContextMenu("Test")]
        public void Test()
        {
            Dictionary<Formula,bool> values = new()
            {
                [Formula.P] = p,
                [Formula.Q] = q,
                [Formula.R] = r
            };
            
            result = challengeSettings.Solve(values);
        }
    }
}