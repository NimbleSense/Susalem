namespace Susalem.Infrastructure.Models.Application
{
    public class ApplicationConfigurationEntity:DataEntityBase<string>
    {
        /// <summary>
        /// Description of the configuration variable.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Value of the configuration variable.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Indicates whether the field contains an encrypted text.
        /// </summary>
        public bool IsEncrypted { get; set; }
    }
}
