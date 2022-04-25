namespace Compiler.CodeAnalysis
{
    //node of the tree, probabily the operand
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
