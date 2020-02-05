namespace SMWControlLibUtils
{
    /// <summary>
    /// The dirty class.
    /// </summary>
    public class DirtyClass<T> : CanFactoryWithObjsParams
    {
        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        public T Object { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether is dirty.
        /// </summary>
        public bool IsDirty
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyClass"/> class.
        /// </summary>
        /// <param name="Ob">The ob.</param>
        public DirtyClass(T Ob) : base (Ob)
        {
        }

        /// <summary>
        /// Sets the dirty.
        /// </summary>
        /// <param name="d">If true, d.</param>
        public virtual void SetDirty(bool d)
        {
            IsDirty = d;
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        public override void Initialize(params object[] args)
        {
            Object = (T)args[0];
            IsDirty = true;
        }
    }
}
