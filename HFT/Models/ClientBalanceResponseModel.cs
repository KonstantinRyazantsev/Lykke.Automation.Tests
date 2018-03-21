// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ClientBalanceResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the ClientBalanceResponseModel class.
        /// </summary>
        public ClientBalanceResponseModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ClientBalanceResponseModel class.
        /// </summary>
        public ClientBalanceResponseModel(double balance, double reserved, string assetId = default(string))
        {
            AssetId = assetId;
            Balance = balance;
            Reserved = reserved;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetId")]
        public string AssetId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Balance")]
        public double Balance { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Reserved")]
        public double Reserved { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}