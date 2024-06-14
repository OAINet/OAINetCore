namespace OAINet.Node.Network;

public class Request
{
    public string? Uri { get; set; }
    public Dictionary<string, string> Parameters { get; set; } = new();
    public List<ObjectParameter> Objects { get; set; } = new();
}

public class ObjectParameter
{
    public string? Name { get; set; }
    public Dictionary<string, string> Properties { get; set; } = new();
    public List<ObjectParameter> NestedObjects { get; set; } = new();
}

public class RequestParser
{
    public static Request Parse(string requestData)
    {
        var request = new Request();
        using (var reader = new StringReader(requestData))
        {
            request.Uri = reader.ReadLine();

            string? line;
            ObjectParameter? currentObject = null;

            while ((line = reader.ReadLine()) is not null)
            {
                line = line.Trim();
                if (string.IsNullOrEmpty(line)) continue;

                if (line.Contains(':'))
                {
                    var parts = line.Split(new[] { ':' }, 2);
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    if (currentObject == null)
                    {
                        request.Parameters[key] = value;
                    }
                    else
                    {
                        currentObject.Properties[key] = value;
                    }
                }
                else if (line.EndsWith(";"))
                {
                    line = line.TrimEnd(';');
                    if (currentObject != null)
                    {
                        request.Objects.Add(currentObject);
                        currentObject = null;
                    }
                    else
                    {
                        currentObject = new ObjectParameter { Name = line };
                    }
                }
                else if (currentObject != null)
                {
                    var nestedObject = new ObjectParameter { Name = line };
                    currentObject.NestedObjects.Add(nestedObject);
                    currentObject = nestedObject;
                }
            }

            if (currentObject != null)
            {
                request.Objects.Add(currentObject);
            }
        }

        return request;
    }
}