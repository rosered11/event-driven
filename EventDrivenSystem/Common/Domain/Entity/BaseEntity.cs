namespace Rosered11.Common.Domain.Entity
{
    public abstract class BaseEntity<T>
    {
        public T? ID { get; protected set; }
        public BaseEntity(T id){
            ID = id;
        }
        public override bool Equals(object? obj)
        {
            if (ID == null || obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            BaseEntity<T> that = (BaseEntity<T>)obj;
            return ID.Equals(that.ID);
        }
        
        public override int GetHashCode()
        {
            if (ID == null)
                return 0;
            return ID.GetHashCode();
        }
    }
}