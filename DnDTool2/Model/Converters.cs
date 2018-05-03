using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DnDTool2.Model
{
    public class GenericEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = Enum.GetName(value.GetType(), value);
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class SkillTypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = Enum.GetName(value.GetType(), value);
            switch (s)
            {
                case "SLEIGHT_OF_HAND":
                    return "Sleight of Hand";
                case "ANIMAL_HANDLING":
                    return "Animal Handling";
                default:
                    return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class DamageTypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = Enum.GetName(value.GetType(), value);
            switch (s)
            {
                case "NONMAGICAL":
                    return "Bludgeoning, piercing, and slashing from nonmagical attacks";
                case "NONMAGICAL_ADAMANTINE":
                    return "Bludgeoning, piercing, and slashing from nonmagical attacks not made with adamantine weapons";
                case "NONMAGICAL_SILVER":
                    return "Bludgeoning, piercing, and slashing from nonmagical attacks not made with silvered weapons";
                default:
                    return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class AlignmentEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case Alignment.U:
                    return "Unaligned";
                case Alignment.ANY:
                    return "Any Alignment";
                case Alignment.ANYG:
                    return "Any Good Alignment";
                case Alignment.ANYE:
                    return "Any Evil Alignment";
                case Alignment.TN:
                    return "True Neutral";
                default:
                    string e = Enum.GetName(value.GetType(), value);
                    string s;
                    switch (e[0])
                    {
                        case 'L':
                            s = "Lawful";
                            break;
                        case 'N':
                            s = "Neutral";
                            break;
                        case 'C':
                            s = "Chaotic";
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    switch (e[1])
                    {
                        case 'G':
                            s += " Good";
                            break;
                        case 'N':
                            s += " Neutral";
                            break;
                        case 'E':
                            s += " Evil";
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    return s;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ChallengeRatingNumberEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int crIndex = (int)value;
            switch (crIndex)
            {
                case 0:
                    return 0;
                case 1:
                    return "1/8";
                case 2:
                    return "1/4";
                case 3:
                    return "1/2";
                default:
                    return crIndex - 3;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class CastingTimeTypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = Enum.GetName(value.GetType(), value);
            switch (s)
            {
                case "BONUS":
                    return "1 bonus action";
                case "ACTION":
                    return "1 action";
                case "REACTION":
                    return "1 reaction";
                default:
                    return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ModDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value < 0 ? "" : "+") + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ModEquationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value < 0 ? "- " : "+ ") + Math.Abs((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class DecToFracConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if ((double)value < 1 && (double)value > 0)
                    return "1/" + (int)(1 / (double)value);
                else
                    return value;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class BooleanToVisibilityInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class EnumComparatorToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(Enum.Parse(value.GetType(), (string)parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.Parse(targetType, (string)parameter);
        }
    }

    public class EnumComparatorToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] strings = ((string)parameter).Split('|');
            foreach (string s in strings)
            {
                if (value.Equals(Enum.Parse(value.GetType(), s)))
                    return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
