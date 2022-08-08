using ExpressionTreeSample.Engine;

namespace ExpressionTreeSample.WebApp.Controllers
{
    public class TransformRequest
    {
        public IList<InputData> Data { get; set; }
        public string Operation { get; set; }
    }
}