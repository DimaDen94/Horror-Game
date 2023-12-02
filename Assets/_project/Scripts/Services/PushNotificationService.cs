using Unity.Notifications.Android;

public class PushNotificationService : IPushNotificationService
{
    private const string ChannelId = "dungeon_horror_game";
    private const string ChannekName = "Dungeon Horror";
    private const string ChannelDescription = "Reminder notifications";

    private const string SmallIconPath = "icon_0";
    private const string LargeIconPath = "icon_1";

    private ILocalizationService _localizationService;

    public PushNotificationService(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public void CreatePushNotification()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        var channel = new AndroidNotificationChannel()
        {
            Id = ChannelId,
            Name = ChannekName,
            Importance = Importance.Default,
            Description = ChannelDescription,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.SmallIcon = SmallIconPath;
        notification.LargeIcon = LargeIconPath;
        notification.Title = _localizationService.GetTranslateByKey(TranslatableKey.ComeBackToTheGame);
        notification.Text = _localizationService.GetTranslateByKey(TranslatableKey.IfYoureNotAfraid);
        notification.FireTime = System.DateTime.Now.AddDays(1);

        var id = AndroidNotificationCenter.SendNotification(notification, ChannelId);

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, ChannelId);
        }
    }
}
