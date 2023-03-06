#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
namespace SFB
{
    public class StandaloneFileBrowserWindows : IStandaloneFileBrowser<ItemWithStream>
    {
        private const int BufferSize = 2048;

        [DllImport("StandaloneFileBrowser", CharSet = CharSet.Unicode)]
        private static extern bool DialogOpenFilePanel(IntPtr buffer, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)]string title, [MarshalAs(UnmanagedType.LPWStr)]string directory, [MarshalAs(UnmanagedType.LPWStr)]string extension, bool multiselect);
        [DllImport("StandaloneFileBrowser", CharSet = CharSet.Unicode)]
        private static extern bool DialogOpenFilePanelAsync(IntPtr buffer, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)]string title, [MarshalAs(UnmanagedType.LPWStr)]string directory, [MarshalAs(UnmanagedType.LPWStr)]string extension, bool multiselect, AsyncCallback callback);
        [DllImport("StandaloneFileBrowser", CharSet = CharSet.Unicode)]
        private static extern bool DialogOpenFolderPanel(IntPtr buffer, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)]string title, [MarshalAs(UnmanagedType.LPWStr)]string directory, bool multiselect);
        [DllImport("StandaloneFileBrowser", CharSet = CharSet.Unicode)]
        private static extern void DialogOpenFolderPanelAsync(IntPtr buffer, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)]string title, [MarshalAs(UnmanagedType.LPWStr)]string directory, bool multiselect, AsyncCallback callback);
        [DllImport("StandaloneFileBrowser", CharSet = CharSet.Unicode)]
        private static extern bool DialogSaveFilePanel(IntPtr buffer, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)]string title, [MarshalAs(UnmanagedType.LPWStr)]string directory, [MarshalAs(UnmanagedType.LPWStr)]string defaultName, [MarshalAs(UnmanagedType.LPWStr)]string extension);
        [DllImport("StandaloneFileBrowser", CharSet = CharSet.Unicode)]
        private static extern void DialogSaveFilePanelAsync(IntPtr buffer, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)] string title, [MarshalAs(UnmanagedType.LPWStr)]string directory, [MarshalAs(UnmanagedType.LPWStr)] string defaultName, [MarshalAs(UnmanagedType.LPWStr)]string extension, AsyncCallback callback);

        public IList<ItemWithStream> OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            var results = new List<ItemWithStream>();
            var buffer = new char[BufferSize];
            var bufferLock = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            if (DialogOpenFilePanel(bufferLock.AddrOfPinnedObject(), BufferSize, title, directory, GetFilterFromFileExtensionList(extensions), multiselect))
            {
                ParseResults(buffer, results, multiselect);
            }
            bufferLock.Free();
            return results.ToArray();
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            //todo: async
            cb(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public IList<ItemWithStream> OpenFolderPanel(string title, string directory, bool multiselect)
        {
            var results = new List<ItemWithStream>();
            var buffer = new char[BufferSize];
            var bufferLock = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            if (DialogOpenFolderPanel(bufferLock.AddrOfPinnedObject(), BufferSize, title, directory, multiselect))
            {
                ParseResults(buffer, results, multiselect);
            }
            bufferLock.Free();
            return results;
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            //todo: async
            cb(OpenFolderPanel(title, directory, multiselect));
        }

        public ItemWithStream SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            var results = new List<ItemWithStream>();
            var buffer = new char[BufferSize];
            var bufferLock = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            if (DialogSaveFilePanel(bufferLock.AddrOfPinnedObject(), BufferSize, title, directory, defaultName, GetFilterFromFileExtensionList(extensions)))
            {
                ParseResults(buffer, results, false);
            }
            bufferLock.Free();
            return results.Count > 0 ? results[0] : null;
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<ItemWithStream> cb)
        {
            //todo: async
            cb(SaveFilePanel(title, directory, defaultName, extensions));
        }

        private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            var filterString = "";
            if (extensions != null)
            {
                foreach (var extension in extensions)
                {
                    if (!string.IsNullOrWhiteSpace(extension.Name))
                    {
                        filterString += extension.Name;
                    }
                    else
                    {
                        var descriptionString = "";
                        foreach (var format in extension.Extensions)
                        {
                            if (descriptionString != "")
                            {
                                descriptionString += ",";
                            }
                            descriptionString += "(*" + (format[0] == '.' ? format.Substring(1) : format) + ")";
                        }
                        filterString += descriptionString;
                    }
                    filterString += "\0";
                    foreach (var format in extension.Extensions)
                    {
                        filterString += "*" + (format[0] == '.' ? format.Substring(1) : format) + ";";
                    }
                    filterString += "\0";
                }
            }
            filterString += "\0";
            return filterString;
        }

        private static void ParseResults(char[] buffer, List<ItemWithStream> results, bool multiselect)
        {
            var currentStringBytes = new List<char>();
            foreach (var c in buffer)
            {
                if (c == 0)
                {
                    var currentString = new string(currentStringBytes.ToArray());
                    if (!string.IsNullOrWhiteSpace(currentString) && currentString != "\0")
                    {
                        var filename = multiselect && results.Count > 0 ? $"{results[0].Name}\\{currentString}" : currentString;
                        results.Add(new ItemWithStream
                        {
                            FullName = filename,
                            Name = filename//,
                            //Stream = File.OpenRead(filename)
                        });
                    }
                    currentStringBytes.Clear();
                    continue;
                }
                currentStringBytes.Add(c);
            }
            if (multiselect && results.Count > 1)
            {
                results.RemoveAt(0);
            }
        }
    }
}
#endif