using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class SubMerchant : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string Name { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }
        public string SwiftCode { get; set; }
        public string Currency { get; set; }
        public string TaxOffice { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string LegalCompanyTitle { get; set; }
        public string SubMerchantExternalId { get; set; }
        public string IdentityNumber { get; set; }
        public string TaxNumber { get; set; }
        public string SubMerchantType { get; set; }
        public string SubMerchantKey { get; set; }
        public string SettlementDescriptionTemplate { get; set; }

        public SubMerchant(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<SubMerchant> CreateAsync(CreateSubMerchantRequest request)
        {
            return await _restHttpClient.PostAsync<SubMerchant>("/onboarding/submerchant", request).ConfigureAwait(false);
        }

        public async Task<SubMerchant> UpdateAsync(UpdateSubMerchantRequest request)
        {
            return await _restHttpClient.PutAsync<SubMerchant>("/onboarding/submerchant", request).ConfigureAwait(false);
        }

        public async Task<SubMerchant> RetrieveAsync(RetrieveSubMerchantRequest request)
        {
            return await _restHttpClient.PostAsync<SubMerchant>("/onboarding/submerchant/detail", request).ConfigureAwait(false);
        }
    }
}
