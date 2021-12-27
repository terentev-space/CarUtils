using CarUtils.CarLicensePlate;
using CarUtils.CarLicensePlate.Interfaces.Localized;

namespace CarUtils.Extensions
{
    public static class StringExtension
    {
        public static IRussianPlate ToRussianPlate(this string value, bool isSearch = false, bool isValidate = true) => new RussianPlate(value, isSearch, isValidate);
    }
}