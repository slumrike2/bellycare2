namespace Barreto.Exe.Maui.Shells
{
    //This can only be implemented by Application class
    public interface IMultishellApp : IApplication
    {
        void GoToAppShell();
        void GoToSessionShell();
    }
}
