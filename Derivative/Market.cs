// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Common;
using Binance.Derivatives.Models;

namespace Derivatives
{
    public class Market : DerivativesService
    {
        public Market(string baseUrl = DEFAULT_SPOT_BASE_URL, string apiKey = null, string apiSecret = null)
        : this(new HttpClient(), baseUrl: baseUrl, apiKey: apiKey, apiSecret: apiSecret)
        {
        }

        public Market(HttpClient httpClient, string baseUrl = DEFAULT_SPOT_BASE_URL, string apiKey = null, string apiSecret = null)
        : base(httpClient, baseUrl: baseUrl, apiKey: apiKey, apiSecret: apiSecret)
        {
        }

        public Market(HttpClient httpClient, IBinanceSignatureService signatureService, string baseUrl = DEFAULT_SPOT_BASE_URL, string apiKey = null)
        : base(httpClient, baseUrl: baseUrl, apiKey: apiKey, signatureService: signatureService)
        {
        }

        private const string TEST_CONNECTIVITY = "/fapi/v1/ping";

        /// <summary>
        /// Test connectivity to the Rest API.<para />
        /// Weight(IP): 1.
        /// </summary>
        /// <returns>OK.</returns>
        public async Task<string> TestConnectivity()
        {
            var result = await this.SendPublicAsync<string>(
                TEST_CONNECTIVITY,
                HttpMethod.Get);

            return result;
        }

        private const string CHECK_SERVER_TIME = "/fapi/v1/time";

        /// <summary>
        /// Test connectivity to the Rest API and get the current server time.<para />
        /// Weight(IP): 1.
        /// </summary>
        /// <returns>Binance server UTC timestamp.</returns>
        public async Task<string> CheckServerTime()
        {
            var result = await this.SendPublicAsync<string>(
                CHECK_SERVER_TIME,
                HttpMethod.Get);

            return result;
        }

        private const string EXCHANGE_INFORMATION = "/fapi/v1/exchangeInfo";

        /// <summary>
        /// Current exchange trading rules and symbol information.<para />
        /// - If any symbol provided in either symbol or symbols do not exist, the endpoint will throw an error.<para />
        /// Weight(IP): 10.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="symbols"></param>
        /// <returns>Current exchange trading rules and symbol information.</returns>
        public async Task<string> ExchangeInformation()
        {
            var result = await this.SendPublicAsync<string>(
                EXCHANGE_INFORMATION,
                HttpMethod.Get);

            return result;
        }

        private const string ORDER_BOOK = "/fapi/v1/depth";

        /// <summary>
        /// | Limit               | Weight(IP)  |.<para />
        /// |---------------------|-------------|.<para />
        /// | 5, 10, 20, 50       | 1           |.<para />
        /// | 100                 | 5           |.<para />
        /// | 500                 | 10          |.<para />
        /// | 1000                | 50          |.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="limit">If limit > 1000, then the response will not successfully be handled and throw BinanceHttpException</param>
        /// <returns>Order book.</returns>
        public async Task<string> OrderBook(string symbol, int? limit = null)
        {
            var result = await this.SendPublicAsync<string>(
                ORDER_BOOK,
                HttpMethod.Get,
                query: new Dictionary<string, object>
                {
                    { "symbol", symbol },
                    { "limit", limit },
                });

            return result;
        }


        private const string KLINE_CANDLESTICK_DATA = "/fapi/v1/klines";

        /// <summary>
        /// Kline/candlestick bars for a symbol.<para />
        /// Klines are uniquely identified by their open time.<para />
        /// - If `startTime` and `endTime` are not sent, the most recent klines are returned.<para />
        /// Weight(IP): 1.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="interval">kline intervals.</param>
        /// <param name="startTime">UTC timestamp in ms.</param>
        /// <param name="endTime">UTC timestamp in ms.</param>
        /// <param name="limit">Default 500; max 1500.</param>
        /// <returns>Kline data.</returns>
        public async Task<string> KlineCandlestickData(string symbol, Interval interval, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var result = await this.SendPublicAsync<string>(
                KLINE_CANDLESTICK_DATA,
                HttpMethod.Get,
                query: new Dictionary<string, object>
                {
                    { "symbol", symbol },
                    { "interval", interval },
                    { "startTime", startTime },
                    { "endTime", endTime },
                    { "limit", limit },
                });

            return result;
        }
    }
}
