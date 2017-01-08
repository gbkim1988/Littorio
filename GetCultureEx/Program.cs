using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCultureEx
{
    class Program
    {
        public static void Main(string[] args)
        {
            var countryCodesMapping = new Dictionary<string, RegionInfo>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var culture in cultures)
            {
                try
                {
                    var region = new RegionInfo(culture.LCID);
                    countryCodesMapping[region.ThreeLetterISORegionName] = region;
                }
                catch (CultureNotFoundException)
                {
                    var consoleColor = Console.ForegroundColor;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Culture not found: " + culture.Name);
                    Console.ForegroundColor = consoleColor;
                }
            }

            Console.WriteLine("var countryCodesMapping = new Dictionary<string, string>() {");
            foreach (var mapping in countryCodesMapping.OrderBy(mapping => mapping.Key))
            {
                Console.WriteLine("   {{ \"{0}\", \"{1}\" }}", mapping.Value.TwoLetterISORegionName, mapping.Value.EnglishName);
            }

            Console.WriteLine("};");
            Console.ReadLine();
        }
    }
}
