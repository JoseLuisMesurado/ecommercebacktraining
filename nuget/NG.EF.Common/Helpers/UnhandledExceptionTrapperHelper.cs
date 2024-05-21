namespace NG.EF.Common.Helpers
{
    public abstract class UnhandledExceptionTrapperHelper
    {
        public static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to Exit");
            Environment.Exit(0);
        }

    }
}
