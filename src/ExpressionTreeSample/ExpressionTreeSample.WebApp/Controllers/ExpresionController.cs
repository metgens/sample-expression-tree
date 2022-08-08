using Microsoft.AspNetCore.Mvc;

namespace ExpressionTreeSample.WebApp.Controllers;
using ExpressionTreeSample.Engine;

[ApiController]
[Route("[controller]")]
public class ExpressionController : ControllerBase
{

    private readonly ILogger<ExpressionController> _logger;

    public ExpressionController(ILogger<ExpressionController> logger)
    {
        _logger = logger;
    }

    [HttpPost("transform")]
    public IEnumerable<int> Transform([FromBody] TransformRequest request)
    {
        var result = request.Data.Select(x => ExpressionTreeHelper.CallAnyMathOperation<InputData>(request.Operation)(x)).ToList();
        return result;
    }
}
