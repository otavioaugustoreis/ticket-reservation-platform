namespace Infrastructure
{
    public class SqlServerOptionsConfiguration
    {
        public const string Section = "SqlServer";
        public string ConnectionString { get; set; } = string.Empty;
    }
}
