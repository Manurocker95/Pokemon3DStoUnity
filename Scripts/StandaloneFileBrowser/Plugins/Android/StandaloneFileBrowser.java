package com.sfb.standalonefilebrowser;

import android.app.Activity;
import android.content.Context;

public class StandaloneFileBrowser {
    public static void showOpenFileDialog(final Activity activity, final String title, final boolean selectMultiple, final StandaloneFileBrowserAndroidListener listener) {
        StandaloneFileBrowserFragment fragment = new StandaloneFileBrowserFragment();
        fragment.setFileBrowserAndroidListener(listener);
        fragment.setMultiple(selectMultiple);
        fragment.setTitle(title);
        activity.getFragmentManager().beginTransaction().add(0, fragment).commit();
    }
}
