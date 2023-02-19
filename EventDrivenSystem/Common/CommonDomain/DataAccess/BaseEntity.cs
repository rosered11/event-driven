namespace Rosered11.OrderService.Common.DataAccess
{
    public abstract class BaseEntity<ID>
    {
        public ID Id { get; private set; }

        public BaseEntity(ID id)
        {
            Id = id;
        }
    }
}