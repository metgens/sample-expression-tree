# Overview
This is the sample usage of Expression Trees provided by .NET Framework. 

The app takes collection of sample `InputData` objects as the **first input parameter**.
The `InputData` has a structure as below:
```
    public class InputData
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }
```

 As the **second parameter** user should provide a binary operation declaration, which may/should use properties of `InputData`. For example:
 ```
A+B
C-A
A/B
A+10
 ```

At the end user gets collection of provided operation results for each `InputData` object from input collection.

# How to start the solution?

Type the following command to run solution locally:
```
cd src/ExpressionTreeSample/ExpressionTreeSample.WebApp
dotnet run
```

Type the following commands to run solution from docker image:
```
cd src/ExpressionTreeSample
docker build -f Dockerfile.WebApp -t expressiontreesample:latest .

docker run -p 5293:5293 expressiontreesample:latest -it bash
```

# Sample request

```
curl --location --request POST 'http://localhost:5293/expression/transform' \
--header 'Content-Type: application/json' \
--data-raw '{
    "data": [
        { "A": 1, "B": "10", "C": 100  },
        { "A": 2, "B": "20", "C": 200  }
    ],
    "operation": "B-5"
}'
```