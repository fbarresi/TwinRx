using System;
using System.Linq;

namespace TwinRx.Interfaces.Enums
{
    /// <summary>
    /// Select the update method for TwinRx observable
    /// </summary>
    public enum TransmissionMode
    {
        /// <summary>
        /// Get notification on variable change
        /// </summary>
        OnChange = 4,
        /// <summary>
        /// Get cyclic notification
        /// </summary>
        Cyclic = 3
    }
}