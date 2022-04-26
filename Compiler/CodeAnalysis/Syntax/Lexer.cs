namespace Compiler.CodeAnalysis.Syntax
{
    public class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => _diagnostics;

        //constructor of lexer. Receive the text input
        public Lexer(string text)
        {
            _text = text;
        }

        //gets current value in the input.
        private char _current => Peek(0);

        private char _lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;

            if (index >= _text.Length)
                return '\0';

            return _text[index];
        }


        //go to next position in the input
        private void Next()
        {
            _position++;
        }

        // make the lexer, identifying the value and grabing the specific token of the value.
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

            if (char.IsLetter(_current))
            {
                var start = _position;
                while (char.IsLetter(_current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.IdentifierToken, start, text, null);
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
                case '=':
                    if (_lookahead == '=')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.EqualsEqualsToken, _position, "==", null);
                    }
                    else
                    {
                        _position += 1;
                        return new SyntaxToken(SyntaxKind.EqualsToken, _position, "=", null);
                    }
            }

            _diagnostics.Add($"ERROR: bad character input: '{_current}'");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);

        }

    }
    
}
