using System;
using Svelto.ECS.Internal;

namespace Svelto.ECS
{
    [Obsolete]
    public abstract class MultiEntityViewsEngine<T, U> : SingleEntityViewEngine<T>, IHandleEntityStructEngine<U>
        where U : class, IEntityStruct where T : class, IEntityStruct
    {
        public void AddInternal(in U entityView, ExclusiveGroup.ExclusiveGroupStruct? previousGroup)
        { Add(entityView, previousGroup); }
        public void RemoveInternal(in U entityView, bool itsaSwap)
        { Remove(entityView); }
        
        protected abstract void Add(U entityView, ExclusiveGroup.ExclusiveGroupStruct? previousGroup);
        protected abstract void Remove(U entityView);
    }

    [Obsolete]
    public abstract class MultiEntityViewsEngine<T, U, V> : MultiEntityViewsEngine<T, U>, IHandleEntityStructEngine<V>
        where V :  class, IEntityStruct where U :  class, IEntityStruct where T :  class, IEntityStruct
    {
        public void AddInternal(in V entityView, ExclusiveGroup.ExclusiveGroupStruct? previousGroup)
        { Add(entityView, previousGroup); }
        public void RemoveInternal(in V entityView, bool itsaSwap)
        { Remove(entityView); }
        
        protected abstract void Add(V entityView, ExclusiveGroup.ExclusiveGroupStruct? previousGroup);
        protected abstract void Remove(V entityView);
    }

    [Obsolete]
    public abstract class MultiEntityViewsEngine<T, U, V, W> : MultiEntityViewsEngine<T, U, V>, IHandleEntityStructEngine<W>
        where W :  class, IEntityStruct where V : class, IEntityStruct where U :  class, IEntityStruct where T : class, IEntityStruct
    {
        public void AddInternal(in W entityView, ExclusiveGroup.ExclusiveGroupStruct? previousGroup)
        { Add(entityView, previousGroup); }
        public void RemoveInternal(in W entityView, bool itsaSwap)
        { Remove(entityView); }
        
        protected abstract void Add(W entityView, ExclusiveGroup.ExclusiveGroupStruct? previousGroup);
        protected abstract void Remove(W entityView);
    }
}