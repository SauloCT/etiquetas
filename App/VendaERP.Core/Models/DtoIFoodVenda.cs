using Newtonsoft.Json;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoIFoodVenda : Entity
    {
        public string VendaId { get; set; }

        public List<IFoodStatusVenda> Status { get; set; }

        [JsonProperty("id")]
        public string VendaIFoodId { get; set; }

        [JsonProperty("delivery")]
        public Delivery Delivery { get; set; }

        [JsonProperty("orderType")]
        public string OrderType { get; set; }

        [JsonProperty("orderTiming")]
        public string OrderTiming { get; set; }

        [JsonProperty("displayId")]
        public string DisplayId { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("preparationStartDateTime")]
        public DateTime PreparationStartDateTime { get; set; }

        [JsonProperty("isTest")]
        public bool IsTest { get; set; }

        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("salesChannel")]
        public string SalesChannel { get; set; }

        [JsonProperty("total")]
        public Total Total { get; set; }

        [JsonProperty("payments")]
        public Payments Payments { get; set; }

        [JsonProperty("additionalInfo")]
        public AdditionalInfo AdditionalInfo { get; set; }

        [JsonProperty("picking")]
        public Picking Picking { get; set; }
    }

    public class Delivery
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("deliveredBy")]
        public string DeliveredBy { get; set; }

        [JsonProperty("deliveryDateTime")]
        public DateTime DeliveryDateTime { get; set; }

        [JsonProperty("deliveryAddress")]
        public DeliveryAddress DeliveryAddress { get; set; }
    }

    public class DeliveryAddress
    {
        [JsonProperty("streetName")]
        public string StreetName { get; set; }

        [JsonProperty("streetNumber")]
        public string StreetNumber { get; set; }

        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class Merchant
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Customer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public Phone Phone { get; set; }

        [JsonProperty("ordersCountOnMerchant")]
        public int OrdersCountOnMerchant { get; set; }
    }

    public class Phone
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("localizer")]
        public string Localizer { get; set; }

        [JsonProperty("localizerExpiration")]
        public DateTime LocalizerExpiration { get; set; }
    }

    public class Item
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("externalCode")]
        public string ExternalCode { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty("optionsPrice")]
        public double OptionsPrice { get; set; }

        [JsonProperty("totalPrice")]
        public double TotalPrice { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }

    public class Option
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("externalCode")]
        public string ExternalCode { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty("addition")]
        public double Addition { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }

    public class Total
    {
        [JsonProperty("subTotal")]
        public double SubTotal { get; set; }

        [JsonProperty("deliveryFee")]
        public double DeliveryFee { get; set; }

        [JsonProperty("benefits")]
        public int Benefits { get; set; }

        [JsonProperty("orderAmount")]
        public double OrderAmount { get; set; }

        [JsonProperty("additionalFees")]
        public double AdditionalFees { get; set; }
    }

    public class Payments
    {
        [JsonProperty("prepaid")]
        public double Prepaid { get; set; }

        [JsonProperty("pending")]
        public int Pending { get; set; }

        [JsonProperty("methods")]
        public List<Methods> Methods { get; set; }
    }

    public class Methods
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("prepaid")]
        public bool Prepaid { get; set; }
    }

    public class Card
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }
    }

    public class AdditionalInfo
    {
        [JsonProperty("metadata")]
        public MetadataVenda Metadata { get; set; }
    }

    public class MetadataVenda
    {
        [JsonProperty("additionalProp1")]
        public string AdditionalProp1 { get; set; }

        [JsonProperty("additionalProp2")]
        public string AdditionalProp2 { get; set; }

        [JsonProperty("additionalProp3")]
        public string AdditionalProp3 { get; set; }
    }
    [Serializable]
    public class IFoodStatusVenda
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("fullCode")]
        public string FullCode { get; set; }
        [JsonProperty("orderId")]
        public string OrderId { get; set; }
        [JsonProperty("merchantId")]
        public string MerchantId { get; set; }
        [JsonProperty("createdAt")]
        public DateTime Data { get; set; }
        [JsonProperty("metadata")]
        public MetadataStatus Metadata { get; set; }
    }

    public class MetadataStatus
    {
        [JsonProperty("CANCEL_STAGE")]
        public string CancelStage { get; set; }
        [JsonProperty("ORIGIN")]
        public string Origin { get; set; }
        [JsonProperty("CANCEL_CODE")]
        public string CancelCode { get; set; }
        [JsonProperty("CANCEL_CODE_DESCRIPTION")]
        public string CancelCodeDescription { get; set; }
        [JsonProperty("CANCELLATION_DISPUTE")]
        public CancellationDispute CancellationDispute { get; set; }
        [JsonProperty("CANCELLATION_OCCURRENCE")]
        public CancellationOccurrence CancellationOccurrence { get; set; }
        [JsonProperty("TIMEOUT_EVENT")]
        public bool TimeoutEvent { get; set; }
        [JsonProperty("CANCEL_ORIGIN")]
        public string CancelOrigin { get; set; }
        [JsonProperty("CANCEL_REASON")]
        public string CancelReason { get; set; }
        [JsonProperty("CANCEL_USER")]
        public string CancelUser { get; set; }
        [JsonProperty("CANCELLATION_REQUESTED_EVENT_ID")]
        public string CancellationRequestedEventId { get; set; }
        [JsonProperty("ownerName")]
        public string OwnerName { get; set; }
        [JsonProperty("CLIENT_ID")]
        public string ClentId { get; set; }
        [JsonProperty("appName")]
        public string AppName { get; set; }
        [JsonProperty("deliveryGroupId")]
        public string DeliveryGroupId { get; set; }
        [JsonProperty("orderIds")]
        public List<string> OrderIds { get; set; }

        [JsonProperty("workerPhone")]
        public string WorkerPhone { get; set; }

        [JsonProperty("workerVehicleType")]
        public string WorkerVehicleType { get; set; }

        [JsonProperty("workerName")]
        public string WorkerName { get; set; }

        [JsonProperty("workerExternalUuid")]
        public string WorkerExternalUuid { get; set; }

        [JsonProperty("workerPhotoUrl")]
        public string WorkerPhotoUrl { get; set; }
        [JsonProperty("ORDER_EXTERNAL_UUID")]
        public string OrderExternalUuId { get; set; }
        [JsonProperty("CANCELLATION_REQUEST_SOURCE")]
        public string CancellationRequestSource { get; set; }
    }

    public class CancellationDispute
    {
        [JsonProperty("IS_CONTESTABLE")]
        public string IsContestable { get; set; }
        [JsonProperty("REASON")]
        public string Reason { get; set; }
        [JsonProperty("REASONS")]
        public List<string> Reasons { get; set; }

        [JsonProperty("DEADLINE")]
        public string Deadline { get; set; }

        [JsonProperty("AUTOMATIC_REFUND")]
        public bool AutomaticRefund { get; set; }

        [JsonProperty("CONTESTABLE_UNTIL")]
        public string ContestableUntil { get; set; }
    }
    public class CancellationOccurrence
    {
        [JsonProperty("RESTAURANT")]
        public Restaurant Restaurant { get; set; }
        [JsonProperty("CONSUMER")]
        public Consumer Consumer { get; set; }
        [JsonProperty("LOGISTIC")]
        public Logistic Logistic { get; set; }
    }
    public class Restaurant
    {
        [JsonProperty("FINANCIAL_OCCURRENCE")]
        public string FinancialOccurrence { get; set; }
        [JsonProperty("PAYMENT_TYPE")]
        public string PaymentType { get; set; }
    }
    public class Consumer
    {
        [JsonProperty("FINANCIAL_OCCURRENCE")]
        public string FinancialOccurrence { get; set; }
        [JsonProperty("PAYMENT_TYPE")]
        public string PaymentType { get; set; }
    }
    public class Logistic
    {
        [JsonProperty("FINANCIAL_OCCURRENCE")]
        public string FinancialOccurrence { get; set; }
        [JsonProperty("PAYMENT_TYPE")]
        public string PaymentType { get; set; }
    }
    public class Picking
    {
        [JsonProperty("replacementOptions")]
        public string replacementOptions { get; set; }
    }
}

