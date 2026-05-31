namespace NormanConquest
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form currentForm = new FormMenu();
            while (currentForm != null)
            {
                Application.Run(currentForm);
                // 读取当前窗体设置的下一窗体
                currentForm = currentForm.Tag as Form;
            }
        }
    }
}