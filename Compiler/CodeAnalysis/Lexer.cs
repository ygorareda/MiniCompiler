namespace Compiler.CodeAnalysis
{
    public class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => _diagnostics;

        public Lexer(string text)
        {
            _text = text;
        }

        private char _current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()
        {
            //recognize if is the end of file
            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            //recognize numbers
            if (char.IsDigit(_current))
            {
                var start = _position;

                while (char.IsDigit(_current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _diagnostics.Add($"The number {_text} isn't valid int.");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            //recognize white space
            if (char.IsWhiteSpace(_current))
            {
                var start = _position;

                while (char.IsWhiteSpace(_current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpace, start, text, null);
            }

            //recognize expression
            switch (_current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
            }

            _diagnostics.Add($"ERROR: bad character input: '{_current}'");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);

        }

    }
    
}
