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

        private static bool TryGetMultiCharacterToken(ReadOnlySpan<char> expression, int initialIndex, out int finalIndex, out Token token)
        {
            string[] multiCharacterTokens = { "->", "<->", "p1", "p2", "p3" };
            finalIndex = initialIndex;
            token = null;

            for (int i = initialIndex; i < expression.Length; i++)
            {
                foreach (string multiToken in multiCharacterTokens)
                {
                    if (expression.Slice(initialIndex, i - initialIndex + 1).SequenceEqual(multiToken.AsSpan()))
                    {
                        finalIndex = i - 1;
                        token = GetTokenFromString(multiToken);
                        return true;
                    }
                }
            }

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