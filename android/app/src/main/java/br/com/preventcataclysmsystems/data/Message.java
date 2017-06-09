package br.com.preventcataclysmsystems.data;

/**
 * Created by danilo.nascimento on 09/06/2017.
 */

public class Message {
    private String title;
    private String message;
    private String details;

    public Message() {
    }

    public Message(String title, String message, String details) {
        this.title = title;
        this.message = message;
        this.details = details;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getDetails() {
        return details;
    }

    public void setDetails(String details) {
        this.details = details;
    }
}
