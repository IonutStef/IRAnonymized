namespace IRAnonymized.Assignment.Common.Settings
{
    /// <summary>
    /// Settings for Mongo Data Storage Type.
    /// </summary>
    public interface IMongoDatabaseSettings
    {
        /// <summary>
        /// Connection string for the MongoDb instance.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Name of the Database.
        /// </summary>
        string DatabaseName { get; set; }
    }
}