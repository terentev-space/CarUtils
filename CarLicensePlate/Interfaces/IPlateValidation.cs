namespace CarUtils.CarLicensePlate.Interfaces
{
    public interface IPlateValidation : IPlateRegex
    {
        public bool IsValid();

        /// <exception cref="CarUtils.Exceptions.CarUtilsPlateValidateException"></exception>
        public void Validate();
    }
}