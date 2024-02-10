
using Person.DTOs;

namespace Person.PesaPalRepository
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
