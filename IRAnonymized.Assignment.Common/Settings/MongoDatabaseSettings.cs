namespace IRAnonymized.Assignment.Common.Settings
{
    /// <summary>
    /// Settings for Mongo Data Storage Type.
    /// </summary>
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        /// <summary>
        /// Connection string for the MongoDb instance.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Name of the Database.
        /// </summary>
        public string DatabaseName { get; set; }
    }
}