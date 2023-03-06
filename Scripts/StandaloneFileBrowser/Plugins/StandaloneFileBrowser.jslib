var StandaloneFileBrowserWebGLPlugin = {
  oldGameObjectName: '',
  // Open file.
  // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
  // methodNamePtr: Callback method name on given GameObject.
  // filter: Filter files. Example filters:
  //     Match all image files: "image/*"
  //     Match all video files: "video/*"
  //     Match all audio files: "audio/*"
  //     Custom: ".plist, .xml, .yaml"
  // multiselect: Allows multiple file selection
  UploadFile: function(gameObjectNamePtr, methodNamePtr, filterPtr, multiselect) {
    gameObjectName = Pointer_stringify(gameObjectNamePtr);
    methodName = Pointer_stringify(methodNamePtr);
    filter = Pointer_stringify(filterPtr);
    var fileInput = document.getElementById(this.oldGameObjectName);
    if (fileInput) {
      document.body.removeChild(fileInput);
    }
    fileInput = document.createElement('input');
    fileInput.setAttribute('id', gameObjectName);
    fileInput.setAttribute('type', 'file');
    fileInput.setAttribute('class', 'standalone-file-picker');
    if (multiselect) {
      fileInput.setAttribute('multiple', 'multiple');
    }
    if (filter) {
      fileInput.setAttribute('accept', filter);
    }
    fileInput.onclick = function(event) {
	  event.stopPropagation();
      this.value = null;
    };
    fileInput.onchange = function(event) {
	  event.stopPropagation();	
      var urls = [];
      for (var i = 0; i < event.target.files.length; i++) {
        urls.push({
          "name": event.target.files[i].name,
          "url": URL.createObjectURL(event.target.files[i])
        });
      }
      SendMessage(gameObjectName, methodName, JSON.stringify(urls));
      document.body.removeChild(fileInput);
    };
    document.body.appendChild(fileInput);
	fileInput.focus();
    fileInput.click();
    this.oldGameObjectName = gameObjectName;
  },

  // Save file
  // DownloadFile method does not open SaveFileDialog like standalone builds, its just allows user to download file
  // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
  // methodNamePtr: Callback method name on given GameObject.
  // filenamePtr: Filename with extension
  // byteArray: byte[]
  // byteArraySize: byte[].Length
  DownloadFile: function(gameObjectNamePtr, methodNamePtr, filenamePtr, byteArray, byteArraySize) {
    gameObjectName = Pointer_stringify(gameObjectNamePtr);
    methodName = Pointer_stringify(methodNamePtr);
    filename = Pointer_stringify(filenamePtr);
    var bytes = new Uint8Array(byteArraySize);
    for (var i = 0; i < byteArraySize; i++) {
      bytes[i] = HEAPU8[byteArray + i];
    }
    var downloader = window.document.createElement('a');
    downloader.setAttribute('id', gameObjectName);
    downloader.href = window.URL.createObjectURL(new Blob([bytes], {
      type: 'application/octet-stream'
    }));
    downloader.download = filename;
    document.body.appendChild(downloader);
    document.onmouseup = function() {
      downloader.click();
      document.body.removeChild(downloader);
      document.onmouseup = null;
      SendMessage(gameObjectName, methodName);
    };
  }
};
mergeInto(LibraryManager.library, StandaloneFileBrowserWebGLPlugin);