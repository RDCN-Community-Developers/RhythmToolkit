using RhythmBase.RhythmDoctor.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components
{
	public struct CpuTypeGroup()
	{
		public readonly CpuType this[int index]
		{
			get => _cpuTypes[index];
			set => _cpuTypes[index] = value;
		}
		private readonly CpuType[] _cpuTypes = new CpuType[16];
	}
}
