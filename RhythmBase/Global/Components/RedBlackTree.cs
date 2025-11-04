using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Components
{
	internal class RedBlackNode<TKey, TValue> where TKey : IComparable<TKey>
	{
		public TKey Key;
		public TValue Value;
		public RedBlackNode<TKey, TValue>? Left;
		public RedBlackNode<TKey, TValue>? Right;
		public RedBlackNode<TKey, TValue>? Parent;
		public bool IsRed;
		public RedBlackNode(TKey key, TValue value)
		{
			Key = key;
			Value = value;
			Left = null;
			Right = null;
			Parent = null;
			IsRed = true; // New nodes are always red
		}
	}
	internal class RedBlackTree<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
	{
		private RedBlackNode<TKey, TValue>? _root;
		private int _count;
		public bool ContainsKey(TKey key)
		{
			var node = FindNode(key);
			return node != null;
		}

		public RedBlackNode<TKey, TValue>? FindNode(TKey key)
		{
			var current = _root;
			while (current != null)
			{
				int cmp = key.CompareTo(current.Key);
				if (cmp == 0)
					return current;
				current = cmp < 0 ? current.Left : current.Right;
			}
			return null;
		}

		public RedBlackNode<TKey, TValue>? Predecessor(RedBlackNode<TKey, TValue> node)
		{
			if (node.Left != null)
			{
				var pred = node.Left;
				while (pred.Right != null)
					pred = pred.Right;
				return pred;
			}
			var parent = node.Parent;
			var current = node;
			while (parent != null && current == parent.Left)
			{
				current = parent;
				parent = parent.Parent;
			}
			return parent;
		}

		public RedBlackNode<TKey, TValue>? Successor(RedBlackNode<TKey, TValue> node)
		{
			if (node.Right != null)
			{
				var succ = node.Right;
				while (succ.Left != null)
					succ = succ.Left;
				return succ;
			}
			var parent = node.Parent;
			var current = node;
			while (parent != null && current == parent.Right)
			{
				current = parent;
				parent = parent.Parent;
			}
			return parent;
		}

		public void Insert(TKey key, TValue value)
		{
			var newNode = new RedBlackNode<TKey, TValue>(key, value);
			RedBlackNode<TKey, TValue>? parent = null;
			var current = _root;
			while (current != null)
			{
				parent = current;
				int cmp = key.CompareTo(current.Key);
				if (cmp < 0)
					current = current.Left;
				else if (cmp > 0)
					current = current.Right;
				else
				{
					current.Value = value;
					return;
				}
			}
			newNode.Parent = parent;
			if (parent == null)
				_root = newNode;
			else if (key.CompareTo(parent.Key) < 0)
				parent.Left = newNode;
			else
				parent.Right = newNode;
			_count++;
			InsertFixup(newNode);
		}

		private void InsertFixup(RedBlackNode<TKey, TValue> node)
		{
			while (node.Parent != null && node.Parent.IsRed)
			{
				var grandparent = node.Parent.Parent;
				if (node.Parent == grandparent?.Left)
				{
					var uncle = grandparent?.Right;
					if (uncle != null && uncle.IsRed)
					{
						node.Parent.IsRed = false;
						uncle.IsRed = false;
						if (grandparent != null)
							grandparent.IsRed = true;
						node = grandparent!;
					}
					else
					{
						if (node == node.Parent.Right)
						{
							node = node.Parent;
							RotateLeft(node);
						}
						if (node.Parent != null)
							node.Parent.IsRed = false;
						if (grandparent != null)
						{
							grandparent.IsRed = true;
							RotateRight(grandparent);
						}
					}
				}
				else
				{
					var uncle = grandparent?.Left;
					if (uncle != null && uncle.IsRed)
					{
						node.Parent.IsRed = false;
						uncle.IsRed = false;
						if (grandparent != null)
							grandparent.IsRed = true;
						node = grandparent!;
					}
					else
					{
						if (node == node.Parent.Left)
						{
							node = node.Parent;
							RotateRight(node);
						}
						if (node.Parent != null)
							node.Parent.IsRed = false;
						if (grandparent != null)
						{
							grandparent.IsRed = true;
							RotateLeft(grandparent);
						}
					}
				}
			}
			if (_root != null)
				_root.IsRed = false;
		}

		private void RotateLeft(RedBlackNode<TKey, TValue> node)
		{
			var right = node.Right;
			if (right == null) return;
			node.Right = right.Left;
			if (right.Left != null)
				right.Left.Parent = node;
			right.Parent = node.Parent;
			if (node.Parent == null)
				_root = right;
			else if (node == node.Parent.Left)
				node.Parent.Left = right;
			else
				node.Parent.Right = right;
			right.Left = node;
			node.Parent = right;
		}

		private void RotateRight(RedBlackNode<TKey, TValue> node)
		{
			var left = node.Left;
			if (left == null) return;
			node.Left = left.Right;
			if (left.Right != null)
				left.Right.Parent = node;
			left.Parent = node.Parent;
			if (node.Parent == null)
				_root = left;
			else if (node == node.Parent.Right)
				node.Parent.Right = left;
			else
				node.Parent.Left = left;
			left.Right = node;
			node.Parent = left;
		}

		public bool Remove(TKey key)
		{
			var node = FindNode(key);
			if (node == null)
				return false;
			DeleteNode(node);
			_count--;
			return true;
		}

		private void DeleteNode(RedBlackNode<TKey, TValue> node)
		{
			RedBlackNode<TKey, TValue>? y = node;
			bool yOriginalIsRed = y.IsRed;
			RedBlackNode<TKey, TValue>? x;
			if (node.Left == null)
			{
				x = node.Right;
				Transplant(node, node.Right);
			}
			else if (node.Right == null)
			{
				x = node.Left;
				Transplant(node, node.Left);
			}
			else
			{
				y = Minimum(node.Right);
				yOriginalIsRed = y.IsRed;
				x = y.Right;
				if (y.Parent == node)
				{
					if (x != null)
						x.Parent = y;
				}
				else
				{
					Transplant(y, y.Right);
					y.Right = node.Right;
					if (y.Right != null)
						y.Right.Parent = y;
				}
				Transplant(node, y);
				y.Left = node.Left;
				if (y.Left != null)
					y.Left.Parent = y;
				y.IsRed = node.IsRed;
			}
			if (!yOriginalIsRed)
				DeleteFixup(x, node.Parent);
		}

		private void DeleteFixup(RedBlackNode<TKey, TValue>? x, RedBlackNode<TKey, TValue>? parent)
		{
			while (x != _root && (x == null || !x.IsRed))
			{
				if (parent == null)
					break;
				if (x == parent.Left)
				{
					var w = parent.Right;
					if (w != null && w.IsRed)
					{
						w.IsRed = false;
						parent.IsRed = true;
						RotateLeft(parent);
						w = parent.Right;
					}
					if ((w?.Left == null || !w.Left.IsRed) && (w?.Right == null || !w.Right.IsRed))
					{
						if (w != null)
							w.IsRed = true;
						x = parent;
						parent = x.Parent;
					}
					else
					{
						if (w?.Right == null || !w.Right.IsRed)
						{
							if (w?.Left != null)
								w.Left.IsRed = false;
							if (w != null)
								w.IsRed = true;
							if (w != null)
								RotateRight(w);
							w = parent.Right;
						}
						if (w != null)
							w.IsRed = parent.IsRed;
						parent.IsRed = false;
						if (w?.Right != null)
							w.Right.IsRed = false;
						RotateLeft(parent);
						x = _root;
					}
				}
				else
				{
					var w = parent.Left;
					if (w != null && w.IsRed)
					{
						w.IsRed = false;
						parent.IsRed = true;
						RotateRight(parent);
						w = parent.Left;
					}
					if ((w?.Right == null || !w.Right.IsRed) && (w?.Left == null || !w.Left.IsRed))
					{
						if (w != null)
							w.IsRed = true;
						x = parent;
						parent = x.Parent;
					}
					else
					{
						if (w?.Left == null || !w.Left.IsRed)
						{
							if (w?.Right != null)
								w.Right.IsRed = false;
							if (w != null)
								w.IsRed = true;
							if (w != null)
								RotateLeft(w);
							w = parent.Left;
						}
						if (w != null)
							w.IsRed = parent.IsRed;
						parent.IsRed = false;
						if (w?.Left != null)
							w.Left.IsRed = false;
						RotateRight(parent);
						x = _root;
					}
				}
			}
			if (x != null)
				x.IsRed = false;
		}

		private void Transplant(RedBlackNode<TKey, TValue> u, RedBlackNode<TKey, TValue>? v)
		{
			if (u.Parent == null)
				_root = v;
			else if (u == u.Parent.Left)
				u.Parent.Left = v;
			else
				u.Parent.Right = v;
			if (v != null)
				v.Parent = u.Parent;
		}

		private RedBlackNode<TKey, TValue> Minimum(RedBlackNode<TKey, TValue> node)
		{
			while (node.Left != null)
				node = node.Left;
			return node;
		}

		public TValue this[TKey key]
		{
			get
			{
				var node = FindNode(key);
				if (node == null)
					throw new KeyNotFoundException();
				return node.Value;
			}
			set
			{
				Insert(key, value);
			}
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var node = FindNode(key);
			if (node != null)
			{
				value = node.Value;
				return true;
			}
			value = default!;
			return false;
		}

		#region ICollection<KeyValuePair<TKey, TValue>> 实现

		public int Count => _count;

		public bool IsReadOnly => false;

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Insert(item.Key, item.Value);
		}

		public void Clear()
		{
			_root = null;
			_count = 0;
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			var node = FindNode(item.Key);
			if (node == null)
				return false;
			return EqualityComparer<TValue>.Default.Equals(node.Value, item.Value);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			if (array.Length - arrayIndex < _count)
				throw new ArgumentException("目标数组空间不足。");

			int i = arrayIndex;
			foreach (var kv in this)
			{
				array[i++] = kv;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			var node = FindNode(item.Key);
			if (node == null)
				return false;
			if (!EqualityComparer<TValue>.Default.Equals(node.Value, item.Value))
				return false;
			DeleteNode(node);
			_count--;
			return true;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return InOrderTraversal(_root).GetEnumerator();
		}
		private IEnumerable<KeyValuePair<TKey, TValue>> InOrderTraversal(RedBlackNode<TKey, TValue>? node)
		{
			if (node == null)
				yield break;
			foreach (var kv in InOrderTraversal(node.Left))
				yield return kv;
			yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
			foreach (var kv in InOrderTraversal(node.Right))
				yield return kv;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
