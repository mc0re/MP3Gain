using System;


namespace Mp3GainLib
{
    [Flags]
    public enum Id3v2Flags : byte
    {
        Footer = 0x10,
        Expr = 0x20,
        ExtHdr = 0x40,
        Unsync = 0x80
    }
}