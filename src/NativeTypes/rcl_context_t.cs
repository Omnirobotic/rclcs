
using System;
using System.Runtime.InteropServices;

namespace rclcs
{
	/// <summary>
	/// Managed implementation of the rcl_context_t in ros2
	/// </summary>
	public struct rcl_context_t
	{
        public rcl_arguments_t global_arguments;
		public IntPtr impl;
	}

    /// <summary>
    /// Managed implementation of the rcl_arguments_t in ros2
    /// </summary>
    public struct rcl_arguments_t
    {
        public IntPtr impl;
    }
}
