namespace Properties.Model
{
    using System.Collections.Generic;

    public class PropertiesResponseDto
    {
        public List<Property> Properties { get; set; }

        public List<PropertyInfo> GetRequiredProperties(List<int> ExistingIds)
        {
            var response = new List<PropertyInfo>();

            foreach (var property in Properties)
            {
                var propertyInfo = new PropertyInfo
                {
                    Id = property.Id,
                    Address = property.Address,
                    YearBuilt = (property?.Physical?.YearBuilt) != null ? property.Physical.YearBuilt : 0,
                    ListPrice = (property?.Financial?.ListPrice) != null ? property.Financial.ListPrice : 0,
                    MonthlyRent = (property?.Financial?.MonthlyRent) != null ? property.Financial.MonthlyRent : 0,
                    PropertyName = property?.PropertyName,
                    GrossYield = (
                                    (property?.Financial?.ListPrice) != null &&
                                    (property?.Financial?.ListPrice) != 0 &&
                                    (property?.Financial?.MonthlyRent) != null
                                 ) ? property.Financial.MonthlyRent * 12 / property.Financial.ListPrice : 0,
                    Exist = ExistingIds.Contains(property.Id)
                };

                response.Add(propertyInfo);
            }

            return response;
        }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ZipPlus4 { get; set; }
    }

    public class Financial 
    {
        public double ListPrice { get; set; } 
        public double MonthlyRent { get; set; } 
    }

    public class Physical 
    {
        public int YearBuilt { get; set; }
    }

    public class Property
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string PropertyType { get; set; }
        public Address Address { get; set; }
        public Financial Financial { get; set; }
        public Physical Physical { get; set; }
    }
}
