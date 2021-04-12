using AutoFixture;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Properties.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Properties.Repository.Tests
{
    public class InteractionGetRepositoryTests
    {
        private readonly Fixture _fixture;
        private readonly IInteractionGetRepository _getRepository;

        public InteractionGetRepositoryTests()
        {
            _fixture = new Fixture();

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(GetSampleResponse()))
            };

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var mockHttpClient = new HttpClient(handlerMock.Object);


            _getRepository = new InteractionGetRepository(mockHttpClient);
        }

        [Fact]
        public void InteractionGetRepository_ShouldThrowExceptio_WhenHttpCallFail()
        {
            var getRepository = new InteractionGetRepository();
            var response = getRepository.GetResponse();
        }

        private PropertiesResponseDto GetSampleResponse()
        {
            return new PropertiesResponseDto
            {
                Properties = _fixture.Build<Property>().CreateMany(10).ToList()
            };
        }
    }
}
