using System;
using System.Collections.Generic;
using LogicTower.ExpressionParsing;

namespace LogicTower.ExpressionParsing
{
    public class ExpressionParser
    {
        private readonly IExpressionNode _root;
        
        public ExpressionParser(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                _root = new FormulaNode(new FormulaToken(Formula.P));
                return;
            }
            
            List<Token> tokens = ExpressionLexer.Tokenize(expression);
            _root = BuildExpressionTree(tokens);
        }

        public bool Solve(Dictionary<Formula, bool> formulaValues)
        {
            return _root.Solve(formulaValues);
        }

        private IExpressionNode BuildExpressionTree(List<Token> tokens)
        {
            RemoveParenthesisAround(tokens);

            if (tokens.Count == 1 && tokens[0].TokenType == TokenType.Formula)
                return new FormulaNode(tokens[0] as FormulaToken);

            int lessPriorityOperatorIndex = GetOperatorWithLastOrder(tokens);

            return tokens[lessPriorityOperatorIndex].TokenType switch
            {
                TokenType.BinaryOperator => CreateBinaryOperatorNode(tokens, lessPriorityOperatorIndex),
                TokenType.UnaryOperator => CreateUnaryOperatorNode(tokens, lessPriorityOperatorIndex),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IExpressionNode CreateUnaryOperatorNode(List<Token> tokens, int operatorIndex)
        {
            IExpressionNode node = BuildExpressionTree(tokens.GetRange(operatorIndex + 1, tokens.Count - operatorIndex - 1));
            return new UnaryOperationNode(tokens[operatorIndex] as UnaryOperatorToken, node);
        }

        private IExpressionNode CreateBinaryOperatorNode(List<Token> tokens, int operatorIndex)
        {
            IExpressionNode leftNode = BuildExpressionTree(tokens.GetRange(0, operatorIndex));
            IExpressionNode rightNode = BuildExpressionTree(tokens.GetRange(operatorIndex + 1, tokens.Count - operatorIndex - 1));

            return new BinaryOperationNode(tokens[operatorIndex] as BinaryOperatorToken, leftNode, rightNode);
        }

        private int GetOperatorWithLastOrder(IReadOnlyList<Token> tokens)
        {
            (int Index, int Order) lastOrderOperator = (-1, -1);
            int openedParenthesis = 0;
            
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].IsType(TokenType.Auxiliary, out AuxiliaryToken auxiliaryToken))
                {
                    if (auxiliaryToken.Auxiliary == Auxiliary.OpenParenthesis)
                        openedParenthesis++;
                    else if (auxiliaryToken.Auxiliary == Auxiliary.CloseParenthesis)
                        openedParenthesis--;
                }
                else if (openedParenthesis == 0 && (tokens[i].TokenType == TokenType.BinaryOperator || tokens[i].TokenType == TokenType.UnaryOperator))
                {
                    int order = GetOperatorOrder(tokens[i]);
                    
                    if (order > lastOrderOperator.Order)
                        lastOrderOperator = (i, order);
                }
            }

            return lastOrderOperator.Index;
        }

        private int GetOperatorOrder(Token token)
        {
            if (token.IsType(TokenType.BinaryOperator, out BinaryOperatorToken binaryOperatorToken))
            {
                return binaryOperatorToken.Operator switch
                {
                    BinaryOperator.AND => 1,
                    BinaryOperator.OR => 2,
                    BinaryOperator.Conditional => 3,
                    BinaryOperator.Biconditional => 4,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            if (token.IsType(TokenType.UnaryOperator, out UnaryOperatorToken unaryOperatorToken))
            {
                return unaryOperatorToken.Operator switch
                {
                    UnaryOperator.NOT => 0,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return -1;
        }

        private static void RemoveParenthesisAround(List<Token> tokens)
        {
            if (HasParenthesisAround(tokens))
            {
                int parenthesisCount = 0;

                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].TokenType == TokenType.Auxiliary && tokens[i] is AuxiliaryToken auxiliaryToken)
                    {
                        if (auxiliaryToken.Auxiliary == Auxiliary.OpenParenthesis)
                            parenthesisCount++;
                        else if (auxiliaryToken.Auxiliary == Auxiliary.CloseParenthesis)
                            parenthesisCount--;
                    }

                    if (parenthesisCount == 0)
                    {
                        if (i == tokens.Count - 1)
                        {
                            tokens.RemoveAt(0);
                            tokens.RemoveAt(tokens.Count - 1);
                         
                            RemoveParenthesisAround(tokens);
                        }
                        
                        break;
                    }
                }
            }
        }

        private static bool HasParenthesisAround(List<Token> tokens)
        {
            return tokens[0].TokenType == TokenType.Auxiliary && tokens[0] is AuxiliaryToken initialToken && tokens[^1].TokenType == TokenType.Auxiliary && tokens[^1] is AuxiliaryToken finalToken && initialToken.Auxiliary == Auxiliary.OpenParenthesis && finalToken.Auxiliary == Auxiliary.CloseParenthesis;
        }

        private class UnaryOperationNode : IExpressionNode
        {
            private readonly UnaryOperatorToken _operatorToken;
            private readonly IExpressionNode _node;

            public UnaryOperationNode(UnaryOperatorToken operatorToken, IExpressionNode node)
            {
                _operatorToken = operatorToken;
                _node = node;
            }
            
            public bool Solve(Dictionary<Formula, bool> formulaValues)
            {
                return _operatorToken.Operator switch
                {
                    UnaryOperator.NOT => !_node.Solve(formulaValues),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private class BinaryOperationNode : IExpressionNode
        {
            private readonly BinaryOperatorToken _operatorToken;
            private readonly IExpressionNode _leftNode;
            private readonly IExpressionNode _rightNode;

            public BinaryOperationNode(BinaryOperatorToken operatorToken, IExpressionNode leftNode, IExpressionNode rightNode)
            {
                _operatorToken = operatorToken;
                _leftNode = leftNode;
                _rightNode = rightNode;
            }

            public bool Solve(Dictionary<Formula, bool> formulaValues)
            {
                bool leftNodeValue = _leftNode.Solve(formulaValues);
                bool rightNodeValue = _rightNode.Solve(formulaValues);

                return _operatorToken.Operator switch
                {
                    BinaryOperator.AND => leftNodeValue && rightNodeValue,
                    BinaryOperator.OR => leftNodeValue || rightNodeValue,
                    BinaryOperator.Conditional => !leftNodeValue || rightNodeValue,
                    BinaryOperator.Biconditional => leftNodeValue == rightNodeValue,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private class FormulaNode : IExpressionNode
        {
            private readonly FormulaToken _formulaToken;

            public FormulaNode(FormulaToken formulaToken)
            {
                _formulaToken = formulaToken;
            }
            
            public bool Solve(Dictionary<Formula, bool> formulaValues)
            {
                return formulaValues[_formulaToken.Formula];
            }
        }

        private interface IExpressionNode
        {
            bool Solve(Dictionary<Formula, bool> formulaValues);
        }
    }
}