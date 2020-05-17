using InfiniDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniDemo.Interfaces
{
    public interface ICustomDialogService
    {
        Task ShowMessageDialogAsync(string message, string title = null);
        Task<ConfirmationDialogResult> ShowConfirmationDialogAsync(string question);
    }
}
