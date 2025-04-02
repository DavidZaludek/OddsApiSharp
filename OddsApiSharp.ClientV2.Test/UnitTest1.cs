using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OddsApiSharp.ClientV2.Test
{
    public class Tests
    {
        private OddsApiConnection _oddsApiClient;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<OddsApiSettings>()
                .Build();

            _oddsApiClient = new OddsApiConnection(Options.Create<OddsApiSettings>(new OddsApiSettings()));
        }

        [Test]
        public void Test1()
        {

            var bookmakers = _oddsApiClient.GetBookmakers();
            
            
            Assert.Pass();
        }
    }
}
