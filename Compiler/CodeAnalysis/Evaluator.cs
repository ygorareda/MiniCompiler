namespace Compiler.CodeAnalysis
{
    //calculates a expression
    public class Evaluator
    {
        public ExpressionSyntax Root { get; }

        // constructor. Gets the tree that was built with the lexer and parser, and calculates the result.
        public Evaluator(ExpressionSyntax root)
        {
            Root = root;
        }

        //fuction to call the evaluate, sending the root of the tree
        public int Evaluate()
        {
            return EvaluateExpression(Root);
        }

        //calculate the value. Receive one node of the tree
        private int EvaluateExpression(ExpressionSyntax node)
        {
            //if the node is a number, return the number
            if (node is LiteralExpressionSyntax n)
            {
                return (int)n.LiteralToken._value;
            }

            //if the node is a unary (-1), return the unary value. -1 = -1 || +1 = 1
            if(node is UnaryExpressionSyntax u)
            {
                var operand = EvaluateExpression(u.Operand);

                if (u.OperatorToken.Kind == SyntaxKind.PlusToken)
                    return operand;
                else if (u.OperatorToken.Kind == SyntaxKind.MinusToken)
                    return -operand;
                else
                    throw new Exception($"Unexpected unary operator {u.OperatorToken.Kind}");
            }

            //if the node is a expression, calculates the result
            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                {
                    return left * right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
                {
                    return left / right;
                }
                else
                {
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }


            throw new Exception($"Unexpected node {node.Kind }");
        }
    }
    
}
