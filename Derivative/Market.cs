// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Common;

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

    }
}
