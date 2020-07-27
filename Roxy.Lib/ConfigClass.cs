using System;
using System.Collections.Generic;
using System.Text;

namespace Roxy.Lib
{
    public static class ConfigDefines
    {
        public static Dictionary<sbyte, int> ComboBoxDict = new Dictionary<sbyte, int>()
        {
            {0, 0 },
            {-2, 1 },
            {-3, 2 },
            {-4, 3 },
            {-6, 4 },
            {-8, 5 },
            {-11, 6 },
            {-16, 7 },
            {2, 8 },
            {3, 9 },
            {4, 10 },
            {6, 11 },
            {8, 12 },
            {11, 13 },
            {16, 14 },
            {-127, 15 },
            {-126, 16 },
            {-125, 17 }
        };

        public static readonly List<string> QeDropdownStrings = new List<string>()
        {
            "1:1",
            "1:2",
            "1:3",
            "1:4",
            "1:6",
            "1:8",
            "1:11",
            "1:16",
            "2:1",
            "3:1",
            "4:1",
            "6:1",
            "8:1",
            "11:1",
            "16:1",
            "600 PPR",
            "400 PPR",
            "360 PPR"
        };

        public static readonly List<KeyEntry> KeyList = new List<KeyEntry>()
        {
            new KeyEntry("Disabled", 0),
            new KeyEntry("A",  4   ),
            new KeyEntry("B",  5   ),
            new KeyEntry("C",  6   ),
            new KeyEntry("D",  7   ),
            new KeyEntry("E",  8   ),
            new KeyEntry("F",  9   ),
            new KeyEntry("G",  10  ),
            new KeyEntry("H",  11  ),
            new KeyEntry("I",  12  ),
            new KeyEntry("J",  13  ),
            new KeyEntry("K",  14  ),
            new KeyEntry("L",  15  ),
            new KeyEntry("M",  16  ),
            new KeyEntry("N",  17  ),
            new KeyEntry("O",  18  ),
            new KeyEntry("P",  19  ),
            new KeyEntry("Q",  20  ),
            new KeyEntry("R",  21  ),
            new KeyEntry("S",  22  ),
            new KeyEntry("T",  23  ),
            new KeyEntry("U",  24  ),
            new KeyEntry("V",  25  ),
            new KeyEntry("W",  26  ),
            new KeyEntry("X",  27  ),
            new KeyEntry("Y",  28  ),
            new KeyEntry("Z",  29  ),
            new KeyEntry("1",  30  ),
            new KeyEntry("2",  31  ),
            new KeyEntry("3",  32  ),
            new KeyEntry("4",  33  ),
            new KeyEntry("5",  34  ),
            new KeyEntry("6",  35  ),
            new KeyEntry("7",  36  ),
            new KeyEntry("8",  37  ),
            new KeyEntry("9",  38  ),
            new KeyEntry("0",  39  ),
            new KeyEntry("Enter",  40  ),
            new KeyEntry("Esc",  41  ),
            new KeyEntry("Backspace",  42  ),
            new KeyEntry("Tab",  43  ),
            new KeyEntry("Space",  44  ),
            new KeyEntry("-",  45  ),
            new KeyEntry("=",  46  ),
            new KeyEntry("[",  47  ),
            new KeyEntry("]",  48  ),
            new KeyEntry(@"\",  49  ),
            new KeyEntry(";",  51  ),
            new KeyEntry("'",  52  ),
            new KeyEntry("~",  53  ),
            new KeyEntry(",",  54  ),
            new KeyEntry(".",  55  ),
            new KeyEntry("/",  56  ),
            new KeyEntry("F1",  58  ),
            new KeyEntry("F2",  59  ),
            new KeyEntry("F3",  60  ),
            new KeyEntry("F4",  61  ),
            new KeyEntry("F5",  62  ),
            new KeyEntry("F6",  63  ),
            new KeyEntry("F7",  64  ),
            new KeyEntry("F8",  65  ),
            new KeyEntry("F9",  66  ),
            new KeyEntry("F10",  67  ),
            new KeyEntry("F11",  68  ),
            new KeyEntry("F12",  69  ),
            new KeyEntry("Print Screen",  70  ),
            new KeyEntry("Right Arrow",  79  ),
            new KeyEntry("Left Arrow",  80  ),
            new KeyEntry("Down Arrow",  81  ),
            new KeyEntry("Up Arrow",  82  ),
            new KeyEntry("Ctrl",  240 ),
            new KeyEntry("Shift",  241 ),
            new KeyEntry("Alt",  242 ),
            new KeyEntry("Windows",  243 )
        };
    }

    public class KeyEntry
    {
        public string Name { get; private set; }
        public byte Code { get; private set; }
        public KeyEntry(string name, byte code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
