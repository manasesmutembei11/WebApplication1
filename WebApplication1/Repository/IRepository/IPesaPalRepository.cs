
using WebApplication1.DTOs.PesapalDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repository.IRepository
{
    public interface IPesaPalRepository
    {
        Task<PesapalAuthResponse> RequestTokenAsync(PesapalAuthRequest request);
        Task<RegisterIPNResponseDTO> RegisterIPNUrlAsync(RegisterIPNRequestDTO request);
        Task<List<GetIPNListResponseDTO>> GetIPNListAsync(string accessToken);
        Task<SubmitOrderResponseDTO> SubmitOrderRequestAsync(string accessToken, SubmitOrderRequestDTO request);
        Task<TransactionStatusDTO> GetTransactionStatusAsync(string accessToken, string orderTrackingId);
    }
}
