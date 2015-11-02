using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ModernDesignTemplate
{
    /// <summary>
    /// Native Windows API methods and interfaces.
    /// </summary>
    internal static class NativeMethods
    {
        #region Constants
        // The WM_GETMINMAXINFO message is sent to a window when the size or
        // position of the window is about to change.
        // An application can use this message to override the window's
        // default maximized size and position, or its default minimum or
        // maximum tracking size.
        private const int WM_GETMINMAXINFO = 0x0024;

        // Returns a handle to the display monitor that is nearest to the window.
        private const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        #endregion

        #region Structs
        /// <summary>
        /// The MINMAXINFO structure contains information about a window's
        /// maximized size and position and its minimum and maximum tracking size.
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms632605%28VS.85%29.aspx"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public POINT ptReserved;

            /// <summary>
            /// Specifies the maximized width (POINT.x)
            /// and the maximized height (POINT.y) of the window.
            /// For top-level windows, this value
            /// is based on the width of the primary monitor.
            /// </summary>
            public POINT ptMaxSize;

            /// <summary>
            /// Specifies the position of the left side
            /// of the maximized window (POINT.x)
            /// and the position of the top
            /// of the maximized window (POINT.y).
            /// For top-level windows, this value is based
            /// on the position of the primary monitor.
            /// </summary>
            public POINT ptMaxPosition;

            /// <summary>
            /// Specifies the minimum tracking width (POINT.x)
            /// and the minimum tracking height (POINT.y) of the window.
            /// This value can be obtained programmatically
            /// from the system metrics SM_CXMINTRACK and SM_CYMINTRACK.
            /// </summary>
            public POINT ptMinTrackSize;

            /// <summary>
            /// Specifies the maximum tracking width (POINT.x)
            /// and the maximum tracking height (POINT.y) of the window.
            /// This value is based on the size of the virtual screen
            /// and can be obtained programmatically
            /// from the system metrics SM_CXMAXTRACK and SM_CYMAXTRACK.
            /// </summary>
            public POINT ptMaxTrackSize;
        }

        /// <summary>
        /// The MONITORINFO structure contains information about a display monitor.
        /// The GetMonitorInfo function stores information in a MONITORINFO structure.
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd145065%28VS.85%29.aspx"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class MONITORINFO
        {
            /// <summary>
            /// The size, in bytes, of the structure. Set this member
            /// to sizeof(MONITORINFO) (40) before calling the GetMonitorInfo function.
            /// Doing so lets the function determine
            /// the type of structure you are passing to it.
            /// </summary>
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// A RECT structure that specifies the display monitor rectangle,
            /// expressed in virtual-screen coordinates.
            /// Note that if the monitor is not the primary display monitor,
            /// some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RECT rcMonitor;

            /// <summary>
            /// A RECT structure that specifies the work area rectangle
            /// of the display monitor that can be used by applications,
            /// expressed in virtual-screen coordinates.
            /// Windows uses this rectangle to maximize an application on the monitor.
            /// The rest of the area in rcMonitor contains system windows
            /// such as the task bar and side bars.
            /// Note that if the monitor is not the primary display monitor,
            /// some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RECT rcWork;

            /// <summary>
            /// The attributes of the display monitor.
            ///
            /// This member can be the following value:
            /// 1 : MONITORINFOF_PRIMARY
            /// </summary>
            public uint dwFlags;
        }

        /// <summary>
        /// Native Windows API-compatible POINT struct
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// The RECT structure defines the coordinates of the upper-left
        /// and lower-right corners of a rectangle.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/dd162897%28VS.85%29.aspx"/>
        /// <remarks>
        /// By convention, the right and bottom edges of the rectangle
        /// are normally considered exclusive.
        /// In other words, the pixel whose coordinates are ( right, bottom )
        /// lies immediately outside of the the rectangle.
        /// For example, when RECT is passed to the FillRect function, the rectangle
        /// is filled up to, but not including,
        /// the right column and bottom row of pixels. This structure is identical
        /// to the RECTL structure.
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        private struct RECT
        {
            /// <summary>
            /// The x-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int Left;

            /// <summary>
            /// The y-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int Top;

            /// <summary>
            /// The x-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int Right;

            /// <summary>
            /// The y-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int Bottom;
        }
        #endregion

        #region Imported methods
        /// <summary>
        /// The MonitorFromWindow function retrieves a handle to the display monitor
        /// that has the largest area of intersection with the bounding rectangle
        /// of a specified window.
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd145064%28VS.85%29.aspx"/>
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="dwFlags">Determines the function's return value
        /// if the window does not intersect any display monitor.</param>
        /// <returns>
        /// Monitor HMONITOR handle on success or based on dwFlags for failure
        /// </returns>
        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        /// <summary>
        /// The GetMonitorInfo function retrieves information about a display monitor
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd144901%28VS.85%29.aspx"/>
        /// </summary>
        /// <param name="hMonitor">A handle to the display monitor of interest.</param>
        /// <param name="lpmi">
        /// A pointer to a MONITORINFO structure that receives information
        /// about the specified display monitor.
        /// </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
        #endregion

        /// <summary>
        /// Window procedure callback.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="msg">The window message.</param>
        /// <param name="wParam">The wParam (word parameter).</param>
        /// <param name="lParam">The lParam (long parameter).</param>
        /// <param name="handled">
        /// if set to <c>true</c> - the message is handled
        /// and should not be processed by other callbacks.
        /// </param>
        /// <returns></returns>
        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_GETMINMAXINFO:
                    // Handle the message and mark it as handled,
                    // so other callbacks do not touch it
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }

        /// <summary>
        /// Creates and populates the MINMAXINFO structure for a maximized window.
        /// Puts the structure into memory address given by lParam.
        /// Only used to process a WM_GETMINMAXINFO message.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="lParam">The lParam.</param>
        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            // Get the MINMAXINFO structure from memory location given by lParam
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                mmi.ptMaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
            }

            // Copy the structure to memory location specified by lParam.
            // This concludes processing of WM_GETMINMAXINFO.
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        public static void OnSourceInitialized(object sender, EventArgs e)
        {
            Window window = sender as Window;

            if (window != null)
            {
                IntPtr handle = (new WindowInteropHelper(window)).Handle;
                HwndSource hwndSource = HwndSource.FromHwnd(handle);
                if (hwndSource != null)
                    hwndSource.AddHook(WindowProc);
            }
        }
    }
}