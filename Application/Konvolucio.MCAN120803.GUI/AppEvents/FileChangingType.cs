// -----------------------------------------------------------------------
// <copyright file="FileChangingType.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Egy általános fájlon az alábbi események következhetnek be...
    /// </summary>
    public enum FileChangingType
    {
        ContentChanged,

        Saving,
        SaveComplete,
        SaveCorrupted,

        Loading,
        LoadComplete,
        LoadCorrupted,

        UnLoading,
        /*Célszerű hasnzálni pl. akkor ha minden elfogyott és valami esemény kell arra, hogy mást már nem tud mutatni csak egy alaphelyzetet.*/
        UnLoadComplete,
    }
}
