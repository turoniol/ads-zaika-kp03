namespace MyGeneric
{
    class Deque<TData>
    {
        private LinkedList<TData> _list;

        public int Count 
        {
            get => _list.Count;
        }

        public Node<TData> Head
        {
            get => _list[0];
            set => _list[0].Data = value.Data;
        }

        public Node<TData> Tail
        {
            get => _list[Count - 1];
            set => _list[Count - 1].Data = value.Data;
        }

        public Deque() => _list = new LinkedList<TData>();

        public void InsertHead(TData value) => _list.AddFirst(value);

        public void RemoveHead() => _list.DeleteFirst();

        public void InsertTail(TData value) => _list.AddLast(value);

        public void RemoveTail() => _list.DeleteLast();
    
        public bool IsEmpty() => _list.IsEmpty();

        public override string ToString() => _list.ToString();
    }
}