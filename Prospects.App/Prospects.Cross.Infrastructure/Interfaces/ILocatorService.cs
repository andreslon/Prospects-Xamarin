namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface ILocatorService
    {
        T Get<T>() where T : class;
    }
}
