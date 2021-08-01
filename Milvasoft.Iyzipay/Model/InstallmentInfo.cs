using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class InstallmentInfo : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public List<InstallmentDetail> InstallmentDetails { get; set; }


        public InstallmentInfo(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<InstallmentInfo> RetrieveAsync(RetrieveInstallmentInfoRequest request)
        {
            return await _restHttpClient.PostAsync<InstallmentInfo>("/payment/iyzipos/installment", request).ConfigureAwait(false);
        }
    }
}
