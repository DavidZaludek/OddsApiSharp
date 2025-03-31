using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OddsApiSharp.ClientV2.Test
{
    public class Tests
    {
        private ApiConnection _apiClient;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<OddsApiSettings>()
                .Build();

            _apiClient = new ApiConnection(Options.Create<OddsApiSettings>(new OddsApiSettings()));
        }

        [Test]
        public void Test1()
        {

            var bookmakers = _apiClient.GetBookmakers();
            
            
            Assert.Pass();
        }
    }
}
