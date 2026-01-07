using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sampler.Codex.AudioUI.Converters
{
            public class KnobToAngleConverter : IValueConverter
            {
                // 0..1 -> -135..+135
                public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                {
                    if (value is double v)
                        return -135 + v * 270;

                    return -135.0;
                }

                // Kąt -> 0..1 (raczej nie będziemy używać, ale niech będzie)
                public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
                {
                    if (value is double angle)
                        return (angle + 135) / 270.0;

                    return 0.0;
                }
            }

}
