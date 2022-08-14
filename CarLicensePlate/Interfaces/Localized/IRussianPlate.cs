namespace CarUtils.CarLicensePlate.Interfaces.Localized
{
    public interface IRussianPlate : IInternationalPlate, IRegionalPlate
    {
        public string NormalizedRuPlate { get; }

        /// <summary> [a] 000aa00 - return a (ru) </summary>
        public string LeftRuPart { get; }

        /// <summary> [a] 000aa00 - return a (en) </summary>
        public string LeftEnPart { get; }

        /// <summary> a [000] aa00 - return 000 </summary>
        public string CenterPart { get; }

        /// <summary> a000 [aa] 00 - return aa (ru) </summary>
        public string RightRuPart { get; }

        /// <summary> a000 [aa] 00 - return aa (en) </summary>
        public string RightEnPart { get; }

        /// <summary> a000aa [00] - return 00 </summary>
        public string RegionPart { get; }

        /// <summary> [a] 000 [aa] 00 - return aaa (ru) </summary>
        public string LettersRuPart { get; }

        /// <summary> [a] 000 [aa] 00 - return aaa (en) </summary>
        public string LettersEnPart { get; }

        /// <summary> a [000] aa [00] - return 00000 </summary>
        public string NumbersPart { get; }
    }
}