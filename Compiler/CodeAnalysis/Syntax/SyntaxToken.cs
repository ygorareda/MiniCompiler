namespace Compiler.CodeAnalysis.Syntax
{
    //syntax of token
    public class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }
        public int _position { get; }
        public string _text { get; }
        public object _value { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            _position = position;
            _text = text;
            _value = value;
        }

    }
    
}
