namespace LogicTower.ExpressionParsing
{
    public class FormulaToken : Token
    {
        public readonly Formula Formula;
        
        public FormulaToken(Formula formula) : base(TokenType.Formula)
        {
            Formula = formula;
        }
    }

    public class BinaryOperatorToken : Token
    {
        public readonly BinaryOperator Operator;

        public BinaryOperatorToken(BinaryOperator binaryOperator) : base(TokenType.BinaryOperator)
        {
            Operator = binaryOperator;
        }
    }
    
    public class UnaryOperatorToken : Token
    {
        public readonly UnaryOperator Operator;

        public UnaryOperatorToken(UnaryOperator unaryOperator) : base(TokenType.UnaryOperator)
        {
            Operator = unaryOperator;
        }
    }
    
    public class AuxiliaryToken : Token
    {
        public readonly Auxiliary Auxiliary;

        public AuxiliaryToken(Auxiliary auxiliary) : base(TokenType.Auxiliary)
        {
            Auxiliary = auxiliary;
        }
    }
    
    public class Token
    {
        public readonly TokenType TokenType;

        protected Token(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public bool IsType<T>(TokenType type, out T convertedToken) where T : Token
        {
            convertedToken = null;
            
            if (type == TokenType && this is T token)
            {
                convertedToken = token;
                return true;
            }

            return false;
        }
    }
    
    public enum TokenType
    {
        Formula, BinaryOperator, UnaryOperator, Auxiliary
    }
    
    public enum Formula
    {
        P, Q, R, S, P1, P2, P3
    }

    public enum BinaryOperator
    {
        AND, OR, Conditional, Biconditional
    }

    public enum Auxiliary
    {
        OpenParenthesis, CloseParenthesis
    }
    
    public enum UnaryOperator
    {
        NOT
    }
}