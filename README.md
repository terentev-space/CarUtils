# CarUtils

[![Latest Version](https://img.shields.io/github/v/tag/terentev-space/CarUtils)](https://github.com/terentev-space/CarUtils/releases)
[![Software License](https://img.shields.io/badge/license-Apache_2.0-brightgreen.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/CarUtils.svg)](https://www.nuget.org/packages/CarUtils)
[![downloads](https://img.shields.io/nuget/dt/CarUtils)](https://www.nuget.org/packages/CarUtils)
[![Size](https://img.shields.io/github/repo-size/terentev-space/CarUtils.svg)]()

`Net Standard 2.1`

## Install

#### PM
```
Install-Package CarUtils
```

#### .NET CLI
```
dotnet add package CarUtils
```

#### NuGet
```
CarUtils
```

## Examples

#### 🔶 Using
```c#
using CarUtils.CarLicensePlate;
using CarUtils.Extensions;
```

#### 🔶 Russian plate
```c#
string value;
bool search;

IRussianPlate plate;

// к - russian
// Y - english
// h - english

value = "к123Yh45";
search = false;
validate = true;
/* OR */
value = "plate-к123Yh45";
search = true;
validate = true;
            
plate = new RussianPlate(value, search, validate);
/* OR */
plate = value.ToRussianPlate(search, validate);

// plate.IsValid()
Console.WriteLine(plate.IsValid());
// Out: true

// plate (.ToString())
Console.WriteLine(plate);
// Out: к123Yh45

// plate.Input
Console.WriteLine(plate.Input);
// Out: plate-к123Yh45

// plate.Plate
Console.WriteLine(plate.Plate);
// Out: к123Yh45

// plate.NormalizedPlate
Console.WriteLine(plate.NormalizedPlate);
// Out: к123yh45

// plate.NormalizedRuPlate
Console.WriteLine(plate.NormalizedRuPlate);
// Out: к123ун45

// plate.NormalizedEnPlate
Console.WriteLine(plate.NormalizedEnPlate);
// Out: k123yh45

// plate.LeftRuPart
Console.WriteLine(plate.LeftRuPart);
// Out: к

// plate.LeftEnPart
Console.WriteLine(plate.LeftEnPart);
// Out: k

// plate.CenterPart
Console.WriteLine(plate.CenterPart);
// Out: 123

// plate.RightRuPart
Console.WriteLine(plate.RightRuPart);
// Out: ун

// plate.RightEnPart
Console.WriteLine(plate.RightEnPart);
// Out: yh

// plate.RegionPart
Console.WriteLine(plate.RegionPart);
// Out: 45

// plate.LettersRuPart
Console.WriteLine(plate.LettersRuPart);
// Out: кун

// plate.LettersEnPart
Console.WriteLine(plate.LettersEnPart);
// Out: kyh

// plate.NumbersPart
Console.WriteLine(plate.NumbersPart);
// Out: 12345
```

## Credits

- [Ivan Terentev](https://github.com/terentev-space)
- [All Contributors](https://github.com/terentev-space/CarUtils/contributors)

## License

The Apache 2.0 License (Apache-2.0). Please see [License File](LICENSE) for more information.