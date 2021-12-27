namespace CarUtils.CarLicensePlate.Interfaces.Localized
{
    public interface IInternationalPlate : IPlate
    {
        public string NormalizedEnPlate { get; }
    }
}