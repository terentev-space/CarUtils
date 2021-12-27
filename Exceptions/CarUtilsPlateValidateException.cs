using CarUtils.CarLicensePlate.Interfaces;

namespace CarUtils.Exceptions
{
    public class CarUtilsPlateValidateException : CarUtilsException
    {
        public CarUtilsPlateValidateException(IPlateValue plate, string reason = "") : base(
            $"The value '{plate.Input}' is not valid" +
            (
                string.IsNullOrWhiteSpace(reason)
                    ? ""
                    : $" for a reason: '{reason}'"
            )
        )
        {
        }
    }
}