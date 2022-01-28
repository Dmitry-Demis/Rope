using System.ComponentModel;
using Rope.Properties;

namespace Rope.BindingEnums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum FacultyType
    {
        [Description("ИВТФ")]
        ITandComputerScienceFaculty,
        [Description("ИФФ")]
        PhysicsAndEngineeringFaculty,
        [Description("ТЭФ")]
        HeatEngineeringFaculty,
        [Description("ФЭУ")]
        EconomicsAndManagementFaculty,
        [Description("ЭМФ")]
        ElectromechanicsFaculty,
        [Description("ЭЭФ")]
        ElectricalPowerEngineeringFaculty       
    }
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    internal enum StageType
    {
        [Description("Время")]
        Time,
        [Description("Количество")]
        Count
    }
}
