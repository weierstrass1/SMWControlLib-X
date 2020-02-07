using SMWControlLibRendering.Delegates;
using SMWControlLibUtils;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The dirty collection.
    /// </summary>
    public abstract class DirtyCollection<K, E1, E2, T, U> : CanFactoryWithObjsParams
                                                                where K: DualKey<E1,E2>
                                                                where T: DirtyClass<U>
    {
        protected ConcurrentDictionary<K, T> elements;
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyCollection"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public DirtyCollection() : base()
        {
        }

        /// <summary>
        /// Dirties the.
        /// </summary>
        public virtual void Dirty()
        {
            _ = Parallel.ForEach(elements, kvp =>
            {
                kvp.Value.SetDirty(true);
            });
        }

        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public override void Initialize(params object[] args)
        {
            elements = new ConcurrentDictionary<K, T>();
        }

        /// <summary>
        /// Dirties the action.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="create">The create.</param>
        /// <param name="update">The update.</param>
        public T DirtyAction(K key, ActionWithReturnHanlder<T> create, Action<T> update)
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            T e;
            if (!elements.ContainsKey(key))
            {
                T newElement = create();
                _ = elements.TryAdd(key, newElement);
                e = elements[key];
                e.SetDirty(true);
            }
            else
            {
                e = elements[key];
            }

            if (e.IsDirty)
            {
                update(e);
                e.SetDirty(false);
            }

            return e;
        }
    }
}
