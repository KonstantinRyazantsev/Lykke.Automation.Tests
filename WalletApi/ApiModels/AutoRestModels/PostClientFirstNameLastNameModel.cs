// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class PostClientFirstNameLastNameModel
    {
        /// <summary>
        /// Initializes a new instance of the PostClientFirstNameLastNameModel
        /// class.
        /// </summary>
        public PostClientFirstNameLastNameModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PostClientFirstNameLastNameModel
        /// class.
        /// </summary>
        public PostClientFirstNameLastNameModel(string firstName = default(string), string lastName = default(string))
        {
            FirstName = firstName;
            LastName = lastName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

    }
}
