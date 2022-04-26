namespace Compiler.CodeAnalysis.Syntax
{
    public class Parser
    {
        private SyntaxToken[] _tokens;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => _diagnostics;

        //Constructor of parser. Build the list of tokens, using the text received, calling lexer to identify the values. Ignores whiteSpaces and badtokens. when reaching the end of file, ends.
        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.Lex();

                if (token.Kind != SyntaxKind.WhiteSpace && token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }


            } while (token.Kind != SyntaxKind.EndOfFileToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        //get the token in position in the list of tokens (_tokens)
        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        //return the current token that will be parsed
        private SyntaxToken Current => Peek(0);

        //return current token and set position for the next token
        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        //check if the token is what i want, the default is number token, if is call nextToken to get the value and go to next 
        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
            {
                return NextToken();
            }

            _diagnostics.Add($"ERROR: unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current._position, null, null);
        }

        //"main" function, parse the tokens. In end of the expression, verify if us the end of file with the endOfFilToken. Return the tree, that contains the result
        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);

            return new SyntaxTree(_diagnostics, expression, endOfFileToken);
        }
        private ExpressionSyntax ParseExpression()
        {
            return ParseAssignmentExpression();
        }


        private ExpressionSyntax ParseAssignmentExpression()
        {
            if(Peek(0).Kind == SyntaxKind.IdentifierToken && Peek(1).Kind == SyntaxKind.EqualsToken)
            {
                var identifierToken = NextToken();
                var operatorToken = NextToken();
                var right = ParseAssignmentExpression();
                return new AssignmentExpressionSyntax(identifierToken, operatorToken, right);
            }

            return ParseBinaryExpression();
        }


        // basically get the precedent of the tokens, and built the tree 
        private ExpressionSyntax ParseBinaryExpression(int parentPrecedence = 0)
        {
            ExpressionSyntax left;

            var precedenceSyntax = new SyntaxPrecedence();

            var unaryOperatorPrecedence = precedenceSyntax.GetUnaryOperatorPrecedence(Current.Kind);
            if(unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
            {
                var operatorToken = NextToken();
                var operand = ParseBinaryExpression(unaryOperatorPrecedence);
                left = new UnaryExpressionSyntax(operatorToken, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
            }

            while (true)
            {
                var precedence = precedenceSyntax.GetBinaryOperatorPrecedence(Current.Kind);
                if(precedence == 0 || precedence <= parentPrecedence)
                    break;

                var operatorToken = NextToken();
                var right = ParseBinaryExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);

            }
            return left;
        }

        

        //parse the primary token, the default value expected is number token
        private ExpressionSyntax ParsePrimaryExpression()
        {
            switch (Current.Kind)
            { 
                case SyntaxKind.IdentifierToken:
                    {
                        var identifierToken = NextToken();
                        return new NameExpressionSyntax(identifierToken);
                    }

                default:
                    {
                        var numberToken = MatchToken(SyntaxKind.NumberToken);
                        return new LiteralExpressionSyntax(numberToken);
                    }
            }
        }
    }
    
}
