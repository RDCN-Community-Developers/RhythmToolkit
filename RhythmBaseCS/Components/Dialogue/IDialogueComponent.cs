namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a component of a dialogue line.
	/// </summary>
	public interface IDialogueComponent
	{
		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		public string Name { get; }
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
		/// <summary>
		/// Deserializes a string into a list of dialogue components.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static List<IDialogueComponent> Deserialize(string str)
		{
			List<IDialogueComponent> dialogues = [];
			var stack = new Stack<ITextDialogueComponent>();
			int index = 0;
			void AddComponent(IDialogueComponent component)
			{
				if (stack.Count > 0)
					stack.Peek().Components.Add(component);
				else
					dialogues.Add(component);
			}
			while (index < str.Length)
			{
				Console.WriteLine(str[0..index] + ", " + index);
				int tagEnd1 = str.IndexOf('>', index + 1);
				int tagEnd2 = str.IndexOf(']', index + 1);
				if (str[index] == '<')
					//&& tagEnd1 > 0
					//&& (str.IndexOf('<', index + 1) <= 0 || str.IndexOf('<', index + 1) > tagEnd1))
				{
					// Check if it's a closing tag
					bool isClosingTag = index + 1 < str.Length && str[index + 1] == '/';
					if (isClosingTag)
					{
						// Handle closing tag
						index += 2; // Skip '</'
						if (tagEnd1 == -1) break; // Invalid tag, ignore

						string tagName = str[index..tagEnd1];
						index = tagEnd1 + 1; // Skip '>'

						// Pop the stack if the tag matches
						if (stack.Count > 0 && stack.Peek().Name == tagName)
							stack.Pop();
						else if (stack.Count > 0 && stack.FirstOrDefault(i => i.Name == tagName) is ITextDialogueComponent dialogueComponent)
						{
							Stack<ITextDialogueComponent> tempStack = [];
							while (stack.Peek() != dialogueComponent)
								tempStack.Push(stack.Pop());
							stack.Pop();
							while (tempStack.Count > 0)
							{
								ITextDialogueComponent component = tempStack.Pop().Clone([]);
								if (stack.Count > 0)
								{
									stack.Peek().Components = [component];
									stack.Push(component);
								}
								else
								{
									dialogues.Add(component);
									stack.Push(component);
								}
							}
							// Ignore unmatched closing tags
						}
						else
							AddComponent(new PlainTextComponent($"</{tagName}>"));
						// Ignore unmatched closing tags
					}
					else
					{
						index++;
						if (tagEnd1 == -1)
						{
							// 如果标签未正确闭合（如 <bold过得），将其视为普通文本
							string invalidTagText = str[(index - 1)..]; // 包括 '<'
							AddComponent(new PlainTextComponent(invalidTagText));
						}

						string tag = str[index..tagEnd1];
						index = tagEnd1 + 1; // Skip '>'

						// Parse tag name and value (if any)
						string tagName = tag;
						string? tagValue = null;
						int equalsIndex = tag.IndexOf('=');
						if (equalsIndex != -1)
						{
							tagName = tag[..equalsIndex];
							tagValue = tag[(equalsIndex + 1)..];
						}

						// Create the component based on the tag
						IDialogueComponent? newComponent = GetClosableDialogueComponent(tagName, tagValue);
						if (newComponent is ITextDialogueComponent newComponent2)
						{
							AddComponent(newComponent2);
							stack.Push(newComponent2);
						}
						else
						{
							newComponent = new PlainTextComponent($"<{tagName}>");
							AddComponent(newComponent);
						}
					}
				}
				else if (str[index] == '[')
					//&& tagEnd1 > 0
					//&& (str.IndexOf('[', index + 1) <= 0 || str.IndexOf('[', index + 1) > tagEnd1))
				{
					// Handle bracket-style tags (e.g., [vfast])
					int tagEnd = str.IndexOf(']', index);
					if (tagEnd == -1) break; // Invalid tag, ignore

					string tag = str[(index + 1).. (tagEnd - index - 1)]; // Skip '[' and ']'
					index = tagEnd + 1; // Skip ']'

					// Create the component based on the tag
					IDialogueComponent? newComponent = GetSingleComponent(tag) ?? new PlainTextComponent($"[{tag}]");

					// Add the new component to the current component
					AddComponent(newComponent);
				}
				else
				{
					// Handle plain text
					int nextTagStart = str.IndexOfAny(['<', '['], index);
					if (nextTagStart == -1) nextTagStart = str.Length;

					string text = str[index..nextTagStart];
					index = nextTagStart;

					// Add the text to the current component
					AddComponent(new PlainTextComponent(text));
				}
			}

			// Close any unclosed tags
			while (stack.Count > 0)
			{
				var component = stack.Pop();
			}

			return dialogues;
		}
		private static ITextDialogueComponent? GetClosableDialogueComponent(string name, List<IDialogueComponent> Content, string? arg = null)
		{
			ITextDialogueComponent? textDialogueComponent = GetClosableDialogueComponent(name, arg);
			if (textDialogueComponent is not null)
				textDialogueComponent.Components = Content;
			return textDialogueComponent;
		}
		private static ITextDialogueComponent? GetClosableDialogueComponent(string name, string? arg = null)
		{
			return name switch
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
				_ => null,
			};
		}

		private static IDialogueComponent? GetSingleComponent(string name)
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
				_ => null,
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
