namespace Gep13.WindowsPhone.VBForumsMetro.Models
{
    public class BaseModel<T> : IBaseModel
        where T : BaseModel<T>
    {
        /// <summary>
        /// Standard Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Make it easy for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is T && ((T)obj).Id.Equals(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} with Id {1}", typeof(T).FullName, Id);
        }
    }
}