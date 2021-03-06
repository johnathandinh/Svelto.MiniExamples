namespace Svelto.ECS
{
    public interface IEntitiesDB: IObsoleteInterfaceDb
    {
        /// <summary>
        /// ECS is meant to work on a set of Entities. Working on a single entity is sometime necessary, but using
        /// the following functions inside a loop would be a mistake as performance can be significantly impacted
        /// return the buffer and the index of the entity inside the buffer using the input EGID
        /// </summary>
        /// <param name="entityGid"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool TryQueryEntitiesAndIndex<T>(uint  id, uint group, out uint index, out T[] array) where T : IEntityStruct;
        bool TryQueryEntitiesAndIndex<T>(EGID entityGid, out uint index, out T[]  array) where T : IEntityStruct;
        bool TryQueryEntitiesAndIndex<T>(uint id, ExclusiveGroup.ExclusiveGroupStruct group, out uint index, out T[] array) where T : IEntityStruct;

        /// <summary>
        /// ECS is meant to work on a set of Entities. Working on a single entity is sometime necessary, but using
        /// the following functions inside a loop would be a mistake as performance can be significantly impacted
        /// return the buffer and the index of the entity inside the buffer using the input EGID
        /// </summary>
        /// <param name="entityGid"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] QueryEntitiesAndIndex<T>(EGID entityGid, out uint index) where T : IEntityStruct;
        T[] QueryEntitiesAndIndex<T>(uint id, ExclusiveGroup.ExclusiveGroupStruct group, out uint index) where T : IEntityStruct;
        T[] QueryEntitiesAndIndex<T>(uint id, uint group, out uint index) where T : IEntityStruct;

        /// <summary>
        ///
        /// </summary>
        /// <param name="group"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ref T QueryUniqueEntity<T>(ExclusiveGroup.ExclusiveGroupStruct group) where T : IEntityStruct;
        ref T QueryUniqueEntity<T>(uint group) where T : IEntityStruct;

        /// <summary>
        ///
        /// </summary>
        /// <param name="entityGid"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ref T  QueryEntity<T>(EGID    entityGid) where T : IEntityStruct;
        ref T  QueryEntity<T>(uint id, ExclusiveGroup.ExclusiveGroupStruct group) where T : IEntityStruct;
        ref T  QueryEntity<T>(uint id, uint group) where T : IEntityStruct;

        /// <summary>
        /// Fast and raw (therefore not safe) return of entities buffer
        /// Modifying a buffer would compromise the integrity of the whole DB
        /// so they are meant to be used only in performance critical path
        /// </summary>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] QueryEntities<T>(uint group, out uint count) where T : IEntityStruct;

        /// <summary>
        ///
        /// </summary>
        /// <param name="groupStruct"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] QueryEntities<T>(ExclusiveGroup.ExclusiveGroupStruct groupStruct, out uint count) where T : IEntityStruct;

        (T1[], T2[]) QueryEntities<T1, T2>(uint group, out uint count) where T1 : IEntityStruct where T2 : IEntityStruct;
        (T1[], T2[]) QueryEntities<T1, T2>(ExclusiveGroup.ExclusiveGroupStruct groupStruct, out uint count)
            where T1 : IEntityStruct where T2 : IEntityStruct;

        (T1[], T2[], T3[]) QueryEntities<T1, T2, T3>(uint group, out uint count)
            where T1 : IEntityStruct where T2 : IEntityStruct where T3 : IEntityStruct;
        (T1[], T2[], T3[]) QueryEntities<T1, T2, T3>(ExclusiveGroup.ExclusiveGroupStruct groupStruct, out uint count)
            where T1 : IEntityStruct where T2 : IEntityStruct where T3 : IEntityStruct;

        /// <summary>
        /// this version returns a mapped version of the entity array so that is possible to find the
        /// index of the entity inside the returned buffer through it's EGID
        /// However mapping can be slow so it must be used for not performance critical paths
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="mapper"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        EGIDMapper<T> QueryMappedEntities<T>(uint groupID) where T : IEntityStruct;
        EGIDMapper<T> QueryMappedEntities<T>(ExclusiveGroup.ExclusiveGroupStruct groupStructId) where T : IEntityStruct;
        /// <summary>
        /// Execute an action on entities. Be sure that the action is not capturing variables
        /// otherwise you will allocate memory which will have a great impact on the execution performance.
        /// ExecuteOnEntities can be used to iterate safely over entities, several checks are in place
        /// to be sure that everything will be done correctly.
        /// Cache friendliness is guaranteed if only Entity Structs are used, but
        /// </summary>
        /// <param name="egid"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        void ExecuteOnEntities<T>(uint groupID, EntitiesAction<T> action) where T : IEntityStruct;
        void ExecuteOnEntities<T>(ExclusiveGroup.ExclusiveGroupStruct groupStructId, EntitiesAction<T> action) where T : IEntityStruct;
        void ExecuteOnEntities<T, W>(uint groupID, ref W value, EntitiesAction<T, W> action) where T : IEntityStruct;
        void ExecuteOnEntities<T, W>(ExclusiveGroup.ExclusiveGroupStruct groupStructId, ref W value, EntitiesAction<T, W> action) where T : IEntityStruct;
        /// <summary>
        /// Execute an action on ALL the entities regardless the group. This function doesn't guarantee cache
        /// friendliness even if just EntityStructs are used.
        /// Safety checks are in place
        /// </summary>
        /// <param name="damageableGroups"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        void ExecuteOnAllEntities<T>(System.Action<T[], uint, IEntitiesDB> action) where T : IEntityStruct;
        void ExecuteOnAllEntities<T, W>(ref W value, System.Action<T[], uint, IEntitiesDB, W> action) where T : IEntityStruct;
        void ExecuteOnAllEntities<T>(ExclusiveGroup[] groups, EntitiesAction<T> action) where T : IEntityStruct;
        void ExecuteOnAllEntities<T, W>(ExclusiveGroup[] groups, ref W value, EntitiesAction<T, W> action) where T : IEntityStruct;

        /// <summary>
        ///
        /// </summary>
        /// <param name="egid"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Exists<T>(EGID egid) where T : IEntityStruct;
        bool Exists<T>(uint id, uint groupid) where T : IEntityStruct;
        bool Exists (ExclusiveGroup.ExclusiveGroupStruct gid);

        /// <summary>
        ///
        /// </summary>
        /// <param name="group"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HasAny<T>(uint group) where T:IEntityStruct;
        bool HasAny<T>(ExclusiveGroup.ExclusiveGroupStruct groupStruct) where T:IEntityStruct;

        /// <summary>
        ///
        /// </summary>
        /// <param name="groupStruct"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        uint Count<T>(ExclusiveGroup.ExclusiveGroupStruct groupStruct)  where T:IEntityStruct;
        uint Count<T>(uint groupStruct)  where T:IEntityStruct;

        /// <summary>
        ///
        /// </summary>
        /// <param name="egid"></param>
        /// <typeparam name="T"></typeparam>
        void PublishEntityChange<T>(EGID egid)  where T : unmanaged, IEntityStruct;
    }

    public delegate void EntityAction<T, W>(ref T target, ref W       value);
    public delegate void EntityAction<T>(ref    T target);

    public delegate void AllEntitiesAction<T, W>(ref T target, ref W value, IEntitiesDB entitiesDb);
    public delegate void AllEntitiesAction<T>(ref T target, IEntitiesDB entitiesDb);

    public delegate void EntitiesAction<T, W>(ref T target, ref W value, EntityActionData extraParams);
    public delegate void EntitiesAction<T>(ref T target, EntityActionData extraParams);

    public struct EntityActionData
    {
        public readonly IEntitiesDB entitiesDB;
        public readonly uint entityIndex;

        public EntityActionData(IEntitiesDB entitiesDb, uint index)
        {
            this.entitiesDB = entitiesDb;
            entityIndex = index;
        }
    }
}