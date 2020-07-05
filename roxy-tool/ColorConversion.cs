using Gdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace roxy_tool
{
    public static class ColorConversion
    {
        // Borrowed from: https://stackoverflow.com/a/6930407

        public static HSV RGB2HSV(RGBA rgb)
        {
            HSV hsv;
            double min, max, delta;

            min = rgb.Red < rgb.Green ? rgb.Red : rgb.Green;
            min = min < rgb.Blue ? min : rgb.Blue;

            max = rgb.Red > rgb.Green ? rgb.Red : rgb.Green;
            max = max > rgb.Blue ? max : rgb.Blue;

            hsv.Value = max;                                // v
            delta = max - min;
            if (delta < 0.00001)
            {
                hsv.Saturation = 0;
                hsv.Hue = 0; // undefrgbed, maybe nan?
                return hsv;
            }
            if (max > 0.0)
            { // NOTE: if Max is == 0, this divide would cause a crash
                hsv.Saturation = (delta / max);                  // s
            }
            else
            {
                // if max is 0, then r = g = b = 0              
                // s = 0, h is undefrgbed
                hsv.Saturation = 0.0;
                hsv.Hue = double.NaN;                            // its now undefrgbed
                return hsv;
            }
            if (rgb.Red >= max)                           // > is bogus, just keeps compilor happy
                hsv.Hue = (rgb.Green - rgb.Blue) / delta;        // between yellow & magenta
            else
            {
                if (rgb.Green >= max)
                    hsv.Hue = 2.0 + (rgb.Blue - rgb.Red) / delta;  // between cyan & yellow
                else
                    hsv.Hue = 4.0 + (rgb.Red - rgb.Green) / delta;  // between magenta & cyan
            }

            hsv.Hue *= 60.0;                              // degrees

            if (hsv.Hue < 0.0)
                hsv.Hue += 360.0;

            return hsv;
        }

        public static RGBA HSV2RGB(HSV hsv)
        {
            double hh, p, q, t, ff;
            long i;
            RGBA rgb = new RGBA() { Alpha = 1.0 };

            if (hsv.Saturation <= 0.0) {       // < is bogus, just shuts up warnings
                rgb.Red = hsv.Value;
                rgb.Green = hsv.Value;
                rgb.Blue = hsv.Value;
                return rgb;
            }
            hh = hsv.Hue;
            if (hh >= 360.0) hh = 0.0;
            hh /= 60.0;
            i = (long)hh;
            ff = hh - i;
            p = hsv.Value * (1.0 - hsv.Saturation);
            q = hsv.Value * (1.0 - (hsv.Saturation * ff));
            t = hsv.Value * (1.0 - (hsv.Saturation * (1.0 - ff)));

            switch (i)
            {
                case 0:
                    rgb.Red = hsv.Value;
                    rgb.Green = t;
                    rgb.Blue = p;
                    break;
                case 1:
                    rgb.Red = q;
                    rgb.Green = hsv.Value;
                    rgb.Blue = p;
                    break;
                case 2:
                    rgb.Red = p;
                    rgb.Green = hsv.Value;
                    rgb.Blue = t;
                    break;

                case 3:
                    rgb.Red = p;
                    rgb.Green = q;
                    rgb.Blue = hsv.Value;
                    break;
                case 4:
                    rgb.Red = t;
                    rgb.Green = p;
                    rgb.Blue = hsv.Value;
                    break;
                case 5:
                default:
                    rgb.Red = hsv.Value;
                    rgb.Green = p;
                    rgb.Blue = q;
                    break;
            }
            return rgb;
        }
    }

    public struct HSV
    {
        public double Hue;
        public double Saturation;
        public double Value;
    }
}
