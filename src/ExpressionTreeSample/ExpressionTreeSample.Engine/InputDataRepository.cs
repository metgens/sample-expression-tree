
namespace ExpressionTreeSample.Engine
{
    public interface IInputDataRepository
    {
        IEnumerable<InputData> GetData();
    }

    public class InputDataRepository : IInputDataRepository
    {
        public IEnumerable<InputData> GetData()
        {
            var data = new List<InputData>();
            for (int i = 0; i < 100; i++)
            {
                data.Add(new InputData
                {
                    A = i,
                    B = i * 10,
                    C = i * 100
                });
            }
            return data;
        }
    }
}