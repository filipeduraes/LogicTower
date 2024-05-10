using System;
using LogicTower.ExpressionParsing;
using UnityEngine;

namespace LogicTower.UI
{
    public static class QuestUIUtils
    {
        public static string GetFormulaRepresentation(Formula formula, Color color)
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

            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{formulaText}</color>";
        }
    }
}