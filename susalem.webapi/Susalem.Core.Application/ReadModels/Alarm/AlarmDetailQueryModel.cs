namespace Susalem.Core.Application.ReadModels.Alarm
{
    /// <summary>
    /// Include confirmed details
    /// </summary>
    public class AlarmDetailQueryModel : AlarmQueryModel
    {
        public string ConfirmUser { get; set; }

        public string ConfirmContent { get; set; }
    }
}
