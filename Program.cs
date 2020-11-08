using Microsoft.Azure.NotificationHubs;
using System;
using System.Threading;

namespace NotificationHubSendMessageCesar
{
    class Program
    {

        private const string ConnectionString = "Endpoint=sb://deskhelpdev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wTai8dxwIiYMt+FhtoTNegYqlfFreGWMFNSykjfDhZs=";
        private static string ConnectionStringCesar = "Endpoint=sb://notificationhubnamespacecesar.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=Ulpe/3TtR2RsHsmrHNN8su+9Ex9Fu/rHdepmbse6tOI=";

        private static async void SendNotificationAsync()
        {

            //var hub = new NotificationHub("NotificationhuBCesar", ConnectionStringCesar);


            NotificationHubClient hubClient = NotificationHubClient.CreateClientFromConnectionString(ConnectionStringCesar, "NotificationhuBCesar", true);
            string[] userTag = new string[2];
            userTag[0] = "username: Test";
            userTag[1] = "from:  user";
            var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">From any .NET App!</text></binding></visual></toast>";
            var rowPayload = "Notification at " + DateTime.Now.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("it-IT"));
            var toasts = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                       "From Test: Message" + "</text></binding></visual></toast>";
            try
            {
                //var outcome = hubClient.SendWindowsNativeNotificationAsync(toasts, userTag);
                var payLoad = @"From any .NET App!";
                var notif = new WindowsNotification(payLoad);
                notif.ContentType = "application/octet-stream";
                notif.Headers.Add("X-WNS-Type", "wns/raw");
                notif.Headers.Add("ServiceBusNotification-Format", "windows");
                //notif.Headers.Add("Authorization", "wTai8dxwIiYMt+FhtoTNegYqlfFreGWMFNSykjfDhZs=");
                //notif.Headers.Add("Host", "cloud.notify.windows.com");
                notif.Body = "hi from .net";

                var ct = new CancellationToken();

                hubClient.SendNotificationAsync(notif, "@1234", ct).Wait();


            }
            catch (Exception e)
            {
                var s = "";
            }
            //            #region case 1: broadcasted
            //            /*
            //            //the payload can be whatever: the Azure Notification Hub pass through everything to WNS and possible errore could be returned froew that is not well formed.
            //            await hubClient.SendWindowsNativeNotificationAsync(toast);
            //            */
            //            #endregion case 1: broadcasted

            //            #region case 2: client subscribed by SubscribeToCategories
            //            /* */
            //            //There is not the pain for a developer to mantain the registry of tags
            //            //If we want a toast notification
            //            // await hubClient.SendWindowsNativeNotificationAsync(toast, "Torino"); // hubClient.SendWindowsNativeNotificationAsync(toast, "Torino").Wait(); //notity to clients subcribed to "World" tag
            //            // //or hubClient.SendWindowsNativeNotificationAsync(toast, "Torino &amp;amp;&amp;amp; !Politics").Wait(); //notify to clients subcribed to "World" tag but not subscribed to the Politics tag too. In expression like this (that can use also parenthesis) it can be used at maximun 6 tags in the expression

            //            //If we want to have a row notification that can be handled by code in the running client app
            //            Notification notification = new WindowsNotification(rowPayload);
            //            notification.Headers = new Dictionary<string, string> {
            //// {"Content-Type", "application/octet-stream")},
            //{"X-WNS-TTL","300"}, // e.g. 300 seconds=> 5 minutes - Specifies the TTL (expiration time) for a notification.
            //{"X-WNS-Type", "wns/raw" },
            //{"ServiceBusNotification-Format", "windows"}
            //};
            //            await hubClient.SendNotificationAsync(notification, "deskhelp");
            //            /* */
            //            #endregion case 2: client subscribed by SubscribeToCategories

            //            #region case 3: client SubscribeToCategoriesWithCustomTemplate
            //            /*
            //            //the template and internalization is own by the client that registes to have notifications
            //            //template back to the mobile app: it is the client that knows the format he will receive
            //            //you can put any property and payload you whant; you can personalize the notification, depending to the registration
            //            //we do not use anymore the var toast but a dictionary: the server code is agnostic of the type of client (IOS, Android, Windows) that has to define a similar template related to News_locale
            //            var notification = new Dictionary<string, string>() {
            //            {"News_English", "World news in English"},
            //            {"News_Italian", "Notizie dal mondo in italiano"}
            //            };
            //            //send then a template notification not a Windows one
            //            await hubClient.SendTemplateNotificationAsync(notification, "World");
            //            */
            //            #endregion case 3: client SubscribeToCategoriesWithCustomTemplate        
        }

        public static void Main(string[] args)
        {
            SendNotificationAsync();
        }
    }
}
