namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a component of a dialogue line.
	/// </summary>
	public interface IDialogueComponent
	{
		/// <summary>
		/// Serializes the dialogue component to a string.
		/// </summary>
		/// <returns>A string representation of the dialogue component.</returns>
		public string Serialize();

		/// <summary>
		/// Returns the plain text representation of the dialogue component.
		/// </summary>
		/// <returns>A plain text representation of the dialogue component.</returns>
		public string Plain() => "";
		public static List<IDialogueComponent> Deserialize(string str)
		{
			int p = 0;
			int labelContentEnd = 0;
			Stack<(string, int)> labelnamestack = new();
			Stack<List<IDialogueComponent>> componentstack = new();
			Stack<int> labelContentStack = new();
			componentstack.Push(new List<IDialogueComponent>());

			while (p < str.Length)
			{
				if (str[p] == '<')
				{
					labelContentEnd = p;
					p++;
					string labelName = "";
					string labelValue = "";
					bool labelclose = false;
					if (str[p] == '/')
					{
						p++;
						labelclose = true;
					}
					while (str[p] != '>' && str[p] != '=')
					{
						labelName += str[p];
						p++;
					}
					if (str[p++] == '=')
					{
						while (str[p] != '>')
						{
							p++;
							labelValue += str[p];
						}
						labelContentStack.Push(p + 1);
						p++;
					}
					if (closablelabels.Contains(labelName))
					{
						if (labelclose)
						{
							if (labelnamestack.TryPeek(out (string name, int start) top) && top.name == labelName)
							{
								labelnamestack.Pop();
								componentstack.Peek().Add(GetClosableDialogueComponent(labelName, componentstack.Pop(), labelValue));
								if (labelContentEnd < p)
								{
									componentstack.Peek().Add(new PlainTextComponent(str[labelContentStack.Pop()..labelContentEnd]));
								}
							}
						}
						else
						{
							labelnamestack.Push((labelName, p + 1));
							componentstack.Push([]);
						}
					}
				}
				else if (str[p] == '[')
				{
					p++;
					string labelName = "";
					while (str[p] != ']')
					{
						labelName += str[p];
						p++;
					}
					if (float.TryParse(labelName, out float value))
					{
						componentstack.Peek().Add(new SpeedComponent(value));
					}
					else if (singlelabels.Contains(labelName))
					{
						componentstack.Peek().Add(GetSingleComponent(labelName));
					}
				}
				else
				{
					labelContentEnd = p;
				}
				p++;
			}

			while (labelnamestack.Count > 0)
			{
				var (name, start) = labelnamestack.Pop();
				componentstack.Peek().Add(new PlainTextComponent(str[start..]));
			}

			return componentstack.Pop();
		}
		private static IDialogueComponent GetClosableDialogueComponent(string name, List<IDialogueComponent> Content, string? arg = null)
		{
			ITextDialogueComponent component = name switch
			{
				"color" => arg is null ? new ColorComponent() : new(RDColor.FromRgba(arg)),
				"speed" => arg is null ? new SpeedComponent() : new(float.Parse(arg)),
				"volume" => arg is null ? new VolumeComponent() : new(float.Parse(arg)),
				"pitch" => arg is null ? new PitchComponent() : new(float.Parse(arg)),
				"pitchRange" => arg is null ? new PitchRangeComponent() : new(float.Parse(arg)),
				"shake" => new ShakeComponent(),
				"shakeRadius" => arg is null ? new ShakeRadiusComponent() : new(float.Parse(arg)),
				"wave" => new WaveComponent(),
				"waveHeight" => arg is null ? new WaveHeightComponent() : new(float.Parse(arg)),
				"waveSpeed" => arg is null ? new WaveSpeedComponent() : new(float.Parse(arg)),
				"swirl" => new SwirlComponent(),
				"swirlRadius" => arg is null ? new SwirlRadiusComponent() : new(float.Parse(arg)),
				"swirlSpeed" => arg is null ? new SwirlSpeedComponent() : new(float.Parse(arg)),
				"sticky" => new StickyComponent(),
				"loud" => new LoudComponent(),
				"bold" => new BoldComponent(),
				"whisper" => new WhisperComponent(),
				_ => throw new ArgumentException("Invalid component name."),
			};
			component.Components = Content;
			return component;
		}

		private static IDialogueComponent GetSingleComponent(string name)
		{
			return name switch
			{
				"static" => new StaticComponent(),
				"flash" => new FlashComponent(),
				"vslow" => new VerySlowSpeedComponent(),
				"slow" => new SlowSpeedComponent(),
				"normal" => new NormalSpeedComponent(),
				"fast" => new FastSpeedComponent(),
				"vfast" => new VeryFastSpeedComponent(),
				"vvvfast" => new VeryVeryFastSpeedComponent(),
				"excited" => new ExcitedComponent(),
				"shout" => new ShoutComponent(),
				_ => throw new ArgumentException("Invalid component name."),
			};
		}

		private static string[] closablelabels =
			[
			"color",
			"speed",
			"volume",
			"pitch",
			"pitchRange",
			"shake",
			"shakeRadius",
			"wave",
			"waveHeight",
			"waveSpeed",
			"swirl",
			"swirlRadius",
			"swirlSpeed",
			"sticky",
			"loud",
			"bold",
			"whisper",
			];
		private static string[] singlelabels =
			[
			"static",
			"flash",
			"vslow",
			"slow",
			"normal",
			"fast",
			"vfast",
			"vvvfast",
			"excited",
			"shout",
			];
	}
}
