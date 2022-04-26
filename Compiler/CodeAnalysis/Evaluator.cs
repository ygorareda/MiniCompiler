using Compiler.CodeAnalysis.Binding;
using Compiler.CodeAnalysis.Syntax;

namespace Compiler.CodeAnalysis
{
    //calculates a expression
    internal sealed class Evaluator
    {
        private readonly BoundExpression _root;
        private readonly Dictionary<string, object> _variables;

        // constructor. Gets the tree that was built with the lexer and parser, and calculates the result.
        public Evaluator(BoundExpression root, Dictionary<string, object> variables)
        {
            _root = root;
            _variables = variables;
        }


        //fuction to call the evaluate, sending the root of the tree
        public object Evaluate()
        {
            return EvaluateExpression(_root);
        }

        //calculate the value. Receive one node of the tree
        private object EvaluateExpression(BoundExpression node)
        {
            //if the node is a number, return the number
            if (node is BoundLiteralExpression n)
            {
                return (int)n.Value;
            }

            if(node is BoundVariablesExpression v)
                return _variables[v.Name];

            if (node is BoundAssignmentExpression a)
            {
                var value = EvaluateExpression(a.Expression);
                _variables[a.Name] = value;
                return value;
            }

            //if the node is a unary (-1), return the unary value. -1 = -1 || +1 = 1
            if(node is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);

                switch (u.OperatorKind)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return (int)operand;
                    case BoundUnaryOperatorKind.Negation:
                        return -(int)operand;
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
                        return (int)left + (int)right;
                    case BoundBinaryOperatorKind.Subtraction:
                        return (int)left - (int)right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return (int)left * (int)right;
                    case BoundBinaryOperatorKind.Division:
                        return (int)left / (int)right;
                    default:
                        throw new Exception($"Unexpected binary operator {b.OperatorKind}");
                }
            }


            throw new Exception($"Unexpected node {node.Kind }");
        }
    }
    
}
