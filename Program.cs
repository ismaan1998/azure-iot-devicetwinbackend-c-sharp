using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace DeviceTwinBackend
{
    class Program
    {
        static RegistryManager registryManager;

        // CHANGE THE CONNECTION STRING TO THE ACTUAL CONNETION STRING OF THE IOT HUB (SERVICE POLICY) 
        static string connectionString="<IOT Hub service connection string>";        
        
        public static async Task SetDeviceTags()  {
            var twin=await registryManager.GetTwinAsync("iotdev1");
            var patch=
                @"{
                        tags: {
                            location:  {
                                country: 'France',
                                city: 'Paris'
                            }
                        },
                        properties: {
                            desired:  {
                                FPS: 60
                            }
                        }
                    }";
            await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);                    
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Device Twin backend...");
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            SetDeviceTags().Wait();
            Console.WriteLine("Hit Enter to exit...");
            Console.ReadLine();
        }
    }
}
