﻿using ProductsBusinessLayer.DTOs;
using System;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task<Guid> RegisterUserAsync(AccountInfoDTO accountInfoDTO);
    }
}