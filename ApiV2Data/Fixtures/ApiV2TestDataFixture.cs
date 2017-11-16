﻿using ApiV2Data.DependencyInjection;
using Autofac;
using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using XUnitTestCommon;
using XUnitTestCommon.Consumers;
using XUnitTestCommon.Utils;
using XUnitTestData.Domains.ApiV2;
using XUnitTestData.Repositories.ApiV2;
using XUnitTestData.Services;
using System.Threading.Tasks;
using XUnitTestData.Domains;
using ApiV2Data.DTOs;
using RestSharp;
using System.Net;
using XUnitTestData.Repositories;
using AssetsData.DTOs.Assets;

namespace ApiV2Data.Fixtures
{
    public partial class ApiV2TestDataFixture : IDisposable
    {
        private ConfigBuilder _configBuilder;
        private IContainer container;

        private List<string> WalletsToDelete;

        public string TestClientId;

        public WalletRepository WalletRepository;
        public List<WalletEntity> AllWalletsFromDB;
        public WalletEntity TestWallet;
        public WalletDTO TestWalletDelete;
        public AccountEntity TestWalletAccount;
        public string TestWalletAssetId;

        public IDictionaryManager<IAccount> AccountManager;

        public Dictionary<string, string> ApiEndpointNames;
        public ApiConsumer Consumer;

        public ApiV2TestDataFixture()
        {
            this._configBuilder = new ConfigBuilder("ApiV2");
            this.Consumer = new ApiConsumer(_configBuilder.Config["UrlPefix"], _configBuilder.Config["BaseUrl"], Boolean.Parse(_configBuilder.Config["IsHttps"]));
            this.Consumer.Authenticate(_configBuilder.Config["BaseUrlAuth"], _configBuilder.Config["AuthPath"], _configBuilder.Config["AuthEmail"],
                _configBuilder.Config["AuthPassword"], _configBuilder.Config["AuthClientInfo"], _configBuilder.Config["AuthPartnerId"], Int32.Parse(_configBuilder.Config["AuthTokenTimeout"]));

            prepareDependencyContainer();
            prepareTestData().Wait();
        }

        private void prepareDependencyContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiV2TestModule(_configBuilder));
            this.container = builder.Build();

            this.WalletRepository = (WalletRepository)this.container.Resolve<IDictionaryRepository<IWallet>>();
            this.AccountManager = RepositoryUtils.PrepareRepositoryManager<IAccount>(this.container);
        }

        private async Task prepareTestData()
        {
            ApiEndpointNames = new Dictionary<string, string>();
            ApiEndpointNames["Wallets"] = "/api/wallets";
            ApiEndpointNames["Assets"] = "/api/assets";
            ApiEndpointNames["AssetsBaseAsset"] = ApiEndpointNames["Assets"] + "/baseAsset";            

            WalletsToDelete = new List<string>();

            TestClientId = this._configBuilder.Config["AuthClientId"];
            var walletsFromDB = this.WalletRepository.GetAllAsync(TestClientId);

            this.AllWalletsFromDB = (await walletsFromDB).Cast<WalletEntity>().ToList();
            this.TestWallet = AllWalletsFromDB.Where(w => w.Id == "fd0f7373-301e-42c0-83a2-1d7b691676c3").FirstOrDefault(); //TODO hardcoded
            this.TestWalletDelete = await CreateTestWallet();
            this.TestWalletAccount = await AccountManager.TryGetAsync(TestWallet.Id) as AccountEntity;
            this.TestWalletAssetId = "LKK";

            // set the id to the default one in case it has been changed by any test
            BaseAssetDTO body = new BaseAssetDTO(this.TestWalletAssetId);
            await Consumer.ExecuteRequest(ApiEndpointNames["AssetsBaseAsset"], Helpers.EmptyDictionary, JsonUtils.SerializeObject(body), Method.POST);
        }

        public void Dispose()
        {
            List<Task<bool>> deleteTasks = new List<Task<bool>>();
            
            foreach (string walletId in WalletsToDelete) { deleteTasks.Add(DeleteTestWallet(walletId)); }

            Task.WhenAll(deleteTasks).Wait();
        }
    }
}