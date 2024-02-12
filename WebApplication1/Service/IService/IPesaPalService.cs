
using WebApplication1.DTOs.PesapalDTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApplication1.Service.IService
{
    public interface IPesaPalService
    {
        Task<PesapalAuthResponse> RequestTokenAsync(string consumerKey, string consumerSecret);
        Task<RegisterIPNResponseDTO> RegisterIPNUrlAsync(string url, string ipnNotificationType, string accessToken);
        Task<List<GetIPNListResponseDTO>> GetIPNListAsync(string accessToken);
        Task<SubmitOrderResponseDTO> SubmitOrderRequestAsync(string accessToken, SubmitOrderRequestDTO request);

        Task<TransactionStatusDTO> GetTransactionStatusAsync(string accessToken, string orderTrackingId);
    }


}
