//main purpose to windows processes
//some need to redirect the command line output 
//and some not. For diagnostics it will need to redirect
namespace PyLauncher
{
    interface ITestrunner
    {
        void Load(Parameter.Test testparameters, string serialno);
        void Runtest();
    }
}
