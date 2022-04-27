namespace Compiler.CodeAnalysis.Syntax
{
    //syntax of token
    public class SyntaxToken : SyntaxNode
    {
        public int _line { get; }
        public int _position { get; }
        public string _text { get; }
        public object _value { get; }

        public SyntaxToken(int line,SyntaxKind kind, int position, string text, object value)
        {
            _line = line;
            Kind = kind;
            _position = position;
            _text = text;
            _value = value;
        }
        public override SyntaxKind Kind { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }

    }
    
}
