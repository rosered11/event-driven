namespace Common.CommonDomain.ValueObject
{
    public abstract class BaseId<T>
    {
        private readonly T? value;
        protected BaseId(T? value)
        {
            this.value = value;
        }
        public T? GetValue() => this.value;

        // override object.Equals
        public override bool Equals(object? obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var prop = obj.GetType().GetProperty("value");
            if (prop == null)
                return false;
            else
            {
                if (this.value == null)
                    return false;
                else
                    return this.value.Equals(prop.GetValue(obj));
            }
                
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            // throw new System.NotImplementedException();
            return this.value?.GetHashCode() ?? 0;
        }
    }
}