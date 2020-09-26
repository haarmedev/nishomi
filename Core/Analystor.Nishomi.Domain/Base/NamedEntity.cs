namespace Analystor.Nishomi.Domain
{
    /// <summary>
    /// NamedEntity
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Domain.Entity" />
    public abstract class NamedEntity : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
            set;
        }
    }
}
