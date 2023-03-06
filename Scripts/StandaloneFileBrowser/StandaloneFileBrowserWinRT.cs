#if UNITY_WSA && !UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using TriLibCore;
using UnityEngine;

namespace SFB
{
    public class StandaloneFileBrowserWinRT : IStandaloneFileBrowser<ItemWithStream>
    {
        public IList<ItemWithStream> OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            return null;
        }

        public IList<ItemWithStream> OpenFolderPanel(string title, string directory, bool multiselect)
        {
            return null;
        }

        public ItemWithStream SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            return null;
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
            {
                var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
                if (extensions != null)
                {
                    var existingExtensions = new List<string>();
                    foreach (var extension in extensions)
                    {
                        foreach (var filter in extension.Extensions)
                        {
                            if (filter == "*" || filter == ".*" || filter == "*.*" || existingExtensions.Contains(filter)) {
                                continue;
                            }
                            filePicker.FileTypeFilter.Add("." + filter);
                            existingExtensions.Add(filter);
                        }
                    }
                }
                if (multiselect)
                {
                    var files = await filePicker.PickMultipleFilesAsync();
                    var result = new ItemWithStream[files.Count];
                    for (var i = 0; i < files.Count; i++)
                    {
                        result[i] = new ItemWithStream()
                        {
                            Name = files[i].Name,
                            Stream = await ReadStorageFile(files[i])
                        };
                    }
                    await Task.Run(() => cb(result));
                }
                else
                {
                    var file = await filePicker.PickSingleFileAsync();
                    var fileWithStream = new ItemWithStream()
                    {
                        Name = file.Name,
                        Stream = await ReadStorageFile(file)
                    };
                    await Task.Run(() => cb(new[] { fileWithStream }));
                }
            }, false);
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<IList<ItemWithStream>> cb)
        {
            UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
            {
                var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                var folder = await folderPicker.PickSingleFolderAsync();
                var folderWithStream = new ItemWithStream()
                {
                    Name = folder.Name
                };
                await Task.Run(() => cb(new[] { folderWithStream }));
            }, false);
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<ItemWithStream> cb)
        {
            UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
            {
                var filePicker = new Windows.Storage.Pickers.FileSavePicker();
                filePicker.SuggestedFileName = defaultName;
                foreach (var extension in extensions)
                {
                    filePicker.FileTypeChoices.Add(extension.Name, extension.Extensions);
                }
                var file = await filePicker.PickSaveFileAsync();
                var fileWithStream = new ItemWithStream()
                {
                    Name = file.Name
                };
                await Task.Run(() => cb(fileWithStream));
            }, false);
        }

        private static async Task<Stream> ReadStorageFile(StorageFile storageFile)
        {
            return await storageFile.OpenStreamForReadAsync();
        }
    }
}
#endif