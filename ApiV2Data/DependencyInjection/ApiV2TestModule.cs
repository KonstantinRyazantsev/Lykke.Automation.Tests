﻿using Autofac;
using AzureStorage.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using XUnitTestCommon;
using XUnitTestCommon.Utils;
using XUnitTestData.Domains;
using XUnitTestData.Domains.ApiV2;
using XUnitTestData.Repositories;
using XUnitTestData.Repositories.ApiV2;

namespace ApiV2Data.DependencyInjection
{
    public class ApiV2TestModule : Module
    {
        private ConfigBuilder _configBuilder;

        public ApiV2TestModule(ConfigBuilder configBuilder)
        {
            this._configBuilder = configBuilder;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new WalletRepository(
                    new AzureTableStorage<WalletEntity>(
                        _configBuilder.Config["MainConnectionString"], "Wallets", null)))
                .As<IDictionaryRepository<IWallet>>();

            builder.Register(c => new AccountRepository(
                    new AzureTableStorage<AccountEntity>(
                        _configBuilder.Config["MainConnectionString"], "Accounts", null)))
                .As<IDictionaryRepository<IAccount>>();

            RepositoryUtils.RegisterDictionaryManager<IAccount>(builder);
        }
    }
}