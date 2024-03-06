using static Dal.XMLTools;
namespace Dal;

internal class Config
{
    static string s_data_config_xml = "data-config";


    internal static DateTime? StartDate
    {
        get { return GetDate(s_data_config_xml, "StartProject"); }
        set { SetDate(value, s_data_config_xml, "StartProject"); }
    }
    internal static DateTime? EndDate
    {
        get { return GetDate(s_data_config_xml, "EndProject"); }
        set { SetDate(value, s_data_config_xml, "EndProject"); }
    }


    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
}

