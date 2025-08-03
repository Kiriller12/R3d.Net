namespace R3d.Net.Types
{
    /// <summary>
    /// Modes for updating shadow maps.
    /// 
    /// Determines how often the shadow maps are refreshed
    /// </summary>
    public enum ShadowUpdateMode
    {
        /// <summary>
        /// Shadow maps update only when explicitly requested
        /// </summary>
        Manual,

        /// <summary>
        /// Shadow maps update at defined time intervals
        /// </summary>
        Interval,

        /// <summary>
        /// Shadow maps update every frame for real-time accuracy
        /// </summary>
        Continuous
    }
}
