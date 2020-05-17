using InfiniDemo.Interfaces;
using InfiniDemo.Models;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniDemo.Services
{
    public class CustomDialogService : IPageDialogService,ICustomDialogService 
    {
        protected readonly IPageDialogService _dialogService;

        private readonly string DEFAULT_CANCEL_BUTTON_TITLE = "Tamam";
        private readonly string DEFAULT_ACCEPT_BUTTON_TITLE = "Evet";
        private readonly string DEFAULT_DECLINE_BUTTON_TITLE = "Hayır";
        private readonly string DEFAULT_TITLE = "Uyarı";

        public CustomDialogService(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            return await _dialogService.DisplayActionSheetAsync(title, cancelButton, destroyButton, otherButtons);
        }

        public async Task DisplayActionSheetAsync(string title, params IActionSheetButton[] buttons)
        {
            await _dialogService.DisplayActionSheetAsync(title, buttons);
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
        {
            return await _dialogService.DisplayAlertAsync(title, message, acceptButton, cancelButton);
        }

        public async Task DisplayAlertAsync(string title, string message, string cancelButton)
        {
            await _dialogService.DisplayAlertAsync(title, message, cancelButton);
        }

        public async Task<ConfirmationDialogResult> ShowConfirmationDialogAsync(string question)
        {
            var conf = await DisplayAlertAsync(DEFAULT_TITLE, question, DEFAULT_ACCEPT_BUTTON_TITLE, DEFAULT_DECLINE_BUTTON_TITLE);

            return conf == true ? ConfirmationDialogResult.Accepted : ConfirmationDialogResult.Declined;
        }
       

        public async Task ShowMessageDialogAsync(string message, string title = null)
        {
            if (string.IsNullOrEmpty(title))
                title = DEFAULT_TITLE;

            await DisplayAlertAsync(title, message, DEFAULT_CANCEL_BUTTON_TITLE);
        }
    }
}
