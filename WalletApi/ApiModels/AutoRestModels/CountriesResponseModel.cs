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

    public partial class CountriesResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the CountriesResponseModel class.
        /// </summary>
        public CountriesResponseModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CountriesResponseModel class.
        /// </summary>
        public CountriesResponseModel(string current = default(string), IList<CountryItem> countriesList = default(IList<CountryItem>))
        {
            Current = current;
            CountriesList = countriesList;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Current")]
        public string Current { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CountriesList")]
        public IList<CountryItem> CountriesList { get; set; }

    }
}