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
    public abstract class DirtyCollection<TK, TE1, TE2, TD, TC> : CanFactoryWithObjsParams
                                                                where TK : DualKey<TE1, TE2>
                                                                where TD : DirtyClass<TC>
    {
        protected ConcurrentDictionary<TK, TD> elements;
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyCollection"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public DirtyCollection(params object[] args) : base(args)
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
            elements = new ConcurrentDictionary<TK, TD>();
        }

        /// <summary>
        /// Dirties the action.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="create">The create.</param>
        /// <param name="update">The update.</param>
        public TD DirtyAction(TK key, ActionWithReturnHanlder<TD> create, Action<TD> update)
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            TD e;
            if (!elements.ContainsKey(key))
            {
                TD newElement = create();
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
