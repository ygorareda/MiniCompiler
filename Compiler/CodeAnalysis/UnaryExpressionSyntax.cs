namespace Compiler.CodeAnalysis
{
    //syntax of a unary (-1) expression
    public class UnaryExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Operand { get; }
        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;


        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }


        //todo bem aki
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;
        }
    }
}

