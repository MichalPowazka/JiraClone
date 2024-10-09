namespace Core.Common
{
    public class BaseEntity<T>
    {
        public T? Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int? ModifyBy { get; set; }


    }
}
