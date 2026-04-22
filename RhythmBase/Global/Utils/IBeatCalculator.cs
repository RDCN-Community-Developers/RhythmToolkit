using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Utils
{
    internal interface IBeatCalculator<TSelf, TLevel>
        where TSelf : IBeatCalculator<TSelf, TLevel>
        where TLevel : ILevel<TLevel, TSelf>
    {
    }
}
