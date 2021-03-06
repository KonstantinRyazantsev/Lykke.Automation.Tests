﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using XUnitTestData.Domains.Assets;

namespace XUnitTestData.Entities.Assets
{
    public class AssetEntity : TableEntity, IAsset
    {
        public static string GeneratePartitionKey()
        {
            return "Asset";
        }

        public static string GenerateRowKey(string id)
        {
            return id;
        }

        public string Id => RowKey;
        public string BlockChainId { get; set; }
        public string BlockChainAssetId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string IdIssuer { get; set; }
        public bool IsBase { get; set; }
        public bool HideIfZero { get; set; }
        public int Accuracy { get; set; }
        public int MultiplierPower { get; set; }
        public bool IsDisabled { get; set; }
        public bool HideWithdraw { get; set; }
        public bool HideDeposit { get; set; }
        public int DefaultOrder { get; set; }
        public bool KycNeeded { get; set; }
        public string AssetAddress { get; set; }
        public bool BankCardsDepositEnabled { get; set; }
        public bool SwiftDepositEnabled { get; set; }
        public bool BlockchainDepositEnabled { get; set; }
        public bool BuyScreen { get; set; }
        public bool SellScreen { get; set; }
        public bool BlockchainWithdrawal { get; set; }
        public bool SwiftWithdrawal { get; set; }
        public double DustLimit { get; set; }
        public string CategoryId { get; set; }
        public string Blockchain { get; set; }
        public string DefinitionUrl { get; set; }
        public bool IssueAllowed { get; set; }
        public double? LowVolumeAmount { get; set; }
        public bool ForwardWithdrawal { get; set; }
        public int ForwardFrozenDays { get; set; }
        public string ForwardBaseAsset { get; set; }
        public string ForwardMemoUrl { get; set; }
        public string DisplayId { get; set; }

        public bool IsTradable { get; set; }
        public bool IsTrusted { get; set; }
        public string Type { get; set; }

        public bool CrosschainWithdrawal { get; set; }
        public string IconUrl { get; set; }

        public string[] PartnerIds
        {
            get => PartnersIdsJson?.DeserializeJson<string[]>();
            set => PartnersIdsJson = value?.ToJson();
        }

        public string PartnersIdsJson { get; set; }

        public bool NotLykkeAsset { get; set; }



    }
}
