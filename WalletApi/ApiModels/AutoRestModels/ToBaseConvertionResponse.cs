// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ToBaseConvertionResponse
    {
        /// <summary>
        /// Initializes a new instance of the ToBaseConvertionResponse class.
        /// </summary>
        public ToBaseConvertionResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ToBaseConvertionResponse class.
        /// </summary>
        public ToBaseConvertionResponse(IList<ConversionResult> converted = default(IList<ConversionResult>))
        {
            Converted = converted;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Converted")]
        public IList<ConversionResult> Converted { get; set; }

    }
}