namespace ComponentsReviewer
{
    public class Settings
    {
        public static string RenderingPath = Sitecore.Configuration.Settings.GetSetting("ComponentsReviewer.RootItemsIds", "{EB2E4FFD-2761-4653-B052-26A64D385227}");
        public static string DatabaseName = Sitecore.Configuration.Settings.GetSetting("ComponentsReviewer.Database", "master");
    }
}