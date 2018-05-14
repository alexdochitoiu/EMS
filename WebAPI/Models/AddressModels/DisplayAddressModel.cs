namespace WebAPI.Models
{
    public class DisplayAddressModel
    {
        public DisplayCountryModel Country { get; set; }
        public DisplayCityModel City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
    }
}
