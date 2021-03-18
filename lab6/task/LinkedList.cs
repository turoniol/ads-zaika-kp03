namespace MyGeneric
{
    class Node<TData>
    {
        public TData Data { get; set; }
        public Node<TData> prev;
        public Node<TData> next;
        public Node(TData data)
        {
          prev = null;
          next = null;
          Data = data;
        }
    }

    class LinkedList<TData>
    {
        private int _size;

        private Node<TData> _head;

        private Node<TData> _tail;

        public int Count
        {
          get 
          {
            return _size;
          }
        }

        public Node<TData> this[int index]
        {
            get 
            {
                if (!(index >= 0 && index < _size))
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                var current = _head;
                for (int i = 0; i < index; ++i)
                {
                    current = current.next;
                }
                return current;
            }
            set
            {
                if (!(index >= 0 && index < _size))
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                var current = this[index];
                current.Data = value.Data;
            }
        }

        public LinkedList(TData data)
        {
            _head = new Node<TData>(data);
            AddFirstElement(_head);
        }

        public LinkedList()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        private void AddFirstElement(Node<TData> node)
        {
            if (!IsEmpty())
            {
                throw new System.Exception("List isn't empty!");
            }
            _head = node;
            _tail = node;
            _head.next = _tail;
            _tail.prev = _head;
            _size = 1;
        }

        public void AddFirst(TData data)
        {
            Node<TData> node = new Node<TData>(data);
            if (IsEmpty())
            {
                AddFirstElement(node);
                return;
            }
            node.next = _head;
            _head.prev = node;
            _head = node;
            _size += 1;
        }

        public void AddLast(TData data)
        {
            Node<TData> node = new Node<TData>(data);
            if (IsEmpty())
            {
                AddFirstElement(node);
                return;
            }
            node.prev = _tail;
            _tail.next = node;
            _tail = node;
            _size += 1;
        }

        public void DeleteFirst()
        {
            if (IsEmpty())
            {
                return;
            }

            _size -= 1;
            if (_tail == _head)
            {
                _tail = null;
                _head = null;
                return;
            }
            _head = _head.next;
            _head.prev = null;
        }

        public void DeleteLast()
        {
            if (IsEmpty())
            {
                return;
            }
            _size -= 1;
            if (_tail == _head)
            {
                _tail = null;
                _head = null;
                return;
            }

            _tail = _tail.prev;
            _tail.next = null;
        }

        public void DeleteAtPos(int pos)
        {
            if (IsEmpty())
            {
                return;
            }
            if (pos == 0)
            {
                DeleteFirst();
                return;
            }
            else if (pos == _size - 1)
            {
                DeleteLast();
                return;
            }

            var current = this[pos];

            current.prev.next = current.next;
            current.next.prev = current.prev;

            _size -= 1;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < Count; ++i)
            {
                if (i == 0)
                {
                    sb.Append("[");
                }
                if (i == Count - 1)
                {
                    sb.Append($"{this[i].Data}]");
                    break;
                }
                sb.Append($"{this[i].Data}, ");
            }
            return sb.ToString();
        }
    }
}