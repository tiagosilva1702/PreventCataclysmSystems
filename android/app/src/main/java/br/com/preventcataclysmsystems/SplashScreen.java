package br.com.preventcataclysmsystems;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

import com.google.firebase.messaging.FirebaseMessaging;

/**
 * Created by danilo.nascimento on 20/12/2016.
 */
public class SplashScreen extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        FirebaseMessaging.getInstance().subscribeToTopic("global");

        Thread timerThread = new Thread() {
            public void run() {
                try {
                    sleep(0);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } finally {
                    Intent intent = new Intent(SplashScreen.this, MainActivity.class);
                    startActivity(intent);

                    finish();
                }
            }
        };

        timerThread.start();
    }
}
