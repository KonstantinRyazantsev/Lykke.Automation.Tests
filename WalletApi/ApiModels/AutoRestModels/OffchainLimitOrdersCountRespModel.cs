// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class OffchainLimitOrdersCountRespModel
    {
        /// <summary>
        /// Initializes a new instance of the OffchainLimitOrdersCountRespModel
        /// class.
        /// </summary>
        public OffchainLimitOrdersCountRespModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the OffchainLimitOrdersCountRespModel
        /// class.
        /// </summary>
        public OffchainLimitOrdersCountRespModel(int? count = default(int?))
        {
            Count = count;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Count")]
        public int? Count { get; set; }

    }
}
