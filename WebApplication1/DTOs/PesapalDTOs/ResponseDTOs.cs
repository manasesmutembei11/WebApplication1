using Newtonsoft.Json;
using System;

namespace WebApplication1.DTOs.PesapalDTOs
{
    public class ResponseDTOs
    {
    }

    public class PesapalErrorObject
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }


    public class PesapalAuthResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }
        [JsonProperty("error")]
        public PesapalErrorObject Error { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class RegisterIPNResponseDTO
    {
        public string Url { get; set; }
        public string CreatedDate { get; set; }
        public string IpnId { get; set; }
        public int? Error { get; set; }
        public string Status { get; set; }
    }

    public class GetIPNListResponseDTO
    {
        public string Url { get; set; }
        public string CreatedDate { get; set; }
        public string IpnId { get; set; }
        public int? Error { get; set; }
        public string Status { get; set; }
    }

    public class SubmitOrderResponseDTO
    {
        [JsonProperty("order_tracking_id")]
        public string OrderTrackingId { get; set; }
        [JsonProperty("merchant_reference")]
        public string MerchantReference { get; set; }
        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }
        [JsonProperty("error")]
        public int? Error { get; set; }
        [JsonProperty("message")]
        public string Status { get; set; }
    }

    public class TransactionStatusDTO
    {
        [JsonProperty("Payment Method")]
        public string PaymentMethod { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("confirmation_code")]
        public string ConfirmationCode { get; set; }
        [JsonProperty("payment_status_description")]
        public string PaymentStatusDescription { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("payment_account")]
        public string PaymentAccount { get; set; }
        [JsonProperty("call_back_url")]
        public string CallBackUrl { get; set; }
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
        [JsonProperty("merchant_reference")]
        public string MerchantReference { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("error")]
        public ErrorDTO Error { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ErrorDTO
    {
        
        public string ErrorType { get; set; }
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
        public string CallBackUrl { get; set; }
    }
}
