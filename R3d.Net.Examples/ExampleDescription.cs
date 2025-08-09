namespace R3d.Net.Examples
{
    /// <summary>
    /// Example description
    /// </summary>
    internal class ExampleDescription
    {
        /// <summary>
        /// Creates example description
        /// </summary>
        /// <param name="name">Name of the example</param>
        /// <param name="create">Example create function</param>
        /// <param name="credits">Credits text</param>
        public ExampleDescription(string name, Func<IExample> create, string? credits = null)
        {
            Name = name;
            Create = create;
            Credits = credits;
        }

        /// <summary>
        /// Name of the example
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Example create function
        /// </summary>
        public Func<IExample> Create { get; }

        /// <summary>
        /// Credits text
        /// </summary>
        public string? Credits { get; }
    }
}
