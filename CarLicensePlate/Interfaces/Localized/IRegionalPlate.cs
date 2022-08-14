namespace CarUtils.CarLicensePlate.Interfaces.Localized
{
    public interface IRegionalPlate
    {
        public string CountryInternationalName { get; }
        
        public string CountryNationalName { get; }
        
        public string CountryIsoCode { get; }
        
        public string PlateInternationalRegionName { get; }
        
        public string PlateNationalRegionName { get; }
    }
}