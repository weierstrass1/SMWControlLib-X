using SMWControlLibCommons.Graphics;

namespace SMWControlLibCommons.Delegates
{
    public delegate TileMaskCollection<T, U> SelectionHandler<T, U>() where T : struct
                                                                        where U : struct;
}
