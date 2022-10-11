﻿using Invoicing.CrossCutting.Domain;
using MediatR;

namespace TaskManagement.Application.Messages
{
    public class LoginCommand : IRequest<Result<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}