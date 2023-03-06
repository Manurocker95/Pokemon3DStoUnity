#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace SFB
{
    public class StandaloneFileBrowserEditor : IStandaloneFileBrowser<ItemWithStream>
    {
        public IList<ItemWithStream> OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            string path = "";
            if (extensions == null)
            {
                path = EditorUtility.OpenFilePanel(title, directory, "");
            }
            else
            {
                path = EditorUtility.OpenFilePanelWithFilters(title, directory, GetFilterFromFileExtensionList(extensions));
            }
            var itemWithStream = new ItemWithStream
            {
                FullName = path,
                Name = path//,
                //Stream = File.OpenRead(path)
            };
            return new [] {itemWithStream};
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            cb(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public IList<ItemWithStream> OpenFolderPanel(string title, string directory, bool multiselect)
        {
            var path = EditorUtility.OpenFolderPanel(title, directory, "");
            var itemWithStream = new ItemWithStream
            {
                FullName = path,
                Name = path//,
                //Stream = File.OpenRead(path)
            };
            return new [] { itemWithStream };
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            cb(OpenFolderPanel(title, directory, multiselect));
        }

        public ItemWithStream SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            var ext = extensions != null ? extensions[0].Extensions[0] : "";
            var name = string.IsNullOrEmpty(ext) ? defaultName : defaultName + "." + ext;
            var path = EditorUtility.SaveFilePanel(title, directory, name, ext);
            var itemWithStream = new ItemWithStream
            {
                FullName = path + ext,
                Name = path//,
                //Stream = File.OpenRead(path)
            };
            return itemWithStream;
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<ItemWithStream> cb)
        {
            cb(SaveFilePanel(title, directory, defaultName, extensions));
        }

        private static string[] GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            var filters = new string[extensions.Length * 2];
            for (int i = 0; i < extensions.Length; i++)
            {
                filters[i * 2] = extensions[i].Name;
                filters[i * 2 + 1] = string.Join(",", extensions[i].Extensions);
            }
            return filters;
        }
    }
}

#endif
