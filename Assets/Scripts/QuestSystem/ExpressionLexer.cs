using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicTower.QuestSystem
{
    public static class ExpressionLexer
    {
        public static List<Token> Tokenize(string expression)
        {
            char[] singleCharacterTokens = { '~', '^', 'v', 'p', 'q', 'r', 's', '(', ')' };
            List<Token> tokens = new();
            
            for (int i = 0; i < expression.Length; i++)
            {
                if (!char.IsSeparator(expression[i]))
                {
                    if (singleCharacterTokens.Contains(expression[i]))
                        tokens.Add(GetSingleCharacterToken(expression[i]));
                    else
                        tokens.Add(GetMultiCharacterToken(expression, i));
                }
            }

            return tokens;
        }

        private static Token GetMultiCharacterToken(string expression, int initialIndex)
        {
            string[] multiCharacterTokens = { "->", "<->", "p1", "p2", "p3" };
            bool[] matchingCharacters = new bool[5];

            for (int i = initialIndex; i < expression.Length; i++)
            {
                for (int j = 0; j < multiCharacterTokens.Length; j++)
                {
                    if (matchingCharacters[j] && expression[i] != multiCharacterTokens[j][i - initialIndex])
                        matchingCharacters[j] = false;
                }
            }

            if (matchingCharacters[0])
                return new BinaryOperatorToken(BinaryOperator.Conditional);
            if(matchingCharacters[1])
                return new BinaryOperatorToken(BinaryOperator.Biconditional);
            if(matchingCharacters[2])
                return new FormulaToken(Formula.P1);
            if(matchingCharacters[3])
                return new FormulaToken(Formula.P2);
            if(matchingCharacters[4])
                return new FormulaToken(Formula.P3);

            throw new ArgumentOutOfRangeException(nameof(expression), expression[initialIndex], null);
        }

        private static Token GetSingleCharacterToken(char character)
        {
            return character switch
            {
                    '~' => new UnaryOperatorToken(UnaryOperator.NOT),
                    '^' => new BinaryOperatorToken(BinaryOperator.AND),
                    'v' => new BinaryOperatorToken(BinaryOperator.OR),
                    'p' => new FormulaToken(Formula.P),
                    'q' => new FormulaToken(Formula.Q),
                    'r' => new FormulaToken(Formula.R),
                    's' => new FormulaToken(Formula.S),
                    '(' => new AuxiliaryToken(Auxiliary.OpenParenthesis),
                    ')' => new AuxiliaryToken(Auxiliary.CloseParenthesis),
                    _ => throw new ArgumentOutOfRangeException(nameof(character), character, null)
            };
        }
    }
}