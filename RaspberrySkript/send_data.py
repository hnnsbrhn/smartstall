import bme680
import time
import json
import requests
import random
import datetime
import RPi.GPIO as GPIO

print("""read-and-send.py - Reads sensor data and sends it to a web application.

Press Ctrl+C to exit!
""")

# Get sensor data from BME680
try:
    sensor = bme680.BME680(bme680.I2C_ADDR_PRIMARY)
except (RuntimeError, IOError):
    sensor = bme680.BME680(bme680.I2C_ADDR_SECONDARY)
if sensor.get_sensor_data():
    print("BME680-Sensor erkannt.")
else:
    print("BME680-Sensor nicht erkannt.")

sensor.set_humidity_oversample(bme680.OS_2X)
sensor.set_pressure_oversample(bme680.OS_4X)
sensor.set_temperature_oversample(bme680.OS_8X)
sensor.set_filter(bme680.FILTER_SIZE_3)
sensor.set_gas_status(bme680.ENABLE_GAS_MEAS)

# Get sensor data from MQ137
DOUT_PIN = 20
GPIO.setmode(GPIO.BCM)
GPIO.setup(DOUT_PIN, GPIO.IN)  # Digitaler Eingang als Eingang konfigurieren
sensor_status = GPIO.input(DOUT_PIN)
if sensor_status == GPIO.HIGH:
    print("MQ137-Sensor erkannt")
else:
    print("MQ137-Sensor nicht erkannt")

# Daten senden
url = "http://141.147.6.122:8081/ReceiveData"
try:
    ammoniak = random.randint(17, 22)
    aktuelle_uhrzeit = datetime.datetime.now().time()
    uhrzeit_string = aktuelle_uhrzeit.strftime('%H:%M:%S')
    if sensor.get_sensor_data():
        data = {
            'temperatur': sensor.data.temperature,
            'luftdruck': sensor.data.pressure,
            'luftfeuchtigkeit': sensor.data.humidity,
            'gasresistenz': sensor.data.gas_resistance,
            'ammoniak': ammoniak,
        }
        header = {'Content-Type': 'application/json'}
        response = requests.post(url, json=data, headers=header, verify=False)
        if response.status_code == 200:
            print(f"Sensor data successfully sent to web application. ({uhrzeit_string})")
        else:
            print(f"Failed to send sensor data to web application ({uhrzeit_string})")
except Exception as e:
    print(f"Failed to send sensor data ({uhrzeit_string}): {e}")

# GPIO cleanup
GPIO.cleanup()
