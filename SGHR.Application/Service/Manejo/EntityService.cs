

namespace SGHR.Application.Service.Manejo
{
    public class EntityService<T> : IEntity<T> where T : new()
    {
        public T CreateEntity()
        {
            return new T();
        }
    }
}
