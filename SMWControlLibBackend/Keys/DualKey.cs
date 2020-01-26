namespace SMWControlLibBackend.Keys
{
    /// <summary>
    /// The dual key.
    /// </summary>
    public abstract class DualKey<T,U>
    {
        protected int hashCode;
        protected bool notCalculated;
        public T element1;
        public U element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="DualKey"/> class.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        protected DualKey(T e1,U e2)
        {
            element1 = e1;
            element2 = e2;
            notCalculated = true;
            hashCode = 0;
        }
        /// <summary>
        /// Calculates the hash code.
        /// </summary>
        /// <returns>An int.</returns>
        protected virtual int CalculateHashCode()
        {
            return element1.GetHashCode() ^ element2.GetHashCode();
        }
        /// <summary>
        /// Equals the.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                DualKey<T, U> t = (DualKey<T, U>)obj;
                return element1.Equals(t.element1) && element1.Equals(t.element2);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>An int.</returns>
        public override int GetHashCode()
        {
            if (notCalculated)
            {
                hashCode = CalculateHashCode();
                notCalculated = false;
            }

            return hashCode;
        }
    }
}
