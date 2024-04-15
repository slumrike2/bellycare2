using Firebase.Database;
using Newtonsoft.Json;

namespace BellyCare.Repositories
{
    public class BaseOnlineRepository <T> (FirebaseClient db) where T : class
    {
        private readonly FirebaseClient db = db;
        private readonly string child = typeof(T).Name;

        /// <summary>
        /// Gets all entities from the database.
        /// </summary>
        /// <returns> A list of entities or an empty list if there are none. </returns>
        public async Task<List<T>> GetAll()
        {
            try
            {
                return (await db.Child(child).OnceAsync<T>())
                    .Select(item => item.Object)
                    .ToList();
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
        public async Task<List<T>> GetAllBy(Func<T, bool> selector)
        {
            try
            {
                return (await db.Child(child).OnceAsync<T>())
                    .Select(item => item.Object)
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
        public async Task Delete(string id)
        {
            try
            {
                await db.Child(child + "/" + id).DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}