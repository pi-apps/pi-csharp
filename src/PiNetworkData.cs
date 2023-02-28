using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PiNetworkNet
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class PiAuthDto
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("user")]
        public PiUser User { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class PiUser
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("credentials")]
        public Credentials Credentials { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class PiValidTime
    {
        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }
        [JsonProperty("iso8601")]
        public DateTimeOffset Iso8601 { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class Credentials
    {
        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }
        [JsonProperty("valid_until")]
        public PiValidTime ValidTime { get; set; }
    };

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class PaymentDto
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("user_uid")]
        public string Useruid { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("from_address")]
        public string FromAddress { get; set; }

        [JsonProperty("to_address")]
        public string ToAddress { get; set; }

        [JsonProperty("created_at")]
        //public string created_at { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("transaction")]
        public TransactionStatus Transaction { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class Metadata
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("cat")]
        public string Category { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class Status
    {
        [JsonProperty("developer_approved")]
        public bool DeveloperApproved { get; set; }

        [JsonProperty("transaction_verified")]
        public bool TransactionVerified { get; set; }

        [JsonProperty("developer_completed")]
        public bool DeveloperCompleted { get; set; }

        [JsonProperty("cancelled")]
        public bool Cancelled { get; set; }

        [JsonProperty("user_cancelled")]
        public bool UserCancelled { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class TransactionStatus
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("_link")]
        public string Link { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class Tx
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class IncompleteServerPayments
    {
        [JsonProperty("incomplete_server_payments")]
        public List<PaymentDto> IncompletePayments { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class PaymentArgs
    {
        [JsonProperty("amount")]
        public double Amount;
        [JsonProperty("memo")]
        public string Memo;
        [JsonProperty("metadata")]
        public object Metadata;
        [JsonProperty("uid")]
        public string Uid;
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class CreatePaymentDto
    {
        [JsonProperty("payment")]
        public PaymentArgs Payment;
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class TransactionData
    {
        public double Amount;
        public string Identifier;
        public string FromAddress;
        public string ToAddress;
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public sealed class PiNetworkError
    {
        [JsonProperty("error")]
        public string ErrorName { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("payment")]
        public PaymentDto Payment { get; set; }
    }

    public class PiNetworkException: Exception
    {
        public PiNetworkError PiError { get; set; }
    }
}
