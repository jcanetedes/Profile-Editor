using Radzen;

namespace ProfileEditor.Core.Utils;

public class NotificationUtils
{
    public static ConfirmOptions GetDefaultDialogOptions() => new()
    {
        Top = "24px",
        Width = "100%",
        Style = "max-width: 992px"
    };

    public static ConfirmOptions GetMediumDialogOptions() => new()
    {
        Top = "24px",
        Width = "100%",
        Style = "max-width: 768px"
    };

    public static ConfirmOptions GetDefaultLargeDialogOptions() => new()
    {
        Top = "0px",
        Width = "100%",
        Style = "max-width: 1140px; padding:0 !important;"
    };

    public static ConfirmOptions GetDefaultConfirmOptions() => new()
    {
        OkButtonText = "Yes",
        CancelButtonText = "No"
    };

    public static NotificationMessage GetErrorNotification(string detail) => new()
    {
        Severity = NotificationSeverity.Error,
        Summary = "Error",
        Detail = detail,
        Duration = 5000
    };

    public static NotificationMessage GetWarningNotification(string detail) => new()
    {
        Severity = NotificationSeverity.Warning,
        Summary = "Warning",
        Detail = detail,
        Duration = 5000
    };

    public static NotificationMessage GetSuccessNotification(string detail) => new()
    {
        Severity = NotificationSeverity.Success,
        Summary = "Success",
        Detail = detail,
        Duration = 5000
    };
}
