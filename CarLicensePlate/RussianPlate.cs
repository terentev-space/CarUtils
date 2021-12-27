using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CarUtils.CarLicensePlate.Interfaces.Localized;
using CarUtils.Exceptions;

namespace CarUtils.CarLicensePlate
{
    public class RussianPlate : IRussianPlate
    {
        public const string PlateRuLetters = "АаВвЕеКкМмНнОоРрСсТтУуХх";
        public const string PlateEnLetters = "AaBbEeKkMmHhOoPpCcTtYyXx";
        public const string PlateNumbers = "[0-9]";

        public const string PlateRegexPlateName = "plate";
        public const string PlateRegexLeftName = "left";
        public const string PlateRegexCenterName = "center";
        public const string PlateRegexRightName = "right";
        public const string PlateRegexRegionName = "region";

        public static readonly string PlateLetters = @$"[{RussianPlate.PlateRuLetters}{RussianPlate.PlateEnLetters}]";

        public static readonly string PlateRegex =
            @$"(?<{RussianPlate.PlateRegexPlateName}>(?<{RussianPlate.PlateRegexLeftName}>{RussianPlate.PlateLetters})(?<{RussianPlate.PlateRegexCenterName}>{RussianPlate.PlateNumbers}{{3}})(?<{RussianPlate.PlateRegexRightName}>{RussianPlate.PlateLetters}{{2}})(?<{RussianPlate.PlateRegexRegionName}>{RussianPlate.PlateNumbers}{{2,3}}))";

        public static readonly Dictionary<string, string> RuToEnUpMap = new Dictionary<string, string>()
        {
            {"А", "A"},
            {"В", "B"},
            {"Е", "E"},
            {"К", "K"},
            {"М", "M"},
            {"Н", "H"},
            {"О", "O"},
            {"Р", "P"},
            {"С", "C"},
            {"Т", "T"},
            {"У", "Y"},
            {"Х", "X"},
        };

        public static readonly Dictionary<string, string> EnToRuUpMap = new Dictionary<string, string>()
        {
            {"A", "А"},
            {"B", "В"},
            {"E", "Е"},
            {"K", "К"},
            {"M", "М"},
            {"H", "Н"},
            {"O", "О"},
            {"P", "Р"},
            {"C", "С"},
            {"T", "Т"},
            {"Y", "У"},
            {"X", "Х"},
        };

        public string Input { get; }

        public string Plate { get; protected set; }

        public string NormalizedPlate => this.plateCache ??= this.NormalizePlate();
        public string NormalizedRuPlate => this.ruPlateCache ??= this.NormalizeRuPlate();
        public string NormalizedEnPlate => this.enPlateCache ??= this.NormalizeEnPlate();

        public string LeftRuPart => this.leftRuPartCache ??= this.GetLeftRuPart();
        public string LeftEnPart => this.leftEnPartCache ??= this.GetLeftEnPart();
        public string CenterPart => this.centerPartCache ??= this.GetCenterPart();
        public string RightRuPart => this.rightRuPartCache ??= this.GetRightRuPart();
        public string RightEnPart => this.rightEnPartCache ??= this.GetRightEnPart();
        public string RegionPart => this.regionPartCache ??= this.GetRegionPart();
        public string LettersRuPart => this.lettersRuPartCache ??= this.GetLettersRuPart();
        public string LettersEnPart => this.lettersEnPartCache ??= this.GetLettersEnPart();
        public string NumbersPart => this.numbersPartCache ??= this.GetNumbersPart();

        private string plateCache;
        private string ruPlateCache;
        private string enPlateCache;

        private string leftRuPartCache;
        private string leftEnPartCache;
        private string centerPartCache;
        private string rightRuPartCache;
        private string rightEnPartCache;
        private string regionPartCache;
        private string lettersRuPartCache;
        private string lettersEnPartCache;
        private string numbersPartCache;

        public RussianPlate([NotNull] string plate, bool isSearch = false, bool isValidate = true)
        {
            this.Input = plate;

            this.Plate = isSearch ? this.GetSearchRegex().Match(this.Input).Value : this.Input;

            if (isValidate)
                this.Validate();
        }

        public override string ToString()
        {
            return this.Plate;
        }

        public virtual bool IsValid() => this.GetCheckRegex().IsMatch(this.Plate);

        /// <exception cref="CarUtilsPlateValidateException"></exception>
        public void Validate()
        {
            if (!this.IsValid())
                throw new CarUtilsPlateValidateException(
                    this,
                    "Failed to identify parts of the plate. Expected formats for Russian plate: a000aa00, a000aa000"
                );
        }

        public Regex GetCheckRegex() => new Regex($"^{RussianPlate.PlateRegex}$");
        public Regex GetSearchRegex() => new Regex($"{RussianPlate.PlateRegex}");

        protected virtual string NormalizePlate()
        {
            string plate = this.Plate.ToLower();

            return plate;
        }

        protected virtual string NormalizeRuPlate()
        {
            string plate = this.NormalizedPlate;

            foreach ((string en, string ru) in RussianPlate.EnToRuUpMap)
                plate = plate.Replace(en.ToLower(), ru.ToLower());

            return plate;
        }

        protected virtual string NormalizeEnPlate()
        {
            string plate = this.NormalizedPlate;

            foreach ((string en, string ru) in RussianPlate.RuToEnUpMap)
                plate = plate.Replace(en.ToLower(), ru.ToLower());

            return plate;
        }

        protected virtual string GetLeftRuPart()
        {
            string plate = this.NormalizedRuPlate;

            plate = this.GetSearchRegex().Match(plate).Groups[RussianPlate.PlateRegexLeftName].Value;

            return plate;
        }

        protected virtual string GetLeftEnPart()
        {
            string plate = this.NormalizedEnPlate;

            plate = this.GetSearchRegex().Match(plate).Groups[RussianPlate.PlateRegexLeftName].Value;

            return plate;
        }

        protected virtual string GetCenterPart()
        {
            string plate = this.NormalizedPlate;

            plate = this.GetSearchRegex().Match(plate).Groups[RussianPlate.PlateRegexCenterName].Value;

            return plate;
        }

        protected virtual string GetRightRuPart()
        {
            string plate = this.NormalizedRuPlate;

            plate = this.GetSearchRegex().Match(plate).Groups[RussianPlate.PlateRegexRightName].Value;

            return plate;
        }

        protected virtual string GetRightEnPart()
        {
            string plate = this.NormalizedEnPlate;

            plate = this.GetSearchRegex().Match(plate).Groups[RussianPlate.PlateRegexRightName].Value;

            return plate;
        }

        protected virtual string GetRegionPart()
        {
            string plate = this.NormalizedPlate;

            plate = this.GetSearchRegex().Match(plate).Groups[RussianPlate.PlateRegexRegionName].Value;

            return plate;
        }

        protected virtual string GetLettersRuPart()
        {
            string plate = $"{this.LeftRuPart}{this.RightRuPart}";

            return plate;
        }

        protected virtual string GetLettersEnPart()
        {
            string plate = $"{this.LeftEnPart}{this.RightEnPart}";

            return plate;
        }

        protected virtual string GetNumbersPart()
        {
            string plate = $"{this.CenterPart}{this.RegionPart}";

            return plate;
        }
    }
}