using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Converters;

namespace Pokidex.Converters
{
    public class TypeToColourConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var c = new ColorTypeConverter();
            var str = (string)value;

            Color colour = (Color)(c.ConvertFromInvariantString("white"));

            if (str.Equals("normal", StringComparison.InvariantCultureIgnoreCase))
            {
                //Grey
                colour = Color.FromRgb(175,175,175);
            }
            else if (str.Equals("fire", StringComparison.InvariantCultureIgnoreCase))
            {
                //Red
                colour = Color.FromRgb(194, 76, 76);
            }
            else if (str.Equals("fighting", StringComparison.InvariantCultureIgnoreCase))
            {
                //Orange
                colour = Color.FromRgb(194, 170, 76);
            }
            else if (str.Equals("water", StringComparison.InvariantCultureIgnoreCase))
            {
                //Blue
                colour = Color.FromRgb(76, 91, 194);
            }
            else if (str.Equals("flying", StringComparison.InvariantCultureIgnoreCase))
            {
                //Sky Bl
                colour = Color.FromRgb(76, 166, 194);
            }
            else if (str.Equals("grass", StringComparison.InvariantCultureIgnoreCase))
            {
                //Green
                colour = Color.FromRgb(76, 194, 85);
            }
            else if (str.Equals("bug", StringComparison.InvariantCultureIgnoreCase))
            {
                //lime
                colour = Color.FromRgb(111, 194, 76);
            }
            else if (str.Equals("poison", StringComparison.InvariantCultureIgnoreCase))
            {
                //Purple
                colour = Color.FromRgb(129, 76, 194);
            }
            else if (str.Equals("ghost", StringComparison.InvariantCultureIgnoreCase))
            {
                //Dark Purple
                colour = Color.FromRgb(105, 76, 194);
            }
            else if (str.Equals("electric", StringComparison.InvariantCultureIgnoreCase))
            {
                //Yellow
                colour = Color.FromRgb(182, 194, 76);
            }
            else if (str.Equals("ground", StringComparison.InvariantCultureIgnoreCase))
            {
                //brown
                colour = Color.FromRgb(115, 103, 87);
            }
            else if (str.Equals("psychic", StringComparison.InvariantCultureIgnoreCase))
            {
                //Pink
                colour = Color.FromRgb(194, 76, 150);
            }
            else if (str.Equals("fairy", StringComparison.InvariantCultureIgnoreCase))
            {
                //Light pink
                colour = Color.FromRgb(194, 76, 115);
            }
            else if (str.Equals("rock", StringComparison.InvariantCultureIgnoreCase))
            {
                //Yellow grey
                colour = Color.FromRgb(167, 171, 149);
            }
            else if (str.Equals("ice", StringComparison.InvariantCultureIgnoreCase))
            {
                //aqua
                colour = Color.FromRgb(76, 194, 182);
            }
            else if (str.Equals("dragon", StringComparison.InvariantCultureIgnoreCase))
            {
                //Dark blue
                colour = Color.FromRgb(76, 78, 194);
            }
            else if (str.Equals("dark", StringComparison.InvariantCultureIgnoreCase))
            {
                //Black
                colour = Color.FromRgb(69, 64, 74);
            }
            else if (str.Equals("steel", StringComparison.InvariantCultureIgnoreCase))
            {
                //blue grey
                colour = Color.FromRgb(92, 116, 138);
            }

            return colour;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string colorString = "White";

            return colorString;
        }
    }
}
