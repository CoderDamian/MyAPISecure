namespace MyModels
{
    // We use this class in our usercontroller action method for authentication.
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}