namespace CodePlus.ApiGateway.Data.Https
{
    public class HttpResponseResult<T>
    {
        public T Result { get; set; }

        public string HttpResponseMessages { get; set; }
    }
}
