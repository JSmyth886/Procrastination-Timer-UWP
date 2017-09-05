using Windows.System.Profile;

namespace Procrastination_Timer.Common
{
  public static class SystemInfo
  {
    public static bool IsMobile => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";
  }
}
