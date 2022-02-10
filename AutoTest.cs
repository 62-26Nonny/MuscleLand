string kobitonServerUrl = "https://api.kobiton.com/wd/hub";

DesiredCapabilities capabilities = new DesiredCapabilities();
capabilities.SetCapability("username", "gurumi44");
capabilities.SetCapability("accessKey", "17ceab6e-bf66-495c-bd7b-1b310ead3a77");
// The generated session will be visible to you only. 
capabilities.SetCapability("sessionName", "Automation test session");
capabilities.SetCapability("sessionDescription", "");
capabilities.SetCapability("deviceOrientation", "portrait");
capabilities.SetCapability("captureScreenshots", true);
capabilities.SetCapability("browserName", "chrome");
capabilities.SetCapability("deviceGroup", "KOBITON");
// For deviceName, platformVersion Kobiton supports wildcard
// character *, with 3 formats: *text, text* and *text*
// If there is no *, Kobiton will match the exact text provided
capabilities.SetCapability("deviceName", "Galaxy A10s");
capabilities.SetCapability("platformVersion", "10");
capabilities.SetCapability("platformName", "Android"); 

console.Log("Starting Automate Test");

