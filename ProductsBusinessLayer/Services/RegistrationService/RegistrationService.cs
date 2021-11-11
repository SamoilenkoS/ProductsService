using AutoMapper;
using ProductsBusinessLayer.DTOs;
using ProductsBusinessLayer.Services.SmtpService;
using ProductsCore.Models;
using ProductsDataLayer.Repositories.EmailRepository;
using ProductsDataLayer.Repositories.UserRepository;
using System;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly ISmtpService _smtpService;
        private readonly IMapper _mapper;

        public RegistrationService(
            IUserRepository userRepository,
            IEmailRepository emailRepository,
            ISmtpService smtpService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
            _smtpService = smtpService;
            _mapper = mapper;
        }

        public async Task<Guid> RegisterUserAsync(
            AccountInfoDTO accountInfoDTO,
            string uri)
        {
            var confirmationMessage = Guid.NewGuid();
            int? emailId = null;

            if (!string.IsNullOrEmpty(accountInfoDTO.Email))
            {
                emailId = await SaveUserEmailAsync(accountInfoDTO.Email, confirmationMessage);
            }

            var result = await SaveUserInfoAsync(accountInfoDTO, emailId);

            if (emailId.HasValue)
            {
                await SendConfirmationEmailAsync(accountInfoDTO.Email, uri, confirmationMessage);
            }

            return result;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string message)
        {
            var confirmationMessage = await _emailRepository.GetConfirmMessageAsync(email);
            var result = confirmationMessage == message;

            if (result)
            {
                await _emailRepository.ConfirmEmailAsync(email);
            }

            return result;
        }

        private async Task<Guid> SaveUserInfoAsync(AccountInfoDTO accountInfoDTO, int? emailId)
        {
            var accountInfo = _mapper.Map<AccountInfo>(accountInfoDTO);
            accountInfo.EmailId = emailId;

            var result = await _userRepository.AddUserAsync(accountInfo);
            return result;
        }

        private async Task SendConfirmationEmailAsync(
            string email,
            string uri,
            Guid confirmationMessage)
        {
            var mailDTO = new MailDTO
            {
                To = email,
                Subject = "ITEA Email confirmation",
                Body = $"{uri}/accounts/" +
                $"confirm?email={email}" +
                $"&message={confirmationMessage}"
            };

            await _smtpService.SendMailAsync(mailDTO);
        }

        private async Task<int> SaveUserEmailAsync(string email, Guid confirmationMessage)
        {
            var mail = new Email
            {
                PostName = email,
                IsConfirmed = false,
                ConfirmationString = confirmationMessage.ToString()
            };

            return await _emailRepository.RegisterEmailAsync(mail);
        }
    }
}
