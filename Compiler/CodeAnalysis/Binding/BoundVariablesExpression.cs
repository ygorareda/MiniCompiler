namespace Compiler.CodeAnalysis.Binding
{
    internal sealed class BoundVariablesExpression : BoundExpression
    {
        public BoundVariablesExpression(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public override Type Type { get; }

        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
    }


}
