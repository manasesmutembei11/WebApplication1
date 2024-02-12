using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace WebApplication1.DTOs.PesapalDTOs
{
    public class RequestDTOs
    {
    }
    public class PesapalAuthRequest
    {
        [JsonProperty("consumer_key")]
        
        public string ConsumerKey { get; set; }
        [JsonProperty("consumer_secret")]
        public string ConsumerSecret { get; set; }
    }

    public class RegisterIPNRequestDTO
    {
        public string Url { get; set; }
        public string IpnNotificationType { get; set; }
    }

    public class SubmitOrderRequestDTO
    {
        public string Id { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public float Amount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
        [JsonProperty("cancellation_url")]
        public string CancellationUrl { get; set; }
        [JsonProperty("notification_id")]
        public Guid NotificationId { get; set; }
        [JsonProperty("billing_address")]
        public BillingAddressDTO BillingAddress { get; set; }
    }

    public class BillingAddressDTO
    {
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("line_1")]
        public string Line1 { get; set; }
        [JsonProperty("line_2")]
        public string Line2 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }
    }
    public class GetTransactionStatusDTO
    {
        [JsonProperty("orderTrackingId")]
        public string OrderTrackingId { get; set; }
    }
}
