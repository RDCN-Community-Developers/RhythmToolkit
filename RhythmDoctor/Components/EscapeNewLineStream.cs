namespace RhythmBase.RhythmDoctor.Components
{
	internal class EscapeNewLineStream : Stream
	{
		private readonly Stream _inner;
		private readonly byte[] _buffer;
		private bool _inQuotes = false;
		private bool _prevIsEscape = false;
		private int _peeked = -1; // 用于缓存下一个字节

		public EscapeNewLineStream(Stream inner)
		{
			_inner = inner;
			_buffer = new byte[1];
		}
		public override bool CanRead => true;
		public override bool CanSeek => false;
		public override bool CanWrite => false;
		public override long Length => throw new NotSupportedException();
		public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
		public override void Flush() { }
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count == 0) return 0;
			int written = 0;

			static bool IsQuote(byte b) => b == (byte)'"';
			static bool IsEscape(byte b) => b == (byte)'\\';
			static bool IsCR(byte b) => b == 0x0D;
			static bool IsLF(byte b) => b == 0x0A;
			static bool IsTab(byte b) => b == (byte)'\t';

			while (written < count)
			{
				byte b;
				if (_peeked >= 0)
				{
					b = (byte)_peeked;
					_peeked = -1;
				}
				else
				{
					int read = _inner.Read(_buffer, 0, 1);
					if (read == 0)
					{
						break; // EOF
					}
					b = _buffer[0];
				}
				if (IsQuote(b) && !_prevIsEscape)
				{
					_inQuotes = !_inQuotes;
					_prevIsEscape = false;
					buffer[offset + written++] = b;
				}
				else if(IsTab(b) && _inQuotes)
				{
					// \t -> \\t
					if (written + 2 > count)
					{
						_peeked = b;
						break;
					}
					buffer[offset + written++] = (byte)'\\';
					buffer[offset + written++] = (byte)'t';
					_prevIsEscape = false;
				}
				else if (IsEscape(b) && _inQuotes)
				{
					_prevIsEscape = !_prevIsEscape;
					buffer[offset + written++] = b;
				}
				else if (_inQuotes && IsCR(b))
				{
					// 可能是 \r\n
					int read = _inner.Read(_buffer, 0, 1);
					if (read == 0) break;
					if (read > 0 && IsLF(_buffer[0]))
					{
						// \r\n -> \\r\\n
						if (written + 4 > count)
						{
							_peeked = _buffer[0];
							break;
						}
						buffer[offset + written++] = (byte)'\\';
						buffer[offset + written++] = (byte)'r';
						buffer[offset + written++] = (byte)'\\';
						buffer[offset + written++] = (byte)'n';
					}
					else
					{
						// 只有 \r
						if (written + 2 > count)
						{
							if (read > 0) _peeked = _buffer[0];
							break;
						}
						buffer[offset + written++] = (byte)'\\';
						buffer[offset + written++] = (byte)'r';
						if (read > 0) _peeked = _buffer[0];
					}
					_prevIsEscape = false;
				}
				else if (_inQuotes && IsLF(b))
				{
					// \n -> \\n
					if (written + 2 > count)
						break;
					buffer[offset + written++] = (byte)'\\';
					buffer[offset + written++] = (byte)'n';
					_prevIsEscape = false;
				}
				else
				{
					buffer[offset + written++] = b;
					_prevIsEscape = false;
				}
			}
			return written > 0 ? written : 0;
		}
		public override async Task<int> ReadAsync(byte[] buffer,
			int offset,
			int count,
			CancellationToken ct = default)
		{
			if (count == 0) return 0;
			int written = 0;

			static bool IsQuote(byte b) => b == (byte)'"';
			static bool IsEscape(byte b) => b == (byte)'\\';
			static bool IsCR(byte b) => b == 0x0D;
			static bool IsLF(byte b) => b == 0x0A;
			static bool IsTab(byte b) => b == (byte)'\t';

			while (written < count)
			{
				byte b;
				if (_peeked >= 0)
				{
					b = (byte)_peeked;
					_peeked = -1;
				}
				else
				{
					int read = await _inner.ReadAsync(_buffer, 0, 1, ct);
					if (read == 0)
					{
						break; // EOF
					}
					b = _buffer[0];
				}
				if (IsQuote(b) && !_prevIsEscape)
				{
					_inQuotes = !_inQuotes;
					_prevIsEscape = false;
					buffer[offset + written++] = b;
				}
				else if(IsTab(b) && _inQuotes)
				{
					// \t -> \\t
					if (written + 2 > count)
					{
						_peeked = b;
						break;
					}
					buffer[offset + written++] = (byte)'\\';
					buffer[offset + written++] = (byte)'t';
					_prevIsEscape = false;
				}
				else if (IsEscape(b) && _inQuotes)
				{
					_prevIsEscape = !_prevIsEscape;
					buffer[offset + written++] = b;
				}
				else if (_inQuotes && IsCR(b))
				{
					// 可能是 \r\n
					int read = await _inner.ReadAsync(_buffer, 0, 1, ct);
					if (read == 0) break;
					if (read > 0 && IsLF(_buffer[0]))
					{
						// \r\n -> \\r\\n
						if (written + 4 > count)
						{
							_peeked = _buffer[0];
							break;
						}
						buffer[offset + written++] = (byte)'\\';
						buffer[offset + written++] = (byte)'r';
						buffer[offset + written++] = (byte)'\\';
						buffer[offset + written++] = (byte)'n';
					}
					else
					{
						// 只有 \r
						if (written + 2 > count)
						{
							if (read > 0) _peeked = _buffer[0];
							break;
						}
						buffer[offset + written++] = (byte)'\\';
						buffer[offset + written++] = (byte)'r';
						if (read > 0) _peeked = _buffer[0];
					}
					_prevIsEscape = false;
				}
				else if (_inQuotes && IsLF(b))
				{
					// \n -> \\n
					if (written + 2 > count)
						break;
					buffer[offset + written++] = (byte)'\\';
					buffer[offset + written++] = (byte)'n';
					_prevIsEscape = false;
				}
				else
				{
					buffer[offset + written++] = b;
					_prevIsEscape = false;
				}
			}
			return written > 0 ? written : 0;
		}
		public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
		public override void SetLength(long value) => throw new NotSupportedException();
		public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => throw new NotSupportedException();
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_inner.Dispose();
			base.Dispose(disposing);
		}
	}
}
