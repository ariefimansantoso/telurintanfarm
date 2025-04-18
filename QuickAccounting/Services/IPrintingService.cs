﻿using System.Threading.Tasks;

namespace QuickAccounting.Services
{
    public interface IPrintingService
    {
        Task Print(PrintOptions options);
        Task Print(string printable, PrintType printType = PrintType.Pdf);
        Task Print(string printable, bool showModal, PrintType printType = PrintType.Pdf);
    }
}