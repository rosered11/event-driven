namespace Common.CommonDomain.Entities
{
    public abstract class BaseEntity<ID>
    {
        private ID? _id;
        public ID? Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
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
            else
            {
                if (_id == null) 
                    return false;
                else
                {
                    var prop = obj.GetType().GetProperty("Id");
                    if (prop == null)
                        return false;
                    else
                        return _id.GetHashCode() == prop?.GetValue(obj)?.GetHashCode();
                }
            }
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            // throw new System.NotImplementedException();
            return _id?.GetHashCode() ?? 0;
        }
    }
}