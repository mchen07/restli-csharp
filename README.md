# Rest.li C#

## How to Build

- clone the repository locally and move into directory
    - `git clone https://github.com/mchen07/restli-csharp.git`
    - `cd restli-csharp`
- run the gradle script to generate data templates:
    - on Windows: `gradlew.bat clean build`
    - on Mac/Linux: `./gradlew clean build`
- open `restlicsharp.sln` in Visual Studio:
    - build the solution
    - run all tests for the solution to ensure everything is setup
     
## Example Data Templates

A good example of the different data template types can be found in the following places after you have generated the data templates:

1. Record:
     - `restlicsharpdata/restlidataintegration/generatedDataTemplate/com/linkedin/rest
li/datagenerator/integration/SimpleRecord.cs`
2. Enum:
     - `restlicsharpdata/restlidataintegration/generatedDataTemplate/com/linkedin/rest
li/datagenerator/integration/TestEnum.cs`
3. Union:
     - `restlicsharpdata/restlidataintegration/generatedDataTemplate/com/linkedin/rest
li/datagenerator/integration/UnionTest.cs`
     - this data template is a Record class with a few Union classes defined inside it
     
## Example Client Usage

### Synchronous Request

Here is a snippet showing how you would use the client to issue a synchronous GET request to a resource `resourceName` for a Record `Greeting` with integer ID `123`

```
string urlPrefix = "http://hostname:port";
RestClient client = new RestClient(urlPrefix);

GetRequestBuilder<int, Greeting> requestBuilder = new GetRequestBuilder<int, Greeting>("/resourceName");
requestBuilder.SetID(123);
GetRequest<int, Greeting> request = requestBuilder.Build();

EntityResponse<Greeting> response = client.RestRequestSync(request);

Greeting greeting = response.element;
```

### Asynchronous Request

Here is a snippet showing how you would use the client to issue the asynchronous analogue of the same request in the previous example

```
string urlPrefix = "http://hostname:port";
RestClient client = new RestClient(urlPrefix);

GetRequestBuilder<int, Greeting> requestBuilder = new GetRequestBuilder<int, Greeting>("/resourceName");
requestBuilder.SetID(123);
GetRequest<int, Greeting> request = requestBuilder.Build();

RestliCallback<EntityResponse<Greeting>>.SuccessHandler successHandler = delegate (EntityResponse<Greeting> response)
{
    Greeting greeting = response.element;
    // Do something with `greeting` here
};
RestliCallback<EntityResponse<Greeting>> callback = new RestliCallback<EntityResponse<Greeting>>(successHandler);

client.RestRequestAsync(request, callback);
```
