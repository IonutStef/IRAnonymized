namespace IRAnonymized.Assignment.Common.Settings
{
    /// <summary>
    /// Settings for Json Data Storage Type.
    /// </summary>
    public class JsonDatabaseSettings : IJsonDatabaseSettings
    {
        /// <summary>
        /// Path to the file which will store the data.
        /// </summary>
        public string DatabaseFilePath { get; set; }
    }
}