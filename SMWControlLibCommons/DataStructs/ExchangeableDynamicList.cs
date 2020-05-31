using System;

namespace SMWControlLibCommons.DataStructs
{
    /// <summary>
    /// The exchangeable dynamic list.
    /// </summary>
    public abstract class ExchangeableDynamicList<T> where T : class
    {
        protected T[] elements;
        /// <summary>
        /// Gets or sets the lenght.
        /// </summary>
        public int Lenght { get; protected set; }
        public event Action<ExchangeableDynamicList<T>, int> OnFrameAdded, OnFrameRemoved, OnExchangeUp, OnExchangeDown;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeableDynamicList"/> class.
        /// </summary>
        protected ExchangeableDynamicList()
        {
            elements = new T[4];
            Lenght = 0;
        }
        public virtual T this[int index]
        {
            get => elements[index];
            private set => elements[index] = value;
        }
        /// <summary>
        /// Exchanges the frame up.
        /// </summary>
        /// <param name="ind">The ind.</param>
        public virtual void ExchangeFrameUp(int ind)
        {
            if (Lenght <= 0 || ind == Lenght - 1) return;

            T aux = elements[ind];
            elements[ind] = elements[ind + 1];
            elements[ind + 1] = aux;
            OnExchangeUp?.Invoke(this, ind);
        }
        /// <summary>
        /// Exchanges the frame down.
        /// </summary>
        /// <param name="ind">The ind.</param>
        public virtual void ExchangeFrameDown(int ind)
        {
            if (Lenght <= 0 || ind == 0) return;

            T aux = elements[ind];
            elements[ind] = elements[ind - 1];
            elements[ind - 1] = aux;
            OnExchangeDown?.Invoke(this, ind);
        }
        /// <summary>
        /// Adds the.
        /// </summary>
        public virtual void Add(T newElement)
        {
            if (elements.Length <= Lenght)
            {
                T[] newFrames = new T[Lenght + 4];
                elements.CopyTo(newFrames, 0);
                elements = newFrames;
            }
            elements[Lenght] = newElement;
            Lenght++;
            OnFrameAdded?.Invoke(this, Lenght - 1);
        }
        /// <summary>
        /// Removes the.
        /// </summary>
        /// <param name="ind">The ind.</param>
        public virtual void Remove(int ind)
        {
            if (Lenght <= 0) return;

            if (ind == Lenght - 1)
            {
                elements[ind] = null;
            }
            else
            {
                Buffer.BlockCopy(elements, ind + 1, elements, ind, Lenght - ind - 1);
            }
            Lenght--;
            OnFrameRemoved?.Invoke(this, ind);
        }
    }
}
