using Newtonsoft.Json;
using UnityEngine;

public class JsonConvertor : IJsonConvertor
{
    public Model DeserializeObject<Model>(string objec)
    {
        Model model = JsonConvert.DeserializeObject<Model>(objec);
        return model;
    }

    public string SerializeObject(object objec)
    {
        string unParse = JsonConvert.SerializeObject(objec);
        Debug.Log("" + unParse);
        return unParse;
    }
}