namespace ASDLib
{
    public class Entry<T, U>
    {
        public T key{get; private set;}
        public U value {get; private set;}
        
        public Entry(T key, U value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{key} : {value}";
        }
    }
}