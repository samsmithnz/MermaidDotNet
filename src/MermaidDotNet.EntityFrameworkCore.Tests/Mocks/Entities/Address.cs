using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        [DescriptionAttribute("37000, 63400, ...")]
        public string PostalCode { get; set; }
    }
}