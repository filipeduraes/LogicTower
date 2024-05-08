using System;
using System.Text;
using LogicTower.ExpressionParsing;
using TMPro;
using UnityEngine;

namespace LogicTower.QuestSystem
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
                    stringBuilder.Append(GetFormulaRepresentation(formulaToken.Formula, questData.GetFormulaValue(formulaToken.Formula)));
            }
            
            text.SetText(stringBuilder.ToString());
        }

        private string GetFormulaRepresentation(Formula formula, bool isTrue)
        {
            string formulaText = formula switch
            {
                Formula.P => "p",
                Formula.Q => "q",
                Formula.R => "r",
                Formula.S => "s",
                Formula.P1 => "p1",
                Formula.P2 => "p2",
                Formula.P3 => "p3",
                _ => throw new ArgumentOutOfRangeException(nameof(formula), formula, null)
            };

            Color color = isTrue ? trueColor : falseColor;
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{formulaText}</color>";
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