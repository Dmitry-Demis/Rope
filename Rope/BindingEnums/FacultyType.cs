using System.ComponentModel;

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
}
