namespace R3d.Net.Examples
{
    /// <summary>
    /// Example interface
    /// </summary>
    internal interface IExample : IDisposable
    {
        /// <summary>
        /// Example initialization
        /// </summary>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        public void Init(int width, int height);

        /// <summary>
        /// Updates game logic
        /// </summary>
        /// <param name="deltaTime">The time in seconds for last frame drawn</param>
        public void Update(float deltaTime);

        /// <summary>
        /// Renders one frame
        /// </summary>
        public void Render();
    }
}
