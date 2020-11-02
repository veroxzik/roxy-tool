using System;

namespace Roxy.Lib
{
    [Flags]
    public enum BoardType
    {
        Roxy = 0x01,
        arcinRoxy = 0x02,
        arcin = 0x04
    }

    public enum SpoofType
    {
        None,
        IIDX,
        SDVX
    }
}
