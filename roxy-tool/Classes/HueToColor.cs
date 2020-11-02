using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace roxy_tool.Classes
{
    public static class HueToColor
    {
        private static Color[] colorList = new Color[256];

        static HueToColor()
        {
            float slope1 = (255.0f - 160.0f) / 31.0f;
            float slope2 = 160.0f / 31.0f;
            float slope3 = 255.0f / 95.0f;

            // Generate color list
            for (int i = 0; i < 256; i++)
            {
                int r = 0, g = 0, b = 0;

                if (i < 32)
                {
                    r = (int)(255.0f - slope1 * i);
                    g = (int)(slope1 * i);
                    b = 0;
                }
                else if (i < 64)
                {
                    r = 170;
                    g = (int)(slope3 * i);
                    b = 0;
                }
                else if (i < 96)
                {
                    r = (int)(160.0f - slope2 * (i - 64));
                    g = (int)(slope3 * i);
                    b = 0;
                }
                else if (i < 128)
                {
                    r = 0;
                    g = (int)(255.0f - slope1 * (i - 96));
                    b = (int)(slope1 * (i - 96));
                }
                else if (i < 160)
                {
                    r = 0;
                    g = (int)(160.0f - slope2 * (i - 128));
                    b = (int)(95.0f + slope2 * (i - 128));
                }
                else
                {
                    r = (int)(slope3 * (i - 160));
                    g = 0;
                    b = (int)(255.0f - slope3 * (i - 160));
                }

                colorList[i] = Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
            }
            colorList[255] = Color.FromArgb(255, 255, 255, 255);
        }

        public static Color Convert(byte hue)
        {
            return colorList[(int)hue];
        }
    }
}
