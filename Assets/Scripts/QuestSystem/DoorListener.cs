using System;
using UnityEngine;

namespace LogicTower.QuestSystem
{
    public class DoorListener : MonoBehaviour
    {
        [SerializeField] private Transform levelTrigger;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private string openState;
        [SerializeField] private string closeState;

        private void OnEnable()
        {
            QuestManager.OnQuestFinishedChanged += SetDoorIsOpenState;
        }

        private void OnDisable()
        {
            QuestManager.OnQuestFinishedChanged -= SetDoorIsOpenState;
        }

        private void SetDoorIsOpenState(bool isOpen)
        {
            doorAnimator.Play(isOpen ? openState : closeState);
            levelTrigger.gameObject.SetActive(isOpen);
        }
    }
}