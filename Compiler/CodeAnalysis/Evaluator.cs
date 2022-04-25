using Compiler.CodeAnalysis.Binding;
using Compiler.CodeAnalysis.Syntax;

namespace Compiler.CodeAnalysis
{
    //calculates a expression
    internal sealed class Evaluator
    {
        private readonly BoundExpression Root;

        // constructor. Gets the tree that was built with the lexer and parser, and calculates the result.
        public Evaluator(BoundExpression root)
        {
            Root = root;
        }

        //fuction to call the evaluate, sending the root of the tree
        public int Evaluate()
        {
            return EvaluateExpression(Root);
        }

        //calculate the value. Receive one node of the tree
        private int EvaluateExpression(BoundExpression node)
        {
            //if the node is a number, return the number
            if (node is BoundLiteralExpression n)
            {
                return (int)n.Value;
            }

            //if the node is a unary (-1), return the unary value. -1 = -1 || +1 = 1
            if(node is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);

                switch (u.OperatorKind)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return operand;
                    case BoundUnaryOperatorKind.Negation:
                        return -operand;
                    default:
                        throw new Exception($"Unexpected unary operator {u.OperatorKind}");
                }
            }

            //if the node is a expression, calculates the result
            if (node is BoundBinaryExpression b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                switch (b.OperatorKind)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return left + right;
                    case BoundBinaryOperatorKind.Subtraction:
                        return left - right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return left * right;
                    case BoundBinaryOperatorKind.Division:
                        return left / right;
                    default:
                        throw new Exception($"Unexpected binary operator {b.OperatorKind}");
                }
            }


            throw new Exception($"Unexpected node {node.Kind }");
        }
    }
    
}
