using Unity.Notifications.Android;

public class PushNotificationService : IPushNotificationService
{
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
            Id = "dungeon_horror_game",
            Name = "Dungeon Horror",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";
        notification.Title = _localizationService.GetTranslateByKey(TranslatableKey.ComeBackToTheGame);
        notification.Text = _localizationService.GetTranslateByKey(TranslatableKey.IfYoureNotAfraid);
        notification.FireTime = System.DateTime.Now.AddDays(1);

        var id = AndroidNotificationCenter.SendNotification(notification, "dungeon_horror_game");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "dungeon_horror_game");
        }
    }
}
