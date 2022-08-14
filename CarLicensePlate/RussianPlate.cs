using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using CarUtils.CarLicensePlate.Interfaces.Localized;
using CarUtils.Entities;
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

        public static readonly ICollection<RegionEntity> Regions = new List<RegionEntity>()
        {
            new RegionEntity("The Republic of Adygea", "Республика Адыгея", "01", "101"),
            new RegionEntity("The Republic of Bashkortostan", "Республика Башкортостан", "02", "102", "702"),
            new RegionEntity("The Republic of Buryatia", "Республика Бурятия", "03"),
            new RegionEntity("The Republic of Altai", "Республика Алтай", "04"),
            new RegionEntity("The Republic of Dagestan", "Республика Дагестан", "05"),
            new RegionEntity("The Republic of Ingushetia", "Республика Ингушетия", "06"),
            new RegionEntity("The Republic of Kabardino-Balkarian", "Кабардино-Балкарская Республика", "07"),
            new RegionEntity("The Republic of Kalmykia", "Республика Калмыкия", "08"),
            new RegionEntity("The Republic of Karachay-Cherkess", "Карачаево-Черкесская Республика", "09"),
            new RegionEntity("The Republic of Karelia", "Республика Карелия", "10"),
            new RegionEntity("The Republic of Komi", "Республика Коми", "11"),
            new RegionEntity("The Republic of Mari", "Республика Марий Эл", "12"),
            new RegionEntity("The Republic of Mordovia", "Республика Мордовия", "13", "113"),
            new RegionEntity("The Republic of Sakha (Yakutia)", "Республика Саха (Якутия)", "14"),
            new RegionEntity("The Republic of North Ossetia - Alania", "Республика Северная Осетия — Алания", "15"),
            new RegionEntity("The Republic of Tatarstan (Tatarstan)", "Республика Татарстан (Татарстан)", "16", "116", "716", "616"),
            new RegionEntity("The Republic of Tyva", "Республика Тыва", "17"),
            new RegionEntity("The Republic of Udmurt", "Удмуртская Республика", "18"),
            new RegionEntity("The Republic of Khakassia", "Республика Хакасия", "19"),
            new RegionEntity("The Republic of Chuvash - Chuvashia", "Чувашская Республика — Чувашия", "21", "121"),
            new RegionEntity("Altai region", "Алтайский край", "22", "222"),
            new RegionEntity("Krasnodar region", "Краснодарский край", "23", "93", "123", "193"),
            new RegionEntity("Krasnoyarsk region", "Красноярский край", "24", "124"),
            new RegionEntity("Primorsky region", "Приморский край", "25", "125"),
            new RegionEntity("Stavropol region", "Ставропольский край", "26", "126"),
            new RegionEntity("Khabarovsk region", "Хабаровский край", "27"),
            new RegionEntity("Amur region", "Амурская область", "28"),
            new RegionEntity("Arhangelsk region", "Архангельская область", "29"),
            new RegionEntity("Astrakhan region", "Астраханская область", "30", "330"),
            new RegionEntity("Belgorod region", "Белгородская область", "31"),
            new RegionEntity("Bryansk region", "Брянская область", "32"),
            new RegionEntity("Vladimir region", "Владимирская область", "33", "333"),
            new RegionEntity("Volgograd region", "Волгоградская область", "34", "134"),
            new RegionEntity("Vologod region", "Вологодская область", "35"),
            new RegionEntity("Voronezh region", "Воронежская область", "36", "136"),
            new RegionEntity("Ivanovo region", "Ивановская область", "37"),
            new RegionEntity("Irkutsk region", "Иркутская область", "38", "138"),
            new RegionEntity("Kaliningrad region", "Калининградская область", "39"),
            new RegionEntity("Kaluga region", "Калужская область", "40"),
            new RegionEntity("Kamchatka region", "Камчатский край", "41"),
            new RegionEntity("Kemerovo region", "Кемеровская область", "42", "142"),
            new RegionEntity("Kirov region", "Кировская область", "43"),
            new RegionEntity("Kostroma region", "Костромская область", "44", "444"),
            new RegionEntity("Kurgan region", "Курганская область", "45"),
            new RegionEntity("Kursk region", "Курская область", "46"),
            new RegionEntity("Leningrad region", "Ленинградская область", "47", "147"),
            new RegionEntity("Lipetsk region", "Липецкая область", "48"),
            new RegionEntity("Magadan region", "Магаданская область", "49"),
            new RegionEntity("Moscow region", "Московская область", "50", "90", "150", "190", "750", "790"),
            new RegionEntity("Murmansk region", "Мурманская область", "51"),
            new RegionEntity("Nizhny Novgorod region", "Нижегородская область", "52", "152"),
            new RegionEntity("Novgorod region", "Новгородская область", "53"),
            new RegionEntity("Novosibirsk region", "Новосибирская область", "54", "154"),
            new RegionEntity("Omsk region", "Омская область", "55"),
            new RegionEntity("Orenburg region", "Оренбургская область", "56", "156"),
            new RegionEntity("Oryol region", "Орловская область", "57"),
            new RegionEntity("Penza region", "Пензенская область", "58"),
            new RegionEntity("Perm region", "Пермский край", "59", "159"),
            new RegionEntity("Pskov region", "Псковская область", "60", "660"),
            new RegionEntity("Rostov region", "Ростовская область", "61", "161", "661", "761"),
            new RegionEntity("Ryazan region", "Рязанская область", "62", "662"),
            new RegionEntity("Samara Region", "Самарская область", "63", "82", "163", "663", "763"),
            new RegionEntity("Saratov region", "Саратовская область", "64", "164", "664"),
            new RegionEntity("Sakhalin region", "Сахалинская область", "65", "665"),
            new RegionEntity("Sverdlovsk region", "Свердловская область", "66", "96", "196", "666"),
            new RegionEntity("Smolensk region", "Смоленская область", "67", "667"),
            new RegionEntity("Tambov region", "Тамбовская область", "68", "668"),
            new RegionEntity("Tver region", "Тверская область", "69", "669"),
            new RegionEntity("Tomsk region", "Томская область", "70"),
            new RegionEntity("Tula region", "Тульская область", "71"),
            new RegionEntity("Tyumen region", "Тюменская область", "72"),
            new RegionEntity("Ulyanovsk region", "Ульяновская область", "73", "82", "173"),
            new RegionEntity("Chelyabinsk region", "Челябинская область", "74", "174", "774"),
            new RegionEntity("Zabaykal region", "Забайкальский край", "75"),
            new RegionEntity("Yaroslavl region", "Ярославская область", "76"),
            new RegionEntity("Moscow city", "город Москва", "77", "97", "99", "177", "197", "199", "497", "777", "797", "799", "999"),
            new RegionEntity("St. Petersburg city", "город Санкт-Петербург", "78", "98", "178", "198", "278", "878"),
            new RegionEntity("Jewish Autonomous region", "Еврейская автономная область", "79"),
            new RegionEntity("The Republic of Crimea", "Республика Крым", "82"),
            new RegionEntity("Nenets Autonomous region", "Ненецкий автономный округ", "83"),
            new RegionEntity("Khanty-Mansi Autonomous region - Yugra", "Ханты-Мансийский автономный округ — Югра", "86", "186"),
            new RegionEntity("Chukotka Autonomous region", "Чукотский автономный округ", "87"),
            new RegionEntity("Yamalo-Nenets Autonomous region", "Ямало-Ненецкий автономный округ", "89"),
            new RegionEntity("Sevastopol city", "город Севастополь", "92"),
            new RegionEntity("The Republic of Chechen", "Чеченская Республика", "95", "995"),// До 2020 года
            new RegionEntity("Zabaykalsky region", "Забайкальский край", "80"),
            new RegionEntity("Perm region", "Пермский край", "81"),
            new RegionEntity("Krasnoyarsk region", "Красноярский край", "84"),
            new RegionEntity("Irkutsk region", "Иркутская область", "85"),
            new RegionEntity("Krasnoyarsk region", "Красноярский край", "88"),
            new RegionEntity("Kaliningrad region", "Калининградская область", "91"),
            new RegionEntity("Territories located outside the Russian Federation and served by the internal affairs bodies of the Russian Federation", "Территории, находящиеся за пределами Российской Федерации и обслуживаемые органами внутренних дел Российской Федерации", "94"),
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
        

        public string CountryInternationalName => "Russian Federation";
        public string CountryNationalName => "Российская Федерация";

        public string CountryIsoCode => "RU";

        public string PlateInternationalRegionName => (this.plateRegionCache ??= this.GetPlateRegion()).InternationalName;
        
        public string PlateNationalRegionName => (this.plateRegionCache ??= this.GetPlateRegion()).NationalName;

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
        private RegionEntity plateRegionCache;

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

        protected virtual RegionEntity GetPlateRegion() => 
            RussianPlate.Regions.First(e => e.Regions.Contains(this.RegionPart));
    }
}