public interface IJsonConvertor
{
    Model DeserializeObject<Model>(string objec);
    string SerializeObject(object objec);
}