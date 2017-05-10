import spidev
from time import sleep

spi = spidev.SpiDev()
spi.open(0, 0)

def getAdc(channel):
        if channel > 7 or channel < 0:
                return -1

        r = spi.xfer([1, 8 + channel << 4, 0])
        adcOut = ((r[1] & 3) << 8) + r[2]
        percent = int(round(adcOut / 10.24))
        print 'ADC Output: {0:4d} Percentage: {1:3}%'.format(adcOut, percent)
        sleep(0.1)

while True:
        getAdc(5)
