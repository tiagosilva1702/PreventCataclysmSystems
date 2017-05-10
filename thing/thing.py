import spidev
import json
import urllib2
import RPi.GPIO as GPIO
import dht11
import time

from time import sleep

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)
GPIO.cleanup()

spi = spidev.SpiDev()
spi.open(0, 0)

instance = dht11.DHT11(pin=14)

def getAdc(channel):
        if channel > 7 or channel < 0:
                return -1

        r = spi.xfer([1, 8 + channel << 4, 0])
        adcOut = ((r[1] & 3) << 8) + r[2]
        solo = int(round(adcOut / 10.24))
        print 'ADC Output: {0:4d} Umidade do solo: {1:3}%'.format(adcOut, solo)
        sleep(0.1)
        return solo

def postData(temperatura, umidade, acelerometro, solo)        
        data = { "Temperatura": temperatura, "Umidade": umidade, "Acelerometro": acelerometro, "Solo": solo }
        req = urllib2.Request('https://homologacao.imap.org.br/prevent/api/store')
        req.add_header('Content-Type', 'application/json')
        response = urllib2.urlopen(req, json.dumps(data))
        
while True:
        solo = getAdc(5)
        
        result = instance.read()        
        temperature = result.is_valid() ? result.temperature : null        
        humidity = result.is_valid() ? result.humidity : null                
        
        time.sleep(10000)
        
        postData(temperature, humidity, 0, solo)
                

