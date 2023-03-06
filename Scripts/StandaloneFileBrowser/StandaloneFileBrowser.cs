using System;
using System.Collections.Generic;
namespace SFB
{
    /// <summary>Represents a platform-specific file browser.</summary>
    public class StandaloneFileBrowser
    {
#if UNITY_EDITOR
    #if UNITY_EDITOR_OSX
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserMac();
    #elif UNITY_EDITOR_LINUX
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserLinux();
    #elif UNITY_EDITOR_WIN
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserWindows();
#else
            private static IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserEditor();
#endif
#else
    #if UNITY_WSA
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserWinRT();
    #elif UNITY_ANDROID
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserAndroid();
    #elif UNITY_WEBGL
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserWebGL();
    #elif UNITY_STANDALONE_OSX
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserMac();
    #elif UNITY_IOS
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserIOS();
    #elif UNITY_STANDALONE_WIN
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserWindows();
    #elif UNITY_STANDALONE_LINUX
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = new StandaloneFileBrowserLinux();
    #else
            private static readonly IStandaloneFileBrowser<ItemWithStream> _platformWrapper = null;
    #endif
#endif
        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns array of chosen items. Zero length array when cancelled</returns>
        public static IList<ItemWithStream> OpenFilePanel(string title, string directory, string extension, bool multiselect)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            return OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns array of chosen items. Zero length array when cancelled</returns>
        public static IList<ItemWithStream> OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            return _platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback")</param>
        public static void OpenFilePanelAsync(string title, string directory, string extension, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback")</param>
        public static void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            _platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open folder dialog
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <returns>Returns array of chosen items. Zero length array when cancelled</returns>
        public static IList<ItemWithStream> OpenFolderPanel(string title, string directory, bool multiselect)
        {
            return _platformWrapper.OpenFolderPanel(title, directory, multiselect);
        }

        /// <summary>
        /// Native open folder dialog async
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <param name="cb">Callback")</param>
        public static void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            _platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, cb);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <returns>Returns chosen item. Null when cancelled</returns>
        public static ItemWithStream SaveFilePanel(string title, string directory, string defaultName, string extension)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            return SaveFilePanel(title, directory, defaultName, extensions);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <returns>Returns chosen item. Null when cancelled</returns>
        public static ItemWithStream SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            return _platformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanelAsync(string title, string directory, string defaultName, string extension, Action<ItemWithStream> cb)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<ItemWithStream> cb)
        {
            _platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
        }
    }
}