﻿using Lykke.Client.AutorestClient.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;

namespace AFTests.HftTests
{
    class HistoryTests
    {

        public class GetHistoryTrades : HftBaseTest
        {
            [Test]
            [Category("HFT")]
            public void GetHistoryTradesTest()
            {
                var assets = hft.AssetPairs.GetAssetPairs();

                string assetId = "AHAU100";
                var take = "10";
                var skip = "0";

                List<HistoryTradeModel> notNullHistoryResponse = null;
                assets.GetResponseObject().ForEach(pair =>
                {
                    if (hft.History.GetHistory(pair.Id, skip, take, ApiKey).GetResponseObject()?.Count > 0)
                    {
                        notNullHistoryResponse = hft.History.GetHistory(pair.Id, skip, take, ApiKey).GetResponseObject();
                        assetId = pair.Id;
                        return;
                    }
                });
                var response = hft.History.GetHistory(assetId, skip, take, ApiKey);
                response.Validate.StatusCode(HttpStatusCode.OK);
                //add trade and validate it prsenets here
            }
        }
    }
}
