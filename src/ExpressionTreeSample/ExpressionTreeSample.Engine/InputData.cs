
namespace ExpressionTreeSample.Engine
{
    public class InputData
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public override string ToString()
        {
            return $"A: {A}; B: {B}; C: {C}";
        }
    }
}