
namespace SGHR.Application.Service.Manejo
{
    public interface IEntity<T> where T : new()
    {
        T CreateEntity();
    }
}
