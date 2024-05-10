using System.Collections;
using LogicTower.InteractionSystem;
using LogicTower.QuestSystem;
using UnityEngine;

namespace LogicTower.UI
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private float popTime = 0.3f;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            QuestManager.OnNewQuestAvailable += ImmediateHideUI;
            Interactor.OnInteractionFound += ShowUI;
            Interactor.OnInteractionReleased += HideUI;
        }

        private void OnDisable()
        {
            QuestManager.OnNewQuestAvailable -= ImmediateHideUI;
            Interactor.OnInteractionFound -= ShowUI;
            Interactor.OnInteractionReleased -= HideUI;
        }

        private void ImmediateHideUI()
        {
            transform.localScale = Vector3.zero;
        }

        private void ShowUI(Transform interactionTransform)
        {
            Vector3 newPosition = interactionTransform.position;
            transform.position = newPosition;
            
            StopAllCoroutines();
            StartCoroutine(PopUI(1f));
        }
        
        private void HideUI()
        {
            StopAllCoroutines();
            StartCoroutine(PopUI(0f));
        }

        private IEnumerator PopUI(float finalScale)
        {
            Vector3 initialScale = transform.localScale;
            float timer = 0f;

            while (timer <= popTime)
            {
                transform.localScale = Vector3.Lerp(initialScale, Vector3.one * finalScale, timer / popTime);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.one * finalScale;
        }
    }
}