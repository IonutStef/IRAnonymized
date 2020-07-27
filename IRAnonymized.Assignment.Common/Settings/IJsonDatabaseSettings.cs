namespace IRAnonymized.Assignment.Common.Settings
{
    /// <summary>
    /// Settings for Json Data Storage Type.
    /// </summary>
    public interface IJsonDatabaseSettings
    {
        /// <summary>
        /// Path to the file which will store the data.
        /// </summary>
        string DatabaseFilePath { get; set; }
    }
}