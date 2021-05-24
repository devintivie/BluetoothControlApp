#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEServer.h>
#include <BLE2902.h>

#define SERVICE_UUID "212faffe-bbe4-11eb-8529-0242ac130003"
#define MESSAGE_UUID "212fb4e0-bbe4-11eb-8529-0242ac130003"

//212fb5f8-bbe4-11eb-8529-0242ac130003
//212fb6ca-bbe4-11eb-8529-0242ac130003

#define DEVINFO_UUID (uint16_t)0x180a
#define DEVINFO_MANUFACTURER_UUID (uint16_t)0x2a29
#define DEVINFO_NAME_UUID (uint16_t)0x2a24
#define DEVINFO_SERIAL_UUID (uint16_t)0x2a25


#define DEVICE_MANUFACTURER "Foobar"
#define DEVICE_NAME "BLETestIvie"

BLECharacteristic *characteristicMessage;

class MyServerCallbacks : public BLEServerCallbacks
{
    void onConnect(BLEServer *server)
    {
        Serial.println("Connected");
    };

    void onDisconnect(BLEServer *server)
    {
        Serial.println("Disconnected");
    }
};

class MessageCallbacks : public BLECharacteristicCallbacks
{
    void onWrite(BLECharacteristic *characteristic)
    {
        std::string data = characteristic->getValue();
        Serial.println(data.c_str());
    }

    void onRead(BLECharacteristic *characteristic)
    {
        characteristic->setValue("Foobar");
    }
};


void setup()
{
    Serial.begin(115200);

    // Setup BLE Server
    BLEDevice::init(DEVICE_NAME);
    BLEServer *server = BLEDevice::createServer();
    server->setCallbacks(new MyServerCallbacks());

    // Register message service that can receive messages and reply with a static message.
    BLEService *service = server->createService(SERVICE_UUID);
    characteristicMessage = service->createCharacteristic(MESSAGE_UUID, BLECharacteristic::PROPERTY_READ | BLECharacteristic::PROPERTY_NOTIFY | BLECharacteristic::PROPERTY_WRITE);
    characteristicMessage->setCallbacks(new MessageCallbacks());
    characteristicMessage->addDescriptor(new BLE2902());
    service->start();

    // Register device info service, that contains the device's UUID, manufacturer and name.
    service = server->createService(DEVINFO_UUID);
    BLECharacteristic *characteristic = service->createCharacteristic(DEVINFO_MANUFACTURER_UUID, BLECharacteristic::PROPERTY_READ);
    characteristic->setValue(DEVICE_MANUFACTURER);
    characteristic = service->createCharacteristic(DEVINFO_NAME_UUID, BLECharacteristic::PROPERTY_READ);
    characteristic->setValue(DEVICE_NAME);
    characteristic = service->createCharacteristic(DEVINFO_SERIAL_UUID, BLECharacteristic::PROPERTY_READ);
    String chipId = String((uint32_t)(ESP.getEfuseMac() >> 24), HEX);
    characteristic->setValue(chipId.c_str());
    service->start();

    // Advertise services
    BLEAdvertising *advertisement = server->getAdvertising();
    BLEAdvertisementData adv;
    adv.setName(DEVICE_NAME);
    adv.setCompleteServices(BLEUUID(SERVICE_UUID));
    advertisement->setAdvertisementData(adv);
    advertisement->start();

    Serial.println("Ready");
}

void loop()
{
    delay(1000);
}
