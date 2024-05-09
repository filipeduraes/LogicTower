using System;
using System.Text;
using LogicTower.ExpressionParsing;
using LogicTower.QuestSystem;
using TMPro;
using UnityEngine;

namespace LogicTower.UI
{
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color trueColor = Color.green;
        [SerializeField] private Color falseColor = Color.red;
        
        private void OnEnable()
        {
            QuestManager.OnQuestDataChanged += Populate;
        }

        private void OnDisable()
        {
            QuestManager.OnQuestDataChanged -= Populate;
        }

        private void Populate(QuestData questData)
        {
            StringBuilder stringBuilder = new();
            
            foreach (Token token in questData.tokens)
            {
                if (token.IsType(TokenType.Auxiliary, out AuxiliaryToken auxiliaryToken))
                    stringBuilder.Append(GetAuxiliaryRepresentation(auxiliaryToken.Auxiliary));
                else if (token.IsType(TokenType.BinaryOperator, out BinaryOperatorToken binaryToken))
                    stringBuilder.Append(GetBinaryOperatorRepresentation(binaryToken.Operator));
                else if (token.IsType(TokenType.UnaryOperator, out UnaryOperatorToken unaryToken))
                    stringBuilder.Append(GetUnaryOperatorRepresentation(unaryToken.Operator));
                else if (token.IsType(TokenType.Formula, out FormulaToken formulaToken))
                    stringBuilder.Append(QuestUIUtils.GetFormulaRepresentation(formulaToken.Formula, questData.GetFormulaValue(formulaToken.Formula) ? trueColor : falseColor));
            }
            
            text.SetText(stringBuilder.ToString());
        }

        private static string GetUnaryOperatorRepresentation(UnaryOperator unaryOperator)
        {
            return unaryOperator switch
            {
                UnaryOperator.NOT => "~",
                _ => throw new ArgumentOutOfRangeException(nameof(unaryOperator), unaryOperator, null)
            };
        }

        private static string GetBinaryOperatorRepresentation(BinaryOperator binaryOperator)
        {
            return binaryOperator switch
            {
                BinaryOperator.AND => " ^ ",
                BinaryOperator.OR => " v ",
                BinaryOperator.Conditional => " -> ",
                BinaryOperator.Biconditional => " <-> ",
                _ => throw new ArgumentOutOfRangeException(nameof(binaryOperator), binaryOperator, null)
            };
        }

        private static string GetAuxiliaryRepresentation(Auxiliary auxiliary)
        {
            return auxiliary switch
            {
                Auxiliary.OpenParenthesis => "(",
                Auxiliary.CloseParenthesis => ")",
                _ => throw new ArgumentOutOfRangeException(nameof(auxiliary), auxiliary, null)
            };
        }
    }
}