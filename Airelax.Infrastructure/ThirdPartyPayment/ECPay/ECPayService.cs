using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Airelax.Infrastructure.Helpers;
using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions;
using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions.Options;
using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request;
using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Response;
using Lazcat.Infrastructure.Common;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay
{
    public interface IECPayService
    {
        
        Task<TransactResponseData> CreateTransaction(CreateTransactionInput createTransactionInput);
        Task<TokenResponseData> GetToken(CreateTokenInput createTokenInput);
    }

    public class ECPayService : IECPayService
    {
        private readonly HttpClient _client;
        private readonly IOptions<ECPaySetting> _options;

        public ECPayService(HttpClient client, IOptions<ECPaySetting> options)
        {
            _options = options;
            client.BaseAddress = new Uri(_options.Value.BaseUrl);
            _client = client;
        }

        /// <summary>
        /// 取得畫面token
        /// </summary>
        /// <returns></returns>
        public async Task<TokenResponseData> GetToken(CreateTokenInput createTokenInput)
        {
            //初始化綠界要的參數(Data)
            var data = new TokenRequestData()
            {
                MerchantId = _options.Value.MerchantId,
                RememberCard = RememberCard.No,
                PaymentUIType = PaymentUIType.PaymentMethodList,
                ChoosePaymentList = string.Join(',', new[] {(int) ChoosePayment.CreditCardPayAllAtOnce}),

                OrderInfo = new OrderInfo() //由外部傳入
                {
                    MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    //訂單編號不可重複
                    MerchantTradeNo = createTokenInput.MerchantTradeNo,  //orderId?
                    TotalAmount = createTokenInput.TotalAmount,  //HousePrices.PerNight* (OrderDetails.EndDate-OrderDetails.StartDate)
                    ReturnUrl = "https://localhost:5001/api/system/suc",
                    TradeDesc = createTokenInput.TradeDesc,
                    ItemName = createTokenInput.ItemName,  //房屋名稱
                    
                },



                ConsumerInfo = new ConsumerInfo()  //由外部傳入  登入後
                {
                    Phone = createTokenInput.Phone,  //EmergencyContact.Phone?
                    Name = createTokenInput.Name,         //Members.Name? EmergencyContact.Name?
                    CountryCode = "158",    //固定158
                    Email = createTokenInput.Email,  //Members.Email
                    MerchantMemberId = createTokenInput.MerchantMemberId  //Members.Id
                },
                CardInfo = new CardInfo() {OrderResultUrl = "https://localhost:5001/Swagger/index.html"}
            };


            //初始化綠界要的參數(Token)
            var tokenRequest = new ECRequest
            {
                //todo 
                MerchantId = _options.Value.MerchantId,  //由外部傳入
                RqHeader = new ECRequestHeader() {
                                TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(), 
                                Revision = _options.Value.Revision
                            },

                // 加密(先序列化成json字串再加密)
                Data = CryptographyHelper.AesEncrypt(JsonConvert.SerializeObject(data), 
                                                        _options.Value.AesKey,
                                                        _options.Value.AesIV,
                                                        true
                                                    ),
            };
            // 發送POST請求到綠界，取得回傳的response
            var responseMessage = await _client.PostAsJsonAsync(_options.Value.Apis.GetTokenByTrade.Url, 
                                                                tokenRequest);
            //將回應json轉成回應物件
            var tokenResponse = await responseMessage.Content.ReadFromJsonAsync<TokenResponse>();
            // 解密回應物件的data
            var tokenResponseData = JsonConvert.DeserializeObject<TokenResponseData>
                (
                    CryptographyHelper.AesDecrypt(
                        tokenResponse.Data,
                        _options.Value.AesKey,
                        _options.Value.AesIV, 
                        true
                        )
                );
            return tokenResponseData;  //以此Data內的token(廠商驗證碼)進行畫面渲染
        }


        //建立交易
        public async Task<TransactResponseData> CreateTransaction(CreateTransactionInput createTransactionInput)
        {
            var request = new ECRequest()
            {
                MerchantId = _options.Value.MerchantId, //
                RqHeader = new ECRequestHeader()
                {
                    //時間戳 Unix TimeStamp
                    TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    //版號
                    Revision = _options.Value.Revision
                    
                },
                //Data="..."
            };

            var transactRequestData = new TransactRequestData()
            {
                MerchantId = _options.Value.MerchantId,
                MerchantTradeNo = createTransactionInput.MerchantTradeNo,
                PayToken = createTransactionInput.Token,
            };

            //先將transactRequestData序列化為JSON再加密
            request.Data = CryptographyHelper.AesEncrypt
                (
                    JsonConvert.SerializeObject(transactRequestData), 
                    _options.Value.AesKey, 
                    _options.Value.AesIV, 
                    true
                );
            //發送POST請求到綠界正式建立交易,取得回傳的response  (JSON)
            var responseMessage = await _client.PostAsJsonAsync(_options.Value.Apis.Transaction.Url, request);
            //將回應json轉成回應物件(TokenResponse)
            var response = await responseMessage.Content.ReadFromJsonAsync<TokenResponse>();
            //解密回應物件的data
            var data = JsonConvert.DeserializeObject<TransactResponseData>
                (CryptographyHelper.AesDecrypt(
                    response.Data, 
                    _options.Value.AesKey, 
                    _options.Value.AesIV, 
                    true)
                );

            
            
            return data;
        }

        public async Task<TokenRequestData> GetTokenEx()
        {
            var data = new TokenRequestData()
            {
                MerchantId = _options.Value.MerchantId,
                RememberCard = RememberCard.No,
                PaymentUIType = PaymentUIType.PaymentMethodList,
                ChoosePaymentList = string.Join(',', new[] { (int)ChoosePayment.CreditCardPayAllAtOnce }),
                OrderInfo = new OrderInfo()
                {
                    MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    MerchantTradeNo = "AirelaxTest003",
                    TotalAmount = 2000,
                    ReturnUrl = "https://localhost:5001/api/system/suc",
                    TradeDesc = "qwdqdc",
                    ItemName = "wecww"
                },
                ConsumerInfo = new ConsumerInfo()
                {
                    Phone = "2131445512",
                    Name = "Rick",
                    CountryCode = "123",
                    Email = "123@gmail.com",
                    MerchantMemberId = "M23455"
                },
                CardInfo = new CardInfo() { OrderResultUrl = "https://localhost:5001/Swagger/index.html" }
            };

            var tokenRequest = new ECRequest
            {
                MerchantId = _options.Value.MerchantId,
                RqHeader = new ECRequestHeader() { TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(), Revision = _options.Value.Revision },
                Data = CryptographyHelper.AesEncrypt(JsonConvert.SerializeObject(data), _options.Value.AesKey, _options.Value.AesIV, true),
            };
            var responseMessage = await _client.PostAsJsonAsync(_options.Value.Apis.GetTokenByTrade.Url, tokenRequest);
            var tokenResponse = await responseMessage.Content.ReadFromJsonAsync<TokenResponse>();
            var tokenResponseData = JsonConvert.DeserializeObject<TokenRequestData>(CryptographyHelper.AesDecrypt(tokenResponse.Data, _options.Value.AesKey, _options.Value.AesIV, true));
            return tokenResponseData;
        }
        public async Task<TransactResponseData>CreateTransactionEx(string token)
        {
            var request = new ECRequest()
            {
                MerchantId = _options.Value.MerchantId,
                RqHeader = new ECRequestHeader()
                {
                    TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    Revision = _options.Value.Revision
                },
            };

            var transactRequestData = new TransactRequestData()
            {
                MerchantId = _options.Value.MerchantId,
                MerchantTradeNo = "AirelaxTest003",
                PayToken = token,
            };

            request.Data = CryptographyHelper.AesEncrypt(JsonConvert.SerializeObject(transactRequestData), _options.Value.AesKey, _options.Value.AesIV, true);
            var responseMessage = await _client.PostAsJsonAsync(_options.Value.Apis.Transaction.Url, request);
            var response = await responseMessage.Content.ReadFromJsonAsync<TokenResponse>();
            var data = JsonConvert.DeserializeObject<TransactResponseData>(CryptographyHelper.AesDecrypt(response.Data, _options.Value.AesKey, _options.Value.AesIV, true));
            return data;
        }
    }
}