using SMWControlLibCommons.Factory;
using SMWControlLibCommons.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Disguise
{
    /// <summary>
    /// The tile dirty collection disguise.
    /// </summary>
    public class TileDirtyCollectionDisguise<T, U> : DisguiseWithObjsParams<TileDirtyCollectionFactory<T,U>,TilesDirtyCollection<T,U>> where T : struct
                                                                                                                                        where U : struct
    {
    }
}
