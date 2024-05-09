using System.Collections;
using System.Text;
using LogicTower.Data;
using LogicTower.QuestSystem;
using TMPro;
using UnityEngine;

namespace LogicTower.UI
{
    public class FormulaScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text formulasText;
        [SerializeField] private Color trueColor = Color.green;
        [SerializeField] private Color falseColor = Color.red;
        [SerializeField] private float hiddenOffset;
        [SerializeField] private float showTime = 0.5f;

        private Vector2 _initialMinAnchors;
        private Vector2 _initialMaxAnchors;
        private bool _uiIsEnabled;

        private void Start()
        {
            if (transform is RectTransform rectTransform)
            {
                _initialMinAnchors = rectTransform.anchorMin;
                _initialMaxAnchors = rectTransform.anchorMax;

                SetUIPositionFromOffset(hiddenOffset);
            }
        }

        private void OnEnable()
        {
            QuestManager.OnQuestDataChanged += UpdateUI;
        }

        private void OnDisable()
        {
            QuestManager.OnQuestDataChanged -= UpdateUI;
        }

        public void ShowUI()
        {
            if (!_uiIsEnabled)
                StartCoroutine(ToggleUI());
        }
        
        public void HideUI()
        {
            if (_uiIsEnabled)
                StartCoroutine(ToggleUI());
        }

        private void UpdateUI(QuestData questData)
        {
            StringBuilder textBuilder = new();
            ChallengeSettings.FormulaDescription[] descriptions = questData.challengeSettings.FormulaDescriptions;

            foreach (ChallengeSettings.FormulaDescription description in descriptions)
            {
                bool formulaValue = questData.GetFormulaValue(description.formula);
                Color formulaColor = formulaValue ? trueColor : falseColor;

                string formulaRepresentation = QuestUIUtils.GetFormulaRepresentation(description.formula, formulaColor);
                textBuilder.Append(formulaRepresentation);
                textBuilder.Append($": {description.description}\n");
            }
            
            formulasText.SetText(textBuilder.ToString());
        }

        private IEnumerator ToggleUI()
        {
            _uiIsEnabled = !_uiIsEnabled;
            float timer = 0f;
            float currentOffset = !_uiIsEnabled ? 0f : hiddenOffset;
            float finalOffset = _uiIsEnabled ? 0f : hiddenOffset;

            while (timer <= showTime)
            {
                float offset = Mathf.Lerp(currentOffset, finalOffset, timer / showTime);
                SetUIPositionFromOffset(offset);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void SetUIPositionFromOffset(float offset)
        {
            if (transform is RectTransform rectTransform)
            {
                rectTransform.anchorMin = _initialMinAnchors + Vector2.left * offset;
                rectTransform.anchorMax = _initialMaxAnchors + Vector2.left * offset;
            }
        }
    }
}