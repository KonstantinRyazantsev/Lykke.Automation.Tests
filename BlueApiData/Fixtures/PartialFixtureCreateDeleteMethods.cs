using System;
using System.Net;
using System.Threading.Tasks;
using BlueApiData.DTOs;
using RestSharp;
using XUnitTestCommon;
using XUnitTestCommon.Utils;
using XUnitTestData.Entities.BlueApi;
using XUnitTestData.Entities.ApiV2;
using XUnitTestCommon.DTOs;
using XUnitTestCommon.GlobalActions;

namespace BlueApiData.Fixtures
{
    public partial class BlueApiTestDataFixture
    {
        public async Task<PledgeDTO> CreateTestPledge(string consumerIndex = null)
        {
            var consumer = String.IsNullOrEmpty(consumerIndex) ? Consumer : PledgeApiConsumers[consumerIndex];

            var url = ApiPaths.PLEDGES_BASE_PATH;
            var newPledge = new CreatePledgeDTO
            {
                CO2Footprint = Helpers.Random.Next(100, 100000),
                ClimatePositiveValue = Helpers.Random.Next(100, 100000)
            };

            var createParam = JsonUtils.SerializeObject(newPledge);
            var response = await consumer.ExecuteRequest(url, Helpers.EmptyDictionary, createParam, Method.POST);
            if (response.Status != HttpStatusCode.Created)
            {
                return null;
            }

            var pledge = await PledgeRepository.TryGetAsync(p => p.PartitionKey == PledgeEntity.GeneratePartitionKey() && p.ClientId == consumer.ClientInfo.Account.Id);

            var returnDto = Mapper.Map<PledgeDTO>(pledge);

            this.AddCleanupAction(async () => await this.DeleteTestPledge(consumerIndex));

            return returnDto;
        }

        public async Task<bool> DeleteTestPledge(string consumerIndex = null)
        {
            var consumer = String.IsNullOrEmpty(consumerIndex) ? Consumer : PledgeApiConsumers[consumerIndex];

            var deletePledgeUrl = ApiPaths.PLEDGES_BASE_PATH;
            var deleteResponse = await consumer.ExecuteRequest(deletePledgeUrl, Helpers.EmptyDictionary, null, Method.DELETE);

            if (deleteResponse.Status != HttpStatusCode.NoContent)
            {
                return false;
            }

            return true;
        }

        public async Task CreateTestPartnerClient()
        {
            await Consumer.RegisterNewUser(
                new ClientRegisterDTO
                {
                    Email = Helpers.RandomString(8) + GlobalConstants.AutoTestEmail,
                    FullName = Helpers.RandomString(5) + " " + Helpers.RandomString(8),
                    ContactPhone = Helpers.Random.Next(1000000, 9999999).ToString(),
                    Password = Helpers.RandomString(10),
                    Hint = Helpers.RandomString(3),
                    PartnerId = _configBuilder.Config["LykkeBluePartnerId"] // "Lykke.blue"
                }
            );

            AddOneTimeCleanupAction(async () => await ClientAccounts.DeleteClientAccount(Consumer.ClientInfo.Account.Id));
        }
    }
}
