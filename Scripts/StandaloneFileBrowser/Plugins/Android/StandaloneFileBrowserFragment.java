package com.sfb.standalonefilebrowser;

import android.app.Activity;
import android.app.Fragment;
import android.content.ContentResolver;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;

public class StandaloneFileBrowserFragment extends Fragment {
    public static final int PICKFILE_RESULT_CODE = 1;
	
    private StandaloneFileBrowserAndroidListener fileBrowserAndroidListener;
    private String title;
    private Boolean multiple;

    public void setFileBrowserAndroidListener(final StandaloneFileBrowserAndroidListener fileBrowserAndroidListener) {
        this.fileBrowserAndroidListener = fileBrowserAndroidListener;
    }
	
	public void setTitle(final String title) {
		this.title = title;
	}
	
	public void setMultiple(final Boolean multiple) {
		this.multiple = multiple;
	}

    @Override
    public void onStart () {
        super.onStart ();
        final Intent intent = new Intent(Intent.ACTION_GET_CONTENT);
        if (title != null) {
            intent.putExtra("android.intent.extra.TITLE", title);
        }
        if (multiple) {
            intent.putExtra("android.intent.extra.ALLOW_MULTIPLE", true);
        }
        intent.setType("*/*");
        startActivityForResult(intent, PICKFILE_RESULT_CODE);
    }

    private String getFileCopyPath(final Uri uri) {
        if (uri == null) {
            return null;
        }
        final ContentResolver contentResolver = getActivity().getContentResolver();
        Cursor cursor = null;
        String filename = null;
        try {
            cursor = contentResolver.query(uri, null, null, null, null);
            if (cursor != null && cursor.moveToFirst()) {
                filename = cursor.getString(cursor.getColumnIndex("_display_name"));
            }
        }
        catch (Exception e) {
            return null;
        }
        finally {
            if (cursor != null) {
                cursor.close();
            }
        }
        try {
            final InputStream input = contentResolver.openInputStream(uri);
            if (input == null) {
                return null;
            }
            final File file = new File(getActivity().getCacheDir(), filename);
            OutputStream output = null;
            try {
                output = new FileOutputStream(file, false);
                final byte[] buffer = new byte[4096];
                int len;
                while ((len = input.read(buffer)) > 0) {
                    output.write(buffer, 0, len);
                }
                return file.getAbsolutePath();
            }
            finally {
                if (output != null) {
                    output.close();
                }
                input.close();
            }
        }
        catch (Exception e) {
            return null;
        }
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (fileBrowserAndroidListener == null) {
            return;
        }
        switch (requestCode) {
            case PICKFILE_RESULT_CODE:
                String filenames = "";
                if (resultCode == Activity.RESULT_OK) {
                    if (data.getClipData() != null) {
                        for (int i = 0; i <  data.getClipData().getItemCount(); ++i) {
                            if (filenames != "") {
                                filenames += "|";
                            }
                            filenames += getFileCopyPath(data.getClipData().getItemAt(i).getUri());
                        }
                    }
                    else if (data.getData() != null) {
                        filenames = getFileCopyPath(data.getData());
                    }
                }
                fileBrowserAndroidListener.onFilesSelected(filenames);
                break;
        }
        getFragmentManager().beginTransaction().remove(this).commit();
    }
}

