namespace UserService.WEB.Util.DI
{
    public interface IServiceLocator
    {
        T Get<T>();
    }
}