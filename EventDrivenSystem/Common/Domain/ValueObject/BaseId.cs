namespace Rosered11.Common.Domain.ValueObject
{
    public abstract class BaseId<T>
    {
        private readonly T _value;
        protected BaseId(T value)
        {
            _value = value;
        }
        public T GetValue() => _value;

        public override bool Equals(object? obj)
        {
            if (_value == null || obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var that = (BaseId<T>)obj;
            return _value.Equals(that._value);
        }
        
        public override int GetHashCode()
        {
            if (_value == null)
                return 0;
            return _value.GetHashCode();
        }
    }
}