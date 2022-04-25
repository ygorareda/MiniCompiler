namespace Compiler.CodeAnalysis.Syntax
{
    //syntax of the tree. indexing the root of the tree
    public class SyntaxTree
    {
        public IEnumerable<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }

        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

    }
    
}
