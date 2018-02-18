namespace ComponentsReviewer
{
    public class Settings
    {
        public static string RenderingPath = Sitecore.Configuration.Settings.GetSetting("ComponentsReviewer.RootItemsIds", "{F2DB8258-D7A7-486D-AF9E-3460423DBE2D}");
        public static string DatabaseName = Sitecore.Configuration.Settings.GetSetting("ComponentsReviewer.Database", "master");
    }
}