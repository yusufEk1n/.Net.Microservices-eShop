namespace Ordering.Domain.Common
{
    public abstract class ValueObject
    {
        /// <summary>
        /// check if two value objects are equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if ((left is null) ^ (right is null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        /// <summary>
        /// check if two value objects are not equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        /// <summary>
        /// get all the properties of the value object
        /// </summary>
        /// <returns>IEnumerable<object></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// check if two value objects are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// get the hash code of aggregated properties
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }
    }
}
