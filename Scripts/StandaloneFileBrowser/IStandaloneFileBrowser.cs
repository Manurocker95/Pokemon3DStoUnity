using System;
using System.Collections.Generic;

namespace SFB
{
    /// <summary>Represents a series of methods to operate platform-specific file pickers.</summary>
    /// <typeparam name="T">File picker return type.</typeparam>
    public interface IStandaloneFileBrowser<T>
    {
        /// <summary>Opens the platform-specific file selection panel.</summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="directory">The initial file panel directory.</param>
        /// <param name="extensions">The allowed extensions.</param>
        /// <param name="multiselect">Pass <c>true</c> to enable multi-selection.</param>
        /// <returns>The list of selected files.</returns>
        IList<T> OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);

        /// <summary>Opens the platform-specific folder selection panel.</summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="directory">The initial file panel directory.</param>
        /// <param name="multiselect">Pass <c>true</c> to enable multi-selection.</param>
        /// <returns>The list of selected folders.</returns>
        IList<T> OpenFolderPanel(string title, string directory, bool multiselect);

        /// <summary>Opens the platform-specific file save panel.</summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="directory">The initial file panel directory.</param>
        /// <param name="defaultName">The initial filename.</param>
        /// <param name="extensions">The allowed extensions.</param>
        /// <returns>The saved file.</returns>
        T SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);

        /// <summary>Opens the platform-specific file selection panel Asynchronously.</summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="directory">The initial file panel directory.</param>
        /// <param name="extensions">The allowed extensions.</param>
        /// <param name="multiselect">Pass <c>true</c> to enable multi-selection.</param>
        /// <param name="cb">The Method to call when the user selects or cancel the file selection dialog.</param>
        void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<IList<T>> cb);

        /// <summary>Opens the platform-specific folder selection panel Asynchronously.</summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="directory">The initial file panel directory.</param>
        /// <param name="multiselect">Pass <c>true</c> to enable multi-selection.</param>
        /// <param name="cb">The Method to call when the user selects or cancel the file selection dialog.</param>
        void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<IList<T>> cb);

        /// <summary>Opens the platform-specific file save panel Asynchronously.</summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="directory">The initial file panel directory.</param>
        /// <param name="defaultName">The initial filename.</param>
        /// <param name="extensions">The allowed extensions.</param>
        /// <param name="cb">The Method to call when the user selects or cancel the file selection dialog.</param>
        void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<T> cb);
    }
}
