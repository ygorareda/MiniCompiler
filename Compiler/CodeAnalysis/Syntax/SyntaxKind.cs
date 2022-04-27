namespace Compiler.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        //tokens
        BadToken,
        EndOfFileToken,
        WhiteSpace,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,

        //Expressions
        LiteralExpression,
        BinaryExpression,
        UnaryExpression,
        NameExpression,
        AssignmentExpression,
        EqualsToken,
        EqualsEqualsToken,
        IdentifierToken,

        //commands
        LineToken,
        RemToken,
        InputToken,
        LetToken,
        PrintToken,
        GotoToken,
        IfToken,
        EndToken
    }
}
