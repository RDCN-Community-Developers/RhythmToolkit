﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Converters
{
	internal class MilliSecondConverter : TimeConverter
	{
		public MilliSecondConverter() : base(TimeType.MiliSecond)
		{
		}
	}
}