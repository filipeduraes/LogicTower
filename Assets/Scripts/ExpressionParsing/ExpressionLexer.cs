using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicTower.ExpressionParsing
{
    public static class ExpressionLexer
    {
        public static List<Token> Tokenize(string expression)
        {
            expression = expression.ToLowerInvariant();
            char[] singleCharacterTokens = { '~', '^', 'v', 'p', 'q', 'r', 's', '(', ')' };
            List<Token> tokens = new();
            
            for (int i = 0; i < expression.Length; i++)
            {
                if (!char.IsSeparator(expression[i]))
                {
                    if(TryGetMultiCharacterToken(expression, i, out i, out Token multiCharacterToken))
                        tokens.Add(multiCharacterToken);
                    else if (singleCharacterTokens.Contains(expression[i]))
                        tokens.Add(GetSingleCharacterToken(expression[i]));
                }
            }

            return tokens;
        }

        private static bool TryGetMultiCharacterToken(string expression, int initialIndex, out int finalIndex, out Token token)
        {
            string[] multiCharacterTokens = { "->", "<->", "p1", "p2", "p3" };
            bool[] foundErrorInMatch = new bool[5];

            for (int i = initialIndex; i < expression.Length; i++)
            {
                for (int j = 0; j < multiCharacterTokens.Length; j++)
                {
                    if (!foundErrorInMatch[j])
                    {
                        int characterLocalIndex = i - initialIndex;
                        
                        if (characterLocalIndex >= multiCharacterTokens[j].Length)
                            break;
                        
                        if (expression[i] != multiCharacterTokens[j][characterLocalIndex])
                            foundErrorInMatch[j] = true;
                    }
                }
            }

            for (int i = 0; i < foundErrorInMatch.Length; i++)
            {
                if (!foundErrorInMatch[i])
                {
                    token = GetTokenFromString(multiCharacterTokens[i]);
                    finalIndex = initialIndex + multiCharacterTokens[i].Length - 1;
                    return true;
                }
            }

            token = null;
            finalIndex = initialIndex;
            return false;
        }

        private static Token GetTokenFromString(string multiCharacterToken)
        {
            return multiCharacterToken switch
            {
                "->" => new BinaryOperatorToken(BinaryOperator.Conditional), 
                "<->" => new BinaryOperatorToken(BinaryOperator.Biconditional), 
                "p1" => new FormulaToken(Formula.P1), 
                "p2" => new FormulaToken(Formula.P2), 
                "p3" => new FormulaToken(Formula.P3),
                _ => throw new ArgumentOutOfRangeException(nameof(multiCharacterToken), multiCharacterToken, null)
            };
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