namespace Compiler.CodeAnalysis
{
    //syntax of "expression" that will be calculated
    public class BinaryExpressionSyntax : ExpressionSyntax
    {
        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Right { get; }
        public override SyntaxKind Kind => SyntaxKind.binaryExpression;


        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }


        //get the childrens of the expression. Remembering the the struct to parse is a tree
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}

