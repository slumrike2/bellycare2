using Firebase.Database;
using Firebase.Database.Offline;
using Newtonsoft.Json;

namespace BellyCare.Repositories
{
    public class BaseOnlineRepository <T> (FirebaseClient db) where T : class
    {
        private readonly FirebaseClient db = db;
        private string child = typeof(T).Name;

        public string Child
        {
            get => child;
            protected set => child = value;
        }

        public BaseOnlineRepository<J> GetChildRepository<J>(params string[] strings) where J : class
        {
            var repository = new BaseOnlineRepository<J>(db)
            {
                Child = typeof(T).Name
            };

            foreach (var str in strings)
            {
                repository.Child += "/" + str;
            }

            return repository;
        }


        /// <summary>
        /// Gets all entities from the database.
        /// </summary>
        /// <returns> A list of entities or an empty list if there are none. </returns>
        public async Task<List<FirebaseObject<T>>> GetAll()
        {
            try
            {
                return (await db.Child(child).OnceAsync<T>()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all entities from the database that match a condition.
        /// </summary>
        /// <param name="selector"> The condition to be met. </param>
        /// <returns> A list of entities or an empty list if there are none. </returns>
        public async Task<List<FirebaseObject<T>>> GetAllBy(Func<FirebaseObject<T>, bool> selector)
        {
            try
            {
                return (await db.Child(child).OnceAsync<T>())
                    .Select(item => item)
                    .Where(selector)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id"> The Firebase Key of the entity. </param>
        /// <returns> The entity or null if it doesn't exist. </returns>
        public async Task<T> GetById(string id)
        {
            try
            {
                return await db.Child(child + "/" + id).OnceSingleAsync<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <param name="entity"> The entity to be added. </param>
        /// <returns> The Firebase Key of the entity. </returns>
        public async Task<string> Add(T entity)
        {
            try
            {
                var result = await db.Child(child).PostAsync(JsonConvert.SerializeObject(entity));

                return result.Key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="id"> The Firebase Key of the entity. </param>
        /// <param name="entity"> The entity to be updated. </param>
        public async Task Update(string id, T entity)
        {
            try
            {
                await db.Child(child + "/" + id).PutAsync(JsonConvert.SerializeObject(entity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="id"> The Firebase Key of the entity. </param>
        public void Delete(string id)
        {
            try
            {
                db.Child(child)
                    .AsRealtimeDatabase<T>()
                    .Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}