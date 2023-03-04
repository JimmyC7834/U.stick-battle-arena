namespace Game
{
    public class ArraySelector<T>
    {
        private T[] _array;
        private int _currIdx;

        public ArraySelector(T[] array)
        {
            _array = array;
        }

        public T GetCurrent() => _array[_currIdx];

        public void Next()
        {
            _currIdx++;
            if (_currIdx == _array.Length)
                _currIdx = 0;
        }
        
        public void Prev()
        {
            _currIdx--;
            if (_currIdx == -1)
                _currIdx = _array.Length - 1;
        }
    }
}