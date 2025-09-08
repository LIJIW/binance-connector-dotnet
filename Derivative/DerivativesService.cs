using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Common;

namespace Derivatives
{
    public class DerivativesService : BinanceService
    {
        protected const string DEFAULT_SPOT_BASE_URL = "https://fapi.binance.com";

        public DerivativesService(HttpClient httpClient, string apiKey, string apiSecret, string baseUrl = DEFAULT_SPOT_BASE_URL)
        : base(httpClient, baseUrl: baseUrl, apiKey: apiKey, apiSecret: apiSecret)
        {
        }

        public DerivativesService(HttpClient httpClient, string apiKey, IBinanceSignatureService signatureService, string baseUrl = DEFAULT_SPOT_BASE_URL)
        : base(httpClient, baseUrl: baseUrl, apiKey: apiKey, signatureService: signatureService)
        {
        }
    }
}
