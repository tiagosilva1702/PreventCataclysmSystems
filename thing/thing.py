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
        sleep(0.1)
        return 100 - solo

def postData(form):       
        req = urllib2.Request('https://homologacao.imap.org.br/prevent/api/store')
        req.add_header('Content-Type', 'application/json')
        response = urllib2.urlopen(req, json.dumps(form))
        
while True:
        result = instance.read()                                
        
	if result.is_valid():
		solo = getAdc(5)

	        data = { "Temperatura":	 result.temperature, "Umidade": result.humidity, "Acelerometro": 0, "Solo": solo }
	
		print(data) 
	
	        postData(data)

	time.sleep(1)


