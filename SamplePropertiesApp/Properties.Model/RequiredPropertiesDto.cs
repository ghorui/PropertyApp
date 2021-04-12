using System.Collections.Generic;

namespace Properties.Model
{
    public class RequiredPropertiesDto
    {
        public List<PropertyInfo> Properties { get; set; }
    }

    public class PropertyInfo
    {
        public int Id { get; set; }

        public string PropertyName { get; set; }

        public Address Address { get; set; }

        public int YearBuilt { get; set; }

        public double ListPrice { get; set; }

        public double MonthlyRent { get; set; }

        public double GrossYield { get; set; }

        public bool Exist { get; set; }
    }
}
