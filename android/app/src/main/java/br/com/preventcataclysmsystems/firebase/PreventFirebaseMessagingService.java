package br.com.preventcataclysmsystems.firebase;

import android.app.ActivityManager;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.media.RingtoneManager;
import android.net.Uri;
import android.support.v4.app.NotificationCompat;

import com.google.firebase.FirebaseApp;
import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;
import com.google.gson.Gson;

import java.util.List;
import java.util.Random;

import br.com.preventcataclysmsystems.MapsActivity;
import br.com.preventcataclysmsystems.R;
import br.com.preventcataclysmsystems.data.Message;

/**
 * Created by Danilo on 06/01/2017.
 */
public class PreventFirebaseMessagingService extends FirebaseMessagingService {
    private Gson gson;
    final static String prevent_cataclysm_systems = "prevent_cataclysm_systems";

    @Override
    public void onCreate() {
        super.onCreate();

        gson = new Gson();

        FirebaseApp.initializeApp(this);
    }

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {
        Message message = gson.fromJson(gson.toJson(remoteMessage.getData()), Message.class);
        sendNotificationMessage(message);
    }

    private void sendNotificationMessage(Message message) {
        int random = new Random().nextInt();

        Intent intent = new Intent(this, MapsActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0, intent, PendingIntent.FLAG_ONE_SHOT);

        Uri defaultSoundUri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_RINGTONE);
        NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this)
                .setSmallIcon(R.drawable.ic_stat_ic_notification)
                .setColor(Color.argb(1, 5, 109, 34))
                .setContentTitle(message.getTitle())
                .setContentText(message.getMessage() + "\n"+ message.getDetails())
                .setContentInfo(message.getDetails())
                .setSubText("Warning!")
                .setAutoCancel(true)
                .setSound(defaultSoundUri)
                .setDefaults(Notification.DEFAULT_VIBRATE)
                .setGroup(prevent_cataclysm_systems)
                .setContentIntent(pendingIntent);

        NotificationManager notificationManager =
                (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

        notificationManager.notify(random, notificationBuilder.build());
    }

    public boolean isAppRunning(Context context, String packageName) {
        ActivityManager activityManager = (ActivityManager) context.getSystemService(ACTIVITY_SERVICE);
        List<ActivityManager.RunningAppProcessInfo> procInfos = activityManager.getRunningAppProcesses();
        for (ActivityManager.RunningAppProcessInfo info : procInfos) {
            if (info.processName.equals(packageName)) {
                return true;
            }
        }

        return false;
    }

    public boolean isActivityRunning(String name) {
        ActivityManager am = (ActivityManager) this.getSystemService(ACTIVITY_SERVICE);
        List<ActivityManager.RunningTaskInfo> taskInfo = am.getRunningTasks(1);
        return name.equals(taskInfo.get(0).topActivity.getClassName()) ? true : false;
    }
}