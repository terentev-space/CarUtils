using System.Text.RegularExpressions;

namespace CarUtils.CarLicensePlate.Interfaces
{
    public interface IPlateRegex
    {
        public Regex GetCheckRegex();
        public Regex GetSearchRegex();
    }
}