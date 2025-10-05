using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class EmailSetting
{
    public int EmailSettingId { get; set; }

    public string? MailHost { get; set; }

    public int? MailPort { get; set; }

    public string? MailAddress { get; set; }

    public string? Password { get; set; }

    public string? MailFromname { get; set; }

    public string? EncryptionName { get; set; }
}
